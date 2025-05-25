using ToolBox.Models;

namespace ToolBox.Interfaces
{
    public interface ITareaService
    {
        Task<IEnumerable<Tarea>> GetAllTareasAsync(int usuarioId);
        Task<Tarea?> GetTareaByIdAsync(int id, int usuarioId);
        Task<Tarea> CreateTareaAsync(Tarea tarea);
        Task<bool> UpdateTareaAsync(Tarea tarea);
        Task<bool> DeleteTareaAsync(int id, int usuarioId);
        Task<bool> ToggleCompletadaAsync(int id, int usuarioId);
        Task<TaskStatisticsViewModel> GetEstadisticasAsync(int usuarioId);
    }
}