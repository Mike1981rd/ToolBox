using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class WheelOfProgressService : IWheelOfProgressService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WheelOfProgressService> _logger;

        public WheelOfProgressService(ApplicationDbContext context, ILogger<WheelOfProgressService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<WheelOfProgressViewModel> GetWheelOfProgressDataAsync(int userId)
        {
            try
            {
                _logger.LogDebug("Getting wheel of progress data for user {UserId}", userId);

                // Get all areas with their categories
                var areas = await _context.AreasProgreso
                    .Include(ap => ap.Categorias)
                    .OrderBy(ap => ap.OrdenVisualizacion)
                    .ToListAsync();

                // Get user's progress for all categories
                var userProgress = await _context.ProgresosMetasUsuarios
                    .Where(pmu => pmu.UserId == userId)
                    .ToListAsync();

                // Build the response
                var areasViewModel = areas.Select(area => new AreaProgresoViewModel
                {
                    Id = area.Id,
                    Nombre = area.Nombre,
                    Descripcion = area.Descripcion,
                    IconClass = area.IconClass,
                    IconColor = area.IconColor,
                    OrdenVisualizacion = area.OrdenVisualizacion,
                    Categorias = area.Categorias
                        .OrderBy(c => c.OrdenVisualizacion)
                        .Select(categoria =>
                        {
                            var progress = userProgress.FirstOrDefault(up => up.CategoriaProgresoId == categoria.Id);
                            return new CategoriaProgresoViewModel
                            {
                                Id = categoria.Id,
                                Nombre = categoria.Nombre,
                                Descripcion = categoria.Descripcion,
                                AreaProgresoId = categoria.AreaProgresoId,
                                OrdenVisualizacion = categoria.OrdenVisualizacion,
                                Meta = progress?.Meta,
                                PorcentajeProgreso = progress?.PorcentajeProgreso ?? 0,
                                SiguienteAccion = progress?.SiguienteAccion,
                                FechaLimite = progress?.FechaLimite,
                                UltimaActualizacion = progress?.UpdatedAt
                            };
                        }).ToList()
                }).ToList();

                // Get stats
                var stats = await GetUserStatsAsync(userId);

                return new WheelOfProgressViewModel
                {
                    Areas = areasViewModel,
                    Stats = stats
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting wheel of progress data for user {UserId}", userId);
                throw;
            }
        }

        public async Task<SaveProgressResponseViewModel> SaveProgressAsync(int userId, SaveProgressRequestViewModel request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _logger.LogDebug("Saving progress for user {UserId}, {CategoryCount} categories", userId, request.Categories.Count);

                int updatedCount = 0;
                foreach (var categoryUpdate in request.Categories)
                {
                    // Skip empty/null data
                    if (string.IsNullOrWhiteSpace(categoryUpdate.Meta) && 
                        categoryUpdate.PorcentajeProgreso == 0 && 
                        string.IsNullOrWhiteSpace(categoryUpdate.SiguienteAccion) && 
                        !categoryUpdate.FechaLimite.HasValue)
                    {
                        continue;
                    }

                    // Find existing progress or create new
                    var existingProgress = await _context.ProgresosMetasUsuarios
                        .FirstOrDefaultAsync(pmu => pmu.UserId == userId && pmu.CategoriaProgresoId == categoryUpdate.CategoriaProgresoId);

                    if (existingProgress != null)
                    {
                        // Update existing
                        existingProgress.Meta = categoryUpdate.Meta;
                        existingProgress.PorcentajeProgreso = categoryUpdate.PorcentajeProgreso;
                        existingProgress.SiguienteAccion = categoryUpdate.SiguienteAccion;
                        existingProgress.FechaLimite = categoryUpdate.FechaLimite;
                        existingProgress.UpdatedAt = DateTime.UtcNow;
                        
                        _context.ProgresosMetasUsuarios.Update(existingProgress);
                    }
                    else
                    {
                        // Create new
                        var newProgress = new ProgresoMetaUsuario
                        {
                            UserId = userId,
                            CategoriaProgresoId = categoryUpdate.CategoriaProgresoId,
                            Meta = categoryUpdate.Meta,
                            PorcentajeProgreso = categoryUpdate.PorcentajeProgreso,
                            SiguienteAccion = categoryUpdate.SiguienteAccion,
                            FechaLimite = categoryUpdate.FechaLimite,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };
                        
                        _context.ProgresosMetasUsuarios.Add(newProgress);
                    }
                    
                    updatedCount++;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Get updated stats
                var updatedStats = await GetUserStatsAsync(userId);

                _logger.LogDebug("Successfully saved progress for user {UserId}, {UpdatedCount} categories updated", userId, updatedCount);

                return new SaveProgressResponseViewModel
                {
                    Success = true,
                    Message = $"Progreso guardado exitosamente. {updatedCount} categorías actualizadas.",
                    UpdatedStats = updatedStats
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error saving progress for user {UserId}", userId);
                
                return new SaveProgressResponseViewModel
                {
                    Success = false,
                    Message = "Error al guardar el progreso. Por favor intente nuevamente."
                };
            }
        }

        public async Task<WheelOfProgressStatsViewModel> GetUserStatsAsync(int userId)
        {
            try
            {
                var userProgress = await _context.ProgresosMetasUsuarios
                    .Where(pmu => pmu.UserId == userId)
                    .ToListAsync();

                var totalCategories = await _context.CategoriasProgreso.CountAsync();
                var metasDefinidas = userProgress.Where(up => !string.IsNullOrWhiteSpace(up.Meta)).ToList();
                
                var totalMetas = metasDefinidas.Count;
                var metasCompletadas = metasDefinidas.Count(up => up.PorcentajeProgreso >= 100);
                var metasEnProgreso = metasDefinidas.Count(up => up.PorcentajeProgreso > 0 && up.PorcentajeProgreso < 100);
                
                var progresoGlobal = totalMetas > 0 
                    ? (decimal)metasDefinidas.Average(up => up.PorcentajeProgreso) 
                    : 0;

                var ultimaActualizacion = metasDefinidas.Any() 
                    ? metasDefinidas.Max(up => up.UpdatedAt) 
                    : (DateTime?)null;

                var ahora = DateTime.UtcNow;
                var metasVencidas = metasDefinidas.Count(up => up.FechaLimite.HasValue && up.FechaLimite.Value < ahora && up.PorcentajeProgreso < 100);
                var metasPorVencer = metasDefinidas.Count(up => up.FechaLimite.HasValue && up.FechaLimite.Value >= ahora && up.FechaLimite.Value <= ahora.AddDays(7) && up.PorcentajeProgreso < 100);

                return new WheelOfProgressStatsViewModel
                {
                    ProgresoGlobal = Math.Round(progresoGlobal, 1),
                    TotalMetas = totalMetas,
                    MetasCompletadas = metasCompletadas,
                    MetasEnProgreso = metasEnProgreso,
                    UltimaActualizacion = ultimaActualizacion,
                    MetasVencidas = metasVencidas,
                    MetasPorVencer = metasPorVencer
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating stats for user {UserId}", userId);
                return new WheelOfProgressStatsViewModel();
            }
        }
    }
}