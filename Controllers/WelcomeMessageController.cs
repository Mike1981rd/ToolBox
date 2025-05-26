using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;
using ToolBox.Interfaces;

namespace ToolBox.Controllers
{
    public class WelcomeMessageController : BaseController
    {
        private readonly IWelcomeMessageService _welcomeMessageService;
        private readonly ILogger<WelcomeMessageController> _logger;

        public WelcomeMessageController(
            IWelcomeMessageService welcomeMessageService,
            ILogger<WelcomeMessageController> logger)
        {
            _welcomeMessageService = welcomeMessageService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            try
            {
                var settings = await _welcomeMessageService.GetSettingsAsync();
                
                var viewModel = new WelcomeMessageEditViewModel
                {
                    WelcomeMessage = new WelcomeMessageViewModel
                    {
                        Title = settings.Title,
                        Description = settings.DescriptionHTML,
                        VideoType = Enum.Parse<DefaultVideoType>(settings.VideoType),
                        UploadedVideoPath = settings.UploadedVideoPath,
                        YouTubeUrl = settings.VideoType == "YouTube" ? settings.VideoUrl : null,
                        VimeoUrl = settings.VideoType == "Vimeo" ? settings.VideoUrl : null
                    }
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la configuración del mensaje de bienvenida");
                TempData["Error"] = "Error al cargar la configuración";
                return View(new WelcomeMessageEditViewModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(WelcomeMessageEditViewModel model)
        {
            try
            {
                // Obtener la configuración actual
                var currentSettings = await _welcomeMessageService.GetSettingsAsync();
                
                // Crear objeto de configuración actualizado
                var settings = new WelcomeMessageSettings
                {
                    Title = model.WelcomeMessage.Title,
                    DescriptionHTML = model.WelcomeMessage.Description,
                    VideoType = model.WelcomeMessage.VideoType.ToString()
                };

                // Manejar la lógica de video según el tipo seleccionado
                switch (model.WelcomeMessage.VideoType)
                {
                    case DefaultVideoType.None:
                        // Si había un video subido anteriormente, eliminarlo
                        if (currentSettings.VideoType == "Uploaded" && !string.IsNullOrEmpty(currentSettings.UploadedVideoPath))
                        {
                            _welcomeMessageService.DeleteOldVideoFile(currentSettings.UploadedVideoPath);
                        }
                        settings.VideoUrl = null;
                        settings.UploadedVideoPath = null;
                        settings.UploadedVideoFileName = null;
                        break;
                        
                    case DefaultVideoType.Uploaded:
                        if (model.WelcomeMessage.VideoFile != null)
                        {
                            // Eliminar video anterior si existe
                            if (!string.IsNullOrEmpty(currentSettings.UploadedVideoPath))
                            {
                                _welcomeMessageService.DeleteOldVideoFile(currentSettings.UploadedVideoPath);
                            }
                            
                            // Guardar nuevo video
                            var videoPath = await _welcomeMessageService.SaveVideoFileAsync(model.WelcomeMessage.VideoFile);
                            settings.UploadedVideoPath = videoPath;
                            settings.UploadedVideoFileName = model.WelcomeMessage.VideoFile.FileName;
                            settings.VideoUrl = null;
                        }
                        else
                        {
                            // Mantener el video existente
                            settings.UploadedVideoPath = currentSettings.UploadedVideoPath;
                            settings.UploadedVideoFileName = currentSettings.UploadedVideoFileName;
                            settings.VideoUrl = null;
                        }
                        break;
                        
                    case DefaultVideoType.YouTube:
                        // Si había un video subido anteriormente, eliminarlo
                        if (currentSettings.VideoType == "Uploaded" && !string.IsNullOrEmpty(currentSettings.UploadedVideoPath))
                        {
                            _welcomeMessageService.DeleteOldVideoFile(currentSettings.UploadedVideoPath);
                        }
                        settings.VideoUrl = model.WelcomeMessage.YouTubeUrl;
                        settings.UploadedVideoPath = null;
                        settings.UploadedVideoFileName = null;
                        break;
                        
                    case DefaultVideoType.Vimeo:
                        // Si había un video subido anteriormente, eliminarlo
                        if (currentSettings.VideoType == "Uploaded" && !string.IsNullOrEmpty(currentSettings.UploadedVideoPath))
                        {
                            _welcomeMessageService.DeleteOldVideoFile(currentSettings.UploadedVideoPath);
                        }
                        settings.VideoUrl = model.WelcomeMessage.VimeoUrl;
                        settings.UploadedVideoPath = null;
                        settings.UploadedVideoFileName = null;
                        break;
                }

                // Guardar la configuración
                var success = await _welcomeMessageService.UpdateSettingsAsync(settings);
                
                if (success)
                {
                    return Json(new { success = true, message = "Mensaje de bienvenida guardado exitosamente" });
                }
                else
                {
                    return Json(new { success = false, message = "Error al guardar el mensaje de bienvenida" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el mensaje de bienvenida");
                return Json(new { success = false, message = $"Error al guardar: {ex.Message}" });
            }
        }

        // API Endpoints
        [HttpGet]
        [Route("api/welcomemessage/settings")]
        public async Task<IActionResult> GetSettings()
        {
            try
            {
                var settings = await _welcomeMessageService.GetSettingsAsync();
                return Ok(new 
                { 
                    success = true,
                    data = new
                    {
                        title = settings.Title,
                        descriptionHTML = settings.DescriptionHTML,
                        videoType = settings.VideoType,
                        videoUrl = settings.VideoUrl,
                        uploadedVideoPath = settings.UploadedVideoPath,
                        uploadedVideoFileName = settings.UploadedVideoFileName,
                        updatedAt = settings.UpdatedAt
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la configuración del mensaje de bienvenida");
                return StatusCode(500, new { success = false, message = "Error al obtener la configuración" });
            }
        }

        [HttpPut]
        [Route("api/welcomemessage/settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSettings([FromForm] WelcomeMessageUpdateDto dto)
        {
            try
            {
                var currentSettings = await _welcomeMessageService.GetSettingsAsync();
                
                var settings = new WelcomeMessageSettings
                {
                    Title = dto.Title,
                    DescriptionHTML = dto.DescriptionHTML,
                    VideoType = dto.VideoType
                };

                // Manejar la lógica de video
                switch (dto.VideoType)
                {
                    case "None":
                        if (currentSettings.VideoType == "Uploaded" && !string.IsNullOrEmpty(currentSettings.UploadedVideoPath))
                        {
                            _welcomeMessageService.DeleteOldVideoFile(currentSettings.UploadedVideoPath);
                        }
                        break;
                        
                    case "Uploaded":
                        if (dto.VideoFile != null)
                        {
                            if (!string.IsNullOrEmpty(currentSettings.UploadedVideoPath))
                            {
                                _welcomeMessageService.DeleteOldVideoFile(currentSettings.UploadedVideoPath);
                            }
                            
                            var videoPath = await _welcomeMessageService.SaveVideoFileAsync(dto.VideoFile);
                            settings.UploadedVideoPath = videoPath;
                            settings.UploadedVideoFileName = dto.VideoFile.FileName;
                        }
                        else
                        {
                            settings.UploadedVideoPath = currentSettings.UploadedVideoPath;
                            settings.UploadedVideoFileName = currentSettings.UploadedVideoFileName;
                        }
                        break;
                        
                    case "YouTube":
                    case "Vimeo":
                        if (currentSettings.VideoType == "Uploaded" && !string.IsNullOrEmpty(currentSettings.UploadedVideoPath))
                        {
                            _welcomeMessageService.DeleteOldVideoFile(currentSettings.UploadedVideoPath);
                        }
                        settings.VideoUrl = dto.VideoUrl;
                        break;
                }

                var success = await _welcomeMessageService.UpdateSettingsAsync(settings);
                
                if (success)
                {
                    return Ok(new { success = true, message = "Configuración actualizada exitosamente" });
                }
                else
                {
                    return StatusCode(500, new { success = false, message = "Error al actualizar la configuración" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la configuración del mensaje de bienvenida");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }

    // DTO para la actualización del mensaje de bienvenida
    public class WelcomeMessageUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string DescriptionHTML { get; set; } = string.Empty;
        public string VideoType { get; set; } = "None";
        public string? VideoUrl { get; set; }
        public IFormFile? VideoFile { get; set; }
    }
}