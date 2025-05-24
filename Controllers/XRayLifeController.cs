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

        public XRayLifeController(ILifeAreaService lifeAreaService, IQuestionService questionService)
        {
            _lifeAreaService = lifeAreaService;
            _questionService = questionService;
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
    }
}