using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ToolBox.Models;
using ToolBox.Models.ViewModels;
using ToolBox.Interfaces;
using ToolBox.Data;

namespace ToolBox.Controllers
{
    public class QuestionnaireTemplatesController : BaseController
    {
        private readonly IQuestionnaireTemplateService _templateService;
        private readonly INotificationService _notificationService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<QuestionnaireTemplatesController> _logger;

        public QuestionnaireTemplatesController(
            IQuestionnaireTemplateService templateService,
            INotificationService notificationService,
            ApplicationDbContext context,
            ILogger<QuestionnaireTemplatesController> logger)
        {
            _templateService = templateService;
            _notificationService = notificationService;
            _context = context;
            _logger = logger;
        }

        // GET: QuestionnaireTemplates
        public IActionResult Index()
        {
            ViewBag.BreadcrumbActiveKey = "breadcrumb_questionnaire_templates_list";
            ViewBag.HideTitleInLayout = true;
            return View();
        }

        // GET: QuestionnaireTemplates/Create
        public IActionResult Create()
        {
            ViewBag.BreadcrumbActiveKey = "breadcrumb_questionnaire_templates_create";
            var viewModel = new QuestionnaireTemplateCreateEditViewModel();
            return View(viewModel);
        }

        // POST: QuestionnaireTemplates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionnaireTemplateCreateEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var template = new QuestionnaireTemplate
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    CoachId = GetCurrentUserId(),
                    IsActive = true
                };

                var createdTemplate = await _templateService.CreateAsync(template);
                
                TempData["SuccessMessage"] = "Plantilla de cuestionario creada exitosamente";
                return RedirectToAction("Edit", new { id = createdTemplate.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating questionnaire template");
                ModelState.AddModelError("", "Ocurrió un error al crear la plantilla");
                return View(viewModel);
            }
        }

        // GET: QuestionnaireTemplates/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var template = await _templateService.GetByIdWithQuestionsAsync(id);
            
            if (template == null || template.CoachId != GetCurrentUserId())
            {
                return NotFound();
            }

            var viewModel = new QuestionnaireTemplateCreateEditViewModel
            {
                Id = template.Id,
                Title = template.Title,
                Description = template.Description,
                IsActive = template.IsActive,
                Questions = template.Questions.Select(q => new QuestionTemplateViewModel
                {
                    Id = q.Id,
                    QuestionnaireTemplateId = q.QuestionnaireTemplateId,
                    QuestionText = q.QuestionText,
                    Type = q.Type,
                    Options = ParseOptionsFromJson(q.OptionsJson),
                    LikertOptions = ParseLikertOptionsFromJson(q.OptionsJson, q.Type),
                    IsRequired = q.IsRequired,
                    Order = q.Order
                }).OrderBy(q => q.Order).ToList()
            };

