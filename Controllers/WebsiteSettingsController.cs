using Microsoft.AspNetCore.Mvc;
using System.IO;
using ToolBox.Models;
using ToolBox.Interfaces;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace ToolBox.Controllers
{
    public class WebsiteSettingsController : BaseController
    {
        private readonly IWebsiteConfigurationService _configService;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<WebsiteSettingsController> _logger;

        public WebsiteSettingsController(
            IWebsiteConfigurationService configService,
            IWebHostEnvironment environment,
            ILogger<WebsiteSettingsController> logger)
        {
            _configService = configService;
            _environment = environment;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.BreadcrumbActiveKey = "breadcrumb_website_settings";
            ViewBag.HideTitleInLayout = true;

            // Load current settings from database
            var config = await _configService.GetConfigurationAsync();
            var model = MapToViewModel(config);
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSettings(WebsiteSettingsViewModel model, IFormFile? appLogo)
        {
            try
            {
                // Process logo upload if provided
                if (appLogo != null && appLogo.Length > 0)
                {
                    var logoPath = await ProcessLogoUpload(appLogo);
                    if (!string.IsNullOrEmpty(logoPath))
                    {
                        model.CurrentLogoPath = logoPath;
                    }
                }

                // Map to entity and save
                var config = MapToEntity(model);
                config.LastUpdatedByUserId = GetCurrentUserId();
                
                var updatedConfig = await _configService.UpdateConfigurationAsync(config);

                // Update logo path if changed
                if (!string.IsNullOrEmpty(model.CurrentLogoPath) && model.CurrentLogoPath != updatedConfig.LogoPath)
                {
                    await _configService.UpdateLogoAsync(model.CurrentLogoPath);
                }

                return Json(new { 
                    success = true, 
                    message = "Configuración guardada exitosamente",
                    logoPath = model.CurrentLogoPath,
                    lastUpdated = updatedConfig.LastUpdated.ToString("dd/MM/yyyy HH:mm")
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving website settings");
                return Json(new { success = false, message = "Error al guardar la configuración" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadLogo(IFormFile logoFile)
        {
            try
            {
                if (logoFile == null || logoFile.Length == 0)
                {
                    return Json(new { success = false, message = "No file selected" });
                }

                var logoPath = await ProcessLogoUpload(logoFile);
                
                if (!string.IsNullOrEmpty(logoPath))
                {
                    return Json(new { 
                        success = true, 
                        message = "Logo uploaded successfully",
                        logoPath = logoPath
                    });
                }
                else
                {
                    return Json(new { success = false, message = "Error uploading logo" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error uploading logo: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSettings()
        {
            try
            {
                var config = await _configService.GetConfigurationAsync();
                var settings = MapToViewModel(config);
                return Json(new { success = true, data = settings });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving settings");
                return Json(new { success = false, message = "Error al obtener la configuración" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetLogo()
        {
            try
            {
                await _configService.ResetLogoAsync();
                var defaultLogoPath = "/img/toolbox-logo.svg";
                
                return Json(new { 
                    success = true, 
                    message = "Logo restablecido a predeterminado",
                    logoPath = defaultLogoPath
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting logo");
                return Json(new { success = false, message = "Error al restablecer el logo" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDefaultLanguage([FromBody] SaveDefaultLanguageRequest request)
        {
            try
            {
                _logger.LogInformation("SaveDefaultLanguage called with Language: {Language}", request?.Language);
                
                if (request == null || string.IsNullOrEmpty(request.Language))
                {
                    _logger.LogWarning("Invalid request: Language is null or empty");
                    return Json(new { success = false, message = "Invalid language parameter" });
                }
                
                var userId = GetCurrentUserId();
                _logger.LogInformation("Current user ID: {UserId}", userId);
                
                var success = await _configService.UpdateUserDefaultLanguageAsync(userId, request.Language);
                
                if (success)
                {
                    _logger.LogInformation("Successfully saved default language {Language} for user {UserId}", request.Language, userId);
                    return Json(new { success = true, message = "Idioma predeterminado guardado correctamente" });
                }
                else
                {
                    _logger.LogWarning("Failed to save default language {Language} for user {UserId}", request.Language, userId);
                    return Json(new { success = false, message = "Error al guardar el idioma predeterminado" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in SaveDefaultLanguage for user {UserId}", GetCurrentUserId());
                return Json(new { success = false, message = "Error al guardar el idioma predeterminado" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDefaultLanguage()
        {
            try
            {
                var userId = GetCurrentUserId();
                var language = await _configService.GetUserDefaultLanguageAsync(userId);
                return Json(new { success = true, language = language ?? "es" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting default language for user {UserId}", GetCurrentUserId());
                return Json(new { success = true, language = "es" }); // Default fallback
            }
        }

        #region Private Methods

        private WebsiteSettingsViewModel MapToViewModel(WebsiteConfiguration config)
        {
            return new WebsiteSettingsViewModel
            {
                SiteEmail = config.SiteEmail ?? "",
                SiteAddress = config.SiteAddress ?? "",
                SitePhone = config.SitePhone ?? "",
                FooterMessage = config.FooterMessage ?? "",
                SiteName = config.SiteName ?? "",
                SiteDescription = config.SiteDescription ?? "",
                CurrentLogoPath = config.LogoPath ?? "/img/toolbox-logo.svg",
                TimeZone = config.DefaultTimeZone ?? "America/Mexico_City",
                DefaultLanguage = config.DefaultLanguage ?? "es",
                
                // Social media links
                FacebookUrl = config.FacebookUrl ?? "",
                TwitterUrl = config.TwitterUrl ?? "",
                GoogleUrl = config.GoogleUrl ?? "",
                LinkedInUrl = config.LinkedInUrl ?? "",
                InstagramUrl = config.InstagramUrl ?? "",
                YouTubeUrl = config.YouTubeUrl ?? "",
                TelegramUrl = config.TelegramUrl ?? "",
                TikTokUrl = config.TikTokUrl ?? "",
                DiscordUrl = config.DiscordUrl ?? "",
                RedditUrl = config.RedditUrl ?? "",
                
                LastUpdated = config.LastUpdated,
                UpdatedBy = config.LastUpdatedByUser?.FullName ?? "Sistema"
            };
        }

        private WebsiteConfiguration MapToEntity(WebsiteSettingsViewModel model)
        {
            return new WebsiteConfiguration
            {
                SiteEmail = model.SiteEmail,
                SiteAddress = model.SiteAddress,
                SitePhone = model.SitePhone,
                FooterMessage = model.FooterMessage,
                SiteName = model.SiteName,
                SiteDescription = model.SiteDescription,
                LogoPath = model.CurrentLogoPath,
                DefaultTimeZone = model.TimeZone,
                DefaultLanguage = model.DefaultLanguage,
                
                // Social media links
                FacebookUrl = model.FacebookUrl,
                TwitterUrl = model.TwitterUrl,
                GoogleUrl = model.GoogleUrl,
                LinkedInUrl = model.LinkedInUrl,
                InstagramUrl = model.InstagramUrl,
                YouTubeUrl = model.YouTubeUrl,
                TelegramUrl = model.TelegramUrl,
                TikTokUrl = model.TikTokUrl,
                DiscordUrl = model.DiscordUrl,
                RedditUrl = model.RedditUrl
            };
        }

        private async Task<string?> ProcessLogoUpload(IFormFile logoFile)
        {
            try
            {
                // Validate file type
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".svg", ".gif" };
                var fileExtension = Path.GetExtension(logoFile.FileName).ToLowerInvariant();
                
                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new ArgumentException("Invalid file type. Please upload PNG, JPG, JPEG, SVG, or GIF files.");
                }

                // Validate file size (max 5MB)
                const long maxFileSize = 5 * 1024 * 1024; // 5MB
                if (logoFile.Length > maxFileSize)
                {
                    throw new ArgumentException("File size too large. Maximum allowed size is 5MB.");
                }

                // Create directory if it doesn't exist
                var uploadPath = Path.Combine(_environment.WebRootPath, "img", "branding");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Handle SVG files differently (no processing needed)
                if (fileExtension == ".svg")
                {
                    var svgFileName = $"logo_{Guid.NewGuid()}.svg";
                    var svgFullPath = Path.Combine(uploadPath, svgFileName);
                    
                    using (var stream = new FileStream(svgFullPath, FileMode.Create))
                    {
                        await logoFile.CopyToAsync(stream);
                    }
                    
                    return $"/img/branding/{svgFileName}";
                }

                // Process raster images (PNG, JPG, GIF) - Optimize for crisp display
                return await ProcessRasterLogo(logoFile, uploadPath, fileExtension);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing logo upload");
                return null;
            }
        }

        private async Task<string?> ProcessRasterLogo(IFormFile logoFile, string uploadPath, string fileExtension)
        {
            try
            {
                using var originalStream = logoFile.OpenReadStream();
                using var originalImage = Image.FromStream(originalStream);
                
                // Ultra high-resolution target for maximum sharpness
                // Target: 900x300 (3:1 ratio) - 3x resolution for ultra-crisp scaling
                const int targetWidth = 900;
                const int targetHeight = 300;
                
                // Calculate optimal dimensions maintaining aspect ratio
                var (newWidth, newHeight) = CalculateOptimalDimensions(
                    originalImage.Width, originalImage.Height, targetWidth, targetHeight);
                
                // Create optimized image
                using var optimizedImage = new Bitmap(targetWidth, targetHeight, PixelFormat.Format32bppArgb);
                using var graphics = Graphics.FromImage(optimizedImage);
                
                // Ultra-crisp graphics settings for maximum logo sharpness
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.CompositingMode = CompositingMode.SourceOver;
                
                // Additional optimizations for text and logo clarity
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                
                // Fill with transparent background
                graphics.Clear(Color.Transparent);
                
                // Calculate position to center the logo
                var x = (targetWidth - newWidth) / 2;
                var y = (targetHeight - newHeight) / 2;
                
                // Draw the resized image
                graphics.DrawImage(originalImage, x, y, newWidth, newHeight);
                
                // Save as PNG for best quality and transparency support
                var fileName = $"logo_{Guid.NewGuid()}.png";
                var fullPath = Path.Combine(uploadPath, fileName);
                
                // Save with high quality PNG settings
                var codec = ImageCodecInfo.GetImageDecoders()
                    .FirstOrDefault(c => c.FormatID == ImageFormat.Png.Guid);
                
                if (codec != null)
                {
                    var encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                    optimizedImage.Save(fullPath, codec, encoderParams);
                }
                else
                {
                    optimizedImage.Save(fullPath, ImageFormat.Png);
                }
                
                return $"/img/branding/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing raster logo");
                return null;
            }
        }
        
        private static (int width, int height) CalculateOptimalDimensions(
            int originalWidth, int originalHeight, int targetWidth, int targetHeight)
        {
            var targetRatio = (double)targetWidth / targetHeight;
            var originalRatio = (double)originalWidth / originalHeight;
            
            int newWidth, newHeight;
            
            if (originalRatio > targetRatio)
            {
                // Original is wider, fit by width
                newWidth = targetWidth;
                newHeight = (int)(targetWidth / originalRatio);
            }
            else
            {
                // Original is taller, fit by height
                newHeight = targetHeight;
                newWidth = (int)(targetHeight * originalRatio);
            }
            
            return (newWidth, newHeight);
        }


        #endregion
    }
}