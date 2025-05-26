using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    /// <summary>
    /// Controller for handling administrative functions and views
    /// </summary>
    public class AdminController : BaseController
    {
        private readonly ILogger<AdminController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class
        /// </summary>
        /// <param name="logger">The logger instance for this controller</param>
        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Displays the main admin dashboard
        /// </summary>
        /// <returns>The admin dashboard view</returns>
        public IActionResult Dashboard()
        {
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