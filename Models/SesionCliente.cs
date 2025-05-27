namespace ToolBox.Models
{
    public class SesionCliente
    {
        public int SesionCalendarioId { get; set; }
        public int ClienteId { get; set; }
        
        // Additional fields for the relationship
        public DateTime? FechaConfirmacion { get; set; }
        public bool Asistio { get; set; } = false;
        public string? Notas { get; set; }
        
        // Navigation properties
        public virtual SesionCalendario SesionCalendario { get; set; } = null!;
        public virtual Customer Cliente { get; set; } = null!;
    }
}