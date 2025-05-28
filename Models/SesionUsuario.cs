namespace ToolBox.Models
{
    public class SesionUsuario
    {
        public int SesionCalendarioId { get; set; }
        public int UsuarioId { get; set; }
        
        // Additional fields for the relationship
        public DateTime? FechaConfirmacion { get; set; }
        public bool Asistio { get; set; } = false;
        public string? Notas { get; set; }
        
        // Navigation properties
        public virtual SesionCalendario SesionCalendario { get; set; } = null!;
        public virtual User Usuario { get; set; } = null!;
    }
}