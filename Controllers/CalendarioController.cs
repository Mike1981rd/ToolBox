using Microsoft.AspNetCore.Mvc;

namespace ToolBox.Controllers
{
    public class CalendarioController : BaseController
    {
        private readonly ILogger<CalendarioController> _logger;

        public CalendarioController(ILogger<CalendarioController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}