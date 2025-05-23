using System.Collections.Generic;
using System.Threading.Tasks;
using ToolBox.Models; // Para la entidad User
using ToolBox.Models.ViewModels; // Para ViewModels/DTOs

namespace ToolBox.Interfaces
{
    public interface IUserService
    {
        // Método para obtener todos los usuarios, potencialmente con filtros y paginación más adelante
        Task<IEnumerable<User>> GetAllUsersAsync(string? roleFilter = null, string? statusFilter = null, string? searchTerm = null);
        
        Task<User> GetUserByIdAsync(int userId);

        // Para la creación, podríamos pasar un ViewModel/DTO más adelante
        Task<(User User, string? ErrorMessage)> CreateUserAsync(User user, string password, int? roleId); // Devuelve tupla con usuario y mensaje de error

        // Para la actualización, usando ViewModel
        Task<(bool Success, string? ErrorMessage)> UpdateUserAsync(int userId, UserEditViewModel model, string? newAvatarUrl); // Devuelve tupla con éxito y mensaje de error
        
        Task<bool> DeleteUserAsync(int userId);

        // Método para cambiar estado del usuario
        Task<(bool Success, string NewStatusMessage, bool NewIsActiveState)> ToggleUserStatusAsync(int userId);

        // Métodos de validación
        Task<bool> IsEmailTakenAsync(string email, int? currentUserId = null);
        Task<bool> IsUserNameTakenAsync(string userName, int? currentUserId = null);

        // (Opcional, si necesitamos obtener solo los roles para los dropdowns desde aquí)
        // Task<IEnumerable<Role>> GetAllRolesAsync(); 
    }
}