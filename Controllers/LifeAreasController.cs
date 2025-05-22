using Microsoft.AspNetCore.Mvc;

public class LifeAreasController : Controller
{
    public IActionResult Index()
    {
        ViewBag.BreadcrumbActiveKey = "breadcrumb_life_areas_list";
        ViewBag.HideTitleInLayout = true;
        // Futuro: Pasar lista de LifeAreasViewModel
        return View();
    }

    // Acciones para Add/Edit/Deactivate (se manejarán con AJAX o carga de partial para el offcanvas)
}