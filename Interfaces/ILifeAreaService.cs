using System.Collections.Generic;
using System.Threading.Tasks;
using ToolBox.Models;

namespace ToolBox.Interfaces
{
    public interface ILifeAreaService
    {
        Task<IEnumerable<LifeArea>> GetAllLifeAreasAsync(bool includeInactive = false);
        Task<LifeArea?> GetLifeAreaByIdAsync(int id);
        Task<bool> CreateLifeAreaAsync(LifeArea lifeArea);
        Task<bool> UpdateLifeAreaAsync(LifeArea lifeArea);
        Task<bool> DeleteLifeAreaAsync(int id);
        Task<(bool Success, string Message, bool NewState)> ToggleLifeAreaStatusAsync(int id);
        Task<bool> IsTitleTakenAsync(string title, int? excludeId = null);
        Task<int> GetNextDisplayOrderAsync();
        Task<bool> UpdateDisplayOrderAsync(int id, int newOrder);
    }
}