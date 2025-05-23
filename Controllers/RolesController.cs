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
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();
            
            // TEMPORAL: Filtrar para mostrar solo roles específicos durante desarrollo
            // Comentar o eliminar esta línea para mostrar todos los roles
            // roles = roles.Where(r => r.Name == "TEST").ToList();
            
            var roleViewModels = roles.Select(r => new RoleListItemViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                AssignedDashboard = r.AssignedDashboard,
                IsActive = r.IsActive
            }).ToList();

            return View(roleViewModels);
        }

        // GET: Roles/Create
        public async Task<IActionResult> Create()
        {
            var allPermissions = await _roleService.GetAllPermissionsAsync();
            var viewModel = new RoleCreateViewModel
            {
                AvailablePermissions = allPermissions.Select(p => new PermissionViewModel
                {
                    Id = p.Id,
                    ModuleName = p.ModuleName,
                    ActionName = p.ActionName,
                    Description = p.Description,
                    Category = p.Category
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validar si el nombre del rol ya existe
                if (await _roleService.IsRoleNameTakenAsync(model.Name))
                {
                    ModelState.AddModelError("Name", "Este nombre de rol ya está en uso.");
                    // Repoblar AvailablePermissions si se vuelve a mostrar el formulario
                    var allPermissions = await _roleService.GetAllPermissionsAsync();
                    model.AvailablePermissions = allPermissions.Select(p => new PermissionViewModel
                    {
                        Id = p.Id,
                        ModuleName = p.ModuleName,
                        ActionName = p.ActionName,
                        Description = p.Description,
                        Category = p.Category
                    }).ToList();
                    return View(model);
                }

                var newRole = new Role
                {
                    Name = model.Name,
                    Description = model.Description ?? string.Empty,
                    AssignedDashboard = model.AssignedDashboard,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var createdRole = await _roleService.CreateRoleAsync(newRole, model.SelectedPermissionIds ?? new List<int>());

                if (createdRole != null)
                {
                    TempData["SuccessMessage"] = "Rol creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al crear el rol.");
                }
            }

            // Si llegamos aquí, algo falló, volvemos a mostrar el formulario
            // Repoblar AvailablePermissions
            var permissionsForForm = await _roleService.GetAllPermissionsAsync();
            model.AvailablePermissions = permissionsForForm.Select(p => new PermissionViewModel
            {
                Id = p.Id,
                ModuleName = p.ModuleName,
                ActionName = p.ActionName,
                Description = p.Description,
                Category = p.Category
            }).ToList();
            return View(model);
        }
    }
}