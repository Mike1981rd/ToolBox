using ToolBox.Models;

namespace ToolBox.Interfaces
{
    public interface ITopicService
    {
        // CRUD Operations
        Task<Topic?> GetByIdAsync(int id);
        Task<Topic> CreateAsync(Topic topic);
        Task<bool> UpdateAsync(Topic topic);
        Task<bool> DeleteAsync(int id);
        
        // Listing with search and pagination
        Task<(IEnumerable<Topic> Topics, int TotalCount)> GetPaginatedAsync(
            string? searchTerm = null,
            int page = 1,
            int pageSize = 10,
            bool includeInactive = false);
        
        // Toggle active state
        Task<(bool Success, string Message, bool NewState)> ToggleStatusAsync(int id);
        
        // Get all active topics (for dropdowns, etc.)
        Task<IEnumerable<Topic>> GetAllActiveAsync();
    }
}