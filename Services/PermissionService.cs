using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.Data;
using ToolBox.Interfaces;

namespace ToolBox.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;

        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, List<string>>> GetUserPermissionsAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user?.Role == null || !user.Role.IsActive)
            {
                return new Dictionary<string, List<string>>();
            }

            var permissions = user.Role.RolePermissions
                .Where(rp => rp.Permission != null)
                .GroupBy(rp => rp.Permission.ModuleName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(rp => rp.Permission.ActionName).ToList()
                );

            return permissions;
        }

        public async Task<bool> UserHasPermissionAsync(int userId, string module, string action)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user?.Role == null || !user.Role.IsActive)
            {
                return false;
            }

            return user.Role.RolePermissions
                .Any(rp => rp.Permission != null && 
                          rp.Permission.ModuleName == module && 
                          rp.Permission.ActionName == action);
        }

        public async Task<List<string>> GetUserReadableModulesAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user?.Role == null || !user.Role.IsActive)
            {
                return new List<string>();
            }

            var modules = user.Role.RolePermissions
                .Where(rp => rp.Permission != null && 
                            rp.Permission.ActionName == "Read")
                .Select(rp => rp.Permission.ModuleName)
                .Distinct()
                .ToList();
                
            return modules;
        }
    }
}