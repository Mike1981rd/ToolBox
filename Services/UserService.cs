using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToolBox.Data; // Namespace de tu DbContext
using ToolBox.Interfaces;
using ToolBox.Models; // Namespace de tus modelos
using ToolBox.Models.ViewModels; // Para UserEditViewModel
// using System.Security.Cryptography; // Para hasheo si no se usa Identity
// using Microsoft.AspNetCore.Identity; // Si se usa ASP.NET Core Identity para contraseñas

namespace ToolBox.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        // private readonly IRoleService _roleService; // Opcional
        // private readonly IPasswordHasher<User> _passwordHasher; // Si se usa Identity

        public UserService(ApplicationDbContext context /*, IRoleService roleService, IPasswordHasher<User> passwordHasher */)
        {
            _context = context;
            // _roleService = roleService;
            // _passwordHasher = passwordHasher;
        }

        public async Task<(User User, string? ErrorMessage)> CreateUserAsync(User userToCreate, string password, int? roleId)
        {
            // 1. Validar si el Email ya está en uso
            if (await IsEmailTakenAsync(userToCreate.Email))
            {
                return (null, "El correo electrónico ya está registrado.");
            }

            // 2. Validar si el UserName ya está en uso
            if (await IsUserNameTakenAsync(userToCreate.UserName))
            {
                return (null, "El nombre de usuario ya está en uso.");
            }

            // 3. Hashear la contraseña usando BCrypt
            if (string.IsNullOrWhiteSpace(password))
            {
                return (null, "La contraseña no puede estar vacía.");
            }
            
            // Usando BCrypt para hashear la contraseña de forma segura
            userToCreate.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            // 4. Asignar RoleId si se proporcionó y es válido
            if (roleId.HasValue)
            {
                var roleExists = await _context.Roles.AnyAsync(r => r.Id == roleId.Value && r.IsActive);
                if (!roleExists)
                {
                    return (null, "El rol seleccionado no es válido o no está activo.");
                }
                userToCreate.RoleId = roleId.Value;
            }
            else 
            {
                userToCreate.RoleId = null; // Asegurar que es null si no se proporciona
            }

            // 5. Establecer campos de auditoría y valores por defecto
            userToCreate.CreatedAt = DateTime.UtcNow;
            userToCreate.UpdatedAt = DateTime.UtcNow;
            // userToCreate.IsActive = true; // Ya se debería haber establecido en el ViewModel o al mapear

            // INICIO DE LA SECCIÓN CRÍTICA DE GUARDADO
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Users.Add(userToCreate);
                int changes = await _context.SaveChangesAsync(); // Asegurarse de que esto se llama y se espera!

                if (changes > 0)
                {
                    await transaction.CommitAsync(); // Confirmar la transacción
                    
                    // Recargar el usuario con su Role para retornarlo completo
                    await _context.Entry(userToCreate).Reference(u => u.Role).LoadAsync();
                    
                    return (userToCreate, null); // Éxito REAL
                }
                else
                {
                    await transaction.RollbackAsync();
                    return (null, "No se pudo guardar el usuario en la base de datos (SaveChanges no afectó filas).");
                }
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                // Log detallado del error
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                Console.WriteLine($"DbUpdateException: {innerMessage}");
                
                // Verificar si es un error de constraint único
                if (innerMessage.Contains("duplicate key value") || innerMessage.Contains("unique constraint"))
                {
                    if (innerMessage.Contains("UserName"))
                        return (null, "El nombre de usuario ya existe.");
                    if (innerMessage.Contains("Email"))
                        return (null, "El correo electrónico ya existe.");
                }
                
                return (null, $"Error de base de datos: {innerMessage}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Exception: {ex.Message}");
                return (null, $"Un error inesperado ocurrió: {ex.Message}");
            }
            // FIN DE LA SECCIÓN CRÍTICA DE GUARDADO
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            // TODO: Implementar lógica de eliminación
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(string? roleFilter = null, string? statusFilter = null, string? searchTerm = null)
        {
            // TODO: Implementar consulta con filtros opcionales
            return await _context.Users.Include(u => u.Role).ToListAsync(); // Carga básica por ahora
        }
        
        public async Task<User> GetUserByIdAsync(int userId)
        {
            // TODO: Implementar consulta
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> IsEmailTakenAsync(string email, int? currentUserId = null)
        {
            var query = _context.Users.AsQueryable();
            if (currentUserId.HasValue)
            {
                query = query.Where(u => u.Id != currentUserId.Value);
            }
            return await query.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> IsUserNameTakenAsync(string userName, int? currentUserId = null)
        {
            var query = _context.Users.AsQueryable();
            if (currentUserId.HasValue)
            {
                query = query.Where(u => u.Id != currentUserId.Value);
            }
            return await query.AnyAsync(u => u.UserName.ToLower() == userName.ToLower());
        }

        public async Task<(bool Success, string? ErrorMessage)> UpdateUserAsync(int userId, UserEditViewModel model, string? newAvatarUrl)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userToUpdate = await _context.Users.FindAsync(userId);
                if (userToUpdate == null)
                {
                    return (false, "Usuario no encontrado.");
                }

                // Validar si el nuevo Email ya está en uso por OTRO usuario
                if (userToUpdate.Email.ToLower() != model.Email.ToLower() && await IsEmailTakenAsync(model.Email, userId))
                {
                    return (false, "El nuevo correo electrónico ya está registrado por otro usuario.");
                }

                // Validar si el nuevo UserName ya está en uso por OTRO usuario
                if (userToUpdate.UserName.ToLower() != model.UserName.ToLower() && await IsUserNameTakenAsync(model.UserName, userId))
                {
                    return (false, "El nuevo nombre de usuario ya está en uso por otro usuario.");
                }

                // Actualizar propiedades básicas
                userToUpdate.FullName = model.FullName;
                userToUpdate.UserName = model.UserName;
                userToUpdate.Email = model.Email;
                userToUpdate.PhoneNumber = model.PhoneNumber;
                userToUpdate.CompanyName = model.CompanyName;
                userToUpdate.Country = model.Country;
                userToUpdate.TaxId = model.TaxId;
                userToUpdate.BillingMethod = model.BillingMethod;
                userToUpdate.Language = model.Language;
                userToUpdate.IsTwoFactorEnabled = model.IsTwoFactorEnabled;
                userToUpdate.IsActive = model.IsActive;

                // Actualizar avatar si se proporciona una nueva URL
                if (!string.IsNullOrEmpty(newAvatarUrl))
                {
                    userToUpdate.AvatarUrl = newAvatarUrl;
                }

                // Actualizar contraseña si se proporcionó una nueva
                if (!string.IsNullOrWhiteSpace(model.NewPassword))
                {
                    // Usar BCrypt para hashear la contraseña de forma segura
                    userToUpdate.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                }

                // Actualizar RoleId si es diferente y válido
                if (userToUpdate.RoleId != model.RoleId)
                {
                    if (model.RoleId.HasValue)
                    {
                        var roleExists = await _context.Roles.AnyAsync(r => r.Id == model.RoleId.Value && r.IsActive);
                        if (!roleExists)
                        {
                            return (false, "El rol seleccionado no es válido o no está activo.");
                        }
                        userToUpdate.RoleId = model.RoleId.Value;
                    }
                    else
                    {
                        userToUpdate.RoleId = null;
                    }
                }
                
                userToUpdate.UpdatedAt = DateTime.UtcNow;

                _context.Users.Update(userToUpdate);
                int changes = await _context.SaveChangesAsync();

                if (changes > 0)
                {
                    await transaction.CommitAsync();
                    return (true, null); // Éxito
                }
                else
                {
                    await transaction.RollbackAsync();
                    return (false, "No se realizaron cambios en el usuario.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                return (false, "Error de concurrencia al actualizar. Otro usuario puede haber modificado los datos. Por favor, intente de nuevo.");
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                Console.WriteLine($"DbUpdateException en UpdateUserAsync: {innerMessage}");
                
                // Verificar si es un error de constraint único
                if (innerMessage.Contains("duplicate key value") || innerMessage.Contains("unique constraint"))
                {
                    if (innerMessage.Contains("UserName"))
                        return (false, "El nombre de usuario ya existe.");
                    if (innerMessage.Contains("Email"))
                        return (false, "El correo electrónico ya existe.");
                }
                
                return (false, $"Error de base de datos al actualizar: {innerMessage}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Exception en UpdateUserAsync: {ex.Message}");
                return (false, "Ocurrió un error inesperado al actualizar el usuario.");
            }
        }

        // Si se decide incluir GetAllRolesAsync aquí:
        // public async Task<IEnumerable<Role>> GetAllRolesAsync()
        // {
        //     return await _context.Roles.Where(r => r.IsActive).OrderBy(r => r.Name).ToListAsync();
        // }
    }
}