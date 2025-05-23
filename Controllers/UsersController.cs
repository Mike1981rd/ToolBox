using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Para SelectList
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting; // Para IWebHostEnvironment
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using ToolBox.Data;
using ToolBox.Models;
using ToolBox.Interfaces;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService; // Para obtener la lista de roles
        private readonly IWebHostEnvironment _webHostEnvironment; // Para manejar archivos

        public UsersController(ApplicationDbContext context, IUserService userService, IRoleService roleService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userService = userService;
            _roleService = roleService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Users
        public async Task<IActionResult> Index(string? roleFilter = null, string? statusFilter = null, string? searchTerm = null)
        {
            // Por defecto mostrar usuarios activos
            statusFilter = statusFilter ?? "active";
            var users = await _userService.GetAllUsersAsync(roleFilter, statusFilter, searchTerm);
            var userViewModels = users.Select(u => new UserListItemViewModel
            {
                Id = u.Id,
                AvatarUrl = u.AvatarUrl,
                FullName = u.FullName,
                Email = u.Email,
                RoleName = u.Role?.Name, // Asumiendo que Role se carga en GetAllUsersAsync
                BillingMethod = u.BillingMethod,
                IsActive = u.IsActive,
                StatusDetail = u.StatusDetail 
            }).ToList();

            // Para los filtros, también necesitaremos pasar la lista de roles y estados
            var roles = await _roleService.GetAllRolesAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name", roleFilter);
            // También pasamos los roles para el offcanvas de crear usuario
            ViewBag.AvailableRoles = roles;
            // Pasar los filtros actuales a la vista para mantener la selección
            ViewBag.CurrentRoleFilter = roleFilter;
            ViewBag.CurrentStatusFilter = statusFilter ?? "active"; // Por defecto active
            ViewBag.CurrentSearchTerm = searchTerm;
            
            return View(userViewModels);
        }
        
        // Acciones para Exportar
        public async Task<IActionResult> ExportToExcel()
        {
            var users = await _context.Users.ToListAsync();
            
            // En una implementación real, utilizaremos EPPlus o ClosedXML para generar Excel
            // Por ahora, crearemos un archivo CSV con formato simple
            var builder = new StringBuilder();
            builder.AppendLine("ID,Name,Email,Role,Status,Created Date");
            
            foreach (var user in users)
            {
                builder.AppendLine($"{user.Id},\"{user.FullName}\",\"{user.Email}\",\"{user.Role?.Name}\",\"{(user.IsActive ? "Active" : "Inactive")}\",\"{user.CreatedAt.ToShortDateString()}\"");
            }
            
            byte[] fileBytes = Encoding.UTF8.GetBytes(builder.ToString());
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
        }

        public async Task<IActionResult> ExportToPdf()
        {
            var users = await _context.Users.ToListAsync();
            
            // En una implementación real, utilizaríamos QuestPDF o similar para generar PDF
            // Por ahora, mostraremos un mensaje indicando que se requiere la biblioteca
            return Content("Para implementar esta funcionalidad, instalar el paquete NuGet QuestPDF");
        }

        public async Task<IActionResult> ExportToCsv()
        {
            var users = await _context.Users.ToListAsync();
            
            var builder = new StringBuilder();
            builder.AppendLine("ID,Name,Email,Role,Status,Created Date");
            
            foreach (var user in users)
            {
                builder.AppendLine($"{user.Id},\"{user.FullName}\",\"{user.Email}\",\"{user.Role?.Name}\",\"{(user.IsActive ? "Active" : "Inactive")}\",\"{user.CreatedAt.ToShortDateString()}\"");
            }
            
            byte[] fileBytes = Encoding.UTF8.GetBytes(builder.ToString());
            return File(fileBytes, "text/csv", "users.csv");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create (o /Users/GetCreateUserModal si es para un modal AJAX)
        public async Task<IActionResult> Create() // O GetCreateUserModal()
        {
            var viewModel = new UserCreateViewModel
            {
                AvailableRoles = new SelectList(await _roleService.GetAllRolesAsync(), "Id", "Name")
            };
            
            // Si es un modal que se carga en la página Index:
            // return PartialView("_CreateUserModal", viewModel); 
            // Si es una página completa:
            return View(viewModel); // Asumiremos página completa por ahora, ajustar si es modal
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            // Función helper para repoblar AvailableRoles en caso de error
            async Task PopulateAvailableRoles(UserCreateViewModel m)
            {
                m.AvailableRoles = new SelectList(await _roleService.GetAllRolesAsync(), "Id", "Name", m.RoleId);
            }

            if (ModelState.IsValid)
            {
                // Manejar la subida del archivo de avatar
                string? avatarUrl = null;
                if (model.AvatarFile != null && model.AvatarFile.Length > 0)
                {
                    // Validar tamaño del archivo (800KB max)
                    if (model.AvatarFile.Length > 800 * 1024)
                    {
                        ModelState.AddModelError("AvatarFile", "El archivo es demasiado grande. El tamaño máximo permitido es 800KB.");
                        await PopulateAvailableRoles(model);
                        return View(model);
                    }

                    // Validar tipo de archivo
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(model.AvatarFile.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("AvatarFile", "Solo se permiten archivos de imagen (JPG, PNG, GIF).");
                        await PopulateAvailableRoles(model);
                        return View(model);
                    }

                    // Crear el directorio si no existe
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "avatars");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generar nombre único para el archivo
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.AvatarFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Guardar el archivo
                    try
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.AvatarFile.CopyToAsync(fileStream);
                        }
                        avatarUrl = "/uploads/avatars/" + uniqueFileName;
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        ModelState.AddModelError("AvatarFile", "Error al guardar el archivo. Por favor intente nuevamente.");
                        await PopulateAvailableRoles(model);
                        return View(model);
                    }
                }

                // Mapear ViewModel a la entidad User
                var newUser = new User
                {
                    FullName = model.FullName,
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    CompanyName = model.CompanyName,
                    Country = model.Country,
                    TaxId = model.TaxId,
                    BillingMethod = model.BillingMethod,
                    Language = model.Language,
                    IsTwoFactorEnabled = model.IsTwoFactorEnabled,
                    IsActive = model.IsActive,
                    AvatarUrl = avatarUrl // Asignar la URL del avatar si se subió
                };

                var (createdUser, errorMessage) = await _userService.CreateUserAsync(newUser, model.Password, model.RoleId);

                if (createdUser != null)
                {
                    TempData["SuccessMessage"] = "Usuario creado exitosamente.";
                    return RedirectToAction(nameof(Index)); // O a donde sea apropiado
                }
                else
                {
                    // Añadir el error específico devuelto por el servicio
                    ModelState.AddModelError(string.Empty, errorMessage ?? "No se pudo crear el usuario. Por favor, revise los datos.");
                }
            }

            // Si llegamos aquí, algo falló, volvemos a mostrar el formulario
            await PopulateAvailableRoles(model);
            // Si es un modal, retornar PartialView("_CreateUserModal", model);
            return View(model); // O PartialView si es un modal
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserEditViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CompanyName = user.CompanyName,
                Country = user.Country,
                TaxId = user.TaxId,
                RoleId = user.RoleId,
                BillingMethod = user.BillingMethod,
                IsActive = user.IsActive,
                Language = user.Language,
                IsTwoFactorEnabled = user.IsTwoFactorEnabled,
                ExistingAvatarUrl = user.AvatarUrl,
                AvailableRoles = new SelectList(await _roleService.GetAllRolesAsync(), "Id", "Name", user.RoleId)
            };

            return View(viewModel);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            // Función helper para repoblar AvailableRoles en caso de error
            async Task PopulateAvailableRoles(UserEditViewModel m)
            {
                m.AvailableRoles = new SelectList(await _roleService.GetAllRolesAsync(), "Id", "Name", m.RoleId);
            }

            if (ModelState.IsValid)
            {
                // Obtener el usuario actual para mantener su avatar si no se sube uno nuevo
                var currentUser = await _userService.GetUserByIdAsync(id);
                if (currentUser == null)
                {
                    return NotFound();
                }

                // Manejar la subida del archivo de avatar
                string? newAvatarUrl = currentUser.AvatarUrl; // Mantener el existente por defecto

                if (model.AvatarFile != null && model.AvatarFile.Length > 0)
                {
                    // Validar tamaño del archivo (800KB max)
                    if (model.AvatarFile.Length > 800 * 1024)
                    {
                        ModelState.AddModelError("AvatarFile", "El archivo es demasiado grande. El tamaño máximo permitido es 800KB.");
                        await PopulateAvailableRoles(model);
                        return View(model);
                    }

                    // Validar tipo de archivo
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(model.AvatarFile.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("AvatarFile", "Solo se permiten archivos de imagen (JPG, PNG, GIF).");
                        await PopulateAvailableRoles(model);
                        return View(model);
                    }

                    // Crear el directorio si no existe
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "avatars");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generar nombre único para el archivo
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.AvatarFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    try
                    {
                        // Guardar el nuevo archivo
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.AvatarFile.CopyToAsync(fileStream);
                        }

                        // Opcional: Eliminar el archivo de avatar antiguo si existe
                        if (!string.IsNullOrEmpty(currentUser.AvatarUrl) && currentUser.AvatarUrl.StartsWith("/uploads/avatars/"))
                        {
                            string oldFileName = Path.GetFileName(currentUser.AvatarUrl);
                            string oldFilePath = Path.Combine(uploadsFolder, oldFileName);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                try
                                {
                                    System.IO.File.Delete(oldFilePath);
                                }
                                catch (Exception ex)
                                {
                                    // Log the error but continue - old file deletion is not critical
                                    Console.WriteLine($"Error al eliminar archivo anterior: {ex.Message}");
                                }
                            }
                        }

                        newAvatarUrl = "/uploads/avatars/" + uniqueFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("AvatarFile", "Error al guardar el archivo. Por favor intente nuevamente.");
                        await PopulateAvailableRoles(model);
                        return View(model);
                    }
                }

                // Llamar al servicio para actualizar el usuario
                var (success, errorMessage) = await _userService.UpdateUserAsync(id, model, newAvatarUrl);

                if (success)
                {
                    TempData["SuccessMessage"] = "Usuario actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, errorMessage ?? "No se pudo actualizar el usuario. Por favor, revise los datos.");
                }
            }

            // Si llegamos aquí, algo falló, volvemos a mostrar el formulario
            await PopulateAvailableRoles(model);
            model.ExistingAvatarUrl = (await _userService.GetUserByIdAsync(id))?.AvatarUrl;
            return View(model);
        }

        // GET: Users/Delete/5 - DESHABILITADO: Ahora usamos ToggleStatus
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var user = await _context.Users
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(user);
        // }

        // POST: Users/Delete/5 - DESHABILITADO: Ahora usamos ToggleStatus
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var user = await _context.Users.FindAsync(id);
        //     _context.Users.Remove(user);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        // POST: Users/ToggleStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var (success, newStatusMessage, newIsActiveState) = await _userService.ToggleUserStatusAsync(id);

            if (success)
            {
                return Json(new { 
                    success = true, 
                    message = $"El estado del usuario ha sido cambiado a '{newStatusMessage}'.",
                    newIsActiveState = newIsActiveState,
                    newStatusMessage = newStatusMessage
                });
            }
            else
            {
                return Json(new { 
                    success = false, 
                    message = newStatusMessage 
                });
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}