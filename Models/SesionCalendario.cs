using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class SesionCalendario
    {
        public int Id { get; set; }
        
        [Required]
        public int CoachId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Titulo { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Descripcion { get; set; }
        
        [Required]
        public DateTime FechaHoraInicio { get; set; }
        
        [Required]
        public DateTime FechaHoraFin { get; set; }
        
        [StringLength(500)]
        public string? UbicacionOEnlace { get; set; }
        
        [Required]
        [StringLength(50)]
        public string EstadoSesion { get; set; } = EstadoSesionCalendario.Programada;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual User Coach { get; set; } = null!;
        
        // Many-to-many relationship with Clientes
        public virtual ICollection<SesionCliente> SesionClientes { get; set; } = new List<SesionCliente>();
    }
    
    // Estado de sesión constants
    public static class EstadoSesionCalendario
    {
        public const string Programada = "Programada";
        public const string Confirmada = "Confirmada";
        public const string Cancelada = "Cancelada";
        public const string Completada = "Completada";
        public const string EnProgreso = "EnProgreso";
    }
}