using System.Collections.Generic;
using System.Threading.Tasks;
using ToolBox.Models;

namespace ToolBox.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<IEnumerable<Question>> GetQuestionsByLifeAreaAsync(int lifeAreaId);
        Task<Question?> GetQuestionByIdAsync(int id);
        Task<bool> CreateQuestionAsync(Question question);
        Task<bool> UpdateQuestionAsync(Question question);
        Task<bool> DeleteQuestionAsync(int id); // Hard delete
        Task<IEnumerable<Question>> SearchQuestionsAsync(string searchTerm, int? lifeAreaId = null);
    }
}