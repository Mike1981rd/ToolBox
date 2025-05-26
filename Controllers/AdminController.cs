using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToolBox.Data;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    /// <summary>
    /// Controller for handling administrative functions and views
    /// </summary>
    public class AdminController : BaseController
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class
        /// </summary>
        /// <param name="logger">The logger instance for this controller</param>
        /// <param name="context">The database context</param>
        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Displays the main admin dashboard
        /// </summary>
        /// <returns>The admin dashboard view</returns>
        public async Task<IActionResult> Dashboard()
        {
            // Get current user ID using the base controller method
            var userId = GetCurrentUserId();
            
            // Get user with role information
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            if (user?.Role == null)
            {
                // Default permissions if no role found
                ViewBag.PuedeVerMensajeBienvenidaDashboard = true;
                ViewBag.PuedeVerVideoBienvenidaDashboard = true;
                ViewBag.PuedeVerCardTotalClientesDashboard = true;
                ViewBag.PuedeVerCardClientesActivosDashboard = true;
            }
            else
            {
                // Pass permissions to view
                ViewBag.PuedeVerMensajeBienvenidaDashboard = user.Role.PuedeVerMensajeBienvenidaDashboard;
                ViewBag.PuedeVerVideoBienvenidaDashboard = user.Role.PuedeVerVideoBienvenidaDashboard;
                ViewBag.PuedeVerCardTotalClientesDashboard = user.Role.PuedeVerCardTotalClientesDashboard;
                ViewBag.PuedeVerCardClientesActivosDashboard = user.Role.PuedeVerCardClientesActivosDashboard;
            }

            return View();
        }

        /// <summary>
        /// Displays the users management page
        /// </summary>
        /// <returns>The users management view</returns>
        public IActionResult Users()
        {
            return View();
        }

        /// <summary>
        /// Displays the analytics page with site statistics
        /// </summary>
        /// <returns>The analytics view</returns>
        public IActionResult Analytics()
        {
            return View();
        }

        /// <summary>
        /// Displays the site settings configuration page
        /// </summary>
        /// <returns>The settings view</returns>
        public IActionResult Settings()
        {
            return View();
        }

        /// <summary>
        /// Handles errors in the admin section
        /// </summary>
        /// <returns>The error view</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}