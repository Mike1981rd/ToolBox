using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class WelcomeMessageController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public WelcomeMessageController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var viewModel = new WelcomeMessageEditViewModel
            {
                WelcomeMessage = LoadWelcomeMessage()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(WelcomeMessageEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            try
            {
                if (model.WelcomeMessage.VideoFile != null)
                {
                    var uploadedPath = await SaveUploadedVideo(model.WelcomeMessage.VideoFile);
                    model.WelcomeMessage.UploadedVideoPath = uploadedPath;
                    model.WelcomeMessage.VideoType = DefaultVideoType.Uploaded;
                }
                else if (!string.IsNullOrEmpty(model.WelcomeMessage.YouTubeUrl))
                {
                    model.WelcomeMessage.VideoType = DefaultVideoType.YouTube;
                }
                else if (!string.IsNullOrEmpty(model.WelcomeMessage.VimeoUrl))
                {
                    model.WelcomeMessage.VideoType = DefaultVideoType.Vimeo;
                }

                SaveWelcomeMessage(model.WelcomeMessage);

                return Json(new { success = true, message = "Mensaje de bienvenida guardado exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al guardar: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideo(IFormFile videoFile)
        {
            try
            {
                if (videoFile == null || videoFile.Length == 0)
                {
                    return Json(new { success = false, message = "No se seleccionó ningún archivo" });
                }

                var allowedTypes = new[] { ".mp4", ".avi", ".mov", ".wmv", ".webm" };
                var extension = Path.GetExtension(videoFile.FileName).ToLowerInvariant();

                if (!allowedTypes.Contains(extension))
                {
                    return Json(new { success = false, message = "Formato de video no válido" });
                }

                var videoPath = await SaveUploadedVideo(videoFile);
                return Json(new { success = true, videoPath = videoPath });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al subir video: {ex.Message}" });
            }
        }

        private WelcomeMessageViewModel LoadWelcomeMessage()
        {
            return new WelcomeMessageViewModel
            {
                Title = "Bienvenido a ToolBox",
                Description = "<p>Configura tu mensaje de bienvenida personalizado aquí.</p>",
                VideoType = DefaultVideoType.None
            };
        }

        private void SaveWelcomeMessage(WelcomeMessageViewModel model)
        {
            // Implementar persistencia en base de datos o archivo
            // Por ahora es un placeholder
        }

        private async Task<string> SaveUploadedVideo(IFormFile videoFile)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "videos");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{videoFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await videoFile.CopyToAsync(fileStream);
            }

            return $"/uploads/videos/{uniqueFileName}";
        }
    }
}