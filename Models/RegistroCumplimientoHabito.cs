using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class RegistroCumplimientoHabito
    {
        public int Id { get; set; }
        
        public int HabitoId { get; set; }
        
        [Required]
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
        
        public bool Cumplido { get; set; } = false;
        
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("HabitoId")]
        public virtual Habito? Habito { get; set; }
    }
}