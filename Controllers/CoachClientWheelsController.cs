using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.Data;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    [Authorize]
    public class CoachClientWheelsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<CoachClientWheelsController> _localizer;

        public CoachClientWheelsController(
            ApplicationDbContext context,
            IStringLocalizer<CoachClientWheelsController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: CoachClientWheels
        public async Task<IActionResult> Index(long? templateId, int? clientId, string? status, int page = 1)
        {
            const int pageSize = 10;
            var coachId = GetCurrentUserId();

            // Base query
            var query = _context.ClientCommunicationWheelInstances
                .Include(cwi => cwi.CommunicationWheelTemplate)
                .Include(cwi => cwi.Client)
                .Where(cwi => cwi.AssignedByCoachId == coachId);

            // Apply filters
            if (templateId.HasValue)
            {
                query = query.Where(cwi => cwi.CommunicationWheelTemplateId == templateId.Value);
            }

            if (clientId.HasValue)
            {
                query = query.Where(cwi => cwi.ClientId == clientId.Value);
            }

            if (!string.IsNullOrEmpty(status) && status != "all")
            {
                if (Enum.TryParse<WheelCompletionStatus>(status, true, out var statusEnum))
                {
                    query = query.Where(cwi => cwi.Status == statusEnum);
                }
            }

            // Count for pagination
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Get paged results
            var wheels = await query
                .OrderByDescending(cwi => cwi.AssignedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(cwi => new AssignedWheelListItem
                {
                    InstanceId = cwi.Id,
                    WheelTemplateName = cwi.CommunicationWheelTemplate.Name,
                    ClientName = cwi.Client.FullName,
                    ClientEmail = cwi.Client.Email,
                    AssignedAt = cwi.AssignedAt,
                    Status = cwi.Status,
                    StartedAt = cwi.StartedAt,
                    CompletedAt = cwi.CompletedAt,
                    DimensionCount = cwi.CommunicationWheelTemplate.Dimensions.Count
                })
                .ToListAsync();

            // Get filter options
            var templateOptions = await _context.CommunicationWheelTemplates
                .Where(t => t.CoachId == coachId && t.IsActive)
                .OrderBy(t => t.Name)
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name,
                    Selected = t.Id == templateId
                })
                .ToListAsync();

            var clientOptions = await _context.ClientCommunicationWheelInstances
                .Where(cwi => cwi.AssignedByCoachId == coachId)
                .Select(cwi => cwi.Client)
                .Distinct()
                .OrderBy(c => c.FullName)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.FullName} ({c.Email})",
                    Selected = c.Id == clientId
                })
                .ToListAsync();

            var statusOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "all", Text = _localizer["All"].Value, Selected = status == "all" || string.IsNullOrEmpty(status) },
                new SelectListItem { Value = "Pending", Text = _localizer["Pending"].Value, Selected = status == "Pending" },
                new SelectListItem { Value = "InProgress", Text = _localizer["In Progress"].Value, Selected = status == "InProgress" },
                new SelectListItem { Value = "Completed", Text = _localizer["Completed"].Value, Selected = status == "Completed" }
            };

            var viewModel = new AssignedWheelsListViewModel
            {
                Wheels = wheels,
                SelectedTemplateId = templateId,
                SelectedClientId = clientId,
                SelectedStatus = status,
                TemplateOptions = templateOptions,
                ClientOptions = clientOptions,
                StatusOptions = statusOptions,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(viewModel);
        }

        // GET: CoachClientWheels/ViewClientWheel/5
        public async Task<IActionResult> ViewClientWheel(long id, List<long>? compareWithInstanceIds = null)
        {
            const int maxComparisons = 3; // Maximum number of additional wheels to compare
            var coachId = GetCurrentUserId();
            
            // Get the primary instance
            var instance = await _context.ClientCommunicationWheelInstances
                .Include(cwi => cwi.CommunicationWheelTemplate)
                    .ThenInclude(cwt => cwt.Dimensions.OrderBy(d => d.Order))
                .Include(cwi => cwi.Client)
                .Include(cwi => cwi.Scores)
                    .ThenInclude(s => s.CommunicationDimension)
                .FirstOrDefaultAsync(cwi => cwi.Id == id && cwi.AssignedByCoachId == coachId);

            if (instance == null)
            {
                return NotFound();
            }

            // Only allow viewing completed wheels
            if (instance.Status != WheelCompletionStatus.Completed)
            {
                TempData["ErrorMessage"] = _localizer["This wheel has not been completed yet"].Value;
                return RedirectToAction(nameof(Index));
            }

            // Prepare dimension labels (same for all instances)
            var dimensionLabels = instance.CommunicationWheelTemplate.Dimensions
                .OrderBy(d => d.Order)
                .Select(d => d.Name)
                .ToList();

            // Prepare series for chart
            var seriesForChart = new List<WheelChartSeries>();
            var colors = new[] { 
                new { Border = "rgb(54, 162, 235)", Background = "rgba(54, 162, 235, 0.2)" },
                new { Border = "rgb(255, 99, 132)", Background = "rgba(255, 99, 132, 0.2)" },
                new { Border = "rgb(75, 192, 192)", Background = "rgba(75, 192, 192, 0.2)" },
                new { Border = "rgb(153, 102, 255)", Background = "rgba(153, 102, 255, 0.2)" }
            };

            // Add primary instance
            var primaryScores = GetScoresForInstance(instance);
            seriesForChart.Add(new WheelChartSeries
            {
                SeriesLabel = $"Evaluación actual - {instance.CompletedAt:dd MMM yyyy}",
                Scores = primaryScores,
                BorderColor = colors[0].Border,
                BackgroundColor = colors[0].Background,
                CompletedAt = instance.CompletedAt ?? DateTime.UtcNow,
                IsPrimary = true
            });

            // Process comparison instances
            if (compareWithInstanceIds != null && compareWithInstanceIds.Any())
            {
                // Limit comparisons
                compareWithInstanceIds = compareWithInstanceIds.Take(maxComparisons).ToList();

                var comparisonInstances = await _context.ClientCommunicationWheelInstances
                    .Include(cwi => cwi.Scores)
                    .Where(cwi => compareWithInstanceIds.Contains(cwi.Id)
                        && cwi.ClientId == instance.ClientId
                        && cwi.CommunicationWheelTemplateId == instance.CommunicationWheelTemplateId
                        && cwi.Status == WheelCompletionStatus.Completed
                        && cwi.Id != id)
                    .ToListAsync();

                var colorIndex = 1;
                foreach (var compInstance in comparisonInstances.OrderBy(c => c.CompletedAt))
                {
                    var scores = GetScoresForInstance(compInstance, instance.CommunicationWheelTemplate.Dimensions.OrderBy(d => d.Order).ToList());
                    seriesForChart.Add(new WheelChartSeries
                    {
                        SeriesLabel = $"Evaluación - {compInstance.CompletedAt:dd MMM yyyy}",
                        Scores = scores,
                        BorderColor = colors[colorIndex % colors.Length].Border,
                        BackgroundColor = colors[colorIndex % colors.Length].Background,
                        CompletedAt = compInstance.CompletedAt ?? DateTime.UtcNow,
                        IsPrimary = false
                    });
                    colorIndex++;
                }
            }

            // Get available instances for comparison
            var availableInstances = await _context.ClientCommunicationWheelInstances
                .Where(cwi => cwi.ClientId == instance.ClientId
                    && cwi.CommunicationWheelTemplateId == instance.CommunicationWheelTemplateId
                    && cwi.Status == WheelCompletionStatus.Completed
                    && cwi.Id != id)
                .OrderByDescending(cwi => cwi.CompletedAt)
                .Select(cwi => new ClientWheelInstanceSelectionViewModel
                {
                    InstanceId = cwi.Id,
                    CompletedAt = cwi.CompletedAt ?? DateTime.UtcNow,
                    IsCurrentlyCompared = compareWithInstanceIds != null && compareWithInstanceIds.Contains(cwi.Id),
                    AverageScore = cwi.Scores.Average(s => s.Score)
                })
                .ToListAsync();

            // Prepare dimension details (from primary instance)
            var dimensionDetails = new List<DimensionScoreDetailViewModel>();
            foreach (var dimension in instance.CommunicationWheelTemplate.Dimensions.OrderBy(d => d.Order))
            {
                var score = instance.Scores.FirstOrDefault(s => s.CommunicationDimensionId == dimension.Id);
                dimensionDetails.Add(new DimensionScoreDetailViewModel
                {
                    DimensionName = dimension.Name,
                    GuidingQuestion = dimension.GuidingQuestion,
                    Score = score?.Score ?? 0,
                    Order = dimension.Order
                });
            }

            // Calculate statistics (for primary instance)
            var primaryNonZeroScores = primaryScores.Where(s => s > 0).ToList();
            var averageScore = primaryNonZeroScores.Any() ? primaryNonZeroScores.Average() : 0;
            var maxScore = primaryNonZeroScores.Any() ? primaryNonZeroScores.Max() : 0;
            var minScore = primaryNonZeroScores.Any() ? primaryNonZeroScores.Min() : 0;

            var maxIndex = primaryScores.IndexOf(maxScore);
            var minIndex = primaryScores.IndexOf(minScore);

            var viewModel = new ViewClientWheelViewModel
            {
                InstanceId = instance.Id,
                WheelTemplateName = instance.CommunicationWheelTemplate.Name,
                WheelTemplateDescription = instance.CommunicationWheelTemplate.Description,
                ClientName = instance.Client.FullName,
                ClientEmail = instance.Client.Email,
                AssignedAt = instance.AssignedAt,
                CompletedAt = instance.CompletedAt,
                ClientNotes = instance.ClientNotes,
                DimensionLabelsForChart = dimensionLabels,
                SeriesForChart = seriesForChart,
                AvailableInstancesForComparison = availableInstances,
                CurrentlyComparedInstanceIds = compareWithInstanceIds ?? new List<long>(),
                DimensionDetails = dimensionDetails,
                AverageScore = averageScore,
                MaxScore = maxScore,
                MinScore = minScore,
                HighestDimension = maxIndex >= 0 ? dimensionLabels[maxIndex] : null,
                LowestDimension = minIndex >= 0 ? dimensionLabels[minIndex] : null
            };

            return View(viewModel);
        }

        private List<int> GetScoresForInstance(ClientCommunicationWheelInstance instance, List<CommunicationDimension>? dimensions = null)
        {
            dimensions = dimensions ?? instance.CommunicationWheelTemplate.Dimensions.OrderBy(d => d.Order).ToList();
            var scores = new List<int>();
            
            foreach (var dimension in dimensions)
            {
                var score = instance.Scores.FirstOrDefault(s => s.CommunicationDimensionId == dimension.Id);
                scores.Add(score?.Score ?? 0);
            }
            
            return scores;
        }

    }
}