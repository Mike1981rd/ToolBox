using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;
using ToolBox.Interfaces;
using ToolBox.Data;

namespace ToolBox.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITareaService _tareaService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITareaService tareaService, ILogger<TasksController> logger)
        {
            _tareaService = tareaService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var tareas = await _tareaService.GetAllTareasAsync(userId);
            var statistics = await _tareaService.GetEstadisticasAsync(userId);
            
            // Convertir Tarea a TaskViewModel
            var taskViewModels = tareas.Select(t => new TaskViewModel
            {
                TaskId = t.Id,
                Description = t.Descripcion,
                IsUrgent = t.EsUrgente,
                IsImportant = t.EsImportante,
                IsCompleted = t.EstaCompletada,
                CreatedAt = t.FechaCreacion,
                CompletedAt = t.FechaCompletado,
                UpdatedAt = t.FechaActualizacion
            }).ToList();
            
            var viewModel = new TasksPageViewModel
            {
                Tasks = taskViewModels,
                Statistics = statistics
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddTask([FromBody] TaskFormViewModel taskForm)
        {
            try
            {
                // No validar ModelState para permitir cualquier tipo de guardado
                var userId = GetCurrentUserId();
                
                // Log para debugging
                _logger.LogInformation($"Attempting to create task for userId: {userId}");
                _logger.LogInformation($"Task description: '{taskForm.Description}'");
                
                var nuevaTarea = new Tarea
                {
                    UsuarioId = userId,
                    Descripcion = taskForm.Description?.Trim() ?? "",
                    EsUrgente = taskForm.IsUrgent,
                    EsImportante = taskForm.IsImportant
                };

                var tareaCreada = await _tareaService.CreateTareaAsync(nuevaTarea);

                var taskViewModel = new TaskViewModel
                {
                    TaskId = tareaCreada.Id,
                    Description = tareaCreada.Descripcion,
                    IsUrgent = tareaCreada.EsUrgente,
                    IsImportant = tareaCreada.EsImportante,
                    IsCompleted = tareaCreada.EstaCompletada,
                    CreatedAt = tareaCreada.FechaCreacion,
                    UpdatedAt = tareaCreada.FechaActualizacion
                };

                return Json(new TaskResponseViewModel
                {
                    Success = true,
                    Message = "Tarea agregada exitosamente",
                    Task = taskViewModel,
                    ActionDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding task");
                return Json(new TaskResponseViewModel
                {
                    Success = false,
                    Message = $"Error al agregar la tarea: {ex.Message}",
                    Errors = new List<string> { ex.Message, ex.InnerException?.Message ?? "" }
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateTask([FromBody] TaskViewModel task)
        {
            try
            {

                var userId = GetCurrentUserId();
                var tareaToUpdate = new Tarea
                {
                    Id = task.TaskId,
                    UsuarioId = userId,
                    Descripcion = task.Description?.Trim() ?? "",
                    EsUrgente = task.IsUrgent,
                    EsImportante = task.IsImportant
                };

                var success = await _tareaService.UpdateTareaAsync(tareaToUpdate);

                if (!success)
                {
                    return Json(new TaskResponseViewModel
                    {
                        Success = false,
                        Message = "Tarea no encontrada"
                    });
                }

                task.UpdatedAt = DateTime.Now;

                return Json(new TaskResponseViewModel
                {
                    Success = true,
                    Message = "Tarea actualizada exitosamente",
                    Task = task,
                    ActionDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task {TaskId}", task.TaskId);
                return Json(new TaskResponseViewModel
                {
                    Success = false,
                    Message = "Error al actualizar la tarea"
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteTask(int taskId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _tareaService.DeleteTareaAsync(taskId, userId);

                if (!success)
                {
                    return Json(new TaskResponseViewModel
                    {
                        Success = false,
                        Message = "Tarea no encontrada"
                    });
                }

                return Json(new TaskResponseViewModel
                {
                    Success = true,
                    Message = "Tarea eliminada exitosamente",
                    ActionDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task {TaskId}", taskId);
                return Json(new TaskResponseViewModel
                {
                    Success = false,
                    Message = "Error al eliminar la tarea"
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ToggleTaskCompletion(int taskId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _tareaService.ToggleCompletadaAsync(taskId, userId);

                if (!success)
                {
                    return Json(new TaskResponseViewModel
                    {
                        Success = false,
                        Message = "Tarea no encontrada"
                    });
                }

                // Obtener la tarea actualizada para devolver
                var tarea = await _tareaService.GetTareaByIdAsync(taskId, userId);
                var taskViewModel = new TaskViewModel
                {
                    TaskId = tarea.Id,
                    Description = tarea.Descripcion,
                    IsUrgent = tarea.EsUrgente,
                    IsImportant = tarea.EsImportante,
                    IsCompleted = tarea.EstaCompletada,
                    CreatedAt = tarea.FechaCreacion,
                    CompletedAt = tarea.FechaCompletado,
                    UpdatedAt = tarea.FechaActualizacion
                };

                return Json(new TaskResponseViewModel
                {
                    Success = true,
                    Message = tarea.EstaCompletada ? "Tarea marcada como completada" : "Tarea marcada como pendiente",
                    Task = taskViewModel,
                    ActionDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling task completion {TaskId}", taskId);
                return Json(new TaskResponseViewModel
                {
                    Success = false,
                    Message = "Error al actualizar el estado de la tarea"
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTaskStatistics()
        {
            var userId = GetCurrentUserId();
            var statistics = await _tareaService.GetEstadisticasAsync(userId);
            return Json(statistics);
        }

        private int GetCurrentUserId()
        {
            // TODO: Obtener el usuario actual desde la sesión o claims
            // Por ahora retornamos un ID hardcodeado para pruebas
            return 1;
        }
    }
}