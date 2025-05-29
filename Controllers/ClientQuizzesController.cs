using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ToolBox.Data;
using ToolBox.Models;
using ToolBox.Models.ViewModels;
using ToolBox.Interfaces;

namespace ToolBox.Controllers
{
    public class ClientQuizzesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly ILogger<ClientQuizzesController> _logger;

        public ClientQuizzesController(
            ApplicationDbContext context,
            INotificationService notificationService,
            ILogger<ClientQuizzesController> logger)
        {
            _context = context;
            _notificationService = notificationService;
            _logger = logger;
        }

        // GET: ClientQuizzes
        public async Task<IActionResult> Index(string? statusFilter = null)
        {
            // Default to pending status
            statusFilter = statusFilter ?? "pending";
            ViewBag.CurrentStatusFilter = statusFilter;
            
            var currentUserId = GetCurrentUserId();
            
            var query = _context.QuestionnaireInstances
                .Include(qi => qi.QuestionnaireTemplate)
                .ThenInclude(qt => qt.Questions)
                .Where(qi => qi.ClientId == currentUserId);
            
            // Apply status filter
            if (statusFilter == "pending")
            {
                query = query.Where(qi => qi.Status == QuestionnaireStatus.Pending);
            }
            else if (statusFilter == "in_progress")
            {
                query = query.Where(qi => qi.Status == QuestionnaireStatus.InProgress);
            }
            else if (statusFilter == "completed")
            {
                query = query.Where(qi => qi.Status == QuestionnaireStatus.Completed);
            }
            // "all" shows everything, no additional filter needed
            
            var instances = await query
                .OrderByDescending(qi => qi.AssignedAt)
                .ToListAsync();

            var viewModels = new List<ClientQuizInstanceViewModel>();

            foreach (var instance in instances)
            {
                var answeredCount = await _context.Answers
                    .Where(a => a.QuestionnaireInstanceId == instance.Id && !string.IsNullOrEmpty(a.ResponseText))
                    .CountAsync();

                viewModels.Add(new ClientQuizInstanceViewModel
                {
                    InstanceId = instance.Id,
                    QuestionnaireTitle = instance.QuestionnaireTemplate.Title,
                    QuestionnaireDescription = instance.QuestionnaireTemplate.Description,
                    AssignedAt = instance.AssignedAt,
                    Status = instance.Status,
                    StartedAt = instance.StartedAt,
                    CompletedAt = instance.CompletedAt,
                    TotalQuestions = instance.QuestionnaireTemplate.Questions.Count,
                    AnsweredQuestions = answeredCount
                });
            }

            ViewBag.BreadcrumbActiveKey = "breadcrumb_client_quizzes";
            return View(viewModels);
        }

