using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    // Request ViewModels
    public class CrearSesionRequest
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;
        
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string? Descripcion { get; set; }
        
        [Required(ErrorMessage = "La fecha y hora de inicio es requerida")]
        public DateTime FechaHoraInicio { get; set; }
        
        [Required(ErrorMessage = "La fecha y hora de fin es requerida")]
        public DateTime FechaHoraFin { get; set; }
        
        [StringLength(500, ErrorMessage = "La ubicación/enlace no puede exceder 500 caracteres")]
        public string? UbicacionOEnlace { get; set; }
        
        [Required(ErrorMessage = "Debe seleccionar al menos un cliente")]
        public List<int> ClienteIds { get; set; } = new List<int>();
    }
    
    public class ActualizarSesionRequest
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;
        
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string? Descripcion { get; set; }
        
        [Required(ErrorMessage = "La fecha y hora de inicio es requerida")]
        public DateTime FechaHoraInicio { get; set; }
        
        [Required(ErrorMessage = "La fecha y hora de fin es requerida")]
        public DateTime FechaHoraFin { get; set; }
        
        [StringLength(500, ErrorMessage = "La ubicación/enlace no puede exceder 500 caracteres")]
        public string? UbicacionOEnlace { get; set; }
        
        [Required(ErrorMessage = "Debe seleccionar al menos un cliente")]
        public List<int> ClienteIds { get; set; } = new List<int>();
    }
    
    public class CambiarEstadoSesionRequest
    {
        [Required(ErrorMessage = "El estado es requerido")]
        [StringLength(50)]
        public string EstadoSesion { get; set; } = string.Empty;
    }
    
    public class FiltroSesionesRequest
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? CoachId { get; set; }
        public int? ClienteId { get; set; }
        public string? EstadoSesion { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
    
    // Response ViewModels
    public class SesionResponse
    {
        public int Id { get; set; }
        public int CoachId { get; set; }
        public string CoachNombre { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public string? UbicacionOEnlace { get; set; }
        public string EstadoSesion { get; set; } = string.Empty;
        public List<ClienteSesionResponse> Clientes { get; set; } = new List<ClienteSesionResponse>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    
    public class ClienteSesionResponse
    {
        public int ClienteId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Email { get; set; }
        public DateTime? FechaConfirmacion { get; set; }
        public bool Asistio { get; set; }
        public string? Notas { get; set; }
    }
    
    public class SesionesListResponse
    {
        public List<SesionResponse> Sesiones { get; set; } = new List<SesionResponse>();
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
    
    public class ClienteSimpleResponse
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}