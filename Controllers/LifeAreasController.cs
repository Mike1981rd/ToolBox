using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    public class LifeAreasController : Controller
    {
        private readonly ILifeAreaService _lifeAreaService;

        public LifeAreasController(ILifeAreaService lifeAreaService)
        {
            _lifeAreaService = lifeAreaService;
        }

        // GET: LifeAreas
        public async Task<IActionResult> Index(string? statusFilter = null)
        {
            ViewBag.BreadcrumbActiveKey = "breadcrumb_life_areas_list";
            ViewBag.HideTitleInLayout = true;
            
            // Default to active if not specified
            statusFilter = statusFilter ?? "active";
            ViewBag.CurrentStatusFilter = statusFilter;

            var lifeAreas = await _lifeAreaService.GetAllLifeAreasAsync(includeInactive: true);
            
            // Filter based on status
            if (statusFilter == "active")
            {
                lifeAreas = lifeAreas.Where(la => la.IsActive);
            }
            else if (statusFilter == "inactive")
            {
                lifeAreas = lifeAreas.Where(la => !la.IsActive);
            }
            
            var viewModels = lifeAreas.Select(la => new LifeAreaListViewModel
            {
                Id = la.Id,
                Title = la.Title,
                Description = la.Description,
                IconClass = la.IconClass,
                IconColor = la.IconColor,
                IsActive = la.IsActive,
                DisplayOrder = la.DisplayOrder
            }).ToList();

            return View(viewModels);
        }

        // GET: LifeAreas/GetAll (AJAX)
        [HttpGet]
        public async Task<IActionResult> GetAll(bool includeInactive = true)
        {
            var lifeAreas = await _lifeAreaService.GetAllLifeAreasAsync(includeInactive);
            var viewModels = lifeAreas.Select(la => new LifeAreaListViewModel
            {
                Id = la.Id,
                Title = la.Title,
                Description = la.Description,
                IconClass = la.IconClass,
                IconColor = la.IconColor,
                IsActive = la.IsActive,
                DisplayOrder = la.DisplayOrder
            });

            return Json(viewModels);
        }

        // GET: LifeAreas/GetById/5 (AJAX)
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var lifeArea = await _lifeAreaService.GetLifeAreaByIdAsync(id);
            if (lifeArea == null)
            {
                return NotFound();
            }

            var viewModel = new LifeAreaEditViewModel
            {
                Id = lifeArea.Id,
                Title = lifeArea.Title,
                Description = lifeArea.Description,
                IconClass = lifeArea.IconClass,
                IconColor = lifeArea.IconColor,
                IsActive = lifeArea.IsActive,
                DisplayOrder = lifeArea.DisplayOrder
            };

            return Json(viewModel);
        }

        // POST: LifeAreas/Create (AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] LifeAreaCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors[0].ErrorMessage })
                    .ToList();
                return Json(new { success = false, message = "Error de validación", errors });
            }

            // Verificar si el título ya existe
            if (await _lifeAreaService.IsTitleTakenAsync(model.Title))
            {
                return Json(new { success = false, message = "Ya existe un área de vida con ese título" });
            }

            var lifeArea = new LifeArea
            {
                Title = model.Title,
                Description = model.Description,
                IconClass = model.IconClass,
                IconColor = model.IconColor,
                IsActive = model.IsActive,
                DisplayOrder = model.DisplayOrder
            };

            var success = await _lifeAreaService.CreateLifeAreaAsync(lifeArea);

            if (success)
            {
                return Json(new { success = true, message = "Área de vida creada exitosamente" });
            }

            return Json(new { success = false, message = "Error al crear el área de vida" });
        }

        // PUT: LifeAreas/Update/5 (AJAX)
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [FromBody] LifeAreaEditViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors[0].ErrorMessage })
                    .ToList();
                return Json(new { success = false, message = "Error de validación", errors });
            }

            // Verificar si el título ya existe (excluyendo el actual)
            if (await _lifeAreaService.IsTitleTakenAsync(model.Title, model.Id))
            {
                return Json(new { success = false, message = "Ya existe un área de vida con ese título" });
            }

            var lifeArea = new LifeArea
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                IconClass = model.IconClass,
                IconColor = model.IconColor,
                IsActive = model.IsActive,
                DisplayOrder = model.DisplayOrder
            };

            var success = await _lifeAreaService.UpdateLifeAreaAsync(lifeArea);

            if (success)
            {
                return Json(new { success = true, message = "Área de vida actualizada exitosamente" });
            }

            return Json(new { success = false, message = "Error al actualizar el área de vida" });
        }

        // DELETE: LifeAreas/Delete/5 (AJAX)
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _lifeAreaService.DeleteLifeAreaAsync(id);

            if (success)
            {
                return Json(new { success = true, message = "Área de vida eliminada exitosamente" });
            }

            return Json(new { success = false, message = "Error al eliminar el área de vida" });
        }

        // POST: LifeAreas/ToggleStatus/5 (AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _lifeAreaService.ToggleLifeAreaStatusAsync(id);

            if (result.Success)
            {
                return Json(new 
                { 
                    success = true, 
                    message = $"El área de vida ha sido {(result.NewState ? "activada" : "desactivada")} exitosamente",
                    newIsActiveState = result.NewState,
                    newStatusMessage = result.Message
                });
            }

            return Json(new { success = false, message = result.Message });
        }

        // GET: LifeAreas/GetIconOptions (AJAX)
        [HttpGet]
        public IActionResult GetIconOptions()
        {
            var icons = GetAvailableIcons();
            return Json(icons);
        }

        private List<IconOption> GetAvailableIcons()
        {
            return new List<IconOption>
            {
                // Espiritual
                new IconOption { Class = "fas fa-pray", Name = "Oración", Category = "Espiritual" },
                new IconOption { Class = "fas fa-om", Name = "Om", Category = "Espiritual" },
                new IconOption { Class = "fas fa-church", Name = "Iglesia", Category = "Espiritual" },
                new IconOption { Class = "fas fa-cross", Name = "Cruz", Category = "Espiritual" },
                new IconOption { Class = "fas fa-hands", Name = "Manos", Category = "Espiritual" },
                
                // Salud
                new IconOption { Class = "fas fa-heartbeat", Name = "Latido", Category = "Salud" },
                new IconOption { Class = "fas fa-dumbbell", Name = "Pesas", Category = "Salud" },
                new IconOption { Class = "fas fa-running", Name = "Correr", Category = "Salud" },
                new IconOption { Class = "fas fa-apple-alt", Name = "Manzana", Category = "Salud" },
                new IconOption { Class = "fas fa-spa", Name = "Spa", Category = "Salud" },
                
                // Relaciones
                new IconOption { Class = "fas fa-users", Name = "Grupo", Category = "Relaciones" },
                new IconOption { Class = "fas fa-heart", Name = "Corazón", Category = "Relaciones" },
                new IconOption { Class = "fas fa-home", Name = "Hogar", Category = "Relaciones" },
                new IconOption { Class = "fas fa-user-friends", Name = "Amigos", Category = "Relaciones" },
                new IconOption { Class = "fas fa-baby", Name = "Bebé", Category = "Relaciones" },
                
                // Carrera
                new IconOption { Class = "fas fa-briefcase", Name = "Maletín", Category = "Carrera" },
                new IconOption { Class = "fas fa-rocket", Name = "Cohete", Category = "Carrera" },
                new IconOption { Class = "fas fa-chart-line", Name = "Gráfico", Category = "Carrera" },
                new IconOption { Class = "fas fa-laptop", Name = "Laptop", Category = "Carrera" },
                new IconOption { Class = "fas fa-building", Name = "Edificio", Category = "Carrera" },
                
                // Finanzas
                new IconOption { Class = "fas fa-dollar-sign", Name = "Dólar", Category = "Finanzas" },
                new IconOption { Class = "fas fa-piggy-bank", Name = "Alcancía", Category = "Finanzas" },
                new IconOption { Class = "fas fa-coins", Name = "Monedas", Category = "Finanzas" },
                new IconOption { Class = "fas fa-wallet", Name = "Cartera", Category = "Finanzas" },
                new IconOption { Class = "fas fa-credit-card", Name = "Tarjeta", Category = "Finanzas" },
                
                // Crecimiento Personal
                new IconOption { Class = "fas fa-graduation-cap", Name = "Graduación", Category = "Crecimiento" },
                new IconOption { Class = "fas fa-book", Name = "Libro", Category = "Crecimiento" },
                new IconOption { Class = "fas fa-brain", Name = "Cerebro", Category = "Crecimiento" },
                new IconOption { Class = "fas fa-lightbulb", Name = "Idea", Category = "Crecimiento" },
                new IconOption { Class = "fas fa-seedling", Name = "Brote", Category = "Crecimiento" },
                
                // Diversión
                new IconOption { Class = "fas fa-gamepad", Name = "Videojuego", Category = "Diversión" },
                new IconOption { Class = "fas fa-music", Name = "Música", Category = "Diversión" },
                new IconOption { Class = "fas fa-film", Name = "Película", Category = "Diversión" },
                new IconOption { Class = "fas fa-palette", Name = "Arte", Category = "Diversión" },
                new IconOption { Class = "fas fa-guitar", Name = "Guitarra", Category = "Diversión" },
                
                // Viajes/Experiencias
                new IconOption { Class = "fas fa-plane", Name = "Avión", Category = "Viajes" },
                new IconOption { Class = "fas fa-globe", Name = "Mundo", Category = "Viajes" },
                new IconOption { Class = "fas fa-map-marked-alt", Name = "Mapa", Category = "Viajes" },
                new IconOption { Class = "fas fa-camera", Name = "Cámara", Category = "Viajes" },
                new IconOption { Class = "fas fa-mountain", Name = "Montaña", Category = "Viajes" }
            };
        }
    }
}