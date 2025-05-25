using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class WelcomeMessageService : IWelcomeMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<WelcomeMessageService> _logger;

        public WelcomeMessageService(
            ApplicationDbContext context,
            IWebHostEnvironment environment,
            ILogger<WelcomeMessageService> logger)
        {
            _context = context;
            _environment = environment;
            _logger = logger;
        }

        public async Task<WelcomeMessageSettings> GetSettingsAsync()
        {
            try
            {
                // Intenta obtener el registro singleton (Id = 1)
                var settings = await _context.WelcomeMessageSettings
                    .FirstOrDefaultAsync(s => s.Id == 1);

                // Si no existe, crea uno por defecto
                if (settings == null)
                {
                    settings = new WelcomeMessageSettings
                    {
                        Id = 1,
                        Title = "Bienvenido a ToolBox",
                        DescriptionHTML = "<p>Configura tu mensaje de bienvenida personalizado aquí.</p>",
                        VideoType = "None",
                        UpdatedAt = DateTime.UtcNow
                    };

                    _context.WelcomeMessageSettings.Add(settings);
                    await _context.SaveChangesAsync();
                }

                return settings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la configuración del mensaje de bienvenida");
                throw;
            }
        }

        public async Task<bool> UpdateSettingsAsync(WelcomeMessageSettings settings)
        {
            try
            {
                // Asegura que siempre sea el registro singleton
                settings.Id = 1;
                settings.UpdatedAt = DateTime.UtcNow;

                // Obtiene el registro existente
                var existingSettings = await _context.WelcomeMessageSettings
                    .FirstOrDefaultAsync(s => s.Id == 1);

                if (existingSettings != null)
                {
                    // Actualiza los valores
                    _context.Entry(existingSettings).CurrentValues.SetValues(settings);
                }
                else
                {
                    // Si por alguna razón no existe, lo crea
                    _context.WelcomeMessageSettings.Add(settings);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la configuración del mensaje de bienvenida");
                return false;
            }
        }

        public async Task<string> SaveVideoFileAsync(IFormFile videoFile)
        {
            try
            {
                // Sin validaciones según requisitos
                var extension = Path.GetExtension(videoFile.FileName).ToLowerInvariant();

                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "videos", "welcome");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(fileStream);
                }

                return $"/uploads/videos/welcome/{uniqueFileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el archivo de video");
                throw;
            }
        }

        public void DeleteOldVideoFile(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            try
            {
                var fullPath = Path.Combine(_environment.WebRootPath, filePath.TrimStart('/'));
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    _logger.LogInformation($"Archivo de video eliminado: {fullPath}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar archivo de video: {filePath}");
                // No lanzamos la excepción para no interrumpir el flujo principal
            }
        }
    }
}