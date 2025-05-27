using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    [Route("api")]
    [ApiController]
    public class CalendarioApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CalendarioApiController> _logger;

        public CalendarioApiController(ApplicationDbContext context, ILogger<CalendarioApiController> logger)
        {
            _context = context;
            _logger = logger;
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
                    .AsQueryable();

                if (start.HasValue)
                    query = query.Where(s => s.FechaHoraFin >= start.Value);
                    
                if (end.HasValue)
                    query = query.Where(s => s.FechaHoraInicio <= end.Value);

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
                        })
                    })
                    .ToListAsync();

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

                // Add clients
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
            // TODO: Get from authentication context
            // For now, return hardcoded value
            return 1;
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
        public List<int>? ClientIds { get; set; }
        public string TipoSesion { get; set; } = "Individual";
    }
}