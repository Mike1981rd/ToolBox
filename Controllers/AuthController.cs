using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToolBox.Data;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ApplicationDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Muestra la página de login
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            // Si ya está autenticado, redirigir al dashboard
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Procesa el login del usuario
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;
                return View(model);
            }

            try
            {
                // Buscar usuario por email O username y verificar que esté activo
                var emailOrUsername = model.EmailOrUsername.ToLower().Trim();
                
                _logger.LogInformation("Intento de login con: {EmailOrUsername}", emailOrUsername);
                
                var user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.IsActive && 
                        (u.Email.ToLower() == emailOrUsername || 
                         (u.UserName != null && u.UserName.ToLower() == emailOrUsername)));

                if (user == null)
                {
                    _logger.LogWarning("Usuario no encontrado o inactivo para: {EmailOrUsername}", emailOrUsername);
                    
                    // Verificar si existe pero está inactivo
                    var inactiveUser = await _context.Users
                        .FirstOrDefaultAsync(u => 
                            u.Email.ToLower() == emailOrUsername || 
                            (u.UserName != null && u.UserName.ToLower() == emailOrUsername));
                    
                    if (inactiveUser != null && !inactiveUser.IsActive)
                    {
                        ModelState.AddModelError(string.Empty, "Tu cuenta está inactiva. Contacta al administrador.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
                    }
                    
                    ViewData["ReturnUrl"] = returnUrl;
                    return View(model);
                }
                
                _logger.LogInformation("Usuario encontrado: ID={UserId}, Email={Email}, UserName={UserName}", 
                    user.Id, user.Email, user.UserName);

                // TODO: Verificar password hash cuando se implemente
                // Por ahora, validación temporal para desarrollo
                bool isValidPassword = ValidatePassword(model.Password, user.PasswordHash);

                if (!isValidPassword)
                {
                    _logger.LogWarning("Password inválido para usuario: {UserId} ({Email})", user.Id, user.Email);
                    ModelState.AddModelError(string.Empty, "Contraseña incorrecta.");
                    ViewData["ReturnUrl"] = returnUrl;
                    return View(model);
                }

                // Crear claims para el usuario
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role?.Name ?? "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(24)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                _logger.LogInformation("Usuario {UserId} ({Email}) ha iniciado sesión", user.Id, user.Email);

                // Redirigir al returnUrl o al dashboard
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Dashboard", "Admin");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el login del usuario {EmailOrUsername}", model.EmailOrUsername);
                ModelState.AddModelError(string.Empty, "Ocurrió un error durante el login. Intente nuevamente.");
                ViewData["ReturnUrl"] = returnUrl;
                return View(model);
            }
        }

        /// <summary>
        /// Procesa el logout del usuario
        /// </summary>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _logger.LogInformation("Usuario {UserId} ha cerrado sesión", userId);

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el logout");
                return RedirectToAction("Login", "Auth");
            }
        }

        /// <summary>
        /// Página de acceso denegado
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// Endpoint de prueba para verificar usuarios (REMOVER EN PRODUCCIÓN)
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> TestUsers(string? search = null)
        {
            var users = await _context.Users.ToListAsync();
            
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower().Trim();
                var foundUser = users.FirstOrDefault(u => 
                    u.Email.ToLower() == search || 
                    (u.UserName != null && u.UserName.ToLower() == search));
                    
                return Json(new { 
                    searchTerm = search,
                    found = foundUser != null,
                    user = foundUser != null ? new {
                        id = foundUser.Id,
                        email = foundUser.Email,
                        userName = foundUser.UserName,
                        fullName = foundUser.FullName,
                        isActive = foundUser.IsActive
                    } : null
                });
            }
            
            return Json(users.Select(u => new {
                id = u.Id,
                email = u.Email,
                userName = u.UserName,
                fullName = u.FullName,
                isActive = u.IsActive
            }));
        }

        /// <summary>
        /// Validación temporal de password para desarrollo
        /// TODO: Reemplazar con hash real (BCrypt, Argon2, etc.)
        /// </summary>
        private bool ValidatePassword(string inputPassword, string storedPassword)
        {
            if (string.IsNullOrEmpty(inputPassword) || string.IsNullOrEmpty(storedPassword))
            {
                return false;
            }
            
            // Si el password está hasheado con BCrypt
            if (storedPassword.StartsWith("$2"))
            {
                try
                {
                    return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al verificar password con BCrypt");
                    return false;
                }
            }
            
            // Para desarrollo - aceptar "temp" o comparación directa
            if (storedPassword == "temp" || storedPassword.StartsWith("hashed"))
            {
                return true;
            }
            
            // Comparación directa para passwords en texto plano (solo desarrollo)
            return inputPassword == storedPassword;
        }
    }
}