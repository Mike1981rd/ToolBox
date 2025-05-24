using ToolBox.Models;

namespace ToolBox.Interfaces
{
    public interface IWheelOfProgressService
    {
        Task<WheelOfProgressViewModel> GetWheelOfProgressDataAsync(int userId);
        Task<SaveProgressResponseViewModel> SaveProgressAsync(int userId, SaveProgressRequestViewModel request);
        Task<WheelOfProgressStatsViewModel> GetUserStatsAsync(int userId);
    }
}