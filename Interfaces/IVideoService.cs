using System.Collections.Generic;
using System.Threading.Tasks;
using ToolBox.Models;

namespace ToolBox.Interfaces
{
    public interface IVideoService
    {
        Task<(IEnumerable<Video> Videos, int TotalCount)> GetPaginatedAsync(string? searchTerm = null, int page = 1, int pageSize = 10, string? statusFilter = null, string? typeFilter = null, string? featuredFilter = null);
        Task<Video?> GetByIdAsync(int id);
        Task<Video> CreateAsync(Video video);
        Task<bool> UpdateAsync(Video video);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Video>> GetFeaturedVideosAsync();
        Task<bool> ToggleFeaturedAsync(int id);
    }
}