        // GET: ClientQuizzes/Respond/5
        public async Task<IActionResult> Respond(long id)
        {
            var currentUserId = GetCurrentUserId();
            
            var instance = await _context.QuestionnaireInstances
                .Include(qi => qi.QuestionnaireTemplate)
                .ThenInclude(qt => qt.Questions.OrderBy(q => q.Order))
                .FirstOrDefaultAsync(qi => qi.Id == id && qi.ClientId == currentUserId);

            if (instance == null)
            {
                return NotFound();
            }

            if (instance.Status == QuestionnaireStatus.Completed)
            {
                TempData["ErrorMessage"] = "Este cuestionario ya ha sido completado.";
                return RedirectToAction("Index");
            }

            // Actualizar estado a InProgress si es la primera vez
            if (instance.Status == QuestionnaireStatus.Pending)
            {
                instance.Status = QuestionnaireStatus.InProgress;
                instance.StartedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            // Obtener respuestas existentes
            var existingAnswers = await _context.Answers
                .Where(a => a.QuestionnaireInstanceId == id)
                .ToListAsync();

            var viewModel = new RespondQuizViewModel
            {
                InstanceId = instance.Id,
                QuestionnaireTitle = instance.QuestionnaireTemplate.Title,
                QuestionnaireDescription = instance.QuestionnaireTemplate.Description,
                Status = instance.Status,
                Questions = instance.QuestionnaireTemplate.Questions.Select(q => 
                {
                    var existingAnswer = existingAnswers.FirstOrDefault(a => a.QuestionTemplateId == q.Id);
                    var questionViewModel = new QuestionToAnswerViewModel
                    {
                        QuestionTemplateId = q.Id,
                        QuestionText = q.QuestionText,
                        Type = q.Type,
                        IsRequired = q.IsRequired,
                        Order = q.Order,
                        ClientResponseText = existingAnswer?.ResponseText
                    };

                    // Procesar opciones según tipo de pregunta
                    if (q.Type == QuestionType.SingleChoice || q.Type == QuestionType.MultipleChoice)
                    {
                        questionViewModel.Options = ParseOptionsFromJson(q.OptionsJson);
                        
                        if (q.Type == QuestionType.MultipleChoice && !string.IsNullOrEmpty(existingAnswer?.ResponseText))
                        {
                            try
                            {
                                questionViewModel.ClientMultipleChoiceResponse = 
                                    JsonSerializer.Deserialize<List<string>>(existingAnswer.ResponseText) ?? new List<string>();
                            }
                            catch
                            {
                                questionViewModel.ClientMultipleChoiceResponse = new List<string>();
                            }
                        }
                    }
                    else if (q.Type == QuestionType.LikertScale)
                    {
                        // For LikertScale, we don't need to parse options anymore
                        // The scale is always 1-10
                        questionViewModel.LikertOptions = new List<LikertOptionViewModel>();
                        
                        if (!string.IsNullOrEmpty(existingAnswer?.ResponseText) && int.TryParse(existingAnswer.ResponseText, out int likertValue))
                        {
                            questionViewModel.ClientLikertResponse = likertValue;
                        }
                    }
                    else if (q.Type == QuestionType.TrueFalse)
                    {
                        questionViewModel.Options = new List<string> { "Verdadero", "Falso" };
                    }

                    return questionViewModel;
                }).ToList()
            };

            ViewBag.BreadcrumbActiveKey = "breadcrumb_respond_quiz";
            return View(viewModel);
        }

        // POST: ClientQuizzes/SaveDraft
        [HttpPost]
        public async Task<IActionResult> SaveDraft([FromBody] SaveQuizResponsesViewModel viewModel)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                
                var instance = await _context.QuestionnaireInstances
                    .FirstOrDefaultAsync(qi => qi.Id == viewModel.InstanceId && qi.ClientId == currentUserId);

                if (instance == null)
                {
                    return Json(new { success = false, message = "Cuestionario no encontrado" });
                }

                if (instance.Status == QuestionnaireStatus.Completed)
                {
                    return Json(new { success = false, message = "Este cuestionario ya está completado" });
                }

                foreach (var answerData in viewModel.Answers)
                {
                    if (string.IsNullOrWhiteSpace(answerData.ResponseText)) continue;

                    var existingAnswer = await _context.Answers
                        .FirstOrDefaultAsync(a => a.QuestionnaireInstanceId == viewModel.InstanceId && 
                                                a.QuestionTemplateId == answerData.QuestionTemplateId);

                    if (existingAnswer != null)
                    {
                        existingAnswer.ResponseText = answerData.ResponseText;
                        existingAnswer.AnsweredAt = DateTime.UtcNow;
                    }
                    else
                    {
                        var newAnswer = new Answer
                        {
                            QuestionnaireInstanceId = viewModel.InstanceId,
                            QuestionTemplateId = answerData.QuestionTemplateId,
                            ResponseText = answerData.ResponseText,
                            AnsweredAt = DateTime.UtcNow
                        };
                        _context.Answers.Add(newAnswer);
                    }
                }

                instance.Status = QuestionnaireStatus.InProgress;
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Borrador guardado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving quiz draft");
                return Json(new { success = false, message = "Error al guardar el borrador" });
            }
        }

        // POST: ClientQuizzes/SubmitAnswers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitAnswers(SaveQuizResponsesViewModel viewModel)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                
                var instance = await _context.QuestionnaireInstances
                    .Include(qi => qi.QuestionnaireTemplate)
                    .ThenInclude(qt => qt.Questions)
                    .Include(qi => qi.Client)
                    .FirstOrDefaultAsync(qi => qi.Id == viewModel.InstanceId && qi.ClientId == currentUserId);

                if (instance == null)
                {
                    TempData["ErrorMessage"] = "Cuestionario no encontrado";
                    return RedirectToAction("Index");
                }

                if (instance.Status == QuestionnaireStatus.Completed)
                {
                    TempData["ErrorMessage"] = "Este cuestionario ya está completado";
                    return RedirectToAction("Index");
                }

                // Validar preguntas requeridas
                var requiredQuestions = instance.QuestionnaireTemplate.Questions.Where(q => q.IsRequired).ToList();
                var answeredQuestionIds = viewModel.Answers
                    .Where(a => !string.IsNullOrWhiteSpace(a.ResponseText))
                    .Select(a => a.QuestionTemplateId)
                    .ToList();

                var missingRequired = requiredQuestions
                    .Where(q => !answeredQuestionIds.Contains(q.Id))
                    .ToList();

                if (missingRequired.Any())
                {
                    TempData["ErrorMessage"] = $"Faltan respuestas obligatorias para: {string.Join(", ", missingRequired.Take(3).Select(q => q.QuestionText))}";
                    return RedirectToAction("Respond", new { id = viewModel.InstanceId });
                }

                // Guardar todas las respuestas
                foreach (var answerData in viewModel.Answers)
                {
                    if (string.IsNullOrWhiteSpace(answerData.ResponseText)) continue;

                    var existingAnswer = await _context.Answers
                        .FirstOrDefaultAsync(a => a.QuestionnaireInstanceId == viewModel.InstanceId && 
                                                a.QuestionTemplateId == answerData.QuestionTemplateId);

                    if (existingAnswer != null)
                    {
                        existingAnswer.ResponseText = answerData.ResponseText;
                        existingAnswer.AnsweredAt = DateTime.UtcNow;
                    }
                    else
                    {
                        var newAnswer = new Answer
                        {
                            QuestionnaireInstanceId = viewModel.InstanceId,
                            QuestionTemplateId = answerData.QuestionTemplateId,
                            ResponseText = answerData.ResponseText,
                            AnsweredAt = DateTime.UtcNow
                        };
                        _context.Answers.Add(newAnswer);
                    }
                }

                // Marcar como completado
                instance.Status = QuestionnaireStatus.Completed;
                instance.CompletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                // Notificar al coach
                if (instance.AssignedByCoachId > 0)
                {
                    await _notificationService.CreateNotificationAsync(
                        instance.AssignedByCoachId,
                        "questionnaire_completed_by_client",
                        new {
                            QuestionnaireTitle = instance.QuestionnaireTemplate.Title,
                            ClientName = instance.Client.FullName,
                            InstanceId = instance.Id
                        }
                    );
                }

                TempData["SuccessMessage"] = "Cuestionario enviado exitosamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting quiz answers");
                TempData["ErrorMessage"] = "Error al enviar el cuestionario";
                return RedirectToAction("Respond", new { id = viewModel.InstanceId });
            }
        }

        // Helper methods

        private List<string> ParseOptionsFromJson(string? optionsJson)
        {
            if (string.IsNullOrEmpty(optionsJson))
                return new List<string>();

            try
            {
                var options = JsonSerializer.Deserialize<List<string>>(optionsJson);
                return options ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        private List<LikertOptionViewModel> ParseLikertOptionsFromJson(string? optionsJson)
        {
            if (string.IsNullOrEmpty(optionsJson))
                return new List<LikertOptionViewModel>();

            try
            {
                var options = JsonSerializer.Deserialize<List<LikertOptionViewModel>>(optionsJson);
                
                // Filter out invalid options (value <= 0 or empty text)
                var validOptions = options?.Where(o => o.Value > 0 && !string.IsNullOrWhiteSpace(o.Text))
                              .OrderBy(o => o.Value)
                              .ToList() ?? new List<LikertOptionViewModel>();
                
                // If no valid options after filtering, check if we have options with value 0
                if (validOptions.Count == 0 && options?.Any() == true)
                {
                    // For backward compatibility, include options with value 0 but fix their values
                    validOptions = options.Where(o => !string.IsNullOrWhiteSpace(o.Text))
                                         .Select((o, index) => new LikertOptionViewModel
                                         {
                                             Value = index + 1, // Fix zero values
                                             Text = o.Text
                                         })
                                         .ToList();
                }
                              
                return validOptions;
            }
            catch
            {
                // Try to parse as simple array and convert
                try
                {
                    var simpleOptions = JsonSerializer.Deserialize<List<string>>(optionsJson);
                    return simpleOptions?.Where(text => !string.IsNullOrWhiteSpace(text))
                                       .Select((text, index) => new LikertOptionViewModel 
                                       { 
                                           Value = index + 1, 
                                           Text = text.Trim() 
                                       }).ToList() ?? new List<LikertOptionViewModel>();
                }
                catch
                {
                    return new List<LikertOptionViewModel>();
                }
            }
        }
    }
}