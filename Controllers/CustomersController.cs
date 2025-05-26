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
            // Por defecto mostrar clientes activos si no se especifica filtro
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
            ViewBag.CurrentStatusFilter = statusFilter;
            ViewBag.CurrentSearchTerm = searchTerm;
            
            return View(customerViewModels);
        }

        /* CÓDIGO HUÉRFANO COMENTADO - ELIMINAR DESPUÉS
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
        FIN DE CÓDIGO HUÉRFANO */

        /* MÉTODO DUPLICADO - COMENTADO
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
        } */

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

        // GET: Customers/ExportToExcel
        [HttpGet]
        public async Task<IActionResult> ExportToExcel(string? statusFilter = null)
        {
            var customers = await _customerService.GetAllCustomersAsync(statusFilter);
            
            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Clientes");
            
            // Headers
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Número Cliente";
            worksheet.Cell(1, 3).Value = "Nombre Completo";
            worksheet.Cell(1, 4).Value = "Email";
            worksheet.Cell(1, 5).Value = "Teléfono";
            worksheet.Cell(1, 6).Value = "Empresa";
            worksheet.Cell(1, 7).Value = "País";
            worksheet.Cell(1, 8).Value = "Estado";
            worksheet.Cell(1, 9).Value = "Fecha Creación";
            worksheet.Cell(1, 10).Value = "Última Actualización";
            
            // Style headers
            var headerRange = worksheet.Range(1, 1, 1, 10);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            
            // Data
            int row = 2;
            foreach (var customer in customers)
            {
                worksheet.Cell(row, 1).Value = customer.Id;
                worksheet.Cell(row, 2).Value = customer.CustomerNumber;
                worksheet.Cell(row, 3).Value = customer.FullName;
                worksheet.Cell(row, 4).Value = customer.Email;
                worksheet.Cell(row, 5).Value = customer.PhoneNumber;
                worksheet.Cell(row, 6).Value = customer.CompanyName ?? "";
                worksheet.Cell(row, 7).Value = customer.Country ?? "";
                worksheet.Cell(row, 8).Value = customer.IsActive ? "Activo" : "Inactivo";
                worksheet.Cell(row, 9).Value = customer.CreatedAt.ToString("dd/MM/yyyy");
                worksheet.Cell(row, 10).Value = customer.UpdatedAt.ToString("dd/MM/yyyy");
                row++;
            }
            
            // Auto-fit columns
            worksheet.Columns().AdjustToContents();
            
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                       $"Clientes_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        // GET: Customers/ExportToCsv
        [HttpGet]
        public async Task<IActionResult> ExportToCsv(string? statusFilter = null)
        {
            var customers = await _customerService.GetAllCustomersAsync(statusFilter);
            
            var csv = new StringBuilder();
            csv.AppendLine("ID,Número Cliente,Nombre Completo,Email,Teléfono,Empresa,País,Estado,Fecha Creación,Última Actualización");
            
            foreach (var customer in customers)
            {
                csv.AppendLine($"{customer.Id}," +
                              $"\"{customer.CustomerNumber}\"," +
                              $"\"{customer.FullName}\"," +
                              $"\"{customer.Email}\"," +
                              $"\"{customer.PhoneNumber}\"," +
                              $"\"{customer.CompanyName ?? ""}\"," +
                              $"\"{customer.Country ?? ""}\"," +
                              $"\"{(customer.IsActive ? "Activo" : "Inactivo")}\"," +
                              $"\"{customer.CreatedAt:dd/MM/yyyy}\"," +
                              $"\"{customer.UpdatedAt:dd/MM/yyyy}\"");
            }
            
            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", $"Clientes_{DateTime.Now:yyyyMMdd}.csv");
        }

        // GET: Customers/ExportToPdf
        [HttpGet]
        public async Task<IActionResult> ExportToPdf(string? statusFilter = null)
        {
            var customers = await _customerService.GetAllCustomersAsync(statusFilter);
            
            var customerDtos = customers.Select(c => new Documents.CustomerPdfDto
            {
                CustomerNumber = c.CustomerNumber,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                CompanyName = c.CompanyName ?? "",
                Country = c.Country ?? "",
                Status = c.IsActive ? "Activo" : "Inactivo",
                CreatedAt = c.CreatedAt.ToString("dd/MM/yyyy")
            }).ToList();
            
            var document = new Documents.CustomerListPdfDocument(customerDtos);
            var pdfBytes = document.GeneratePdf();
            
            return File(pdfBytes, "application/pdf", $"Clientes_{DateTime.Now:yyyyMMdd}.pdf");
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