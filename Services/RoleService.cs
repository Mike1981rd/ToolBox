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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}