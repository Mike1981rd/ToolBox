using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Interfaces
{
    public interface IWheelOfLifeService
    {
        Task<WheelOfLifeDataViewModel> GetWheelDataForUserAsync(int userId);
        Task<(bool Success, int SavedCount, int UpdatedCount)> SaveUserScoresAsync(int userId, List<SaveWheelScoreViewModel> scores);
        Task<WheelOfLifeScore?> GetUserScoreForAreaAsync(int userId, int lifeAreaId);
        Task<bool> DeleteUserScoreAsync(int userId, int lifeAreaId);
    }
}