using Microsoft.AspNetCore.Mvc;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    [ApiController]
    [Route("api/calendario")]
    public class SesionesCalendarioController : BaseController
    {
        private readonly ISesionCalendarioService _sesionCalendarioService;
        private readonly ILogger<SesionesCalendarioController> _logger;

        public SesionesCalendarioController(
            ISesionCalendarioService sesionCalendarioService,
            ILogger<SesionesCalendarioController> logger)
        {
            _sesionCalendarioService = sesionCalendarioService;
            _logger = logger;
        }

        /// <summary>
        /// Crear una nueva sesión de calendario
        /// </summary>
        [HttpPost("sesiones")]
        public async Task<IActionResult> CrearSesion([FromBody] CrearSesionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Datos inválidos", errors = ModelState });
                }

                var coachId = GetCurrentUserId();
                var result = await _sesionCalendarioService.CrearSesionAsync(coachId, request);

                if (result.Success)
                {
                    return Ok(new { 
                        success = true, 
                        message = result.Message, 
                        data = new { sesionId = result.SesionId } 
                    });
                }

                return BadRequest(new { success = false, message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear sesión");
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtener sesiones con filtros opcionales
        /// </summary>
        [HttpGet("sesiones")]
        public async Task<IActionResult> ObtenerSesiones([FromQuery] FiltroSesionesRequest filtro)
        {
            try
            {
                // Por defecto, mostrar solo las sesiones del coach actual
                if (!filtro.CoachId.HasValue)
                {
                    filtro.CoachId = GetCurrentUserId();
                }

                var result = await _sesionCalendarioService.ObtenerSesionesAsync(filtro);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sesiones");
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtener detalles de una sesión específica
        /// </summary>
        [HttpGet("sesiones/{id}")]
        public async Task<IActionResult> ObtenerSesion(int id)
        {
            try
            {
                var sesion = await _sesionCalendarioService.ObtenerSesionPorIdAsync(id);
                
                if (sesion == null)
                {
                    return NotFound(new { success = false, message = "Sesión no encontrada" });
                }

                // Verificar que el coach actual tenga acceso a esta sesión
                var coachId = GetCurrentUserId();
                if (sesion.CoachId != coachId)
                {
                    return Forbid();
                }

                return Ok(new { success = true, data = sesion });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sesión {SesionId}", id);
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Actualizar una sesión existente
        /// </summary>
        [HttpPut("sesiones/{id}")]
        public async Task<IActionResult> ActualizarSesion(int id, [FromBody] ActualizarSesionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Datos inválidos", errors = ModelState });
                }

                var coachId = GetCurrentUserId();
                var result = await _sesionCalendarioService.ActualizarSesionAsync(id, coachId, request);

                if (result.Success)
                {
                    return Ok(new { success = true, message = result.Message });
                }

                return BadRequest(new { success = false, message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar sesión {SesionId}", id);
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Cambiar el estado de una sesión
        /// </summary>
        [HttpPatch("sesiones/{id}/estado")]
        public async Task<IActionResult> CambiarEstadoSesion(int id, [FromBody] CambiarEstadoSesionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Datos inválidos", errors = ModelState });
                }

                var coachId = GetCurrentUserId();
                var result = await _sesionCalendarioService.CambiarEstadoSesionAsync(id, coachId, request.EstadoSesion);

                if (result.Success)
                {
                    return Ok(new { success = true, message = result.Message });
                }

                return BadRequest(new { success = false, message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar estado de sesión {SesionId}", id);
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Eliminar una sesión
        /// </summary>
        [HttpDelete("sesiones/{id}")]
        public async Task<IActionResult> EliminarSesion(int id)
        {
            try
            {
                var coachId = GetCurrentUserId();
                var result = await _sesionCalendarioService.EliminarSesionAsync(id, coachId);

                if (result.Success)
                {
                    return Ok(new { success = true, message = result.Message });
                }

                return BadRequest(new { success = false, message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar sesión {SesionId}", id);
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtener lista de clientes del coach para selección
        /// </summary>
        [HttpGet("mis-clientes")]
        public async Task<IActionResult> ObtenerMisClientes()
        {
            try
            {
                var coachId = GetCurrentUserId();
                var clientes = await _sesionCalendarioService.ObtenerClientesDelCoachAsync(coachId);
                
                return Ok(new { success = true, data = clientes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener clientes del coach");
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

    }
}