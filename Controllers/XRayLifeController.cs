using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    public class XRayLifeController : Controller
    {
        private readonly ILifeAreaService _lifeAreaService;
        private readonly IQuestionService _questionService;
        private readonly IUserAnswerService _userAnswerService;

        public XRayLifeController(ILifeAreaService lifeAreaService, IQuestionService questionService, IUserAnswerService userAnswerService)
        {
            _lifeAreaService = lifeAreaService;
            _questionService = questionService;
            _userAnswerService = userAnswerService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.BreadcrumbActiveKey = "breadcrumb_xray_life_index";
            ViewBag.HideTitleInLayout = true;
            
            // Cargar todas las áreas de vida activas
            var lifeAreas = await _lifeAreaService.GetAllLifeAreasAsync(includeInactive: false);
            ViewBag.LifeAreas = lifeAreas.ToList();
            
            // Por defecto, cargar preguntas de la primera área
            var firstAreaId = lifeAreas.FirstOrDefault()?.Id;
            if (firstAreaId.HasValue)
            {
                var questions = await _questionService.GetQuestionsByLifeAreaAsync(firstAreaId.Value);
                ViewBag.Questions = questions.ToList();
                ViewBag.SelectedAreaId = firstAreaId.Value;
            }
            else
            {
                ViewBag.Questions = new List<Question>();
                ViewBag.SelectedAreaId = 0;
            }
            
            return View();
        }

        // API Endpoints para AJAX
        
        [HttpGet]
        public async Task<IActionResult> GetQuestionsByArea(int areaId)
        {
            var questions = await _questionService.GetQuestionsByLifeAreaAsync(areaId);
            var questionViewModels = questions.Select(q => new QuestionViewModel
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                LifeAreaId = q.LifeAreaId,
                LifeAreaTitle = q.LifeArea?.Title,
                LifeAreaIconClass = q.LifeArea?.IconClass,
                LifeAreaIconColor = q.LifeArea?.IconColor
            });
            
            return Json(new { success = true, questions = questionViewModels });
        }
        
        [HttpGet]
        public async Task<IActionResult> SearchQuestions(string searchTerm, int? areaId)
        {
            var questions = await _questionService.SearchQuestionsAsync(searchTerm, areaId);
            var questionViewModels = questions.Select(q => new QuestionViewModel
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                LifeAreaId = q.LifeAreaId,
                LifeAreaTitle = q.LifeArea?.Title,
                LifeAreaIconClass = q.LifeArea?.IconClass,
                LifeAreaIconColor = q.LifeArea?.IconColor
            });
            
            return Json(new { success = true, questions = questionViewModels });
        }
        
        [HttpGet]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return Json(new { success = false, message = "Pregunta no encontrada" });
            }
            
            var viewModel = new QuestionEditViewModel
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                LifeAreaId = question.LifeAreaId
            };
            
            return Json(new { success = true, question = viewModel });
        }
        
        [HttpGet]
        public async Task<IActionResult> GetActiveLifeAreas()
        {
            var lifeAreas = await _lifeAreaService.GetAllLifeAreasAsync(includeInactive: false);
            var areaViewModels = lifeAreas.Select(la => new
            {
                la.Id,
                la.Title,
                la.IconClass,
                la.IconColor
            });
            
            return Json(new { success = true, lifeAreas = areaViewModels });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .Select(x => new { key = x.Key, errorMessage = x.Value?.Errors.FirstOrDefault()?.ErrorMessage })
                    .ToList();
                    
                return Json(new { success = false, message = "Datos inválidos", errors });
            }
            
            var question = new Question
            {
                QuestionText = model.QuestionText,
                LifeAreaId = model.LifeAreaId
            };
            
            var result = await _questionService.CreateQuestionAsync(question);
            
            if (result)
            {
                return Json(new { success = true, message = "Pregunta creada exitosamente" });
            }
            
            return Json(new { success = false, message = "Error al crear la pregunta" });
        }
        
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionEditViewModel model)
        {
            if (id != model.Id)
            {
                return Json(new { success = false, message = "ID de pregunta no coincide" });
            }
            
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .Select(x => new { key = x.Key, errorMessage = x.Value?.Errors.FirstOrDefault()?.ErrorMessage })
                    .ToList();
                    
                return Json(new { success = false, message = "Datos inválidos", errors });
            }
            
            var question = new Question
            {
                Id = model.Id,
                QuestionText = model.QuestionText,
                LifeAreaId = model.LifeAreaId
            };
            
            var result = await _questionService.UpdateQuestionAsync(question);
            
            if (result)
            {
                return Json(new { success = true, message = "Pregunta actualizada exitosamente" });
            }
            
            return Json(new { success = false, message = "Error al actualizar la pregunta" });
        }
        
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var result = await _questionService.DeleteQuestionAsync(id);
            
            if (result)
            {
                return Json(new { success = true, message = "Pregunta eliminada exitosamente" });
            }
            
            return Json(new { success = false, message = "Error al eliminar la pregunta" });
        }
        
        // ====== USER ANSWER FUNCTIONALITY ======
        
        [HttpGet]
        public async Task<IActionResult> GetUserAnswersByArea(int areaId)
        {
            try
            {
                // Get current user ID (hardcoded for now, should get from authentication)
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
                    questions = questionsWithAnswers,
                    progress = new
                    {
                        total = totalQuestions,
                        answered = answeredQuestions,
                        percentage = completionPercentage
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error al cargar las preguntas con respuestas",
                    questions = new List<object>()
                });
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAnswers([FromBody] SaveAnswersRequestViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .Select(x => x.Value?.Errors.FirstOrDefault()?.ErrorMessage ?? "Error de validación")
                        .ToList();
                        
                    return Json(new
                    {
                        success = false,
                        message = string.Join(", ", errors)
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
                
                // Save/Update answers
                var (success, savedCount, updatedCount) = await _userAnswerService.SaveUserAnswersAsync(
                    userId, 
                    model.AreaId, 
                    model.Answers
                );
                
                if (success)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Respuestas guardadas exitosamente",
                        savedCount = savedCount,
                        updatedCount = updatedCount
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "Error al guardar las respuestas. Por favor intente nuevamente."
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error al procesar la solicitud. Por favor intente nuevamente."
                });
            }
        }
        
        // Helper method to get current user ID
        // TODO: Replace with actual authentication when implemented
        private int GetCurrentUserId()
        {
            // For now, return a hardcoded user ID
            // In production, this should get the ID from the authenticated user
            // Example: return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            return 1; // Hardcoded for testing
        }
    }
}