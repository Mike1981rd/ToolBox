using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class TareaService : ITareaService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TareaService> _logger;

        public TareaService(ApplicationDbContext context, ILogger<TareaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Tarea>> GetAllTareasAsync(int usuarioId)
        {
            return await _context.Tareas
                .Where(t => t.UsuarioId == usuarioId)
                .OrderByDescending(t => t.FechaCreacion)
                .ToListAsync();
        }

        public async Task<Tarea?> GetTareaByIdAsync(int id, int usuarioId)
        {
            return await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);
        }

        public async Task<Tarea> CreateTareaAsync(Tarea tarea)
        {
            // Verificar que el usuario existe
            var userExists = await _context.Users.AnyAsync(u => u.Id == tarea.UsuarioId);
            if (!userExists)
            {
                _logger.LogError($"User with ID {tarea.UsuarioId} does not exist");
                throw new InvalidOperationException($"Usuario con ID {tarea.UsuarioId} no existe");
            }
            
            tarea.FechaCreacion = DateTime.UtcNow;
            tarea.FechaActualizacion = DateTime.UtcNow;
            
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            
            return tarea;
        }

        public async Task<bool> UpdateTareaAsync(Tarea tarea)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingTarea = await _context.Tareas
                    .FirstOrDefaultAsync(t => t.Id == tarea.Id && t.UsuarioId == tarea.UsuarioId);
                
                if (existingTarea == null)
                    return false;
                
                existingTarea.Descripcion = tarea.Descripcion;
                existingTarea.EsUrgente = tarea.EsUrgente;
                existingTarea.EsImportante = tarea.EsImportante;
                existingTarea.FechaActualizacion = DateTime.UtcNow;
                
                _context.Tareas.Update(existingTarea);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating task {TaskId}", tarea.Id);
                return false;
            }
        }

        public async Task<bool> DeleteTareaAsync(int id, int usuarioId)
        {
            var tarea = await GetTareaByIdAsync(id, usuarioId);
            if (tarea == null)
                return false;
            
            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> ToggleCompletadaAsync(int id, int usuarioId)
        {
            var tarea = await GetTareaByIdAsync(id, usuarioId);
            if (tarea == null)
                return false;
            
            tarea.EstaCompletada = !tarea.EstaCompletada;
            tarea.FechaCompletado = tarea.EstaCompletada ? DateTime.UtcNow : null;
            tarea.FechaActualizacion = DateTime.UtcNow;
            
            _context.Tareas.Update(tarea);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<TaskStatisticsViewModel> GetEstadisticasAsync(int usuarioId)
        {
            var tareas = await GetAllTareasAsync(usuarioId);
            var tareasList = tareas.ToList();
            
            return new TaskStatisticsViewModel
            {
                TotalTasks = tareasList.Count,
                CompletedTasks = tareasList.Count(t => t.EstaCompletada),
                PendingTasks = tareasList.Count(t => !t.EstaCompletada),
                UrgentImportantCount = tareasList.Count(t => t.EsUrgente && t.EsImportante),
                NotUrgentImportantCount = tareasList.Count(t => !t.EsUrgente && t.EsImportante),
                UrgentNotImportantCount = tareasList.Count(t => t.EsUrgente && !t.EsImportante),
                NotUrgentNotImportantCount = tareasList.Count(t => !t.EsUrgente && !t.EsImportante)
            };
        }
    }
}