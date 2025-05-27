using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Services
{
    public class SesionCalendarioService : ISesionCalendarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SesionCalendarioService> _logger;

        public SesionCalendarioService(ApplicationDbContext context, ILogger<SesionCalendarioService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(bool Success, string Message, int? SesionId)> CrearSesionAsync(int coachId, CrearSesionRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Validaciones
                if (request.FechaHoraFin <= request.FechaHoraInicio)
                {
                    return (false, "La fecha/hora de fin debe ser posterior a la fecha/hora de inicio", null);
                }

                if (!request.ClienteIds.Any())
                {
                    return (false, "Debe seleccionar al menos un cliente", null);
                }

                // Verificar conflicto de horario
                if (await ExisteConflictoHorarioAsync(coachId, request.FechaHoraInicio, request.FechaHoraFin))
                {
                    return (false, "Ya existe una sesión programada en ese horario", null);
                }

                // Verificar que todos los clientes existan y estén activos
                var clientesValidos = await _context.Customers
                    .Where(c => request.ClienteIds.Contains(c.Id) && c.IsActive)
                    .Select(c => c.Id)
                    .ToListAsync();

                if (clientesValidos.Count != request.ClienteIds.Count)
                {
                    return (false, "Uno o más clientes seleccionados no son válidos o están inactivos", null);
                }

                // Crear la sesión
                var sesion = new SesionCalendario
                {
                    CoachId = coachId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaHoraInicio = request.FechaHoraInicio.ToUniversalTime(),
                    FechaHoraFin = request.FechaHoraFin.ToUniversalTime(),
                    UbicacionOEnlace = request.UbicacionOEnlace,
                    EstadoSesion = EstadoSesionCalendario.Programada,
                    CreatedAt = DateTime.UtcNow
                };

                _context.SesionesCalendario.Add(sesion);
                await _context.SaveChangesAsync();

                // Crear las relaciones con los clientes
                foreach (var clienteId in clientesValidos)
                {
                    var sesionCliente = new SesionCliente
                    {
                        SesionCalendarioId = sesion.Id,
                        ClienteId = clienteId
                    };
                    _context.SesionesClientes.Add(sesionCliente);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Sesión creada exitosamente. Id: {SesionId}, Coach: {CoachId}", sesion.Id, coachId);
                return (true, "Sesión creada exitosamente", sesion.Id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error al crear sesión para coach {CoachId}", coachId);
                return (false, "Error al crear la sesión", null);
            }
        }

        public async Task<SesionesListResponse> ObtenerSesionesAsync(FiltroSesionesRequest filtro)
        {
            try
            {
                var query = _context.SesionesCalendario
                    .Include(s => s.Coach)
                    .Include(s => s.SesionClientes)
                        .ThenInclude(sc => sc.Cliente)
                    .AsQueryable();

                // Aplicar filtros
                if (filtro.CoachId.HasValue)
                {
                    query = query.Where(s => s.CoachId == filtro.CoachId.Value);
                }

                if (filtro.ClienteId.HasValue)
                {
                    query = query.Where(s => s.SesionClientes.Any(sc => sc.ClienteId == filtro.ClienteId.Value));
                }

                if (filtro.FechaInicio.HasValue)
                {
                    var fechaInicioUtc = filtro.FechaInicio.Value.ToUniversalTime();
                    query = query.Where(s => s.FechaHoraInicio >= fechaInicioUtc);
                }

                if (filtro.FechaFin.HasValue)
                {
                    var fechaFinUtc = filtro.FechaFin.Value.ToUniversalTime();
                    query = query.Where(s => s.FechaHoraInicio <= fechaFinUtc);
                }

                if (!string.IsNullOrEmpty(filtro.EstadoSesion))
                {
                    query = query.Where(s => s.EstadoSesion == filtro.EstadoSesion);
                }

                // Obtener total de registros
                var totalCount = await query.CountAsync();

                // Ordenar y paginar
                var sesiones = await query
                    .OrderBy(s => s.FechaHoraInicio)
                    .Skip((filtro.Page - 1) * filtro.PageSize)
                    .Take(filtro.PageSize)
                    .Select(s => new SesionResponse
                    {
                        Id = s.Id,
                        CoachId = s.CoachId,
                        CoachNombre = s.Coach.FullName,
                        Titulo = s.Titulo,
                        Descripcion = s.Descripcion,
                        FechaHoraInicio = s.FechaHoraInicio,
                        FechaHoraFin = s.FechaHoraFin,
                        UbicacionOEnlace = s.UbicacionOEnlace,
                        EstadoSesion = s.EstadoSesion,
                        Clientes = s.SesionClientes.Select(sc => new ClienteSesionResponse
                        {
                            ClienteId = sc.ClienteId,
                            NombreCompleto = sc.Cliente.FirstName + " " + sc.Cliente.LastName,
                            Email = sc.Cliente.Email,
                            FechaConfirmacion = sc.FechaConfirmacion,
                            Asistio = sc.Asistio,
                            Notas = sc.Notas
                        }).ToList(),
                        CreatedAt = s.CreatedAt,
                        UpdatedAt = s.UpdatedAt
                    })
                    .ToListAsync();

                return new SesionesListResponse
                {
                    Sesiones = sesiones,
                    TotalCount = totalCount,
                    CurrentPage = filtro.Page,
                    PageSize = filtro.PageSize,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)filtro.PageSize)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sesiones con filtro");
                return new SesionesListResponse
                {
                    Sesiones = new List<SesionResponse>(),
                    TotalCount = 0,
                    CurrentPage = 1,
                    PageSize = filtro.PageSize,
                    TotalPages = 0
                };
            }
        }

        public async Task<SesionResponse?> ObtenerSesionPorIdAsync(int sesionId)
        {
            try
            {
                var sesion = await _context.SesionesCalendario
                    .Include(s => s.Coach)
                    .Include(s => s.SesionClientes)
                        .ThenInclude(sc => sc.Cliente)
                    .Where(s => s.Id == sesionId)
                    .Select(s => new SesionResponse
                    {
                        Id = s.Id,
                        CoachId = s.CoachId,
                        CoachNombre = s.Coach.FullName,
                        Titulo = s.Titulo,
                        Descripcion = s.Descripcion,
                        FechaHoraInicio = s.FechaHoraInicio,
                        FechaHoraFin = s.FechaHoraFin,
                        UbicacionOEnlace = s.UbicacionOEnlace,
                        EstadoSesion = s.EstadoSesion,
                        Clientes = s.SesionClientes.Select(sc => new ClienteSesionResponse
                        {
                            ClienteId = sc.ClienteId,
                            NombreCompleto = sc.Cliente.FirstName + " " + sc.Cliente.LastName,
                            Email = sc.Cliente.Email,
                            FechaConfirmacion = sc.FechaConfirmacion,
                            Asistio = sc.Asistio,
                            Notas = sc.Notas
                        }).ToList(),
                        CreatedAt = s.CreatedAt,
                        UpdatedAt = s.UpdatedAt
                    })
                    .FirstOrDefaultAsync();

                return sesion;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sesión con Id {SesionId}", sesionId);
                return null;
            }
        }

        public async Task<List<ClienteSimpleResponse>> ObtenerClientesDelCoachAsync(int coachId)
        {
            try
            {
                // Por ahora, retornamos todos los clientes activos
                // En el futuro, esto podría filtrar por clientes asignados al coach
                var clientes = await _context.Customers
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.FirstName).ThenBy(c => c.LastName)
                    .Select(c => new ClienteSimpleResponse
                    {
                        Id = c.Id,
                        NombreCompleto = c.FirstName + " " + c.LastName,
                        Email = c.Email,
                        IsActive = c.IsActive
                    })
                    .ToListAsync();

                return clientes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener clientes del coach {CoachId}", coachId);
                return new List<ClienteSimpleResponse>();
            }
        }

        public async Task<(bool Success, string Message)> ActualizarSesionAsync(int sesionId, int coachId, ActualizarSesionRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var sesion = await _context.SesionesCalendario
                    .Include(s => s.SesionClientes)
                    .FirstOrDefaultAsync(s => s.Id == sesionId);

                if (sesion == null)
                {
                    return (false, "Sesión no encontrada");
                }

                if (sesion.CoachId != coachId)
                {
                    return (false, "No tiene permisos para editar esta sesión");
                }

                // Validaciones
                if (request.FechaHoraFin <= request.FechaHoraInicio)
                {
                    return (false, "La fecha/hora de fin debe ser posterior a la fecha/hora de inicio");
                }

                if (!request.ClienteIds.Any())
                {
                    return (false, "Debe seleccionar al menos un cliente");
                }

                // Verificar conflicto de horario (excluyendo la sesión actual)
                if (await ExisteConflictoHorarioAsync(coachId, request.FechaHoraInicio, request.FechaHoraFin, sesionId))
                {
                    return (false, "Ya existe una sesión programada en ese horario");
                }

                // Actualizar datos de la sesión
                sesion.Titulo = request.Titulo;
                sesion.Descripcion = request.Descripcion;
                sesion.FechaHoraInicio = request.FechaHoraInicio.ToUniversalTime();
                sesion.FechaHoraFin = request.FechaHoraFin.ToUniversalTime();
                sesion.UbicacionOEnlace = request.UbicacionOEnlace;
                sesion.UpdatedAt = DateTime.UtcNow;

                // Actualizar clientes
                // Primero eliminar todas las relaciones existentes
                _context.SesionesClientes.RemoveRange(sesion.SesionClientes);

                // Verificar que todos los clientes nuevos existan y estén activos
                var clientesValidos = await _context.Customers
                    .Where(c => request.ClienteIds.Contains(c.Id) && c.IsActive)
                    .Select(c => c.Id)
                    .ToListAsync();

                if (clientesValidos.Count != request.ClienteIds.Count)
                {
                    return (false, "Uno o más clientes seleccionados no son válidos o están inactivos");
                }

                // Crear las nuevas relaciones
                foreach (var clienteId in clientesValidos)
                {
                    var sesionCliente = new SesionCliente
                    {
                        SesionCalendarioId = sesion.Id,
                        ClienteId = clienteId
                    };
                    _context.SesionesClientes.Add(sesionCliente);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Sesión actualizada exitosamente. Id: {SesionId}", sesionId);
                return (true, "Sesión actualizada exitosamente");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error al actualizar sesión {SesionId}", sesionId);
                return (false, "Error al actualizar la sesión");
            }
        }

        public async Task<(bool Success, string Message)> CambiarEstadoSesionAsync(int sesionId, int coachId, string nuevoEstado)
        {
            try
            {
                var estadosValidos = new[] { 
                    EstadoSesionCalendario.Programada, 
                    EstadoSesionCalendario.Confirmada, 
                    EstadoSesionCalendario.Cancelada, 
                    EstadoSesionCalendario.Completada,
                    EstadoSesionCalendario.EnProgreso
                };

                if (!estadosValidos.Contains(nuevoEstado))
                {
                    return (false, "Estado no válido");
                }

                var sesion = await _context.SesionesCalendario
                    .FirstOrDefaultAsync(s => s.Id == sesionId);

                if (sesion == null)
                {
                    return (false, "Sesión no encontrada");
                }

                if (sesion.CoachId != coachId)
                {
                    return (false, "No tiene permisos para modificar esta sesión");
                }

                sesion.EstadoSesion = nuevoEstado;
                sesion.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Estado de sesión actualizado. Id: {SesionId}, Nuevo estado: {Estado}", sesionId, nuevoEstado);
                return (true, "Estado actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar estado de sesión {SesionId}", sesionId);
                return (false, "Error al cambiar el estado de la sesión");
            }
        }

        public async Task<(bool Success, string Message)> EliminarSesionAsync(int sesionId, int coachId)
        {
            try
            {
                var sesion = await _context.SesionesCalendario
                    .FirstOrDefaultAsync(s => s.Id == sesionId);

                if (sesion == null)
                {
                    return (false, "Sesión no encontrada");
                }

                if (sesion.CoachId != coachId)
                {
                    return (false, "No tiene permisos para eliminar esta sesión");
                }

                // No permitir eliminar sesiones completadas
                if (sesion.EstadoSesion == EstadoSesionCalendario.Completada)
                {
                    return (false, "No se pueden eliminar sesiones completadas");
                }

                _context.SesionesCalendario.Remove(sesion);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Sesión eliminada. Id: {SesionId}", sesionId);
                return (true, "Sesión eliminada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar sesión {SesionId}", sesionId);
                return (false, "Error al eliminar la sesión");
            }
        }

        public async Task<bool> ExisteConflictoHorarioAsync(int coachId, DateTime fechaInicio, DateTime fechaFin, int? sesionIdExcluir = null)
        {
            var fechaInicioUtc = fechaInicio.ToUniversalTime();
            var fechaFinUtc = fechaFin.ToUniversalTime();

            var query = _context.SesionesCalendario
                .Where(s => s.CoachId == coachId && 
                           s.EstadoSesion != EstadoSesionCalendario.Cancelada &&
                           s.EstadoSesion != EstadoSesionCalendario.Completada);

            if (sesionIdExcluir.HasValue)
            {
                query = query.Where(s => s.Id != sesionIdExcluir.Value);
            }

            // Verificar si hay conflicto de horario
            var conflicto = await query.AnyAsync(s =>
                (fechaInicioUtc >= s.FechaHoraInicio && fechaInicioUtc < s.FechaHoraFin) ||
                (fechaFinUtc > s.FechaHoraInicio && fechaFinUtc <= s.FechaHoraFin) ||
                (fechaInicioUtc <= s.FechaHoraInicio && fechaFinUtc >= s.FechaHoraFin)
            );

            return conflicto;
        }

        public async Task<bool> PuedeCoachModificarSesionAsync(int coachId, int sesionId)
        {
            return await _context.SesionesCalendario
                .AnyAsync(s => s.Id == sesionId && s.CoachId == coachId);
        }
    }
}