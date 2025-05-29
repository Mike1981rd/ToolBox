using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Interfaces
{
    public interface IQuestionnaireTemplateService
    {
        // CRUD Operations para QuestionnaireTemplate
        Task<QuestionnaireTemplate?> GetByIdAsync(long id);
        Task<QuestionnaireTemplate?> GetByIdWithQuestionsAsync(long id);
        Task<QuestionnaireTemplate> CreateAsync(QuestionnaireTemplate template);
        Task<bool> UpdateAsync(QuestionnaireTemplate template);
        Task<bool> DeleteAsync(long id);
        
        // Listing with search and pagination (filtrado por coach)
        Task<(IEnumerable<QuestionnaireTemplate> Templates, int TotalCount)> GetPaginatedAsync(
            int coachId,
            string? searchTerm = null,
            int page = 1,
            int pageSize = 10,
            bool includeInactive = false);
        
        // Toggle active state
        Task<(bool Success, string Message, bool NewState)> ToggleStatusAsync(long id, int coachId);
        
        // Get all active templates for a coach
        Task<IEnumerable<QuestionnaireTemplate>> GetAllActiveAsync(int coachId);

        // CRUD Operations para QuestionTemplate
        Task<QuestionTemplate?> GetQuestionByIdAsync(long questionId);
        Task<QuestionTemplate> CreateQuestionAsync(QuestionTemplate question);
        Task<bool> UpdateQuestionAsync(QuestionTemplate question);
        Task<bool> DeleteQuestionAsync(long questionId, int coachId);
        
        // Reordenar preguntas
        Task<bool> ReorderQuestionsAsync(long templateId, List<QuestionOrderViewModel> newOrder, int coachId);
        
        // Verificar ownership
        Task<bool> IsTemplateOwnedByCoachAsync(long templateId, int coachId);
        Task<bool> IsQuestionOwnedByCoachAsync(long questionId, int coachId);
    }
}