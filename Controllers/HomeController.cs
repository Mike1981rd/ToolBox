using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToolBox.Data;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get current user ID using the base controller method
            var userId = GetCurrentUserId();
            
            // Also log what permissions are available in ViewBag from BaseController
            _logger.LogInformation($"UserPermissions from BaseController: {ViewBag.UserPermissions != null}");
            _logger.LogInformation($"UserReadableModules count: {(ViewBag.UserReadableModules as List<string>)?.Count ?? 0}");
            
            // Get user with role information
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            // Log for debugging
            _logger.LogInformation($"Dashboard accessed by UserId: {userId}");
            
            if (user?.Role == null)
            {
                _logger.LogWarning($"User with ID {userId} or their role not found. Using default permissions.");
                // Default permissions if no role found
                ViewBag.PuedeVerMensajeBienvenidaDashboard = true;
                ViewBag.PuedeVerVideoBienvenidaDashboard = true;
                ViewBag.PuedeVerCardTotalClientesDashboard = true;
                ViewBag.PuedeVerCardClientesActivosDashboard = true;
            }
            else
            {
                var role = user.Role;
                _logger.LogInformation($"User '{user.FullName}' with Role '{role.Name}' found. Permissions: " +
                    $"Message={role.PuedeVerMensajeBienvenidaDashboard}, " +
                    $"Video={role.PuedeVerVideoBienvenidaDashboard}, " +
                    $"TotalClients={role.PuedeVerCardTotalClientesDashboard}, " +
                    $"ActiveClients={role.PuedeVerCardClientesActivosDashboard}");
                    
                // Pass permissions to view
                ViewBag.PuedeVerMensajeBienvenidaDashboard = role.PuedeVerMensajeBienvenidaDashboard;
                ViewBag.PuedeVerVideoBienvenidaDashboard = role.PuedeVerVideoBienvenidaDashboard;
                ViewBag.PuedeVerCardTotalClientesDashboard = role.PuedeVerCardTotalClientesDashboard;
                ViewBag.PuedeVerCardClientesActivosDashboard = role.PuedeVerCardClientesActivosDashboard;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TestDashboard()
        {
            return View();
        }

        // Temporary endpoint for debugging
        [HttpGet]
        public async Task<IActionResult> GetCurrentUserDebugInfo()
        {
            var userId = GetCurrentUserId();
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Json(new { error = "User not found", userId });
            }

            var debugInfo = new
            {
                userId = user.Id,
                userName = user.FullName,
                userEmail = user.Email,
                userIsActive = user.IsActive,
                roleId = user.RoleId,
                roleName = user.Role?.Name ?? "No role assigned",
                roleIsActive = user.Role?.IsActive ?? false,
                dashboardPermissions = user.Role == null ? null : new
                {
                    puedeVerMensajeBienvenida = user.Role.PuedeVerMensajeBienvenidaDashboard,
                    puedeVerVideoBienvenida = user.Role.PuedeVerVideoBienvenidaDashboard,
                    puedeVerCardTotalClientes = user.Role.PuedeVerCardTotalClientesDashboard,
                    puedeVerCardClientesActivos = user.Role.PuedeVerCardClientesActivosDashboard
                }
            };

            return Json(debugInfo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
