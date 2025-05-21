using Microsoft.AspNetCore.Mvc;

namespace ToolBox.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            // Futuro: Pasar un ViewModel con datos de roles y usuarios
            return View();
        }

        // Futuro: Acción para mostrar el modal de añadir rol o partial view
        // Futuro: Acción HttpPost para guardar el nuevo rol
    }
}