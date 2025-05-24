using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class ProgresoMetaUsuario
    {
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int CategoriaProgresoId { get; set; }
        
        [StringLength(1000)]
        public string? Meta { get; set; }
        
        [Range(0, 100)]
        public int PorcentajeProgreso { get; set; } = 0;
        
        [StringLength(1000)]
        public string? SiguienteAccion { get; set; }
        
        public DateTime? FechaLimite { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        
        [ForeignKey("CategoriaProgresoId")]
        public virtual CategoriaProgreso? CategoriaProgreso { get; set; }
    }
}