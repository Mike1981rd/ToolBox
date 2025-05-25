using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
using ClosedXML.Excel;

namespace ToolBox.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerService _customerService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CustomersController(ApplicationDbContext context, ICustomerService customerService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _customerService = customerService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string? statusFilter = null, string? searchTerm = null)
        {
            // Por defecto mostrar clientes activos
            statusFilter = statusFilter ?? "active";
            var customers = await _customerService.GetAllCustomersAsync(statusFilter, searchTerm);
            var customerViewModels = customers.Select(c => new CustomerListItemViewModel
            {
                Id = c.Id,
                FullName = c.FullName,
                CustomerNumber = c.CustomerNumber,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                CompanyName = c.CompanyName,
                Country = c.Country,
                AvatarUrl = c.AvatarUrl,
                IsActive = c.IsActive,
                StatusDetail = c.StatusDetail,
                CreatedAt = c.CreatedAt,
                CreatedByUserName = c.CreatedByUser?.FullName
            }).ToList();

            // Pasar los filtros actuales a la vista para mantener la selección
            ViewBag.CurrentStatusFilter = statusFilter ?? "active";
            ViewBag.CurrentSearchTerm = searchTerm;
            
            return View(customerViewModels);
        }

        // Mantener compatibilidad con la vista existente
        public async Task<IActionResult> AllCustomers(string? statusFilter = null, string? searchTerm = null)
        {
            return await Index(statusFilter, searchTerm);
        }
        
        // Acciones para Exportar
        public async Task<IActionResult> ExportToExcel(string? statusFilter = null, string? searchTerm = null)
        {
            var customers = await _customerService.GetAllCustomersAsync(statusFilter, searchTerm);
            var dataToExport = customers.Select(c => new {
                CustomerNumber = c.CustomerNumber,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                CompanyName = c.CompanyName ?? "N/A",
                Country = c.Country ?? "N/A",
                Status = c.IsActive ? "Activo" : "Inactivo",
                CreatedAt = c.CreatedAt.ToString("dd/MM/yyyy")
            }).ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Clientes");
                
                // Cabeceras
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Número Cliente";
                worksheet.Cell(currentRow, 2).Value = "Nombre Completo";
                worksheet.Cell(currentRow, 3).Value = "Email";
                worksheet.Cell(currentRow, 4).Value = "Teléfono";
                worksheet.Cell(currentRow, 5).Value = "Empresa";
                worksheet.Cell(currentRow, 6).Value = "País";
                worksheet.Cell(currentRow, 7).Value = "Estado";
                worksheet.Cell(currentRow, 8).Value = "Fecha Creación";
                
                // Aplicar estilo a la cabecera
                worksheet.Row(currentRow).Style.Font.Bold = true;
                worksheet.Row(currentRow).Style.Fill.BackgroundColor = XLColor.LightBlue;
                worksheet.Row(currentRow).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Datos
                foreach (var customer in dataToExport)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = customer.CustomerNumber;
                    worksheet.Cell(currentRow, 2).Value = customer.FullName;
                    worksheet.Cell(currentRow, 3).Value = customer.Email;
                    worksheet.Cell(currentRow, 4).Value = customer.PhoneNumber;
                    worksheet.Cell(currentRow, 5).Value = customer.CompanyName;
                    worksheet.Cell(currentRow, 6).Value = customer.Country;
                    worksheet.Cell(currentRow, 7).Value = customer.Status;
                    worksheet.Cell(currentRow, 8).Value = customer.CreatedAt;
                }

                // Ajustar ancho de columnas
                worksheet.Columns().AdjustToContents();
                
                // Agregar bordes a toda la tabla
                var tableRange = worksheet.Range(1, 1, currentRow, 8);
                tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    if (content == null || content.Length == 0) {
                        return Content("Error generando el archivo Excel.", "text/plain");
                    }
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Clientes_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
                }
            }
        }

        public async Task<IActionResult> ExportToCsv(string? statusFilter = null, string? searchTerm = null)
        {
            // Por defecto mostrar clientes activos si no se especifica filtro
            statusFilter = statusFilter ?? "active";
            var customers = await _customerService.GetAllCustomersAsync(statusFilter, searchTerm);
            
            var builder = new StringBuilder();
            builder.AppendLine("ID,Número Cliente,Nombre,Apellido,Email,Teléfono,Empresa,País,Estado,Fecha Creación");
            
            foreach (var customer in customers)
            {
                builder.AppendLine($"{customer.Id},\"{customer.CustomerNumber}\",\"{customer.FirstName}\",\"{customer.LastName}\",\"{customer.Email}\",\"{customer.PhoneNumber}\",\"{customer.CompanyName ?? "N/A"}\",\"{customer.Country ?? "N/A"}\",\"{(customer.IsActive ? "Activo" : "Inactivo")}\",\"{customer.CreatedAt:dd/MM/yyyy}\"");
            }
            
            byte[] fileBytes = Encoding.UTF8.GetBytes(builder.ToString());
            string fileName = $"Clientes_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            return File(fileBytes, "text/csv", fileName);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerDetailsViewModel
            {
                Id = customer.Id,
                FullName = customer.FullName,
                CustomerNumber = customer.CustomerNumber,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CompanyName = customer.CompanyName,
                Country = customer.Country,
                AvatarUrl = customer.AvatarUrl,
                IsActive = customer.IsActive,
                StatusDetail = customer.StatusDetail,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt,
                CreatedByUserName = customer.CreatedByUser?.FullName
            };

            return View(viewModel);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            var viewModel = new CustomerCreateViewModel();
            return View(viewModel);
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Manejar la subida del archivo de avatar
                string? avatarUrl = null;
                if (model.AvatarFile != null && model.AvatarFile.Length > 0)
                {
                    try
                    {
                        avatarUrl = await _customerService.ProcessAvatarUploadAsync(model.AvatarFile);
                    }
                    catch (ArgumentException ex)
                    {
                        ModelState.AddModelError("AvatarFile", ex.Message);
                        return View(model);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("AvatarFile", "Error al guardar el archivo. Por favor intente nuevamente.");
                        return View(model);
                    }
                }

                var currentUserId = GetCurrentUserId();
                var (createdCustomer, errorMessage) = await _customerService.CreateCustomerAsync(model, avatarUrl, currentUserId);

                if (createdCustomer != null)
                {
                    TempData["SuccessMessage"] = "Cliente creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, errorMessage ?? "No se pudo crear el cliente. Por favor, revise los datos.");
                }
            }

            return View(model);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerEditViewModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CompanyName = customer.CompanyName,
                Country = customer.Country,
                IsActive = customer.IsActive,
                StatusDetail = customer.StatusDetail,
                CurrentAvatarUrl = customer.AvatarUrl,
                CustomerNumber = customer.CustomerNumber,
                CreatedAt = customer.CreatedAt,
                CreatedByUserName = customer.CreatedByUser?.FullName
            };

            return View(viewModel);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Obtener el cliente actual para mantener su avatar si no se sube uno nuevo
                var currentCustomer = await _customerService.GetCustomerByIdAsync(id);
                if (currentCustomer == null)
                {
                    return NotFound();
                }

                // Manejar la subida del archivo de avatar
                string? newAvatarUrl = null;

                if (model.AvatarFile != null && model.AvatarFile.Length > 0)
                {
                    try
                    {
                        newAvatarUrl = await _customerService.ProcessAvatarUploadAsync(model.AvatarFile);
                    }
                    catch (ArgumentException ex)
                    {
                        ModelState.AddModelError("AvatarFile", ex.Message);
                        return View(model);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("AvatarFile", "Error al guardar el archivo. Por favor intente nuevamente.");
                        return View(model);
                    }
                }

                var (success, errorMessage) = await _customerService.UpdateCustomerAsync(id, model, newAvatarUrl);

                if (success)
                {
                    TempData["SuccessMessage"] = "Cliente actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, errorMessage ?? "No se pudo actualizar el cliente.");
                }
            }

            // Recargar información del cliente en caso de error
            var customer = await _customerService.GetCustomerByIdAsync(id);
            model.CurrentAvatarUrl = customer?.AvatarUrl;
            return View(model);
        }

        // POST: Customers/ToggleStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var (success, newStatusMessage, newIsActiveState) = await _customerService.ToggleCustomerStatusAsync(id);

            if (success)
            {
                return Json(new { 
                    success = true, 
                    message = $"El estado del cliente ha sido cambiado a '{newStatusMessage}'.",
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

        private int GetCurrentUserId()
        {
            // TODO: Obtener el usuario actual desde la sesión o claims
            // Por ahora retornamos un ID hardcodeado para pruebas
            return 1;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}