using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToolBox.Interfaces;

namespace ToolBox.Controllers
{
    [Authorize]
    public class NotificationsMvcController : BaseController
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationsMvcController> _logger;

        public NotificationsMvcController(INotificationService notificationService, ILogger<NotificationsMvcController> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Notificaciones";
            return View();
        }

        [Authorize]
        public IActionResult Preferences()
        {
            ViewData["Title"] = "Preferencias de Notificaciones";
            return View();
        }
    }
}