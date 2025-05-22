using Microsoft.AspNetCore.Mvc;

namespace ToolBox.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult AllCustomers()
        {
            // Futuro: ViewModel con datos de clientes
            return View();
        }

        public IActionResult Details(string id /* Usar string para IDs como #895280 */)
        {
            // Futuro: Obtener datos del cliente por ID y pasarlos a un ViewModel
            // Por ahora, datos placeholder para la UI
            ViewBag.IsEditMode = false; // Modo solo lectura
            ViewBag.CustomerId = id; // O un ViewModel completo
            ViewBag.CustomerName = "Lorine Hischke"; // Placeholder
            ViewBag.CustomerJoinDate = "Aug 17 2020, 5:48 (ET)"; // Placeholder
            ViewBag.PageTitleKey = "details_customer_page_title";
            ViewBag.BreadcrumbActiveKey = "breadcrumb_customer_details";
            return View("CustomerForm");
        }

        public IActionResult Create()
        {
            ViewBag.IsEditMode = false;
            ViewBag.PageTitleKey = "create_customer_page_title";
            ViewBag.BreadcrumbActiveKey = "breadcrumb_customer_create";
            ViewBag.CustomerId = "";
            ViewBag.CustomerName = "";
            ViewBag.CustomerJoinDate = "";
            return View("CustomerForm");
        }

        public IActionResult Edit(string id)
        {
            ViewBag.IsEditMode = true;
            ViewBag.PageTitleKey = "edit_customer_page_title";
            ViewBag.BreadcrumbActiveKey = "breadcrumb_customer_edit";
            ViewBag.CustomerId = id;
            ViewBag.CustomerName = "Lorine Hischke"; // Placeholder
            ViewBag.CustomerJoinDate = "Aug 17 2020, 5:48 (ET)"; // Placeholder
            return View("CustomerForm");
        }
    }
}