            ViewBag.BreadcrumbActiveKey = "breadcrumb_questionnaire_templates_edit";
            return View(viewModel);
        }

        // POST: QuestionnaireTemplates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuestionnaireTemplateCreateEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Recargar las preguntas si hay error de validación
                var templateWithQuestions = await _templateService.GetByIdWithQuestionsAsync(viewModel.Id);
                if (templateWithQuestions != null)
                {
                    viewModel.Questions = templateWithQuestions.Questions.Select(q => new QuestionTemplateViewModel
                    {
                        Id = q.Id,
                        QuestionnaireTemplateId = q.QuestionnaireTemplateId,
                        QuestionText = q.QuestionText,
                        Type = q.Type,
                        Options = ParseOptionsFromJson(q.OptionsJson),
                        LikertOptions = ParseLikertOptionsFromJson(q.OptionsJson, q.Type),
                        IsRequired = q.IsRequired,
                        Order = q.Order
                    }).OrderBy(q => q.Order).ToList();
                }
                return View(viewModel);
            }

            try
            {
                var template = await _templateService.GetByIdAsync(viewModel.Id);
                
                if (template == null || template.CoachId != GetCurrentUserId())
                {
                    return NotFound();
                }

                template.Title = viewModel.Title;
                template.Description = viewModel.Description;

                var success = await _templateService.UpdateAsync(template);
                
                if (success)
                {
                    TempData["SuccessMessage"] = "Plantilla actualizada exitosamente";
                    return RedirectToAction("Edit", new { id = viewModel.Id });
                }
                else
                {
                    ModelState.AddModelError("", "Error al actualizar la plantilla");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating questionnaire template {TemplateId}", viewModel.Id);
                ModelState.AddModelError("", "Ocurrió un error al actualizar la plantilla");
            }

            return View(viewModel);
        }

        // GET: QuestionnaireTemplates/Assign/5
        public async Task<IActionResult> Assign(long id)
        {
            var currentUserId = GetCurrentUserId();
            var template = await _templateService.GetByIdWithQuestionsAsync(id);
            
            if (template == null || template.CoachId != currentUserId || !template.IsActive)
            {
                return NotFound();
            }

            // Obtener todos los usuarios activos excepto el coach actual
            var users = await _context.Users
                .Where(u => u.IsActive && u.Id != currentUserId)
                .OrderBy(u => u.FullName)
                .ToListAsync();

            // Obtener asignaciones existentes
            var existingAssignments = await _context.QuestionnaireInstances
                .Where(qi => qi.QuestionnaireTemplateId == id)
                .Include(qi => qi.Client)
                .ToListAsync();

            var viewModel = new AssignQuestionnaireViewModel
            {
                QuestionnaireTemplateId = template.Id,
                TemplateTitle = template.Title,
                TemplateDescription = template.Description,
                QuestionCount = template.Questions.Count,
                AvailableClients = users
                    .Where(u => !existingAssignments.Any(ea => 
                        ea.ClientId == u.Id && 
                        (ea.Status == QuestionnaireStatus.Pending || ea.Status == QuestionnaireStatus.InProgress)))
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = $"{u.FullName} ({u.Email})"
                    })
                    .ToList(),
                AlreadyAssignedClients = existingAssignments
                    .Select(ea => new ExistingAssignmentViewModel
                    {
                        ClientId = ea.ClientId,
                        ClientName = ea.Client.FullName,
                        Status = ea.Status,
                        AssignedAt = ea.AssignedAt
                    })
                    .ToList()
            };

            ViewBag.BreadcrumbActiveKey = "breadcrumb_questionnaire_templates_assign";
            return View(viewModel);
        }

        // POST: QuestionnaireTemplates/Assign
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(AssignQuestionnaireViewModel viewModel)
        {
            if (!ModelState.IsValid || !viewModel.SelectedClientIds.Any())
            {
                ModelState.AddModelError("", "Debes seleccionar al menos un cliente");
                
                // Recargar datos para la vista
                var users = await _context.Users
                    .Where(u => u.IsActive && u.Id != GetCurrentUserId())
                    .OrderBy(u => u.FullName)
                    .ToListAsync();
                    
                viewModel.AvailableClients = users.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FullName} ({u.Email})"
                }).ToList();
                
                return View(viewModel);
            }

            try
            {
                var currentUserId = GetCurrentUserId();
                var template = await _templateService.GetByIdAsync(viewModel.QuestionnaireTemplateId);
                
                if (template == null || template.CoachId != currentUserId || !template.IsActive)
                {
                    return NotFound();
                }

                int assignedCount = 0;
                int skippedCount = 0;

                foreach (var clientId in viewModel.SelectedClientIds)
                {
                    // Verificar si ya existe una instancia pendiente o en progreso
                    var existingInstance = await _context.QuestionnaireInstances
                        .AnyAsync(qi => 
                            qi.ClientId == clientId && 
                            qi.QuestionnaireTemplateId == viewModel.QuestionnaireTemplateId && 
                            (qi.Status == QuestionnaireStatus.Pending || qi.Status == QuestionnaireStatus.InProgress));

                    if (existingInstance)
                    {
                        skippedCount++;
                        continue;
                    }

                    // Crear nueva instancia
                    var instance = new QuestionnaireInstance
                    {
                        QuestionnaireTemplateId = viewModel.QuestionnaireTemplateId,
                        ClientId = clientId,
                        AssignedByCoachId = currentUserId,
                        Status = QuestionnaireStatus.Pending,
                        AssignedAt = DateTime.UtcNow
                    };

                    _context.QuestionnaireInstances.Add(instance);
                    assignedCount++;

                    // Crear notificación
                    try
                    {
                        await _notificationService.CreateNotificationAsync(
                            clientId,
                            "questionnaire_assigned",
                            new 
                            { 
                                QuestionnaireTitle = template.Title,
                                TemplateId = template.Id,
                                CoachName = User.Identity?.Name ?? "Tu coach"
                            }
                        );
                    }
                    catch (Exception notificationEx)
                    {
                        _logger.LogError(notificationEx, "Error al crear notificación para cliente {ClientId}", clientId);
                        // Continuar aunque falle la notificación
                    }
                }

                await _context.SaveChangesAsync();

                string message = assignedCount > 0 
                    ? $"Cuestionario asignado exitosamente a {assignedCount} cliente(s)."
                    : "No se pudo asignar el cuestionario a ningún cliente.";
                    
                if (skippedCount > 0)
                {
                    message += $" Se omitieron {skippedCount} cliente(s) que ya tienen este cuestionario asignado.";
                }

                TempData["SuccessMessage"] = message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al asignar cuestionario");
                ModelState.AddModelError("", "Ocurrió un error al asignar el cuestionario");
                
                // Recargar datos para la vista
                var users = await _context.Users
                    .Where(u => u.IsActive && u.Id != GetCurrentUserId())
                    .OrderBy(u => u.FullName)
                    .ToListAsync();
                    
                viewModel.AvailableClients = users.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FullName} ({u.Email})"
                }).ToList();
                return View(viewModel);
            }
        }

        // GET: QuestionnaireTemplates/Assignments
        public IActionResult Assignments()
        {
            ViewBag.BreadcrumbActiveKey = "breadcrumb_questionnaire_assignments";
            ViewBag.HideTitleInLayout = true;
            return View();
        }

        // GET: QuestionnaireTemplates/ViewResponses/5
        public async Task<IActionResult> ViewResponses(long instanceId)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                
                var instance = await _context.QuestionnaireInstances
                    .Include(qi => qi.QuestionnaireTemplate)
                    .ThenInclude(qt => qt.Questions.OrderBy(q => q.Order))
                    .Include(qi => qi.Client)
                    .Include(qi => qi.Answers)
                    .ThenInclude(a => a.QuestionTemplate)
                    .FirstOrDefaultAsync(qi => qi.Id == instanceId && qi.AssignedByCoachId == currentUserId);

                if (instance == null)
                {
                    return NotFound();
                }

                if (instance.Status != QuestionnaireStatus.Completed)
                {
                    TempData["ErrorMessage"] = "Este cuestionario aún no ha sido completado por el cliente.";
                    return RedirectToAction("Assignments", new { id = instance.QuestionnaireTemplateId });
                }

                var viewModel = new ViewResponsesViewModel
                {
                    InstanceId = instance.Id,
                    QuestionnaireTitle = instance.QuestionnaireTemplate.Title,
                    QuestionnaireDescription = instance.QuestionnaireTemplate.Description,
                    ClientName = instance.Client.FullName,
                    ClientEmail = instance.Client.Email,
                    ClientAvatarUrl = instance.Client.AvatarUrl,
                    AssignedAt = instance.AssignedAt,
                    StartedAt = instance.StartedAt,
                    CompletedAt = instance.CompletedAt,
                    Status = instance.Status,
                    QuestionAnswers = instance.QuestionnaireTemplate.Questions.Select(q =>
                    {
                        var answer = instance.Answers.FirstOrDefault(a => a.QuestionTemplateId == q.Id);
                        var questionAnswer = new ToolBox.Models.ViewModels.QuestionAnswerViewModel
                        {
                            QuestionTemplateId = q.Id,
                            QuestionText = q.QuestionText,
                            QuestionType = q.Type,
                            IsRequired = q.IsRequired,
                            QuestionOrder = q.Order,
                            ClientResponseText = answer?.ResponseText,
                            AnsweredAt = answer?.AnsweredAt,
                            OriginalOptions = ParseOptionsFromJson(q.OptionsJson),
                            OriginalLikertOptions = ParseLikertOptionsFromJson(q.OptionsJson, q.Type)
                        };

                        // Procesar respuestas según tipo de pregunta
                        if (!string.IsNullOrEmpty(answer?.ResponseText))
                        {
                            if (q.Type == QuestionType.MultipleChoice)
                            {
                                try
                                {
                                    questionAnswer.ClientMultipleChoiceResponse = 
                                        JsonSerializer.Deserialize<List<string>>(answer.ResponseText) ?? new List<string>();
                                }
                                catch
                                {
                                    questionAnswer.ClientMultipleChoiceResponse = new List<string>();
                                }
                            }
                            else if (q.Type == QuestionType.LikertScale && int.TryParse(answer.ResponseText, out int likertValue))
                            {
                                questionAnswer.ClientLikertResponse = likertValue;
                            }
                        }

                        return questionAnswer;
                    }).OrderBy(qa => qa.QuestionOrder).ToList()
                };

                ViewBag.BreadcrumbActiveKey = "breadcrumb_view_responses";
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error viewing responses for instance {InstanceId}", instanceId);
                TempData["ErrorMessage"] = "Error al cargar las respuestas del cuestionario.";
                return RedirectToAction("Index");
            }
        }

        // GET: QuestionnaireTemplates/ExportResponses/5
        public async Task<IActionResult> ExportResponses(long instanceId)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                
                var instance = await _context.QuestionnaireInstances
                    .Include(qi => qi.QuestionnaireTemplate)
                    .ThenInclude(qt => qt.Questions.OrderBy(q => q.Order))
                    .Include(qi => qi.Client)
                    .Include(qi => qi.Answers)
                    .ThenInclude(a => a.QuestionTemplate)
                    .FirstOrDefaultAsync(qi => qi.Id == instanceId && qi.AssignedByCoachId == currentUserId);

                if (instance == null)
                {
                    return NotFound();
                }

                if (instance.Status != QuestionnaireStatus.Completed)
                {
                    TempData["ErrorMessage"] = "Este cuestionario aún no ha sido completado por el cliente.";
                    return RedirectToAction("Assignments", new { id = instance.QuestionnaireTemplateId });
                }

                // TODO: Implementar exportación a PDF usando QuestPDF
                // Por ahora retornamos un mensaje indicando que está en desarrollo
                TempData["InfoMessage"] = "La funcionalidad de exportación a PDF estará disponible próximamente.";
                return RedirectToAction("ViewResponses", new { instanceId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting responses for instance {InstanceId}", instanceId);
                TempData["ErrorMessage"] = "Error al exportar las respuestas del cuestionario.";
                return RedirectToAction("ViewResponses", new { instanceId });
            }
        }

        // GET: QuestionnaireTemplates/AllAssignments - Vista mejorada de todas las asignaciones
        public async Task<IActionResult> AllAssignments(
            long? templateId = null,
            int? clientId = null,
            string? status = null,
            string? searchQuery = null,
            int page = 1,
            int pageSize = 20)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                
                var query = _context.QuestionnaireInstances
                    .Include(qi => qi.QuestionnaireTemplate)
                    .Include(qi => qi.Client)
                    .Where(qi => qi.AssignedByCoachId == currentUserId);

                // Aplicar filtros
                if (templateId.HasValue)
                {
                    query = query.Where(qi => qi.QuestionnaireTemplateId == templateId.Value);
                }

                if (clientId.HasValue)
                {
                    query = query.Where(qi => qi.ClientId == clientId.Value);
                }

                if (!string.IsNullOrEmpty(status) && status != "all")
                {
                    if (Enum.TryParse<QuestionnaireStatus>(status, true, out var statusEnum))
                    {
                        query = query.Where(qi => qi.Status == statusEnum);
                    }
                }

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    query = query.Where(qi => 
                        qi.QuestionnaireTemplate.Title.Contains(searchQuery) ||
                        qi.Client.FullName.Contains(searchQuery) ||
                        qi.Client.Email.Contains(searchQuery));
                }

                var totalCount = await query.CountAsync();
                var assignments = await query
                    .OrderByDescending(qi => qi.AssignedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // Obtener conteos de respuestas para cada asignación
                var assignmentViewModels = new List<CoachAssignmentItemViewModel>();
                foreach (var assignment in assignments)
                {
                    var answeredCount = await _context.Answers
                        .Where(a => a.QuestionnaireInstanceId == assignment.Id && !string.IsNullOrEmpty(a.ResponseText))
                        .CountAsync();

                    var totalQuestions = await _context.QuestionTemplates
                        .Where(qt => qt.QuestionnaireTemplateId == assignment.QuestionnaireTemplateId)
                        .CountAsync();

                    assignmentViewModels.Add(new CoachAssignmentItemViewModel
                    {
                        InstanceId = assignment.Id,
                        QuestionnaireTitle = assignment.QuestionnaireTemplate.Title,
                        ClientName = assignment.Client.FullName,
                        ClientEmail = assignment.Client.Email,
                        ClientAvatarUrl = assignment.Client.AvatarUrl,
                        AssignedAt = assignment.AssignedAt,
                        Status = assignment.Status,
                        StartedAt = assignment.StartedAt,
                        CompletedAt = assignment.CompletedAt,
                        TotalQuestions = totalQuestions,
                        AnsweredQuestions = answeredCount
                    });
                }

                // Obtener templates y clientes para filtros
                var availableTemplates = await _context.QuestionnaireTemplates
                    .Where(qt => qt.CoachId == currentUserId && qt.IsActive)
                    .Select(qt => new QuestionnaireTemplateSelectViewModel
                    {
                        Id = qt.Id,
                        Title = qt.Title,
                        AssignmentCount = _context.QuestionnaireInstances.Count(qi => qi.QuestionnaireTemplateId == qt.Id)
                    })
                    .ToListAsync();

                var availableClients = await _context.QuestionnaireInstances
                    .Where(qi => qi.AssignedByCoachId == currentUserId)
                    .Include(qi => qi.Client)
                    .Select(qi => qi.Client)
                    .Distinct()
                    .Select(u => new ClientSelectViewModel
                    {
                        Id = u.Id,
                        FullName = u.FullName,
                        Email = u.Email,
                        AssignmentCount = _context.QuestionnaireInstances.Count(qi => qi.ClientId == u.Id)
                    })
                    .ToListAsync();

                var viewModel = new CoachAssignmentsIndexViewModel
                {
                    Assignments = assignmentViewModels,
                    AvailableTemplates = availableTemplates,
                    AvailableClients = availableClients,
                    SelectedTemplateId = templateId,
                    SelectedClientId = clientId,
                    SelectedStatus = status,
                    SearchQuery = searchQuery,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalCount = totalCount
                };

                ViewBag.BreadcrumbActiveKey = "breadcrumb_all_assignments";
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading all assignments");
                TempData["ErrorMessage"] = "Error al cargar las asignaciones.";
                
                // Devolver un modelo vacío en lugar de redirect para evitar el error de vista
                var emptyViewModel = new CoachAssignmentsIndexViewModel
                {
                    Assignments = new List<CoachAssignmentItemViewModel>(),
                    AvailableTemplates = new List<QuestionnaireTemplateSelectViewModel>(),
                    AvailableClients = new List<ClientSelectViewModel>(),
                    CurrentPage = 1,
                    PageSize = 20,
                    TotalCount = 0
                };
                
                ViewBag.BreadcrumbActiveKey = "breadcrumb_all_assignments";
                return View(emptyViewModel);
            }
        }

        // API Endpoints
        [HttpGet]
        [Route("api/questionnaire-templates")]
        public async Task<IActionResult> GetTemplatesApi(
            string? searchTerm = null, 
            int page = 1, 
            int pageSize = 10,
            string? statusFilter = null)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var includeInactive = statusFilter == "all" || statusFilter == "inactive";
                
                var (templates, totalCount) = await _templateService.GetPaginatedAsync(
                    currentUserId, searchTerm, page, pageSize, includeInactive);

                var viewModels = templates.Select(t => new QuestionnaireTemplateListViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    IsActive = t.IsActive,
                    QuestionCount = t.Questions?.Count ?? 0,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    CoachName = t.Coach?.FullName ?? "Desconocido"
                }).ToList();

                // Filtrar por estado si es necesario
                if (statusFilter == "active")
                {
                    viewModels = viewModels.Where(t => t.IsActive).ToList();
                }
                else if (statusFilter == "inactive")
                {
                    viewModels = viewModels.Where(t => !t.IsActive).ToList();
                }

                var response = new QuestionnaireTemplatePaginatedResponseViewModel
                {
                    Templates = viewModels,
                    TotalCount = totalCount,
                    CurrentPage = page,
                    PageSize = pageSize
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting questionnaire templates API");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPost]
        [Route("api/questionnaire-templates/{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(long id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var (success, message, newState) = await _templateService.ToggleStatusAsync(id, currentUserId);
                
                return Json(new { success, message, newIsActiveState = newState });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling questionnaire template status {TemplateId}", id);
                return Json(new { success = false, message = "Error al cambiar el estado" });
            }
        }

        // API Endpoints para gestión de preguntas
        [HttpPost]
        [Route("api/questionnaire-templates/{templateId}/questions")]
        public async Task<IActionResult> CreateQuestion(long templateId, [FromBody] QuestionTemplateCreateEditViewModel questionViewModel)
        {
            try
            {
                // Validar que el modelo no sea null
                if (questionViewModel == null)
                {
                    return Json(new { success = false, message = "Datos de la pregunta inválidos" });
                }

                var currentUserId = GetCurrentUserId();
                
                // Verificar que la plantilla pertenece al coach actual
                if (!await _templateService.IsTemplateOwnedByCoachAsync(templateId, currentUserId))
                {
                    return NotFound();
                }

                var question = new QuestionTemplate
                {
                    QuestionnaireTemplateId = templateId,
                    QuestionText = questionViewModel.QuestionText,
                    Type = questionViewModel.Type,
                    OptionsJson = questionViewModel.OptionsJson,
                    IsRequired = questionViewModel.IsRequired
                };

                var createdQuestion = await _templateService.CreateQuestionAsync(question);

                var responseViewModel = new QuestionTemplateViewModel
                {
                    Id = createdQuestion.Id,
                    QuestionnaireTemplateId = createdQuestion.QuestionnaireTemplateId,
                    QuestionText = createdQuestion.QuestionText,
                    Type = createdQuestion.Type,
                    Options = ParseOptionsFromJson(createdQuestion.OptionsJson),
                    LikertOptions = ParseLikertOptionsFromJson(createdQuestion.OptionsJson, createdQuestion.Type),
                    IsRequired = createdQuestion.IsRequired,
                    Order = createdQuestion.Order
                };

                return Json(new { success = true, question = responseViewModel });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating question for template {TemplateId}", templateId);
                return Json(new { success = false, message = "Error al crear la pregunta" });
            }
        }

        [HttpPut]
        [Route("api/questionnaire-templates/questions/{questionId}")]
        public async Task<IActionResult> UpdateQuestion(long questionId, [FromBody] QuestionTemplateCreateEditViewModel questionViewModel)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                
                // Verificar que la pregunta pertenece al coach actual
                if (!await _templateService.IsQuestionOwnedByCoachAsync(questionId, currentUserId))
                {
                    return NotFound();
                }

                var question = await _templateService.GetQuestionByIdAsync(questionId);
                if (question == null)
                {
                    return NotFound();
                }

                question.QuestionText = questionViewModel.QuestionText;
                question.Type = questionViewModel.Type;
                question.OptionsJson = questionViewModel.OptionsJson;
                question.IsRequired = questionViewModel.IsRequired;

                var success = await _templateService.UpdateQuestionAsync(question);

                if (success)
                {
                    var responseViewModel = new QuestionTemplateViewModel
                    {
                        Id = question.Id,
                        QuestionnaireTemplateId = question.QuestionnaireTemplateId,
                        QuestionText = question.QuestionText,
                        Type = question.Type,
                        Options = ParseOptionsFromJson(question.OptionsJson),
                        LikertOptions = ParseLikertOptionsFromJson(question.OptionsJson, question.Type),
                        IsRequired = question.IsRequired,
                        Order = question.Order
                    };

                    return Json(new { success = true, question = responseViewModel });
                }
                else
                {
                    return Json(new { success = false, message = "Error al actualizar la pregunta" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating question {QuestionId}", questionId);
                return Json(new { success = false, message = "Error al actualizar la pregunta" });
            }
        }

        [HttpDelete]
        [Route("api/questionnaire-templates/questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(long questionId)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var success = await _templateService.DeleteQuestionAsync(questionId, currentUserId);
                
                return Json(new { success, message = success ? "Pregunta eliminada exitosamente" : "Error al eliminar la pregunta" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting question {QuestionId}", questionId);
                return Json(new { success = false, message = "Error al eliminar la pregunta" });
            }
        }

        [HttpPost]
        [Route("api/questionnaire-templates/{templateId}/questions/reorder")]
        public async Task<IActionResult> ReorderQuestions(long templateId, [FromBody] ReorderQuestionsViewModel reorderViewModel)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var success = await _templateService.ReorderQuestionsAsync(templateId, reorderViewModel.Questions, currentUserId);
                
                return Json(new { success, message = success ? "Preguntas reordenadas exitosamente" : "Error al reordenar las preguntas" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reordering questions for template {TemplateId}", templateId);
                return Json(new { success = false, message = "Error al reordenar las preguntas" });
            }
        }

        // Helper methods

        private List<string> ParseOptionsFromJson(string? optionsJson)
        {
            if (string.IsNullOrEmpty(optionsJson))
                return new List<string>();

            try
            {
                // Intentar parsear como array simple de strings
                var options = JsonSerializer.Deserialize<List<string>>(optionsJson);
                return options ?? new List<string>();
            }
            catch
            {
                // Si falla, intentar parsear como array de objetos Likert
                try
                {
                    var likertOptions = JsonSerializer.Deserialize<List<LikertOptionViewModel>>(optionsJson);
                    return likertOptions?.Select(lo => lo.Text).ToList() ?? new List<string>();
                }
                catch
                {
                    return new List<string>();
                }
            }
        }

        private List<LikertOptionViewModel> ParseLikertOptionsFromJson(string? optionsJson, QuestionType type)
        {
            if (type != QuestionType.LikertScale || string.IsNullOrEmpty(optionsJson))
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
                return new List<LikertOptionViewModel>();
            }
        }
    }
}