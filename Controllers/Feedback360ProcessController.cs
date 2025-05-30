using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    [Authorize]
    public class Feedback360ProcessController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IFeedback360Service _feedback360Service;
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        private readonly IStringLocalizer<Feedback360ProcessController> _localizer;
        private readonly ILogger<Feedback360ProcessController> _logger;

        public Feedback360ProcessController(
            ApplicationDbContext context,
            IFeedback360Service feedback360Service,
            IUserService userService,
            IPermissionService permissionService,
            IStringLocalizer<Feedback360ProcessController> localizer,
            ILogger<Feedback360ProcessController> logger)
        {
            _context = context;
            _feedback360Service = feedback360Service;
            _userService = userService;
            _permissionService = permissionService;
            _localizer = localizer;
            _logger = logger;
        }

        // GET: Feedback360Process
        public async Task<IActionResult> Index()
        {
            var coachId = GetCurrentUserId();
            var instances = await _feedback360Service.GetInstancesByCoachAsync(coachId);
            
            var viewModels = new List<Feedback360InstanceSummaryViewModel>();
            
            foreach (var instance in instances)
            {
                var (total, completed) = await _feedback360Service.GetRaterStatsAsync(instance.Id);
                
                viewModels.Add(new Feedback360InstanceSummaryViewModel
                {
                    Id = instance.Id,
                    InstanceTitle = instance.InstanceTitle,
                    SubjectUserName = instance.SubjectUser?.FullName ?? "Unknown",
                    Status = instance.Status,
                    CreatedAt = instance.CreatedAt,
                    FeedbackDeadline = instance.FeedbackDeadline,
                    TotalRaters = total,
                    CompletedRaters = completed
                });
            }
            
            return View(viewModels);
        }

        // GET: Feedback360Process/Initiate
        public async Task<IActionResult> Initiate()
        {
            var viewModel = new InitiateFeedback360ViewModel();
            
            // Get all users except the current coach
            var currentUserId = GetCurrentUserId();
            var potentialSubjects = await _context.Users
                .Where(u => u.Id != currentUserId && u.IsActive)
                .OrderBy(u => u.FullName)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.FullName
                })
                .ToListAsync();
            
            viewModel.PotentialSubjects = potentialSubjects;
            
            return View(viewModel);
        }

        // POST: Feedback360Process/Initiate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Initiate(InitiateFeedback360ViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdown data
                var currentUserId = GetCurrentUserId();
                viewModel.PotentialSubjects = await _context.Users
                    .Where(u => u.Id != currentUserId && u.IsActive)
                    .OrderBy(u => u.FullName)
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.FullName
                    })
                    .ToListAsync();
                    
                return View(viewModel);
            }
            
            // Create the instance
            var coachId = GetCurrentUserId();
            var instance = await _feedback360Service.CreateInstanceAsync(
                viewModel.SelectedSubjectUserId,
                coachId,
                viewModel.InstanceTitle,
                viewModel.FeedbackDeadline
            );
            
            // Redirect to manage raters
            return RedirectToAction(nameof(ManageRaters), new { instanceId = instance.Id });
        }

        // GET: Feedback360Process/ManageRaters/5
        public async Task<IActionResult> ManageRaters(long instanceId)
        {
            var instance = await _feedback360Service.GetInstanceByIdAsync(instanceId, includeRaters: true);
            if (instance == null)
            {
                return NotFound();
            }
            
            // Verify the current user is the coach
            if (instance.InitiatedByCoachId != GetCurrentUserId())
            {
                return Forbid();
            }
            
            var raters = await _feedback360Service.GetRatersByInstanceAsync(instanceId);
            
            var viewModel = new ManageRatersViewModel
            {
                InstanceId = instanceId,
                InstanceTitle = instance.InstanceTitle,
                SubjectUserName = instance.SubjectUser?.FullName ?? "Unknown",
                FeedbackDeadline = instance.FeedbackDeadline,
                Status = instance.Status,
                NewRater = new AddRaterViewModel { InstanceId = instanceId }
            };
            
            // Map raters to view models
            foreach (var rater in raters)
            {
                viewModel.CurrentRaters.Add(new Feedback360RaterViewModel
                {
                    Id = rater.Id,
                    RaterNameOrEmail = rater.RaterUserId.HasValue && rater.RaterUser != null
                        ? rater.RaterUser.FullName
                        : rater.ExternalRaterName ?? rater.ExternalRaterEmail ?? "Unknown",
                    Email = rater.RaterUserId.HasValue && rater.RaterUser != null
                        ? rater.RaterUser.Email
                        : rater.ExternalRaterEmail ?? "",
                    Category = rater.Category,
                    Status = rater.Status,
                    InvitedAt = rater.InvitedAt,
                    CompletedAt = rater.CompletedAt,
                    CanRemove = await _feedback360Service.CanRemoveRaterAsync(rater.Id)
                });
            }
            
            // Populate dropdowns
            viewModel.RaterCategories = Enum.GetValues<RaterCategory>()
                .Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = GetCategoryDisplayName(c)
                })
                .ToList();
            
            // Optional: populate existing users dropdown
            var currentUserId = GetCurrentUserId();
            viewModel.ExistingUsers = await _context.Users
                .Where(u => u.IsActive && u.Id != instance.SubjectUserId)
                .OrderBy(u => u.FullName)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FullName} ({u.Email})"
                })
                .ToListAsync();
            
            return View(viewModel);
        }

        // POST: Feedback360Process/AddRater (AJAX)
        [HttpPost]
        public async Task<IActionResult> AddRater([FromBody] AddRaterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AddRaterResponseModel
                {
                    Success = false,
                    Message = string.Join(", ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage))
                });
            }
            
            // Verify permission
            var instance = await _context.Feedback360Instances.FindAsync(viewModel.InstanceId);
            if (instance == null || instance.InitiatedByCoachId != GetCurrentUserId())
            {
                return Json(new AddRaterResponseModel
                {
                    Success = false,
                    Message = "Access denied"
                });
            }
            
            // Add the rater
            var rater = await _feedback360Service.AddRaterAsync(
                viewModel.InstanceId,
                viewModel.RaterEmail,
                viewModel.RaterName,
                viewModel.Category,
                viewModel.ExistingUserId
            );
            
            if (rater == null)
            {
                return Json(new AddRaterResponseModel
                {
                    Success = false,
                    Message = "This email has already been added as a rater"
                });
            }
            
            // Map to view model
            var raterViewModel = new Feedback360RaterViewModel
            {
                Id = rater.Id,
                RaterNameOrEmail = rater.RaterUserId.HasValue && rater.RaterUser != null
                    ? rater.RaterUser.FullName
                    : rater.ExternalRaterName ?? rater.ExternalRaterEmail ?? "Unknown",
                Email = rater.RaterUserId.HasValue && rater.RaterUser != null
                    ? rater.RaterUser.Email
                    : rater.ExternalRaterEmail ?? "",
                Category = rater.Category,
                Status = rater.Status,
                CanRemove = true
            };
            
            return Json(new AddRaterResponseModel
            {
                Success = true,
                Message = "Rater added successfully",
                Rater = raterViewModel
            });
        }

        // POST: Feedback360Process/RemoveRater (AJAX)
        [HttpPost]
        public async Task<IActionResult> RemoveRater([FromBody] long raterId)
        {
            var success = await _feedback360Service.RemoveRaterAsync(raterId);
            
            return Json(new RemoveRaterResponseModel
            {
                Success = success,
                Message = success ? "Rater removed successfully" : "Cannot remove this rater"
            });
        }

        // POST: Feedback360Process/FinalizeSetupAndSendInvitations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizeSetupAndSendInvitations(long instanceId)
        {
            // Verify permission
            var instance = await _context.Feedback360Instances.FindAsync(instanceId);
            if (instance == null || instance.InitiatedByCoachId != GetCurrentUserId())
            {
                return Forbid();
            }
            
            var (success, message) = await _feedback360Service.FinalizeAndSendInvitationsAsync(instanceId);
            
            if (success)
            {
                TempData["SuccessMessage"] = "Invitations have been sent successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = message;
                return RedirectToAction(nameof(ManageRaters), new { instanceId });
            }
        }

        // GET: Feedback360Process/ViewReport/5
        public async Task<IActionResult> ViewReport(long instanceId)
        {
            ViewBag.HideTitleInLayout = true;
            
            try
            {
                var currentUserId = GetCurrentUserId();
                
                // Verify permissions
                if (!await _feedback360Service.CanViewReportAsync(instanceId, currentUserId, isCoach: true))
                {
                    return Forbid();
                }
                
                // Generate the report
                var report = await _feedback360Service.GenerateReportAsync(instanceId, currentUserId);
                
                return View(report);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating report for instance {InstanceId}", instanceId);
                TempData["ErrorMessage"] = "An error occurred while generating the report. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

        // Removed GetCurrentUserId() - now using the one from BaseController

        private string GetCategoryDisplayName(RaterCategory category)
        {
            return category switch
            {
                RaterCategory.Self => "Self-Evaluation",
                RaterCategory.Manager => "Manager",
                RaterCategory.Peer => "Peer/Colleague",
                RaterCategory.DirectReport => "Direct Report",
                RaterCategory.Client => "Client",
                RaterCategory.Other => "Other",
                _ => category.ToString()
            };
        }
    }
}