using System.Collections.Generic;
using System.Threading.Tasks;
using ToolBox.Models;

namespace ToolBox.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
        Task<Role> CreateRoleAsync(Role role, IEnumerable<int> permissionIds);
        Task<bool> UpdateRoleAsync(int roleId, Role roleDetails, IEnumerable<int> permissionIds);
        Task<bool> DeleteRoleAsync(int roleId);
        Task<bool> IsRoleNameTakenAsync(string name, int? currentRoleId = null);
    }
}