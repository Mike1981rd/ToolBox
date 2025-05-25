using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class Habito
    {
        public int Id { get; set; }
        
        public int UsuarioId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Descripcion { get; set; }
        
        [Required]
        [StringLength(7)] // Para colores hex como #FFFFFF
        public string Color { get; set; } = "#3498db";
        
        public int CategoriaHabitoId { get; set; }
        
        public int FrecuenciaHabitoId { get; set; }
        
        public bool HabilitarRecordatorios { get; set; } = false;
        
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        [ForeignKey("UsuarioId")]
        public virtual User? Usuario { get; set; }
        
        [ForeignKey("CategoriaHabitoId")]
        public virtual CategoriaHabito? CategoriaHabito { get; set; }
        
        [ForeignKey("FrecuenciaHabitoId")]
        public virtual FrecuenciaHabito? FrecuenciaHabito { get; set; }
        
        public virtual ICollection<RegistroCumplimientoHabito> RegistrosCumplimiento { get; set; } = new List<RegistroCumplimientoHabito>();
    }
}