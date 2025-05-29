using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Services
{
    public class QuestionnaireTemplateService : IQuestionnaireTemplateService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<QuestionnaireTemplateService> _logger;

        public QuestionnaireTemplateService(ApplicationDbContext context, ILogger<QuestionnaireTemplateService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // CRUD Operations para QuestionnaireTemplate
        public async Task<QuestionnaireTemplate?> GetByIdAsync(long id)
        {
            return await _context.QuestionnaireTemplates
                .Include(qt => qt.Coach)
                .FirstOrDefaultAsync(qt => qt.Id == id);
        }

        public async Task<QuestionnaireTemplate?> GetByIdWithQuestionsAsync(long id)
        {
            return await _context.QuestionnaireTemplates
                .Include(qt => qt.Coach)
                .Include(qt => qt.Questions.OrderBy(q => q.Order))
                .FirstOrDefaultAsync(qt => qt.Id == id);
        }

        public async Task<QuestionnaireTemplate> CreateAsync(QuestionnaireTemplate template)
        {
            template.CreatedAt = DateTime.UtcNow;
            template.UpdatedAt = DateTime.UtcNow;
            
            _context.QuestionnaireTemplates.Add(template);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Created questionnaire template {TemplateId} by coach {CoachId}", template.Id, template.CoachId);
            return template;
        }

        public async Task<bool> UpdateAsync(QuestionnaireTemplate template)
        {
            try
            {
                template.UpdatedAt = DateTime.UtcNow;
                _context.QuestionnaireTemplates.Update(template);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Updated questionnaire template {TemplateId}", template.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating questionnaire template {TemplateId}", template.Id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                var template = await _context.QuestionnaireTemplates.FindAsync(id);
                if (template == null) return false;

                // Borrado lógico
                template.IsActive = false;
                template.UpdatedAt = DateTime.UtcNow;
                
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Logically deleted questionnaire template {TemplateId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting questionnaire template {TemplateId}", id);
                return false;
            }
        }

        public async Task<(IEnumerable<QuestionnaireTemplate> Templates, int TotalCount)> GetPaginatedAsync(
            int coachId, 
            string? searchTerm = null, 
            int page = 1, 
            int pageSize = 10, 
            bool includeInactive = false)
        {
            var query = _context.QuestionnaireTemplates
                .Include(qt => qt.Coach)
                .Include(qt => qt.Questions)
                .Where(qt => qt.CoachId == coachId)
                .AsQueryable();

            // Filtrar por estado activo
            if (!includeInactive)
            {
                query = query.Where(qt => qt.IsActive);
            }

            // Filtrar por término de búsqueda
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(qt => 
                    qt.Title.Contains(searchTerm) || 
                    (qt.Description != null && qt.Description.Contains(searchTerm)));
            }

            var totalCount = await query.CountAsync();

            var templates = await query
                .OrderByDescending(qt => qt.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (templates, totalCount);
        }

        public async Task<(bool Success, string Message, bool NewState)> ToggleStatusAsync(long id, int coachId)
        {
            try
            {
                var template = await _context.QuestionnaireTemplates
                    .FirstOrDefaultAsync(qt => qt.Id == id && qt.CoachId == coachId);
                
                if (template == null)
                {
                    return (false, "Plantilla no encontrada", false);
                }

                template.IsActive = !template.IsActive;
                template.UpdatedAt = DateTime.UtcNow;
                
                await _context.SaveChangesAsync();
                
                var statusMessage = template.IsActive ? "activada" : "desactivada";
                _logger.LogInformation("Toggled status of questionnaire template {TemplateId} to {Status}", id, template.IsActive);
                
                return (true, $"Plantilla {statusMessage} exitosamente", template.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling status for questionnaire template {TemplateId}", id);
                return (false, "Error al cambiar el estado de la plantilla", false);
            }
        }

        public async Task<IEnumerable<QuestionnaireTemplate>> GetAllActiveAsync(int coachId)
        {
            return await _context.QuestionnaireTemplates
                .Where(qt => qt.CoachId == coachId && qt.IsActive)
                .OrderBy(qt => qt.Title)
                .ToListAsync();
        }

        // CRUD Operations para QuestionTemplate
        public async Task<QuestionTemplate?> GetQuestionByIdAsync(long questionId)
        {
            return await _context.QuestionTemplates
                .Include(q => q.QuestionnaireTemplate)
                .FirstOrDefaultAsync(q => q.Id == questionId);
        }

        public async Task<QuestionTemplate> CreateQuestionAsync(QuestionTemplate question)
        {
            // Establecer el orden como el último + 1
            var maxOrder = await _context.QuestionTemplates
                .Where(q => q.QuestionnaireTemplateId == question.QuestionnaireTemplateId)
                .MaxAsync(q => (int?)q.Order) ?? 0;
            
            question.Order = maxOrder + 1;
            question.CreatedAt = DateTime.UtcNow;
            question.UpdatedAt = DateTime.UtcNow;
            
            _context.QuestionTemplates.Add(question);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Created question {QuestionId} for template {TemplateId}", question.Id, question.QuestionnaireTemplateId);
            return question;
        }

        public async Task<bool> UpdateQuestionAsync(QuestionTemplate question)
        {
            try
            {
                question.UpdatedAt = DateTime.UtcNow;
                _context.QuestionTemplates.Update(question);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Updated question {QuestionId}", question.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating question {QuestionId}", question.Id);
                return false;
            }
        }

        public async Task<bool> DeleteQuestionAsync(long questionId, int coachId)
        {
            try
            {
                var question = await _context.QuestionTemplates
                    .Include(q => q.QuestionnaireTemplate)
                    .FirstOrDefaultAsync(q => q.Id == questionId && q.QuestionnaireTemplate.CoachId == coachId);
                
                if (question == null) return false;

                _context.QuestionTemplates.Remove(question);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Deleted question {QuestionId}", questionId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting question {QuestionId}", questionId);
                return false;
            }
        }

        public async Task<bool> ReorderQuestionsAsync(long templateId, List<QuestionOrderViewModel> newOrder, int coachId)
        {
            try
            {
                // Verificar que la plantilla pertenece al coach
                var template = await _context.QuestionnaireTemplates
                    .FirstOrDefaultAsync(qt => qt.Id == templateId && qt.CoachId == coachId);
                
                if (template == null) return false;

                // Obtener todas las preguntas de la plantilla
                var questions = await _context.QuestionTemplates
                    .Where(q => q.QuestionnaireTemplateId == templateId)
                    .ToListAsync();

                // Actualizar el orden
                foreach (var orderItem in newOrder)
                {
                    var question = questions.FirstOrDefault(q => q.Id == orderItem.Id);
                    if (question != null)
                    {
                        question.Order = orderItem.Order;
                        question.UpdatedAt = DateTime.UtcNow;
                    }
                }

                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Reordered questions for template {TemplateId}", templateId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reordering questions for template {TemplateId}", templateId);
                return false;
            }
        }

        // Verificar ownership
        public async Task<bool> IsTemplateOwnedByCoachAsync(long templateId, int coachId)
        {
            return await _context.QuestionnaireTemplates
                .AnyAsync(qt => qt.Id == templateId && qt.CoachId == coachId);
        }

        public async Task<bool> IsQuestionOwnedByCoachAsync(long questionId, int coachId)
        {
            return await _context.QuestionTemplates
                .Include(q => q.QuestionnaireTemplate)
                .AnyAsync(q => q.Id == questionId && q.QuestionnaireTemplate.CoachId == coachId);
        }
    }
}