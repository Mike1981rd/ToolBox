using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    public class WheelOfLifeController : BaseController
    {
        private readonly ILogger<WheelOfLifeController> _logger;
        private readonly IWheelOfLifeService _wheelOfLifeService;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public WheelOfLifeController(
            ILogger<WheelOfLifeController> logger,
            IWheelOfLifeService wheelOfLifeService,
            ApplicationDbContext context,
            IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _wheelOfLifeService = wheelOfLifeService;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: /WheelOfLife/GetWheelData
        [HttpGet]
        public async Task<IActionResult> GetWheelData()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0)
                {
                    return Json(new { success = false, message = "Usuario no autenticado" });
                }

                var wheelData = await _wheelOfLifeService.GetWheelDataForUserAsync(userId);
                
                return Json(new 
                { 
                    success = true, 
                    data = wheelData 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting wheel data");
                return Json(new { success = false, message = "Error al cargar los datos de la rueda de la vida" });
            }
        }

        // POST: /WheelOfLife/SaveScores
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveScores([FromBody] SaveWheelScoresRequestViewModel model)
        {
            try
            {
                _logger.LogInformation("=== SaveScores START ===");
                _logger.LogInformation("Scores count: {Count}", model?.Scores?.Count);

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

                var userId = GetCurrentUserId();
                if (userId == 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Usuario no autenticado"
                    });
                }

                if (model.Scores == null || !model.Scores.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "Por favor proporcione al menos una puntuación"
                    });
                }

                _logger.LogInformation("About to save scores for User {UserId}, Count: {Count}", 
                    userId, model.Scores.Count);
                
                // Log each score being sent
                foreach (var score in model.Scores)
                {
                    _logger.LogInformation("Score: LifeAreaId={LifeAreaId}, Score={Score}", 
                        score.LifeAreaId, score.Score);
                }

                // Save/Update scores
                var (success, savedCount, updatedCount) = await _wheelOfLifeService.SaveUserScoresAsync(
                    userId, 
                    model.Scores
                );

                _logger.LogInformation("SaveUserScoresAsync returned: Success={Success}, SavedCount={SavedCount}, UpdatedCount={UpdatedCount}", 
                    success, savedCount, updatedCount);

                if (success)
                {
                    _logger.LogInformation(
                        "User {UserId} saved wheel scores: {SavedCount} new, {UpdatedCount} updated", 
                        userId, savedCount, updatedCount
                    );

                    string message;
                    if (savedCount == 0 && updatedCount == 0)
                    {
                        message = "No se realizaron cambios - las puntuaciones ya estaban guardadas con los mismos valores";
                    }
                    else if (savedCount > 0 && updatedCount > 0)
                    {
                        message = $"Puntuaciones guardadas exitosamente: {savedCount} nuevas, {updatedCount} actualizadas";
                    }
                    else if (savedCount > 0)
                    {
                        message = $"Se guardaron {savedCount} puntuaciones nuevas exitosamente";
                    }
                    else
                    {
                        message = $"Se actualizaron {updatedCount} puntuaciones exitosamente";
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
                    _logger.LogWarning("SaveUserScoresAsync failed for User {UserId}", userId);
                    return Json(new
                    {
                        success = false,
                        message = "Error al guardar las puntuaciones"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving wheel scores");
                
                var errorMessage = "Error al guardar las puntuaciones";
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

    }
}