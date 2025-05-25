using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class FrecuenciaHabito
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string? Descripcion { get; set; }
        
        public int DiasIntervalo { get; set; } = 1; // 1 = diario, 7 = semanal, etc.
        
        public int OrdenVisualizacion { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual ICollection<Habito> Habitos { get; set; } = new List<Habito>();
    }
}