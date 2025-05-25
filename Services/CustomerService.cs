using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CustomerService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<(Customer Customer, string? ErrorMessage)> CreateCustomerAsync(CustomerCreateViewModel model, string? avatarUrl, int createdByUserId)
        {
            // 1. Validar si el Email ya está en uso
            if (await IsEmailTakenAsync(model.Email))
            {
                return (null, "El correo electrónico ya está registrado.");
            }

            // 2. Crear nuevo cliente
            var customerToCreate = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CompanyName = model.CompanyName,
                Country = model.Country,
                IsActive = model.IsActive,
                StatusDetail = model.StatusDetail,
                AvatarUrl = avatarUrl,
                CreatedByUserId = createdByUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // INICIO DE LA SECCIÓN CRÍTICA DE GUARDADO
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Customers.Add(customerToCreate);
                int changes = await _context.SaveChangesAsync();

                if (changes > 0)
                {
                    await transaction.CommitAsync();
                    
                    // Recargar el cliente con su CreatedByUser para retornarlo completo
                    await _context.Entry(customerToCreate).Reference(c => c.CreatedByUser).LoadAsync();
                    
                    return (customerToCreate, null); // Éxito
                }
                else
                {
                    await transaction.RollbackAsync();
                    return (null, "No se pudo guardar el cliente en la base de datos.");
                }
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                Console.WriteLine($"DbUpdateException: {innerMessage}");
                
                // Verificar si es un error de constraint único
                if (innerMessage.Contains("duplicate key value") || innerMessage.Contains("unique constraint"))
                {
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
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            // TODO: Implementar lógica de eliminación física si es necesaria
            throw new NotImplementedException();
        }

        public async Task<(bool Success, string NewStatusMessage, bool NewIsActiveState)> ToggleCustomerStatusAsync(int customerId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var customer = await _context.Customers.FindAsync(customerId);
                if (customer == null)
                {
                    return (false, "Cliente no encontrado.", false);
                }

                // Cambiar el estado
                customer.IsActive = !customer.IsActive;
                customer.UpdatedAt = DateTime.UtcNow;

                _context.Customers.Update(customer);
                int changes = await _context.SaveChangesAsync();

                if (changes > 0)
                {
                    await transaction.CommitAsync();
                    string newStatusMessage = customer.IsActive ? "Activo" : "Inactivo";
                    return (true, newStatusMessage, customer.IsActive);
                }
                else
                {
                    await transaction.RollbackAsync();
                    return (false, "No se pudo actualizar el estado del cliente.", customer.IsActive);
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error en ToggleCustomerStatusAsync: {ex.Message}");
                return (false, "Error al actualizar el estado del cliente.", false);
            }
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(string? statusFilter = null, string? searchTerm = null)
        {
            var query = _context.Customers.Include(c => c.CreatedByUser).AsQueryable();

            // Filtrar por estado si se proporciona
            if (!string.IsNullOrEmpty(statusFilter))
            {
                if (statusFilter.ToLower() == "active")
                {
                    query = query.Where(c => c.IsActive);
                }
                else if (statusFilter.ToLower() == "inactive")
                {
                    query = query.Where(c => !c.IsActive);
                }
            }

            // Filtrar por término de búsqueda si se proporciona
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(c => 
                    c.FirstName.ToLower().Contains(searchTerm) ||
                    c.LastName.ToLower().Contains(searchTerm) ||
                    c.Email.ToLower().Contains(searchTerm) ||
                    (c.CompanyName != null && c.CompanyName.ToLower().Contains(searchTerm)) ||
                    (c.Country != null && c.Country.ToLower().Contains(searchTerm))
                );
            }

            // Ordenar por fecha de creación descendente (más recientes primero)
            query = query.OrderByDescending(c => c.CreatedAt);

            return await query.ToListAsync();
        }
        
        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customers
                .Include(c => c.CreatedByUser)
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<bool> IsEmailTakenAsync(string email, int? currentCustomerId = null)
        {
            var query = _context.Customers.AsQueryable();
            if (currentCustomerId.HasValue)
            {
                query = query.Where(c => c.Id != currentCustomerId.Value);
            }
            return await query.AnyAsync(c => c.Email.ToLower() == email.ToLower());
        }

        public async Task<(bool Success, string? ErrorMessage)> UpdateCustomerAsync(int customerId, CustomerEditViewModel model, string? newAvatarUrl)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var customerToUpdate = await _context.Customers.FindAsync(customerId);
                if (customerToUpdate == null)
                {
                    return (false, "Cliente no encontrado.");
                }

                // Validar si el nuevo Email ya está en uso por OTRO cliente
                if (customerToUpdate.Email.ToLower() != model.Email.ToLower() && await IsEmailTakenAsync(model.Email, customerId))
                {
                    return (false, "El nuevo correo electrónico ya está registrado por otro cliente.");
                }

                // Actualizar propiedades básicas
                customerToUpdate.FirstName = model.FirstName;
                customerToUpdate.LastName = model.LastName;
                customerToUpdate.Email = model.Email;
                customerToUpdate.PhoneNumber = model.PhoneNumber;
                customerToUpdate.CompanyName = model.CompanyName;
                customerToUpdate.Country = model.Country;
                customerToUpdate.IsActive = model.IsActive;
                customerToUpdate.StatusDetail = model.StatusDetail;

                // Actualizar avatar si se proporciona una nueva URL
                if (!string.IsNullOrEmpty(newAvatarUrl))
                {
                    customerToUpdate.AvatarUrl = newAvatarUrl;
                }
                
                customerToUpdate.UpdatedAt = DateTime.UtcNow;

                _context.Customers.Update(customerToUpdate);
                int changes = await _context.SaveChangesAsync();

                if (changes > 0)
                {
                    await transaction.CommitAsync();
                    return (true, null); // Éxito
                }
                else
                {
                    await transaction.RollbackAsync();
                    return (false, "No se realizaron cambios en el cliente.");
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
                Console.WriteLine($"DbUpdateException en UpdateCustomerAsync: {innerMessage}");
                
                // Verificar si es un error de constraint único
                if (innerMessage.Contains("duplicate key value") || innerMessage.Contains("unique constraint"))
                {
                    if (innerMessage.Contains("Email"))
                        return (false, "El correo electrónico ya existe.");
                }
                
                return (false, $"Error de base de datos al actualizar: {innerMessage}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Exception en UpdateCustomerAsync: {ex.Message}");
                return (false, "Ocurrió un error inesperado al actualizar el cliente.");
            }
        }

        public async Task<string?> ProcessAvatarUploadAsync(IFormFile avatarFile)
        {
            try
            {
                // Validar tipo de archivo
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".gif" };
                var fileExtension = Path.GetExtension(avatarFile.FileName).ToLowerInvariant();
                
                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new ArgumentException("Tipo de archivo no válido. Solo se permiten PNG, JPG, JPEG y GIF.");
                }

                // Validar tamaño de archivo (máx 800KB)
                const long maxFileSize = 800 * 1024; // 800KB
                if (avatarFile.Length > maxFileSize)
                {
                    throw new ArgumentException("El archivo es muy grande. El tamaño máximo permitido es 800KB.");
                }

                // Generar nombre único para el archivo
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var uploadPath = Path.Combine(_environment.WebRootPath, "uploads", "avatars");
                var fullPath = Path.Combine(uploadPath, fileName);

                // Crear directorio si no existe
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Guardar el archivo
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await avatarFile.CopyToAsync(stream);
                }

                // Retornar la URL relativa
                return $"/uploads/avatars/{fileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ProcessAvatarUploadAsync: {ex.Message}");
                throw;
            }
        }
    }
}