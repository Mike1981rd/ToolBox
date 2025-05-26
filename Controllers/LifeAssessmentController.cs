using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolBox.Models;
using ToolBox.Models.ViewModels;
using ToolBox.Interfaces;
using ToolBox.Data;
using System.Linq;

namespace ToolBox.Controllers
{
    public class LifeAssessmentController : BaseController
    {
        private readonly ILogger<LifeAssessmentController> _logger;
        private readonly ILifeAreaService _lifeAreaService;
        private readonly IQuestionService _questionService;
        private readonly IUserAnswerService _userAnswerService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LifeAssessmentController(
            ILogger<LifeAssessmentController> logger,
            ILifeAreaService lifeAreaService,
            IQuestionService questionService,
            IUserAnswerService userAnswerService,
            IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _lifeAreaService = lifeAreaService;
            _questionService = questionService;
            _userAnswerService = userAnswerService;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Life Assessment index page");
                return View();
            }
        }

        // Get active life areas for the dropdown filter
        [HttpGet]
        public async Task<IActionResult> GetActiveLifeAreas()
        {
            try
            {
                var lifeAreas = await _lifeAreaService.GetAllLifeAreasAsync(includeInactive: false);
                var lifeAreasList = lifeAreas.ToList();
                
                // Get question counts for each area
                var result = new List<object>();
                foreach (var la in lifeAreasList)
                {
                    var questions = await _questionService.GetQuestionsByLifeAreaAsync(la.Id);
                    var questionCount = questions.Count();
                    
                    result.Add(new
                    {
                        id = la.Id,
                        name = la.Title,
                        description = la.Description,
                        icon = la.IconClass,
                        iconColor = la.IconColor,
                        questionCount = questionCount
                    });
                }

                return Json(new { success = true, areas = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active life areas");
                return Json(new { success = false, message = "Error al cargar las áreas de vida" });
            }
        }

        // Get questions for a specific area with user's existing answers
        [HttpGet]
        public async Task<IActionResult> GetQuestionsByArea(int areaId)
        {
            try
            {
                // Get current user ID (for now using a hardcoded value, should get from authentication)
                var userId = GetCurrentUserId();
                if (userId == 0)
                {
                    return Json(new { success = false, message = "Usuario no autenticado" });
                }

                // Get all questions for the area
                var questions = await _questionService.GetQuestionsByLifeAreaAsync(areaId);
                var questionsList = questions.ToList();

                // Get user's existing answers for these questions
                var questionIds = questionsList.Select(q => q.Id).ToList();
                var userAnswers = await _userAnswerService.GetUserAnswersForQuestionsAsync(userId, questionIds);

                // Build the response with questions and existing answers
                var questionsWithAnswers = questionsList.Select(q => new
                {
                    questionId = q.Id,
                    questionText = q.QuestionText,
                    existingScore = userAnswers.ContainsKey(q.Id) ? userAnswers[q.Id] : (int?)null
                }).ToList();

                // Calculate progress
                var totalQuestions = questionsList.Count;
                var answeredQuestions = userAnswers.Count;
                var completionPercentage = totalQuestions > 0 
                    ? Math.Round((decimal)answeredQuestions / totalQuestions * 100, 0) 
                    : 0;

                // Get area info
                var area = await _lifeAreaService.GetLifeAreaByIdAsync(areaId);
                
                return Json(new
                {
                    success = true,
                    areaName = area?.Title ?? "",
                    questions = questionsWithAnswers
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting questions for area {AreaId}", areaId);
                return Json(new
                {
                    success = false,
                    message = "Error al cargar las preguntas",
                    questions = new List<object>()
                });
            }
        }

        // Save/Update user answers (upsert)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAnswers([FromBody] SaveAnswersRequestViewModel model)
        {
            try
            {
                _logger.LogInformation("=== SaveAnswers START ===");
                _logger.LogInformation("Raw model received: {@Model}", model);
                _logger.LogInformation("SaveAnswers called with AreaId: {AreaId}, Answers count: {Count}", 
                    model?.AreaId, model?.Answers?.Count);

                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors.Select(e => $"{x.Key}: {e.ErrorMessage}"))
                        .ToList();
                    
                    _logger.LogWarning("ModelState is invalid: {Errors}", string.Join(", ", errors));
                        
                    return Json(new
                    {
                        success = false,
                        message = "Error de validación: " + string.Join(", ", errors)
                    });
                }

                // Get current user ID
                var userId = GetCurrentUserId();
                if (userId == 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Usuario no autenticado"
                    });
                }

                // Validate answers - allow saving even with just one answer
                if (model.Answers == null || !model.Answers.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "Por favor responda al menos una pregunta antes de guardar"
                    });
                }

