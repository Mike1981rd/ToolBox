using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ToolBox.Data;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    // Note: No [Authorize] attribute - this is accessible via token
    public class Feedback360ResponseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<Feedback360ResponseController> _localizer;
        private readonly ILogger<Feedback360ResponseController> _logger;

        public Feedback360ResponseController(
            ApplicationDbContext context,
            IStringLocalizer<Feedback360ResponseController> localizer,
            ILogger<Feedback360ResponseController> logger)
        {
            _context = context;
            _localizer = localizer;
            _logger = logger;
        }

        // GET: Feedback360Response/Respond/{token}
        [HttpGet]
        [Route("Feedback360Response/Respond/{token}")]
        public async Task<IActionResult> Respond(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return View("Error", new Feedback360ErrorViewModel
                {
                    ErrorType = "InvalidToken",
                    Message = _localizer["Invalid or missing token."]
                });
            }

            // Find the rater by token
            var rater = await _context.Feedback360Raters
                .Include(r => r.Feedback360Instance)
                    .ThenInclude(i => i.SubjectUser)
                .FirstOrDefaultAsync(r => r.UniqueResponseToken == token);

            if (rater == null)
            {
                return View("Error", new Feedback360ErrorViewModel
                {
                    ErrorType = "InvalidToken",
                    Message = _localizer["Invalid feedback link. Please check your email for the correct link."]
                });
            }

            // Check if already completed
            if (rater.Status == RaterStatus.Completed)
            {
                return View("Error", new Feedback360ErrorViewModel
                {
                    ErrorType = "AlreadyCompleted",
                    Message = _localizer["You have already submitted your feedback. Thank you!"]
                });
            }

            // Check deadline
            if (rater.Feedback360Instance.FeedbackDeadline.HasValue && 
                rater.Feedback360Instance.FeedbackDeadline.Value < DateTime.UtcNow)
            {
                return View("Error", new Feedback360ErrorViewModel
                {
                    ErrorType = "DeadlinePassed",
                    Message = _localizer["The deadline for submitting feedback has passed."],
                    Deadline = rater.Feedback360Instance.FeedbackDeadline
                });
            }

            // Update status to InProgress if it was Invited
            if (rater.Status == RaterStatus.Invited)
            {
                rater.Status = RaterStatus.InProgress;
                await _context.SaveChangesAsync();
            }

            // Build the view model
            var viewModel = await BuildResponseViewModel(rater);

            return View(viewModel);
        }

        // POST: Feedback360Response/SubmitResponse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitResponse(Feedback360ResponseViewModel viewModel)
        {
            // Validate token
            var rater = await ValidateAndGetRater(viewModel.Token);
            if (rater == null)
            {
                return View("Error", new Feedback360ErrorViewModel
                {
                    ErrorType = "InvalidToken",
                    Message = _localizer["Invalid feedback link."]
                });
            }

            // Custom validation - all scale questions must be answered
            bool hasValidationErrors = false;
            foreach (var competency in viewModel.Competencies)
            {
                foreach (var question in competency.ScaleQuestions)
                {
                    if (!question.Score.HasValue && question.IsRequired)
                    {
                        ModelState.AddModelError($"Competencies[{viewModel.Competencies.IndexOf(competency)}].ScaleQuestions[{competency.ScaleQuestions.IndexOf(question)}].Score", 
                            _localizer["This question is required."]);
                        hasValidationErrors = true;
                    }
                }
            }

            if (hasValidationErrors || !ModelState.IsValid)
            {
                // Rebuild the view model with scale options
                await RebuildViewModelOptions(viewModel, rater);
                return View("Respond", viewModel);
            }

            // Save responses
            await SaveResponses(rater, viewModel);

            // Mark as completed
            rater.Status = RaterStatus.Completed;
            rater.CompletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Show thank you page
            var thankYouModel = new Feedback360ThankYouViewModel
            {
                SubjectUserName = rater.Feedback360Instance.SubjectUser?.FullName ?? "the subject",
                InstanceTitle = rater.Feedback360Instance.InstanceTitle,
                WasSelfEvaluation = rater.Category == RaterCategory.Self
            };

            return View("ThankYou", thankYouModel);
        }

        // POST: Feedback360Response/SaveDraft (AJAX)
        [HttpPost]
        public async Task<IActionResult> SaveDraft([FromBody] Feedback360ResponseViewModel viewModel)
        {
            try
            {
                var rater = await ValidateAndGetRater(viewModel.Token);
                if (rater == null)
                {
                    return Json(new SaveDraftResponseModel
                    {
                        Success = false,
                        Message = _localizer["Invalid token."]
                    });
                }

                // Save responses without validation
                await SaveResponses(rater, viewModel);

                return Json(new SaveDraftResponseModel
                {
                    Success = true,
                    Message = _localizer["Draft saved successfully."],
                    LastSaved = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving draft for token {Token}", viewModel.Token);
                return Json(new SaveDraftResponseModel
                {
                    Success = false,
                    Message = _localizer["An error occurred while saving."]
                });
            }
        }

        private async Task<Feedback360ResponseViewModel> BuildResponseViewModel(Feedback360Rater rater)
        {
            var viewModel = new Feedback360ResponseViewModel
            {
                Token = rater.UniqueResponseToken,
                SubjectUserName = rater.Feedback360Instance.SubjectUser?.FullName ?? "Unknown",
                InstanceTitle = rater.Feedback360Instance.InstanceTitle,
                CurrentRaterCategory = rater.Category,
                FeedbackDeadline = rater.Feedback360Instance.FeedbackDeadline
            };

            // Get the current language
            var currentLanguage = HttpContext.Request.Cookies["SelectedLanguage"] ?? "en";
            var isSpanish = currentLanguage == "es";

            // Load competencies from definitions
            var competencyDefinitions = Feedback360Definitions.GetCompetencies();
            var scaleLabels = Feedback360Definitions.GetLikertScaleLabels();

            // Get existing responses
            var existingScaleResponses = await _context.Feedback360ResponseScales
                .Where(r => r.Feedback360RaterId == rater.Id)
                .ToDictionaryAsync(r => r.QuestionCode, r => r.Score);

            var existingOpenResponses = await _context.Feedback360ResponseOpens
                .Where(r => r.Feedback360RaterId == rater.Id)
                .ToDictionaryAsync(r => r.QuestionCode, r => r.ResponseText);

            // Build competency view models
            foreach (var competency in competencyDefinitions)
            {
                var competencyVm = new CompetencyResponseViewModel
                {
                    CompetencyCode = competency.Code,
                    CompetencyName = isSpanish ? competency.NameSpanish : competency.NameEnglish
                };

                foreach (var question in competency.Questions)
                {
                    var questionText = isSpanish ? question.TextSpanish : question.TextEnglish;
                    questionText = questionText.Replace("{SubjectName}", viewModel.SubjectUserName);

                    var scaleOptions = new List<SelectListItem>();
                    foreach (var scale in scaleLabels)
                    {
                        scaleOptions.Add(new SelectListItem
                        {
                            Value = scale.Key.ToString(),
                            Text = isSpanish ? scale.Value.Spanish : scale.Value.English
                        });
                    }

                    competencyVm.ScaleQuestions.Add(new ScaleQuestionResponseViewModel
                    {
                        QuestionCode = question.Code,
                        QuestionText = questionText,
                        Score = existingScaleResponses.TryGetValue(question.Code, out var score) ? score : null,
                        LikertScaleOptions = scaleOptions,
                        IsRequired = true
                    });
                }

                viewModel.Competencies.Add(competencyVm);
            }

            // Build open-ended questions
            var openQuestions = Feedback360Definitions.GetOpenQuestions();
            foreach (var openQuestion in openQuestions)
            {
                var questionText = isSpanish ? openQuestion.TextSpanish : openQuestion.TextEnglish;
                questionText = questionText.Replace("{SubjectName}", viewModel.SubjectUserName);

                viewModel.OpenEndedQuestions.Add(new OpenEndedQuestionResponseViewModel
                {
                    QuestionCode = openQuestion.Code,
                    QuestionText = questionText,
                    ResponseText = existingOpenResponses.TryGetValue(openQuestion.Code, out var response) ? response : ""
                });
            }

            return viewModel;
        }

        private async Task<Feedback360Rater?> ValidateAndGetRater(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;

            var rater = await _context.Feedback360Raters
                .Include(r => r.Feedback360Instance)
                    .ThenInclude(i => i.SubjectUser)
                .FirstOrDefaultAsync(r => r.UniqueResponseToken == token);

            if (rater == null) return null;
            if (rater.Status == RaterStatus.Completed) return null;
            
            if (rater.Feedback360Instance.FeedbackDeadline.HasValue && 
                rater.Feedback360Instance.FeedbackDeadline.Value < DateTime.UtcNow)
            {
                return null;
            }

            return rater;
        }

        private async Task SaveResponses(Feedback360Rater rater, Feedback360ResponseViewModel viewModel)
        {
            // Save scale responses
            foreach (var competency in viewModel.Competencies)
            {
                foreach (var question in competency.ScaleQuestions)
                {
                    if (question.Score.HasValue)
                    {
                        var existingResponse = await _context.Feedback360ResponseScales
                            .FirstOrDefaultAsync(r => r.Feedback360RaterId == rater.Id && 
                                                    r.QuestionCode == question.QuestionCode);

                        if (existingResponse != null)
                        {
                            existingResponse.Score = question.Score.Value;
                        }
                        else
                        {
                            _context.Feedback360ResponseScales.Add(new Feedback360ResponseScale
                            {
                                Feedback360RaterId = rater.Id,
                                QuestionCode = question.QuestionCode,
                                Score = question.Score.Value
                            });
                        }
                    }
                }
            }

            // Save open-ended responses
            foreach (var openQuestion in viewModel.OpenEndedQuestions)
            {
                if (!string.IsNullOrWhiteSpace(openQuestion.ResponseText))
                {
                    var existingResponse = await _context.Feedback360ResponseOpens
                        .FirstOrDefaultAsync(r => r.Feedback360RaterId == rater.Id && 
                                                r.QuestionCode == openQuestion.QuestionCode);

                    if (existingResponse != null)
                    {
                        existingResponse.ResponseText = openQuestion.ResponseText.Trim();
                    }
                    else
                    {
                        _context.Feedback360ResponseOpens.Add(new Feedback360ResponseOpen
                        {
                            Feedback360RaterId = rater.Id,
                            QuestionCode = openQuestion.QuestionCode,
                            ResponseText = openQuestion.ResponseText.Trim()
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task RebuildViewModelOptions(Feedback360ResponseViewModel viewModel, Feedback360Rater rater)
        {
            var currentLanguage = HttpContext.Request.Cookies["SelectedLanguage"] ?? "en";
            var isSpanish = currentLanguage == "es";
            var scaleLabels = Feedback360Definitions.GetLikertScaleLabels();

            foreach (var competency in viewModel.Competencies)
            {
                foreach (var question in competency.ScaleQuestions)
                {
                    question.LikertScaleOptions = new List<SelectListItem>();
                    foreach (var scale in scaleLabels)
                    {
                        question.LikertScaleOptions.Add(new SelectListItem
                        {
                            Value = scale.Key.ToString(),
                            Text = isSpanish ? scale.Value.Spanish : scale.Value.English
                        });
                    }
                }
            }

            // Update other properties
            viewModel.SubjectUserName = rater.Feedback360Instance.SubjectUser?.FullName ?? "Unknown";
            viewModel.InstanceTitle = rater.Feedback360Instance.InstanceTitle;
            viewModel.CurrentRaterCategory = rater.Category;
            viewModel.FeedbackDeadline = rater.Feedback360Instance.FeedbackDeadline;
        }
    }
}