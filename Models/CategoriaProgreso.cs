using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class CategoriaProgreso
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Descripcion { get; set; }
        
        [Required]
        public int AreaProgresoId { get; set; }
        
        public int OrdenVisualizacion { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("AreaProgresoId")]
        public virtual AreaProgreso? AreaProgreso { get; set; }
        
        public virtual ICollection<ProgresoMetaUsuario> ProgresosUsuarios { get; set; } = new List<ProgresoMetaUsuario>();
    }
}