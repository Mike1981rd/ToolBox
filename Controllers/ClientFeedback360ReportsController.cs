using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ClientFeedback360ReportsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IFeedback360Service _feedback360Service;
        private readonly IPermissionService _permissionService;
        private readonly IStringLocalizer<ClientFeedback360ReportsController> _localizer;
        private readonly ILogger<ClientFeedback360ReportsController> _logger;

        public ClientFeedback360ReportsController(
            ApplicationDbContext context,
            IFeedback360Service feedback360Service,
            IPermissionService permissionService,
            IStringLocalizer<ClientFeedback360ReportsController> localizer,
            ILogger<ClientFeedback360ReportsController> logger)
        {
            _context = context;
            _feedback360Service = feedback360Service;
            _permissionService = permissionService;
            _localizer = localizer;
            _logger = logger;
        }

        // GET: ClientFeedback360Reports
        public async Task<IActionResult> Index()
        {
            var currentUserId = GetCurrentUserId();
            
            // Get all feedback instances where the current user is the subject
            var instances = await _context.Feedback360Instances
                .Include(i => i.InitiatedByCoach)
                .Where(i => i.SubjectUserId == currentUserId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();

            var viewModels = new List<ClientFeedback360ReportSummaryViewModel>();
            
            foreach (var instance in instances)
            {
                var (total, completed) = await _feedback360Service.GetRaterStatsAsync(instance.Id);
                
                // Determine if report is available
                bool isReportAvailable = instance.Status == Feedback360Status.Completed || 
                                        instance.ReportGeneratedAt != null;
                
                viewModels.Add(new ClientFeedback360ReportSummaryViewModel
                {
                    Id = instance.Id,
                    InstanceTitle = instance.InstanceTitle,
                    CoachName = instance.InitiatedByCoach?.FullName ?? "Unknown",
                    Status = instance.Status,
                    CreatedAt = instance.CreatedAt,
                    FeedbackDeadline = instance.FeedbackDeadline,
                    ReportGeneratedAt = instance.ReportGeneratedAt,
                    TotalRaters = total,
                    CompletedRaters = completed,
                    IsReportAvailable = isReportAvailable,
                    CanViewReport = isReportAvailable // Could add additional logic here for coach approval
                });
            }
            
            return View(viewModels);
        }

        // GET: ClientFeedback360Reports/ViewReport/5
        public async Task<IActionResult> ViewReport(long instanceId)
        {
            ViewBag.HideTitleInLayout = true;
            
            try
            {
                var currentUserId = GetCurrentUserId();
                
                // Verify permissions
                if (!await _feedback360Service.CanViewReportAsync(instanceId, currentUserId, isCoach: false))
                {
                    TempData["ErrorMessage"] = "This report is not yet available or you don't have permission to view it.";
                    return RedirectToAction(nameof(Index));
                }
                
                // Generate the report
                var report = await _feedback360Service.GenerateReportAsync(instanceId, currentUserId);
                
                // Client view has some restrictions compared to coach view
                report.IsCoachView = false;
                report.CanViewDetailedBreakdown = false; // Clients see less detailed breakdowns
                
                return View(report);
            }
            catch (ArgumentException)
            {
                TempData["ErrorMessage"] = "The requested report was not found.";
                return RedirectToAction(nameof(Index));
            }
            catch (UnauthorizedAccessException)
            {
                TempData["ErrorMessage"] = "You don't have permission to view this report.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating client report for instance {InstanceId}", instanceId);
                TempData["ErrorMessage"] = "An error occurred while loading your report. Please try again later.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}