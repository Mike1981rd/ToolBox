using System.Collections.Generic;
using System.Threading.Tasks;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Interfaces
{
    public interface IUserAnswerService
    {
        Task<IEnumerable<UserAnswer>> GetUserAnswersByAreaAsync(int userId, int lifeAreaId);
        Task<UserAnswer?> GetUserAnswerAsync(int userId, int questionId);
        Task<(bool Success, int SavedCount, int UpdatedCount)> SaveUserAnswersAsync(int userId, int areaId, List<UserAnswerViewModel> answers);
        Task<bool> DeleteUserAnswerAsync(int userId, int questionId);
        Task<Dictionary<int, int>> GetUserAnswersForQuestionsAsync(int userId, List<int> questionIds);
        Task<(int TotalQuestions, int AnsweredQuestions)> GetAreaProgressAsync(int userId, int lifeAreaId);
    }
}