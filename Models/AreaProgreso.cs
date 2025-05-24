using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class AreaProgreso
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Descripcion { get; set; }
        
        [StringLength(50)]
        public string IconClass { get; set; } = "fas fa-circle";
        
        [StringLength(7)]
        public string IconColor { get; set; } = "#667eea";
        
        public int OrdenVisualizacion { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual ICollection<CategoriaProgreso> Categorias { get; set; } = new List<CategoriaProgreso>();
    }
}