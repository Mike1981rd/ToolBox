using Microsoft.AspNetCore.Mvc;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class WheelOfProgressController : BaseController
    {
        private readonly IWheelOfProgressService _wheelOfProgressService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WheelOfProgressController> _logger;

        public WheelOfProgressController(
            IWheelOfProgressService wheelOfProgressService,
            ApplicationDbContext context,
            ILogger<WheelOfProgressController> logger)
        {
            _wheelOfProgressService = wheelOfProgressService;
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetWheelData()
        {
            try
            {
                var userId = GetCurrentUserId();
                var wheelData = await _wheelOfProgressService.GetWheelOfProgressDataAsync(userId);
                
                return Json(new { success = true, data = wheelData });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting wheel of progress data");
                return Json(new { success = false, message = "Error al cargar los datos" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveProgress([FromBody] SaveProgressRequestViewModel request)
        {
            try
            {
                // No validar ModelState para permitir cualquier tipo de guardado
                var userId = GetCurrentUserId();
                var result = await _wheelOfProgressService.SaveProgressAsync(userId, request);
                
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving wheel of progress data");
                return Json(new { success = false, message = "Error al guardar el progreso" });
            }
        }

    }
}