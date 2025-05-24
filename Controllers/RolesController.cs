using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public RolesController(IRoleService roleService, IUserService userService, ApplicationDbContext context)
        {
            _roleService = roleService;
            _userService = userService;
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();
            var roleCardVMs = roles.Select(r => new RoleListItemViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                UserCount = r.Users?.Count() ?? 0,
                UserAvatarUrls = r.Users?.Where(u => !string.IsNullOrEmpty(u.AvatarUrl))
                                        .Select(u => u.AvatarUrl!)
                                        .Take(4)
                                        .ToList() ?? new List<string>()
            }).ToList();

            var allUsersFromDb = await _userService.GetAllUsersAsync();
            var allUserVMs = allUsersFromDb.Select(u => new UserListItemViewModel
            {
                Id = u.Id,
                AvatarUrl = u.AvatarUrl,
                FullName = u.FullName,
                Email = u.Email,
                RoleName = u.Role?.Name,
                IsActive = u.IsActive
            }).ToList();

            var pageViewModel = new RolesIndexPageViewModel
            {
                RoleCards = roleCardVMs,
                AllSystemUsers = allUserVMs
            };

            return View(pageViewModel);
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

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleService.GetRoleByIdAsync(id.Value); // GetRoleByIdAsync debe incluir RolePermissions
            if (role == null)
            {
                return NotFound();
            }

            var allPermissions = await _roleService.GetAllPermissionsAsync();
            var selectedPermissionIds = role.RolePermissions.Select(rp => rp.PermissionId).ToList();

            var viewModel = new RoleEditViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                AssignedDashboard = role.AssignedDashboard,
                IsActive = role.IsActive,
                SelectedPermissionIds = selectedPermissionIds,
                AvailablePermissions = allPermissions.Select(p => new PermissionViewModel
                {
                    Id = p.Id,
                    ModuleName = p.ModuleName,
                    ActionName = p.ActionName,
                    Description = p.Description,
                    Category = p.Category,
                    IsSelected = selectedPermissionIds.Contains(p.Id) // Marcar los permisos que el rol ya tiene
                }).ToList()
            };
            // (Opciones para AssignedDashboard podrían venir de una constante o configuración)

            return View(viewModel); // Pasa el ViewModel a la vista Views/Roles/Edit.cshtml
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoleEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            // Repopular AvailablePermissions para la vista en caso de error
            var परिशिष्टPopulatePermissions = async (RoleEditViewModel m) => {
                var allPerms = await _roleService.GetAllPermissionsAsync();
                m.AvailablePermissions = allPerms.Select(p => new PermissionViewModel {
                    Id = p.Id, ModuleName = p.ModuleName, ActionName = p.ActionName,
                    Description = p.Description, Category = p.Category,
                    IsSelected = m.SelectedPermissionIds?.Contains(p.Id) ?? false
                }).ToList();
            };

            if (ModelState.IsValid)
            {
                if (await _roleService.IsRoleNameTakenAsync(model.Name, model.Id))
                {
                    ModelState.AddModelError("Name", "Este nombre de rol ya está en uso por otro rol.");
                    await परिशिष्टPopulatePermissions(model);
                    return View(model);
                }

                try
                {
                    // Mapear ViewModel a una entidad Role para actualizar
                    var roleToUpdate = new Role 
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description ?? string.Empty,
                        AssignedDashboard = model.AssignedDashboard,
                        IsActive = model.IsActive,
                        UpdatedAt = DateTime.UtcNow
                    };

                    // Llamar al servicio con los 3 parámetros esperados
                    bool success = await _roleService.UpdateRoleAsync(model.Id, roleToUpdate, model.SelectedPermissionIds ?? new List<int>());

                    if (success)
                    {
                        TempData["SuccessMessage"] = "Rol actualizado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "No se pudo actualizar el rol. Puede que ya no exista o haya ocurrido un error.");
                    }
                }
                catch (DbUpdateConcurrencyException) // O cualquier otra excepción relevante
                {
                    if (await _roleService.GetRoleByIdAsync(model.Id) == null) // Verificar si el rol aún existe
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrió un error de concurrencia. Inténtelo de nuevo.");
                    }
                }
            }
            
            await परिशिष्टPopulatePermissions(model);
            return View(model);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleService.GetRoleByIdAsync(id.Value); // GetRoleByIdAsync ya debería cargar datos básicos del rol
            if (role == null)
            {
                return NotFound();
            }

            // Podríamos pasar un ViewModel simple a la vista de confirmación si es necesario más información.
            // Por ahora, pasaremos la entidad Role directamente si la vista de confirmación es simple.
            return View(role); // Pasa el rol a Views/Roles/Delete.cshtml
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleToDelete = await _roleService.GetRoleByIdAsync(id); // Verificar que el rol existe antes de intentar eliminar
            if (roleToDelete == null)
            {
                TempData["ErrorMessage"] = "El rol que intentas eliminar ya no existe.";
                return RedirectToAction(nameof(Index));
            }

            // Llama al servicio para eliminar. El servicio se encarga de la lógica de no eliminar si tiene usuarios.
            bool success = await _roleService.DeleteRoleAsync(id);

            if (success)
            {
                TempData["SuccessMessage"] = "Rol eliminado exitosamente.";
            }
            else
            {
                // El rol no se eliminó. Puede ser porque tiene usuarios asignados o por otro error.
                // Deberíamos proporcionar un mensaje más específico si es por usuarios asignados.
                // Podríamos modificar DeleteRoleAsync para que devuelva un enum de resultado o lance excepciones específicas.
                // Por ahora, un mensaje genérico o uno basado en si el rol aún existe y tiene usuarios.
                
                var roleCheck = await _roleService.GetRoleByIdAsync(id); // Re-consultar
                if (roleCheck != null && roleCheck.Users != null && roleCheck.Users.Any())
                {
                    TempData["ErrorMessage"] = $"El rol '{roleCheck.Name}' no se puede eliminar porque tiene usuarios asignados.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se pudo eliminar el rol. Inténtelo de nuevo.";
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}