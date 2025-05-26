using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Role> CreateRoleAsync(Role role, IEnumerable<int> permissionIds)
        {
            if (await IsRoleNameTakenAsync(role.Name))
            {
                return null;
            }

            role.CreatedAt = DateTime.UtcNow;
            role.UpdatedAt = DateTime.UtcNow;
            role.IsActive = true;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Roles.Add(role);
                    await _context.SaveChangesAsync();

                    if (permissionIds != null && permissionIds.Any())
                    {
                        var validPermissions = await _context.Permissions
                                                        .Where(p => permissionIds.Contains(p.Id))
                                                        .Select(p => p.Id)
                                                        .ToListAsync();

                        if (validPermissions.Count != permissionIds.Distinct().Count())
                        {
                            await transaction.RollbackAsync();
                            return null;
                        }

                        foreach (var permissionId in validPermissions)
                        {
                            _context.RolePermissions.Add(new RolePermission { RoleId = role.Id, PermissionId = permissionId });
                        }
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                    return role;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            var role = await _context.Roles
                                     .Include(r => r.Users) // Necesario para verificar usuarios asignados
                                     .Include(r => r.RolePermissions) // Para eliminar en cascada o manualmente las asociaciones
                                     .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                return false; // Rol no encontrado
            }

            // CONDICIÓN CLAVE: No eliminar si tiene usuarios asignados
            if (role.Users != null && role.Users.Any())
            {
                // Podrías lanzar una excepción específica aquí o simplemente retornar false
                // para que el controlador maneje el mensaje al usuario.
                // Por ejemplo: throw new InvalidOperationException("El rol no se puede eliminar porque tiene usuarios asignados.");
                return false; // Indica fallo debido a usuarios asignados
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Eliminar las asociaciones RolePermissions primero si no hay eliminación en cascada configurada
                    // Si la FK en RolePermissions tiene ON DELETE CASCADE para RoleId, esto no es estrictamente necesario,
                    // pero es más explícito y seguro hacerlo.
                    if (role.RolePermissions != null && role.RolePermissions.Any())
                    {
                        _context.RolePermissions.RemoveRange(role.RolePermissions);
                        // No es necesario SaveChanges aquí si el SaveChanges final lo cubre.
                    }
                    
                    _context.Roles.Remove(role);
                    int affectedRows = await _context.SaveChangesAsync();
                    
                    await transaction.CommitAsync();
                    return affectedRows > 0;
                }
                catch (Exception ex) // Ser más específico con las excepciones si es posible
                {
                    await transaction.RollbackAsync();
                    // Loggear la excepción (ex.Message)
                    return false; // Indica fallo en la eliminación
                }
            }
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions
                .OrderBy(p => p.Category)
                .ThenBy(p => p.ModuleName)
                .ThenBy(p => p.ActionName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles
                .Include(r => r.Users)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<bool> IsRoleNameTakenAsync(string name, int? currentRoleId = null)
        {
            var query = _context.Roles.AsQueryable();
            if (currentRoleId.HasValue)
            {
                query = query.Where(r => r.Id != currentRoleId.Value);
            }
            return await query.AnyAsync(r => r.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> UpdateRoleAsync(int roleId, Role roleDetails, IEnumerable<int> permissionIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                // Buscar el rol existente
                var existingRole = await _context.Roles
                    .Include(r => r.RolePermissions)
                    .FirstOrDefaultAsync(r => r.Id == roleId);
                
                if (existingRole == null)
                {
                    return false;
                }
                
                // Actualizar propiedades del rol
                existingRole.Name = roleDetails.Name;
                existingRole.Description = roleDetails.Description;
                existingRole.AssignedDashboard = roleDetails.AssignedDashboard;
                existingRole.IsActive = roleDetails.IsActive;
                existingRole.PuedeVerMensajeBienvenidaDashboard = roleDetails.PuedeVerMensajeBienvenidaDashboard;
                existingRole.PuedeVerVideoBienvenidaDashboard = roleDetails.PuedeVerVideoBienvenidaDashboard;
                existingRole.PuedeVerCardTotalClientesDashboard = roleDetails.PuedeVerCardTotalClientesDashboard;
                existingRole.PuedeVerCardClientesActivosDashboard = roleDetails.PuedeVerCardClientesActivosDashboard;
                existingRole.UpdatedAt = DateTime.UtcNow;
                
                // Eliminar permisos existentes
                _context.RolePermissions.RemoveRange(existingRole.RolePermissions);
                
                // Agregar nuevos permisos
                foreach (var permissionId in permissionIds)
                {
                    _context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permissionId
                    });
                }
                
                // Guardar cambios
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}