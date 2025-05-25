using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class CategoriaHabito
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string? Descripcion { get; set; }
        
        [Required]
        [StringLength(50)]
        public string IconClass { get; set; } = "fas fa-star";
        
        [Required]
        [StringLength(7)]
        public string Color { get; set; } = "#3498db";
        
        public int OrdenVisualizacion { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual ICollection<Habito> Habitos { get; set; } = new List<Habito>();
    }
}