                _logger.LogInformation("About to save answers for User {UserId}, Area {AreaId}, Count: {Count}", 
                    userId, model.AreaId, model.Answers.Count);
                
                // Log each answer being sent
                foreach (var answer in model.Answers)
                {
                    _logger.LogInformation("Answer: QuestionId={QuestionId}, Score={Score}", 
                        answer.QuestionId, answer.Score);
                }

                // Save/Update answers
                var (success, savedCount, updatedCount) = await _userAnswerService.SaveUserAnswersAsync(
                    userId, 
                    model.AreaId, 
                    model.Answers
                );

                _logger.LogInformation("SaveUserAnswersAsync returned: Success={Success}, SavedCount={SavedCount}, UpdatedCount={UpdatedCount}", 
                    success, savedCount, updatedCount);

                if (success)
                {
                    _logger.LogInformation(
                        "User {UserId} saved answers for area {AreaId}: {SavedCount} new, {UpdatedCount} updated", 
                        userId, model.AreaId, savedCount, updatedCount
                    );

                    string message;
                    if (savedCount == 0 && updatedCount == 0)
                    {
                        message = "No se realizaron cambios - las respuestas ya estaban guardadas con los mismos valores";
                    }
                    else if (savedCount > 0 && updatedCount > 0)
                    {
                        message = $"Respuestas guardadas exitosamente: {savedCount} nuevas, {updatedCount} actualizadas";
                    }
                    else if (savedCount > 0)
                    {
                        message = $"Se guardaron {savedCount} respuestas nuevas exitosamente";
                    }
                    else
                    {
                        message = $"Se actualizaron {updatedCount} respuestas exitosamente";
                    }

                    return Json(new
                    {
                        success = true,
                        message = message,
                        savedCount = savedCount,
                        updatedCount = updatedCount
                    });
                }
                else
                {
                    _logger.LogWarning("SaveUserAnswersAsync failed for User {UserId}, Area {AreaId}", userId, model.AreaId);
                    return Json(new
                    {
                        success = false,
                        message = "Error al guardar las respuestas - El servicio retornó false"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving user answers for area {AreaId}", model?.AreaId);
                
                // In development, return more detailed error information
                var errorMessage = "Error al guardar las respuestas";
                if (_hostEnvironment?.IsDevelopment() == true)
                {
                    errorMessage += $": {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        errorMessage += $" - Inner: {ex.InnerException.Message}";
                    }
                }
                
                return Json(new
                {
                    success = false,
                    message = errorMessage
                });
            }
        }

        // Helper method to get current user ID
        // TODO: Replace with actual authentication when implemented
        private int GetCurrentUserId()
        {
            // For now, get the first active user from the database for testing
            // In production, this should get the ID from the authenticated user
            // Example: return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            
            try
            {
                var context = HttpContext.RequestServices.GetService<ApplicationDbContext>();
                var firstActiveUser = context.Users
                    .Where(u => u.IsActive)
                    .OrderBy(u => u.Id)
                    .FirstOrDefault();
                    
                if (firstActiveUser != null)
                {
                    _logger.LogDebug("Using test user: {UserId} - {UserName}", firstActiveUser.Id, firstActiveUser.FullName);
                    return firstActiveUser.Id;
                }
                else
                {
                    _logger.LogError("No active users found in database!");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting test user ID");
                return 0;
            }
        }
    }
}