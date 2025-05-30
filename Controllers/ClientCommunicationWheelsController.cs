using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ClientCommunicationWheelsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<ClientCommunicationWheelsController> _localizer;
        private readonly INotificationService _notificationService;

        public ClientCommunicationWheelsController(
            ApplicationDbContext context,
            IStringLocalizer<ClientCommunicationWheelsController> localizer,
            INotificationService notificationService)
        {
            _context = context;
            _localizer = localizer;
            _notificationService = notificationService;
        }

        // GET: ClientCommunicationWheels
        public async Task<IActionResult> Index()
        {
            var clientId = GetCurrentUserId();
            
            var allInstances = await _context.ClientCommunicationWheelInstances
                .Where(cwi => cwi.ClientId == clientId)
                .Include(cwi => cwi.CommunicationWheelTemplate)
                    .ThenInclude(cwt => cwt.Dimensions)
                .OrderByDescending(cwi => cwi.AssignedAt)
                .Select(cwi => new ClientWheelInstanceViewModel
                {
                    InstanceId = cwi.Id,
                    WheelTemplateName = cwi.CommunicationWheelTemplate.Name,
                    WheelTemplateDescription = cwi.CommunicationWheelTemplate.Description,
                    AssignedAt = cwi.AssignedAt,
                    Status = cwi.Status,
                    StartedAt = cwi.StartedAt,
                    CompletedAt = cwi.CompletedAt,
                    DimensionCount = cwi.CommunicationWheelTemplate.Dimensions.Count
                })
                .ToListAsync();

            var viewModel = new ClientWheelsIndexViewModel
            {
                PendingWheels = allInstances.Where(i => i.Status == WheelCompletionStatus.Pending).ToList(),
                InProgressWheels = allInstances.Where(i => i.Status == WheelCompletionStatus.InProgress).ToList(),
                CompletedWheels = allInstances.Where(i => i.Status == WheelCompletionStatus.Completed).ToList(),
                TotalCount = allInstances.Count
            };

            return View(viewModel);
        }

        // GET: ClientCommunicationWheels/CompleteWheel/5
        public async Task<IActionResult> CompleteWheel(long id)
        {
            var clientId = GetCurrentUserId();
            var instance = await _context.ClientCommunicationWheelInstances
                .Include(cwi => cwi.CommunicationWheelTemplate)
                    .ThenInclude(cwt => cwt.Dimensions.OrderBy(d => d.Order))
                .Include(cwi => cwi.Scores)
                .FirstOrDefaultAsync(cwi => cwi.Id == id && cwi.ClientId == clientId);

            if (instance == null)
            {
                return NotFound();
            }

            // Don't allow completing an already completed wheel
            if (instance.Status == WheelCompletionStatus.Completed)
            {
                return RedirectToAction(nameof(ViewWheel), new { id = instance.Id });
            }

            // Update status if this is the first time
            if (instance.Status == WheelCompletionStatus.Pending)
            {
                instance.Status = WheelCompletionStatus.InProgress;
                instance.StartedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            // Prepare view model
            var viewModel = new CompleteWheelViewModel
            {
                InstanceId = instance.Id,
                WheelTemplateName = instance.CommunicationWheelTemplate.Name,
                WheelTemplateDescription = instance.CommunicationWheelTemplate.Description,
                ClientNotes = instance.ClientNotes,
                AssignedAt = instance.AssignedAt,
                IsCompleted = false,
                DimensionsToScore = instance.CommunicationWheelTemplate.Dimensions.Select(d => new DimensionToScoreViewModel
                {
                    DimensionId = d.Id,
                    DimensionName = d.Name,
                    GuidingQuestion = d.GuidingQuestion,
                    Order = d.Order,
                    Score = instance.Scores.FirstOrDefault(s => s.CommunicationDimensionId == d.Id)?.Score ?? 5
                }).OrderBy(d => d.Order).ToList()
            };

            return View(viewModel);
        }

        // POST: ClientCommunicationWheels/SubmitWheel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitWheel(CompleteWheelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CompleteWheel", viewModel);
            }

            var clientId = GetCurrentUserId();
            var instance = await _context.ClientCommunicationWheelInstances
                .Include(cwi => cwi.CommunicationWheelTemplate)
                .Include(cwi => cwi.Client)
                .Include(cwi => cwi.Scores)
                .FirstOrDefaultAsync(cwi => cwi.Id == viewModel.InstanceId && cwi.ClientId == clientId);

            if (instance == null)
            {
                return NotFound();
            }

            // Don't allow resubmitting a completed wheel
            if (instance.Status == WheelCompletionStatus.Completed)
            {
                return RedirectToAction(nameof(ViewWheel), new { id = instance.Id });
            }

            // Update or create dimension scores
            foreach (var dimensionScore in viewModel.DimensionsToScore)
            {
                var existingScore = instance.Scores
                    .FirstOrDefault(s => s.CommunicationDimensionId == dimensionScore.DimensionId);

                if (existingScore != null)
                {
                    existingScore.Score = dimensionScore.Score;
                    existingScore.ScoredAt = DateTime.UtcNow;
                }
                else
                {
                    var newScore = new DimensionScore
                    {
                        ClientCommunicationWheelInstanceId = instance.Id,
                        CommunicationDimensionId = dimensionScore.DimensionId,
                        Score = dimensionScore.Score,
                        ScoredAt = DateTime.UtcNow
                    };
                    _context.DimensionScores.Add(newScore);
                }
            }

            // Update instance
            instance.Status = WheelCompletionStatus.Completed;
            instance.CompletedAt = DateTime.UtcNow;
            instance.ClientNotes = viewModel.ClientNotes;

            await _context.SaveChangesAsync();

            // Notify the coach
            if (instance.AssignedByCoachId > 0)
            {
                await _notificationService.CreateNotificationAsync(
                    instance.AssignedByCoachId,
                    "communication_wheel_completed",
                    new Dictionary<string, object>
                    {
                        { "WheelName", instance.CommunicationWheelTemplate.Name },
                        { "ClientName", instance.Client.FullName },
                        { "InstanceId", instance.Id }
                    }
                );
            }

            TempData["SuccessMessage"] = _localizer["Wheel completed successfully"].Value;
            return RedirectToAction(nameof(ViewWheel), new { id = instance.Id });
        }

        // GET: ClientCommunicationWheels/ViewWheel/5
        public async Task<IActionResult> ViewWheel(long id)
        {
            var clientId = GetCurrentUserId();
            var instance = await _context.ClientCommunicationWheelInstances
                .Include(cwi => cwi.CommunicationWheelTemplate)
                    .ThenInclude(cwt => cwt.Dimensions.OrderBy(d => d.Order))
                .Include(cwi => cwi.Scores)
                    .ThenInclude(s => s.CommunicationDimension)
                .Include(cwi => cwi.AssignedByCoach)
                .FirstOrDefaultAsync(cwi => cwi.Id == id && cwi.ClientId == clientId);

            if (instance == null)
            {
                return NotFound();
            }

            // Only allow viewing completed wheels
            if (instance.Status != WheelCompletionStatus.Completed)
            {
                return RedirectToAction(nameof(CompleteWheel), new { id = instance.Id });
            }

            // Prepare data for radar chart
            var dimensionLabels = new List<string>();
            var dimensionScores = new List<int>();

            foreach (var dimension in instance.CommunicationWheelTemplate.Dimensions.OrderBy(d => d.Order))
            {
                dimensionLabels.Add(dimension.Name);
                var score = instance.Scores.FirstOrDefault(s => s.CommunicationDimensionId == dimension.Id);
                dimensionScores.Add(score?.Score ?? 0);
            }

            var viewModel = new ViewWheelViewModel
            {
                InstanceId = instance.Id,
                WheelTemplateName = instance.CommunicationWheelTemplate.Name,
                WheelTemplateDescription = instance.CommunicationWheelTemplate.Description,
                CompletedAt = instance.CompletedAt ?? DateTime.UtcNow,
                ClientNotes = instance.ClientNotes,
                DimensionLabels = dimensionLabels,
                DimensionScores = dimensionScores,
                AssignedByCoachName = instance.AssignedByCoach?.FullName ?? "Coach",
                AssignedAt = instance.AssignedAt
            };

            return View(viewModel);
        }

    }
}