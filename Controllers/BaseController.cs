using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using ToolBox.Interfaces;

namespace ToolBox.Controllers
{
    public abstract class BaseController : Controller
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Cargar permisos del usuario para el sidebar
            if (User.Identity?.IsAuthenticated == true)
            {
                try
                {
                    var permissionService = HttpContext.RequestServices.GetService<IPermissionService>();
                    if (permissionService != null)
                    {
                        var userId = GetCurrentUserId();
                        var permissions = await permissionService.GetUserPermissionsAsync(userId);
                        var readableModules = await permissionService.GetUserReadableModulesAsync(userId);
                        
                        ViewBag.UserPermissions = permissions;
                        ViewBag.UserReadableModules = readableModules;
                    }
                    
                    // Cargar logo actual del sitio
                    var configService = HttpContext.RequestServices.GetService<IWebsiteConfigurationService>();
                    if (configService != null)
                    {
                        try
                        {
                            var config = await configService.GetConfigurationAsync();
                            ViewBag.SiteLogo = config.LogoPath ?? "/img/toolbox-logo.svg";
                        }
                        catch
                        {
                            ViewBag.SiteLogo = "/img/toolbox-logo.svg";
                        }
                    }
                    else
                    {
                        ViewBag.UserPermissions = new Dictionary<string, List<string>>();
                        ViewBag.UserReadableModules = new List<string>();
                        ViewBag.SiteLogo = "/img/toolbox-logo.svg";
                    }
                }
                catch (Exception ex)
                {
                    // Log error pero no interrumpir la ejecución
                    ViewBag.UserPermissions = new Dictionary<string, List<string>>();
                    ViewBag.UserReadableModules = new List<string>();
                    ViewBag.SiteLogo = "/img/toolbox-logo.svg";
                }
            }
            else
            {
                ViewBag.UserPermissions = new Dictionary<string, List<string>>();
                ViewBag.UserReadableModules = new List<string>();
                ViewBag.SiteLogo = "/img/toolbox-logo.svg";
            }

            await next();
        }
        /// <summary>
        /// Obtiene el ID del usuario actualmente autenticado
        /// </summary>
        /// <returns>ID del usuario actual</returns>
        /// <exception cref="UnauthorizedAccessException">Cuando el usuario no está autenticado</exception>
        protected int GetCurrentUserId()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    return userId;
                }
            }
            // Para desarrollo - retornar usuario de prueba mientras no hay login implementado
            // TODO: Cambiar esto cuando se implemente el login real
            return 1; 
            
            // Para producción - descomentar la línea siguiente:
            // throw new UnauthorizedAccessException("Usuario no autenticado");
        }
        
        /// <summary>
        /// Obtiene el nombre del usuario actualmente autenticado
        /// </summary>
        /// <returns>Nombre del usuario actual</returns>
        protected string GetCurrentUserName()
        {
            return User.Identity?.Name ?? "Usuario Anónimo";
        }
        
        /// <summary>
        /// Verifica si el usuario está autenticado
        /// </summary>
        /// <returns>True si está autenticado, false en caso contrario</returns>
        protected bool IsUserAuthenticated()
        {
            return User.Identity?.IsAuthenticated == true;
        }
        
        /// <summary>
        /// Obtiene el email del usuario actual desde los claims
        /// </summary>
        /// <returns>Email del usuario actual</returns>
        protected string? GetCurrentUserEmail()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value;
        }
        
        /// <summary>
        /// Obtiene el rol del usuario actual desde los claims
        /// </summary>
        /// <returns>Rol del usuario actual</returns>
        protected string? GetCurrentUserRole()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}