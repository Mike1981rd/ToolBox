using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
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
    [Authorize]
    public class CommunicationWheelTemplatesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<CommunicationWheelTemplatesController> _localizer;
        private readonly INotificationService _notificationService;

        public CommunicationWheelTemplatesController(
            ApplicationDbContext context,
            IStringLocalizer<CommunicationWheelTemplatesController> localizer,
            INotificationService notificationService)
        {
            _context = context;
            _localizer = localizer;
            _notificationService = notificationService;
        }

        // GET: CommunicationWheelTemplates
        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 10;
            var coachId = GetCurrentUserId();

            var query = _context.CommunicationWheelTemplates
                .Where(t => t.CoachId == coachId && t.IsActive)
                .OrderByDescending(t => t.CreatedAt);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var templates = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new CommunicationWheelTemplateListItem
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    DimensionCount = t.Dimensions.Count,
                    CreatedAt = t.CreatedAt,
                    IsActive = t.IsActive
                })
                .ToListAsync();

            var viewModel = new CommunicationWheelTemplateListViewModel
            {
                Templates = templates,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(viewModel);
        }

        // GET: CommunicationWheelTemplates/Create
        public IActionResult Create()
        {
            return View(new CommunicationWheelTemplateViewModel());
        }

        // POST: CommunicationWheelTemplates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommunicationWheelTemplateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var template = new CommunicationWheelTemplate
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    CoachId = GetCurrentUserId(),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.CommunicationWheelTemplates.Add(template);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = _localizer["Template created successfully"].Value;
                return RedirectToAction(nameof(Edit), new { id = template.Id });
            }

            return View(viewModel);
        }

        // GET: CommunicationWheelTemplates/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var coachId = GetCurrentUserId();
            var template = await _context.CommunicationWheelTemplates
                .Include(t => t.Dimensions.OrderBy(d => d.Order))
                .FirstOrDefaultAsync(t => t.Id == id && t.CoachId == coachId);

            if (template == null)
            {
                return NotFound();
            }

            var viewModel = new CommunicationWheelTemplateViewModel
            {
                Id = template.Id,
                Name = template.Name,
                Description = template.Description,
                IsActive = template.IsActive,
                CreatedAt = template.CreatedAt,
                Dimensions = template.Dimensions.Select(d => new CommunicationDimensionViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    GuidingQuestion = d.GuidingQuestion,
                    Order = d.Order,
                    CommunicationWheelTemplateId = d.CommunicationWheelTemplateId
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: CommunicationWheelTemplates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, CommunicationWheelTemplateViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            var coachId = GetCurrentUserId();
            var template = await _context.CommunicationWheelTemplates
                .FirstOrDefaultAsync(t => t.Id == id && t.CoachId == coachId);

            if (template == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                template.Name = viewModel.Name;
                template.Description = viewModel.Description;
                template.UpdatedAt = DateTime.UtcNow;

                _context.Update(template);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = _localizer["Template updated successfully"].Value;
                return RedirectToAction(nameof(Index));
            }

            // Reload dimensions if model state is invalid
            viewModel.Dimensions = await _context.CommunicationDimensions
                .Where(d => d.CommunicationWheelTemplateId == id)
                .OrderBy(d => d.Order)
                .Select(d => new CommunicationDimensionViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    GuidingQuestion = d.GuidingQuestion,
                    Order = d.Order,
                    CommunicationWheelTemplateId = d.CommunicationWheelTemplateId
                })
                .ToListAsync();

            return View(viewModel);
        }

        // POST: CommunicationWheelTemplates/ToggleActiveStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActiveStatus(long id)
        {
            var coachId = GetCurrentUserId();
            var template = await _context.CommunicationWheelTemplates
                .FirstOrDefaultAsync(t => t.Id == id && t.CoachId == coachId);

            if (template == null)
            {
                return NotFound();
            }

            template.IsActive = !template.IsActive;
            template.UpdatedAt = DateTime.UtcNow;

            _context.Update(template);
            await _context.SaveChangesAsync();

            var message = template.IsActive 
                ? _localizer["Template activated successfully"].Value 
                : _localizer["Template deactivated successfully"].Value;
            
            TempData["SuccessMessage"] = message;
            return RedirectToAction(nameof(Index));
        }

        #region API Endpoints for Dimensions

        // POST: CommunicationWheelTemplates/CreateDimension
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDimension([FromBody] CreateDimensionRequest request)
        {
            var coachId = GetCurrentUserId();
            var template = await _context.CommunicationWheelTemplates
                .Include(t => t.Dimensions)
                .FirstOrDefaultAsync(t => t.Id == request.TemplateId && t.CoachId == coachId);

            if (template == null)
            {
                return NotFound();
            }

            // Validate dimension limit (max 12 for readability)
            if (template.Dimensions.Count >= 12)
            {
                return BadRequest(new { error = _localizer["Maximum number of dimensions (12) reached"].Value });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            var dimension = new CommunicationDimension
            {
                CommunicationWheelTemplateId = request.TemplateId,
                Name = request.Name,
                GuidingQuestion = request.GuidingQuestion,
                Order = template.Dimensions.Count > 0 ? template.Dimensions.Max(d => d.Order) + 1 : 1,
                CreatedAt = currentTime,
                UpdatedAt = currentTime
            };

            _context.CommunicationDimensions.Add(dimension);
            await _context.SaveChangesAsync();

            var responseData = new CommunicationDimensionViewModel
            {
                Id = dimension.Id,
                CommunicationWheelTemplateId = dimension.CommunicationWheelTemplateId,
                Name = dimension.Name,
                GuidingQuestion = dimension.GuidingQuestion,
                Order = dimension.Order
            };

            return Json(new { success = true, dimension = responseData });
        }

        // POST: CommunicationWheelTemplates/UpdateDimension
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDimension([FromBody] UpdateDimensionRequest request)
        {
            var coachId = GetCurrentUserId();
            var dimension = await _context.CommunicationDimensions
                .Include(d => d.CommunicationWheelTemplate)
                .FirstOrDefaultAsync(d => d.Id == request.DimensionId && d.CommunicationWheelTemplate.CoachId == coachId);

            if (dimension == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dimension.Name = request.Name;
            dimension.GuidingQuestion = request.GuidingQuestion;
            dimension.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

            _context.Update(dimension);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // POST: CommunicationWheelTemplates/DeleteDimension
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDimension([FromBody] DeleteDimensionRequest request)
        {
            var coachId = GetCurrentUserId();
            var dimension = await _context.CommunicationDimensions
                .Include(d => d.CommunicationWheelTemplate)
                .FirstOrDefaultAsync(d => d.Id == request.DimensionId && d.CommunicationWheelTemplate.CoachId == coachId);

            if (dimension == null)
            {
                return NotFound();
            }

            var templateId = dimension.CommunicationWheelTemplateId;
            _context.CommunicationDimensions.Remove(dimension);
            await _context.SaveChangesAsync();

            // Reorder remaining dimensions
            var remainingDimensions = await _context.CommunicationDimensions
                .Where(d => d.CommunicationWheelTemplateId == templateId)
                .OrderBy(d => d.Order)
                .ToListAsync();

            for (int i = 0; i < remainingDimensions.Count; i++)
            {
                remainingDimensions[i].Order = i + 1;
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // POST: CommunicationWheelTemplates/ReorderDimensions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReorderDimensions([FromBody] ReorderDimensionsRequest request)
        {
            var coachId = GetCurrentUserId();
            var template = await _context.CommunicationWheelTemplates
                .FirstOrDefaultAsync(t => t.Id == request.TemplateId && t.CoachId == coachId);

            if (template == null)
            {
                return NotFound();
            }

            var dimensions = await _context.CommunicationDimensions
                .Where(d => d.CommunicationWheelTemplateId == request.TemplateId)
                .ToListAsync();

            for (int i = 0; i < request.DimensionIds.Count; i++)
            {
                var dimension = dimensions.FirstOrDefault(d => d.Id == request.DimensionIds[i]);
                if (dimension != null)
                {
                    dimension.Order = i + 1;
                    dimension.UpdatedAt = DateTime.UtcNow;
                }
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        #endregion

        #region Assignment Methods

        // GET: CommunicationWheelTemplates/Assign/5
        public async Task<IActionResult> Assign(long id)
        {
            var coachId = GetCurrentUserId();
            var template = await _context.CommunicationWheelTemplates
                .FirstOrDefaultAsync(t => t.Id == id && t.CoachId == coachId && t.IsActive);

            if (template == null)
            {
                return NotFound();
            }

            // Get all users except the coach
            var allUsers = await _context.Users
                .Where(u => u.Id != coachId && u.IsActive)
                .OrderBy(u => u.FullName)
                .ToListAsync();

            var potentialClients = allUsers.Select(u => new SelectListItem 
            { 
                Value = u.Id.ToString(), 
                Text = $"{u.FullName} ({u.Email})"
            }).ToList();

            // Get already assigned clients for this template
            var alreadyAssigned = await _context.ClientCommunicationWheelInstances
                .Where(cwi => cwi.CommunicationWheelTemplateId == id)
                .Include(cwi => cwi.Client)
                .Select(cwi => new AssignedClientInfo
                {
                    ClientId = cwi.ClientId,
                    ClientName = cwi.Client.FullName,
                    Status = cwi.Status,
                    AssignedAt = cwi.AssignedAt
                })
                .ToListAsync();

            var viewModel = new AssignCommunicationWheelViewModel
            {
                TemplateId = template.Id,
                TemplateName = template.Name,
                TemplateDescription = template.Description,
                PotentialClients = potentialClients,
                AlreadyAssignedClients = alreadyAssigned
            };

            return View(viewModel);
        }

        // POST: CommunicationWheelTemplates/Assign
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(AssignCommunicationWheelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Reload data if validation fails
                var allUsers = await _context.Users
                    .Where(u => u.Id != GetCurrentUserId() && u.IsActive)
                    .OrderBy(u => u.FullName)
                    .ToListAsync();

                viewModel.PotentialClients = allUsers.Select(u => new SelectListItem 
                { 
                    Value = u.Id.ToString(), 
                    Text = $"{u.FullName} ({u.Email})"
                }).ToList();

                return View(viewModel);
            }

            var coachId = GetCurrentUserId();
            var template = await _context.CommunicationWheelTemplates
                .FirstOrDefaultAsync(t => t.Id == viewModel.TemplateId && t.CoachId == coachId);

            if (template == null)
            {
                return NotFound();
            }

            // Check for existing pending/in-progress assignments
            var existingAssignments = await _context.ClientCommunicationWheelInstances
                .Where(cwi => cwi.CommunicationWheelTemplateId == viewModel.TemplateId 
                    && viewModel.SelectedClientIds.Contains(cwi.ClientId)
                    && (cwi.Status == WheelCompletionStatus.Pending || cwi.Status == WheelCompletionStatus.InProgress))
                .Select(cwi => cwi.ClientId)
                .ToListAsync();

            var successCount = 0;
            var skippedCount = 0;

            foreach (var clientId in viewModel.SelectedClientIds)
            {
                // Skip if already has pending/in-progress assignment
                if (existingAssignments.Contains(clientId))
                {
                    skippedCount++;
                    continue;
                }

                var instance = new ClientCommunicationWheelInstance
                {
                    CommunicationWheelTemplateId = viewModel.TemplateId,
                    ClientId = clientId,
                    AssignedByCoachId = coachId,
                    Status = WheelCompletionStatus.Pending,
                    AssignedAt = DateTime.UtcNow
                };

                _context.ClientCommunicationWheelInstances.Add(instance);
                successCount++;

                // Create notification for the client
                // Ensure template name is not null or empty
                var wheelName = !string.IsNullOrEmpty(template.Name) ? template.Name : "Rueda de Comunicación";
                
                await _notificationService.CreateNotificationAsync(
                    clientId,
                    "communication_wheel_assigned",
                    new 
                    {
                        WheelName = wheelName,
                        TemplateId = viewModel.TemplateId,
                        InstanceId = instance.Id,
                        CoachName = User.Identity?.Name ?? "Tu coach"
                    }
                );
            }

            await _context.SaveChangesAsync();

            // Create appropriate success message
            var message = successCount > 0 
                ? _localizer["Wheel assigned successfully to {0} clients", successCount].Value 
                : "";
            
            if (skippedCount > 0)
            {
                message += " " + _localizer["{0} clients already have pending assignments", skippedCount].Value;
            }

            TempData["SuccessMessage"] = message;
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}