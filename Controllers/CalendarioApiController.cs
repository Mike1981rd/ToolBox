using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    [Route("api")]
    [ApiController]
    public class CalendarioApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CalendarioApiController> _logger;
        private readonly INotificationService _notificationService;

        public CalendarioApiController(ApplicationDbContext context, ILogger<CalendarioApiController> logger, INotificationService notificationService)
        {
            _context = context;
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpGet("users/current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var user = await _context.Users
                    .Where(u => u.Id == currentUserId)
                    .Select(u => new
                    {
                        id = u.Id,
                        fullName = u.FullName,
                        userName = u.UserName,
                        email = u.Email,
                        avatarUrl = u.AvatarUrl ?? "/img/default-avatar.png"
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                    return NotFound("Current user not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting current user");
                return StatusCode(500, "Error retrieving current user");
            }
        }

        [HttpGet("users/active")]
        public async Task<IActionResult> GetActiveUsers()
        {
            try
            {
                var users = await _context.Users
                    .Where(u => u.IsActive)
                    .Select(u => new
                    {
                        id = u.Id,
                        fullName = u.FullName,
                        userName = u.UserName,
                        email = u.Email,
                        avatarUrl = u.AvatarUrl ?? "/img/default-avatar.png"
                    })
                    .OrderBy(u => u.fullName)
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active users");
                return StatusCode(500, "Error retrieving users");
            }
        }
        
        [HttpGet("users/invitable")]
        public async Task<IActionResult> GetInvitableUsers()
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                
                // Get all active users except the current coach
                var users = await _context.Users
                    .Where(u => u.IsActive && u.Id != currentUserId)
                    .Select(u => new
                    {
                        id = u.Id,
                        fullName = u.FullName,
                        userName = u.UserName,
                        email = u.Email,
                        avatarUrl = u.AvatarUrl ?? "/img/default-avatar.png"
                    })
                    .OrderBy(u => u.fullName)
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting invitable users");
                return StatusCode(500, "Error retrieving users");
            }
        }

        [HttpGet("customers/active")]
        public async Task<IActionResult> GetActiveCustomers()
        {
            try
            {
                var customers = await _context.Customers
                    .Where(c => c.IsActive)
                    .Select(c => new
                    {
                        id = c.Id,
                        firstName = c.FirstName,
                        lastName = c.LastName,
                        email = c.Email,
                        profileImageUrl = c.AvatarUrl ?? "/img/default-avatar.png"
                    })
                    .OrderBy(c => c.firstName)
                    .ThenBy(c => c.lastName)
                    .ToListAsync();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active customers");
                return StatusCode(500, "Error retrieving customers");
            }
        }

        // Calendar session endpoints
        [HttpGet("sesionescalendario")]
        public async Task<IActionResult> GetCalendarSessions([FromQuery] DateTime? start, [FromQuery] DateTime? end)
        {
            try
            {
                var query = _context.SesionesCalendario
                    .Include(s => s.Coach)
                    .Include(s => s.SesionClientes)
                        .ThenInclude(sc => sc.Cliente)
                    .Include(s => s.SesionUsuarios)
                        .ThenInclude(su => su.Usuario)
                    .AsQueryable();

                // Temporalmente comentado para debug de fechas PostgreSQL
                // if (start.HasValue)
                //     query = query.Where(s => s.FechaHoraFin >= start.Value);
                //     
                // if (end.HasValue)
                //     query = query.Where(s => s.FechaHoraInicio <= end.Value);

                var sessions = await query
                    .Select(s => new
                    {
                        id = s.Id,
                        title = s.Titulo,
                        start = s.FechaHoraInicio,
                        end = s.FechaHoraFin,
                        estadoSesion = s.EstadoSesion,
                        ubicacion = s.UbicacionOEnlace,
                        descripcion = s.Descripcion,
                        coach = s.Coach != null ? new { id = s.Coach.Id, name = s.Coach.FullName } : null,
                        clients = s.SesionClientes.Select(sc => new
                        {
                            id = sc.Cliente.Id,
                            name = sc.Cliente.FullName
                        }),
                        users = s.SesionUsuarios.Select(su => new
                        {
                            id = su.Usuario.Id,
                            name = su.Usuario.FullName
                        }),
                        // Debug: agregar conteos
                        userCount = s.SesionUsuarios.Count(),
                        clientCount = s.SesionClientes.Count()
                    })
                    .ToListAsync();
                
                // Debug log
                foreach (var session in sessions)
                {
                    _logger.LogInformation("Session {SessionId}: {UserCount} users, {ClientCount} clients", 
                        session.id, session.userCount, session.clientCount);
                }

                return Ok(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting calendar sessions");
                return StatusCode(500, "Error retrieving sessions");
            }
        }

        [HttpPost("sesionescalendario")]
        public async Task<IActionResult> CreateCalendarSession([FromBody] CreateSessionDto dto)
        {
            try
            {
                var session = new SesionCalendario
                {
                    Titulo = dto.Titulo,
                    EstadoSesion = dto.EstadoSesion,
                    FechaHoraInicio = dto.FechaHoraInicio,
                    FechaHoraFin = dto.FechaHoraFin,
                    Descripcion = dto.Descripcion,
                    UbicacionOEnlace = dto.Ubicacion,
                    CoachId = dto.CoachId ?? GetCurrentUserId(),
                    CreatedAt = DateTime.UtcNow
                };

                _context.SesionesCalendario.Add(session);
                await _context.SaveChangesAsync();

                // Add clients (mantener para compatibilidad)
                if (dto.ClientIds != null && dto.ClientIds.Any())
                {
                    foreach (var clientId in dto.ClientIds)
                    {
                        _context.SesionesClientes.Add(new SesionCliente
                        {
                            SesionCalendarioId = session.Id,
                            ClienteId = clientId
                        });
                    }
                    await _context.SaveChangesAsync();
                }
                
                // Add users (nueva funcionalidad)
                if (dto.UserIds != null && dto.UserIds.Any())
                {
                    foreach (var userId in dto.UserIds)
                    {
                        _context.SesionesUsuarios.Add(new SesionUsuario
                        {
                            SesionCalendarioId = session.Id,
                            UsuarioId = userId
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                // Trigger notifications for calendar events
                // NOTE: Similar to Sessions, customers don't have user accounts in the system.
                // This notification logic assumes we'll implement a Customer-User relationship.
                try
                {
                    // Get coach info for notifications
                    var coach = await _context.Users.FindAsync(session.CoachId);
                    var coachName = coach?.FullName ?? "Coach";
                    
                    // For each client associated with this calendar event
                    if (dto.ClientIds != null && dto.ClientIds.Any())
                    {
                        // TODO: Once Customer-User relationship is established, implement notifications:
                        /*
                        foreach (var clientId in dto.ClientIds)
                        {
                            var customer = await _context.Customers.FindAsync(clientId);
                            if (customer?.UserId != null)
                            {
                                var eventData = new
                                {
                                    EventId = session.Id,
                                    EventTitle = session.Titulo,
                                    EventStartDate = session.FechaHoraInicio,
                                    EventEndDate = session.FechaHoraFin,
                                    CoachName = coachName,
                                    Location = session.UbicacionOEnlace
                                };
                                
                                await _notificationService.CreateNotificationAsync(
                                    customer.UserId.Value,
                                    "calendar_event_scheduled_for_client",
                                    eventData
                                );
                            }
                        }
                        */
                        
                        // For now, log the event creation
                        _logger.LogInformation("Calendar event {EventId} created by coach {CoachId} for {ClientCount} clients", 
                            session.Id, session.CoachId, dto.ClientIds.Count());
                    }
                    
                    // Send notifications to invited users
                    if (dto.UserIds != null && dto.UserIds.Any())
                    {
                        foreach (var userId in dto.UserIds)
                        {
                            var eventData = new
                            {
                                EventId = session.Id,
                                EventTitle = session.Titulo,
                                EventStartDate = session.FechaHoraInicio,
                                InvitedBy = coachName
                            };
                            
                            await _notificationService.CreateNotificationAsync(
                                userId,
                                "calendar_event_invitation",
                                eventData
                            );
                        }
                        
                        _logger.LogInformation("Sent notifications to {UserCount} users for event {EventId}", 
                            dto.UserIds.Count(), session.Id);
                    }
                }
                catch (Exception notificationEx)
                {
                    // Don't fail the request if notification fails
                    _logger.LogError(notificationEx, "Failed to create notifications for calendar event {EventId}", session.Id);
                }

                return Ok(new { id = session.Id, message = "Session created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating calendar session");
                return StatusCode(500, "Error creating session");
            }
        }

        [HttpPut("sesionescalendario/{id}")]
        public async Task<IActionResult> UpdateCalendarSession(int id, [FromBody] CreateSessionDto dto)
        {
            try
            {
                var session = await _context.SesionesCalendario
                    .Include(s => s.SesionClientes)
                    .Include(s => s.SesionUsuarios)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (session == null)
                    return NotFound("Session not found");

                session.Titulo = dto.Titulo;
                session.EstadoSesion = dto.EstadoSesion;
                session.FechaHoraInicio = dto.FechaHoraInicio;
                session.FechaHoraFin = dto.FechaHoraFin;
                session.Descripcion = dto.Descripcion;
                session.UbicacionOEnlace = dto.Ubicacion;
                session.CoachId = dto.CoachId ?? session.CoachId;
                session.UpdatedAt = DateTime.UtcNow;

                // Update clients
                _context.SesionesClientes.RemoveRange(session.SesionClientes);
                
                if (dto.ClientIds != null && dto.ClientIds.Any())
                {
                    foreach (var clientId in dto.ClientIds)
                    {
                        _context.SesionesClientes.Add(new SesionCliente
                        {
                            SesionCalendarioId = session.Id,
                            ClienteId = clientId
                        });
                    }
                }
                
                // Update users
                _context.SesionesUsuarios.RemoveRange(session.SesionUsuarios);
                
                if (dto.UserIds != null && dto.UserIds.Any())
                {
                    foreach (var userId in dto.UserIds)
                    {
                        _context.SesionesUsuarios.Add(new SesionUsuario
                        {
                            SesionCalendarioId = session.Id,
                            UsuarioId = userId
                        });
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(new { message = "Session updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating calendar session");
                return StatusCode(500, "Error updating session");
            }
        }

        [HttpDelete("sesionescalendario/{id}")]
        public async Task<IActionResult> DeleteCalendarSession(int id)
        {
            try
            {
                var session = await _context.SesionesCalendario.FindAsync(id);
                if (session == null)
                    return NotFound("Session not found");

                _context.SesionesCalendario.Remove(session);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Session deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting calendar session");
                return StatusCode(500, "Error deleting session");
            }
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }

            // For development - return test user while login is not implemented
            // TODO: Remove this when real authentication is implemented
            return 1; // Test user ID
        }
    }

    public class CreateSessionDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string EstadoSesion { get; set; } = "Programada";
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public string? Descripcion { get; set; }
        public string? Ubicacion { get; set; }
        public int? CoachId { get; set; }
        public List<int>? ClientIds { get; set; } // Mantener para compatibilidad
        public List<int>? UserIds { get; set; } // Nueva propiedad para usuarios
        public string TipoSesion { get; set; } = "Individual";
    }
}