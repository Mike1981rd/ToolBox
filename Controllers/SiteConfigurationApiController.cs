using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolBox.Data;

namespace ToolBox.Controllers
{
    [ApiController]
    [Route("api/site-configuration")]
    public class SiteConfigurationApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SiteConfigurationApiController> _logger;

        public SiteConfigurationApiController(ApplicationDbContext context, ILogger<SiteConfigurationApiController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get site configuration for public pages (login, etc.)
        /// This endpoint is accessible without authentication
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetSiteConfiguration()
        {
            try
            {
                // Get the latest website configuration
                var config = await _context.WebsiteConfiguration
                    .OrderByDescending(w => w.LastUpdated)
                    .FirstOrDefaultAsync();

                if (config == null)
                {
                    // Return default configuration if none exists
                    return Ok(new
                    {
                        siteName = "ToolBox Admin",
                        logoUrl = "/img/logo.png",
                        welcomeMessage = "Bienvenido de vuelta! Por favor inicia sesión en tu cuenta.",
                        companyName = "ToolBox",
                        supportEmail = config?.SiteEmail ?? "support@toolbox.com"
                    });
                }

                // Return sanitized configuration (only public data)
                var response = new
                {
                    siteName = !string.IsNullOrEmpty(config.SiteName) ? config.SiteName : "ToolBox Admin",
                    logoUrl = !string.IsNullOrEmpty(config.LogoPath) ? config.LogoPath : "/img/logo.png",
                    welcomeMessage = $"Bienvenido a {config.SiteName ?? "ToolBox"}! Por favor inicia sesión en tu cuenta.",
                    companyName = config.SiteName ?? "ToolBox",
                    supportEmail = config.SiteEmail ?? "support@toolbox.com"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving site configuration");
                
                // Return default configuration on error
                return Ok(new
                {
                    siteName = "ToolBox Admin",
                    logoUrl = "/img/logo.png", 
                    welcomeMessage = "Bienvenido de vuelta! Por favor inicia sesión en tu cuenta.",
                    companyName = "ToolBox",
                    supportEmail = "support@toolbox.com"
                });
            }
        }

        /// <summary>
        /// Get basic site info for page titles, etc.
        /// </summary>
        [HttpGet("basic")]
        public async Task<IActionResult> GetBasicSiteInfo()
        {
            try
            {
                var config = await _context.WebsiteConfiguration
                    .OrderByDescending(w => w.LastUpdated)
                    .FirstOrDefaultAsync();

                var response = new
                {
                    siteName = config?.SiteName ?? "ToolBox Admin",
                    logoUrl = config?.LogoPath ?? "/img/logo.png"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving basic site info");
                
                return Ok(new
                {
                    siteName = "ToolBox Admin",
                    logoUrl = "/img/logo.png"
                });
            }
        }
    }
}