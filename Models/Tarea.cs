using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        
        public int UsuarioId { get; set; }
        
        [StringLength(500)]
        public string? Descripcion { get; set; } = string.Empty;
        
        public bool EsUrgente { get; set; } = false;
        
        public bool EsImportante { get; set; } = false;
        
        public bool EstaCompletada { get; set; } = false;
        
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        
        public DateTime? FechaCompletado { get; set; }
        
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
        
        // Navigation property
        [ForeignKey("UsuarioId")]
        public virtual User? Usuario { get; set; }
        
        // Computed property for Eisenhower Quadrant
        [NotMapped]
        public string CuadranteEisenhower
        {
            get
            {
                if (EsUrgente && EsImportante)
                    return "Hacer";
                else if (!EsUrgente && EsImportante)
                    return "Programar";
                else if (EsUrgente && !EsImportante)
                    return "Delegar";
                else
                    return "Eliminar";
            }
        }
    }
}