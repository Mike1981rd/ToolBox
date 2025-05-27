using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Interfaces
{
    public interface ISesionCalendarioService
    {
        // Create
        Task<(bool Success, string Message, int? SesionId)> CrearSesionAsync(int coachId, CrearSesionRequest request);
        
        // Read
        Task<SesionesListResponse> ObtenerSesionesAsync(FiltroSesionesRequest filtro);
        Task<SesionResponse?> ObtenerSesionPorIdAsync(int sesionId);
        Task<List<ClienteSimpleResponse>> ObtenerClientesDelCoachAsync(int coachId);
        
        // Update
        Task<(bool Success, string Message)> ActualizarSesionAsync(int sesionId, int coachId, ActualizarSesionRequest request);
        Task<(bool Success, string Message)> CambiarEstadoSesionAsync(int sesionId, int coachId, string nuevoEstado);
        
        // Delete
        Task<(bool Success, string Message)> EliminarSesionAsync(int sesionId, int coachId);
        
        // Validation helpers
        Task<bool> ExisteConflictoHorarioAsync(int coachId, DateTime fechaInicio, DateTime fechaFin, int? sesionIdExcluir = null);
        Task<bool> PuedeCoachModificarSesionAsync(int coachId, int sesionId);
    }
}