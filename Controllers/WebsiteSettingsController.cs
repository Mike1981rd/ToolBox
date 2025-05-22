using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class WebsiteSettingsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.BreadcrumbActiveKey = "breadcrumb_website_settings";
            ViewBag.HideTitleInLayout = true;

            // Load current settings (placeholder - in real implementation, load from database)
            var model = GetCurrentSettings();
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSettings(WebsiteSettingsViewModel model, IFormFile? appLogo)
        {
            try
            {
                // Validate model
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Please correct the validation errors" });
                }

                // Process logo upload if provided
                if (appLogo != null && appLogo.Length > 0)
                {
                    var logoPath = await ProcessLogoUpload(appLogo);
                    if (!string.IsNullOrEmpty(logoPath))
                    {
                        model.CurrentLogoPath = logoPath;
                    }
                }

                // Save settings (placeholder - in real implementation, save to database)
                var result = await SaveSettingsToDatabase(model);

                if (result)
                {
                    return Json(new { 
                        success = true, 
                        message = "Settings saved successfully",
                        logoPath = model.CurrentLogoPath
                    });
                }
                else
                {
                    return Json(new { success = false, message = "Error saving settings" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
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
        public IActionResult GetSettings()
        {
            try
            {
                var settings = GetCurrentSettings();
                return Json(new { success = true, data = settings });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error retrieving settings: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult ResetLogo()
        {
            try
            {
                // Reset to default logo (placeholder)
                var defaultLogoPath = "/img/toolbox-logo.svg";
                
                return Json(new { 
                    success = true, 
                    message = "Logo reset to default",
                    logoPath = defaultLogoPath
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error resetting logo: {ex.Message}" });
            }
        }

        #region Private Methods

        private WebsiteSettingsViewModel GetCurrentSettings()
        {
            // Placeholder - in real implementation, load from database or configuration
            return new WebsiteSettingsViewModel
            {
                SiteEmail = "info@toolbox.com",
                SiteAddress = "123 Business Street, City, State 12345",
                SitePhone = "+1 (555) 123-4567",
                FooterMessage = "© 2024 ToolBox. All rights reserved. Empowering your journey to success.",
                SiteName = "ToolBox Admin Dashboard",
                SiteDescription = "Comprehensive coaching and life management platform",
                CurrentLogoPath = "/img/toolbox-logo.svg",
                TimeZone = "UTC-5",
                DefaultLanguage = "en",
                
                // Sample social media links
                FacebookUrl = "https://facebook.com/toolbox",
                TwitterUrl = "https://twitter.com/toolbox",
                LinkedInUrl = "https://linkedin.com/company/toolbox",
                InstagramUrl = "https://instagram.com/toolbox",
                YouTubeUrl = "https://youtube.com/c/toolbox",
                
                LastUpdated = DateTime.Now.AddDays(-7),
                UpdatedBy = "Admin User"
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

                // Generate unique filename
                var fileName = $"logo_{Guid.NewGuid()}{fileExtension}";
                var uploadPath = Path.Combine("wwwroot", "img", "branding");
                var fullPath = Path.Combine(uploadPath, fileName);

                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Simulate file save (in real implementation, save the actual file)
                await Task.Delay(100); // Simulate async operation
                
                // Return the web path
                return $"/img/branding/{fileName}";
            }
            catch (Exception ex)
            {
                // Log error in real implementation
                Console.WriteLine($"Error processing logo upload: {ex.Message}");
                return null;
            }
        }

        private async Task<bool> SaveSettingsToDatabase(WebsiteSettingsViewModel model)
        {
            try
            {
                // Placeholder - in real implementation, save to database
                await Task.Delay(100); // Simulate async operation
                
                // Update model properties
                model.LastUpdated = DateTime.Now;
                model.UpdatedBy = "Current User"; // In real implementation, get from authenticated user
                
                // Simulate successful save
                return true;
            }
            catch (Exception ex)
            {
                // Log error in real implementation
                Console.WriteLine($"Error saving settings: {ex.Message}");
                return false;
            }
        }

        #endregion
    }
}