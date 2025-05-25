using System.Collections.Generic;
using System.Threading.Tasks;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Interfaces
{
    public interface ICustomerService
    {
        // Método para obtener todos los clientes, con filtros y paginación
        Task<IEnumerable<Customer>> GetAllCustomersAsync(string? statusFilter = null, string? searchTerm = null);
        
        Task<Customer> GetCustomerByIdAsync(int customerId);

        // Para la creación de cliente
        Task<(Customer Customer, string? ErrorMessage)> CreateCustomerAsync(CustomerCreateViewModel model, string? avatarUrl, int createdByUserId);

        // Para la actualización de cliente
        Task<(bool Success, string? ErrorMessage)> UpdateCustomerAsync(int customerId, CustomerEditViewModel model, string? newAvatarUrl);
        
        Task<bool> DeleteCustomerAsync(int customerId);

        // Método para cambiar estado del cliente
        Task<(bool Success, string NewStatusMessage, bool NewIsActiveState)> ToggleCustomerStatusAsync(int customerId);

        // Métodos de validación
        Task<bool> IsEmailTakenAsync(string email, int? currentCustomerId = null);

        // Método para procesar subida de avatar
        Task<string?> ProcessAvatarUploadAsync(IFormFile avatarFile);
    }
}