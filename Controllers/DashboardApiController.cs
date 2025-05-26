using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToolBox.Data;
using Microsoft.AspNetCore.Authorization;

namespace ToolBox.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/dashboard/stats/total-clientes
        [HttpGet("stats/total-clientes")]
        public async Task<IActionResult> GetTotalClientes()
        {
            try
            {
                var totalClientes = await _context.Customers.CountAsync();
                return Ok(new { total = totalClientes });
            }
            catch
            {
                return StatusCode(500, new { error = "Error al obtener el total de clientes" });
            }
        }

        // GET: api/dashboard/stats/clientes-activos
        [HttpGet("stats/clientes-activos")]
        public async Task<IActionResult> GetClientesActivos()
        {
            try
            {
                var clientesActivos = await _context.Customers
                    .Where(c => c.IsActive)
                    .CountAsync();
                return Ok(new { total = clientesActivos });
            }
            catch
            {
                return StatusCode(500, new { error = "Error al obtener clientes activos" });
            }
        }

        // GET: api/dashboard/welcome-message-config
        [HttpGet("welcome-message-config")]
        public async Task<IActionResult> GetWelcomeMessageConfig()
        {
            try
            {
                var config = await _context.WelcomeMessageSettings.FirstOrDefaultAsync();
                
                if (config == null)
                {
                    return Ok(new 
                    { 
                        title = "Bienvenido",
                        htmlContent = "<p>Bienvenido al dashboard</p>",
                        videoType = "None",
                        videoUrl = ""
                    });
                }

                return Ok(new
                {
                    title = config.Title,
                    htmlContent = config.DescriptionHTML,
                    videoType = config.VideoType,
                    videoUrl = config.VideoUrl ?? config.UploadedVideoPath
                });
            }
            catch
            {
                return StatusCode(500, new { error = "Error al obtener configuración del mensaje de bienvenida" });
            }
        }
    }
}