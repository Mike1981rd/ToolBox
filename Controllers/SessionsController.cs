using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    [Authorize]
    public class SessionsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SessionsController> _logger;
        private readonly INotificationService _notificationService;

        public SessionsController(ApplicationDbContext context, ILogger<SessionsController> logger, INotificationService notificationService)
        {
            _context = context;
            _logger = logger;
            _notificationService = notificationService;
        }

        // GET: Sessions
        public async Task<IActionResult> Index(string? statusFilter = null)
        {
            // Default to active sessions
            statusFilter = statusFilter ?? "active";
            ViewBag.CurrentStatusFilter = statusFilter;

            var query = _context.Sessions
                .Include(s => s.User)
                .AsQueryable();

            // Apply status filter
            if (statusFilter == "active")
            {
                query = query.Where(s => s.Status == true);
            }
            else if (statusFilter == "inactive")
            {
                query = query.Where(s => s.Status == false);
            }
            // If statusFilter is "all" or null, don't filter by status
            
            // Search is handled on client-side for better performance

            // Order by session date descending
            query = query.OrderByDescending(s => s.SessionDateTime);

            // Pagination
            var pageSize = 10;
            var sessions = await query.ToListAsync();

            var viewModel = new SessionsIndexViewModel
            {
                Sessions = sessions,
                CurrentStatusFilter = statusFilter
            };

            return View(viewModel);
        }

        // GET: Sessions/Create
        public async Task<IActionResult> Create()
        {
            // Get active users for dropdown
            var activeUsers = await _context.Users
                .Where(u => u.IsActive)
                .OrderBy(u => u.FullName)
                .Select(u => new
                {
                    u.Id,
                    u.FullName
                })
                .ToListAsync();

            ViewBag.Users = activeUsers;
            return View();
        }

        // POST: Sessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Store(SessionCreateViewModel model, List<IFormFile>? attachments)
        {
            // Custom validation for dates
            if (model.NextSessionDateTime.HasValue && model.NextSessionDateTime < model.SessionDateTime)
            {
                ModelState.AddModelError("NextSessionDateTime", "La próxima sesión debe ser posterior a la sesión actual");
            }

            // Validate attachments
            if (attachments != null && attachments.Any())
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx", ".mp4", ".mov" };
                var maxFileSize = 10 * 1024 * 1024; // 10MB

                foreach (var file in attachments)
                {
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("attachments", $"El archivo {file.FileName} tiene un formato no permitido.");
                    }
                    
                    if (file.Length > maxFileSize)
                    {
                        ModelState.AddModelError("attachments", $"El archivo {file.FileName} excede el tamaño máximo de 10MB.");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                
                try
                {
                    var session = new Session
                    {
                        UserId = model.UserId,
                        SessionDateTime = DateTime.SpecifyKind(model.SessionDateTime, DateTimeKind.Utc),
                        NextSessionDateTime = model.NextSessionDateTime.HasValue 
                            ? DateTime.SpecifyKind(model.NextSessionDateTime.Value, DateTimeKind.Utc) 
                            : null,
                        KeyPoints = model.KeyPoints,
                        WaysToDevelop = model.WaysToDevelop,
                        Assignments = model.Assignments,
                        Challenges = model.Challenges,
                        Feedback = model.Feedback,
                        Twitter = model.Twitter,
                        Status = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    _context.Sessions.Add(session);
                    await _context.SaveChangesAsync();

                    // Handle file uploads
                    if (attachments != null && attachments.Any())
                    {
                        var uploadsPath = Path.Combine("wwwroot", "uploads", "session_attachments", session.Id.ToString());
                        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), uploadsPath);
                        
                        if (!Directory.Exists(fullPath))
                        {
                            Directory.CreateDirectory(fullPath);
                        }

                        foreach (var file in attachments)
                        {
                            if (file.Length > 0)
                            {
                                // Generate unique filename
                                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                                var filePath = Path.Combine(fullPath, uniqueFileName);
                                
                                // Save file to disk
                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                // Save file info to database
                                var sessionFile = new SessionFile
                                {
                                    SessionId = session.Id,
                                    OriginalName = file.FileName,
                                    FilePath = $"/uploads/session_attachments/{session.Id}/{uniqueFileName}",
                                    MimeType = file.ContentType,
                                    Size = (int)file.Length,
                                    CreatedAt = DateTime.UtcNow,
                                    UpdatedAt = DateTime.UtcNow
                                };

                                _context.SessionFiles.Add(sessionFile);
                            }
                        }

                        await _context.SaveChangesAsync();
                    }

                    // Trigger notification for client (if client has an associated user account)
                    // NOTE: Currently, customers don't have user accounts in the system.
                    // This notification would only work if we implement a Customer-User relationship
                    // Create notification for the user
                    try
                    {
                        var coachUser = await _context.Users.FindAsync(GetCurrentUserId());
                        var coachName = coachUser?.FullName ?? User.Identity?.Name ?? "Coach";
                        var sessionData = new
                        {
                            SessionId = session.Id,
                            SessionDateTime = session.SessionDateTime,
                            CoachName = coachName
                        };
                        
                        await _notificationService.CreateNotificationAsync(
                            session.UserId,
                            "session_scheduled_by_coach",
                            sessionData
                        );
                        
                        _logger.LogInformation("Session {SessionId} created for user {UserId} by coach {CoachName}", 
                            session.Id, session.UserId, coachName);
                    }
                    catch (Exception notificationEx)
                    {
                        // Don't fail the transaction if notification fails
                        _logger.LogError(notificationEx, "Failed to create notification for session {SessionId}", session.Id);
                    }

                    await transaction.CommitAsync();
                    TempData["SuccessMessage"] = "Sesión creada exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error al crear la sesión");
                    ModelState.AddModelError("", "Ocurrió un error al guardar la sesión");
                }
            }

            // If we got this far, something failed, redisplay form
            var activeUsers = await _context.Users
                .Where(u => u.IsActive)
                .OrderBy(u => u.FullName)
                .Select(u => new
                {
                    u.Id,
                    u.FullName
                })
                .ToListAsync();

            ViewBag.Users = activeUsers;
            return View("Create", model);
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _context.Sessions
                .Include(s => s.User)
                .Include(s => s.SessionFiles)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            // Get active users for dropdown
            var activeUsers = await _context.Users
                .Where(u => u.IsActive)
                .OrderBy(u => u.FullName)
                .Select(u => new
                {
                    u.Id,
                    u.FullName
                })
                .ToListAsync();

            var viewModel = new SessionEditViewModel
            {
                Id = session.Id,
                UserId = session.UserId,
                SessionDateTime = session.SessionDateTime,
                NextSessionDateTime = session.NextSessionDateTime,
                KeyPoints = session.KeyPoints,
                WaysToDevelop = session.WaysToDevelop,
                Assignments = session.Assignments,
                Challenges = session.Challenges,
                Feedback = session.Feedback,
                Twitter = session.Twitter,
                Status = session.Status,
                SessionFiles = session.SessionFiles.ToList()
            };

            ViewBag.Users = activeUsers;
            return View(viewModel);
        }

        // POST: Sessions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, SessionEditViewModel model, List<IFormFile>? attachments)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            // Custom validation for dates
            if (model.NextSessionDateTime.HasValue && model.NextSessionDateTime < model.SessionDateTime)
            {
                ModelState.AddModelError("NextSessionDateTime", "La próxima sesión debe ser posterior a la sesión actual");
            }

            // Validate new attachments
            if (attachments != null && attachments.Any())
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx", ".mp4", ".mov" };
                var maxFileSize = 10 * 1024 * 1024; // 10MB

                foreach (var file in attachments)
                {
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("attachments", $"El archivo {file.FileName} tiene un formato no permitido.");
                    }
                    
                    if (file.Length > maxFileSize)
                    {
                        ModelState.AddModelError("attachments", $"El archivo {file.FileName} excede el tamaño máximo de 10MB.");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                
                try
                {
                    var session = await _context.Sessions.FindAsync(id);
                    if (session == null)
                    {
                        return NotFound();
                    }

                    // Update session properties
                    session.UserId = model.UserId;
                    session.SessionDateTime = DateTime.SpecifyKind(model.SessionDateTime, DateTimeKind.Utc);
                    session.NextSessionDateTime = model.NextSessionDateTime.HasValue 
                        ? DateTime.SpecifyKind(model.NextSessionDateTime.Value, DateTimeKind.Utc) 
                        : null;
                    session.KeyPoints = model.KeyPoints;
                    session.WaysToDevelop = model.WaysToDevelop;
                    session.Assignments = model.Assignments;
                    session.Challenges = model.Challenges;
                    session.Feedback = model.Feedback;
                    session.Twitter = model.Twitter;
                    session.UpdatedAt = DateTime.UtcNow;

                    _context.Update(session);
                    await _context.SaveChangesAsync();

                    // Handle new file uploads
                    if (attachments != null && attachments.Any())
                    {
                        var uploadsPath = Path.Combine("wwwroot", "uploads", "session_attachments", session.Id.ToString());
                        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), uploadsPath);
                        
                        if (!Directory.Exists(fullPath))
                        {
                            Directory.CreateDirectory(fullPath);
                        }

                        foreach (var file in attachments)
                        {
                            if (file.Length > 0)
                            {
                                // Generate unique filename
                                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                                var filePath = Path.Combine(fullPath, uniqueFileName);
                                
                                // Save file to disk
                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                // Save file info to database
                                var sessionFile = new SessionFile
                                {
                                    SessionId = session.Id,
                                    OriginalName = file.FileName,
                                    FilePath = $"/uploads/session_attachments/{session.Id}/{uniqueFileName}",
                                    MimeType = file.ContentType,
                                    Size = (int)file.Length,
                                    CreatedAt = DateTime.UtcNow,
                                    UpdatedAt = DateTime.UtcNow
                                };

                                _context.SessionFiles.Add(sessionFile);
                            }
                        }

                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                    TempData["SuccessMessage"] = "Sesión actualizada exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    if (!SessionExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error al actualizar la sesión");
                    ModelState.AddModelError("", "Ocurrió un error al actualizar la sesión");
                }
            }

            // If we got this far, something failed, redisplay form
            var activeUsers = await _context.Users
                .Where(u => u.IsActive)
                .OrderBy(u => u.FullName)
                .Select(u => new
                {
                    u.Id,
                    u.FullName
                })
                .ToListAsync();

            // Reload session files if validation failed
            model.SessionFiles = await _context.SessionFiles
                .Where(sf => sf.SessionId == id)
                .ToListAsync();

            ViewBag.Users = activeUsers;
            return View("Edit", model);
        }

        // POST: Sessions/ToggleStatus
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var session = await _context.Sessions.FindAsync(id);
                if (session == null)
                {
                    return Json(new { success = false, message = "Sesión no encontrada" });
                }

                session.Status = !session.Status;
                session.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                var message = session.Status ? "Sesión activada exitosamente" : "Sesión desactivada exitosamente";
                return Json(new { success = true, message = message, newIsActiveState = session.Status });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar el estado de la sesión");
                return Json(new { success = false, message = "Error al cambiar el estado de la sesión" });
            }
        }

        // DELETE: Sessions/DeleteFile
        [HttpPost]
        public async Task<IActionResult> DeleteFile(int fileId)
        {
            try
            {
                // TODO: Check permissions when authentication is implemented
                // For now, allow deletion for development

                var sessionFile = await _context.SessionFiles
                    .Include(sf => sf.Session)
                    .FirstOrDefaultAsync(sf => sf.Id == fileId);

                if (sessionFile == null)
                {
                    return Json(new { success = false, message = "Archivo no encontrado" });
                }

                // Delete physical file
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", sessionFile.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Delete from database
                _context.SessionFiles.Remove(sessionFile);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Archivo eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el archivo");
                return Json(new { success = false, message = "Error al eliminar el archivo" });
            }
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}