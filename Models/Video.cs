using System;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class Video
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;
        
        public string? DescripcionHTML { get; set; }
        
        public int? AutorId { get; set; }
        public virtual User? Autor { get; set; }
        
        public int? TemaId { get; set; }
        public virtual Topic? Tema { get; set; }
        
        [MaxLength(50)]
        public string TipoFuenteVideo { get; set; } = "YouTube"; // YouTube, Vimeo, Upload
        
        [MaxLength(500)]
        public string? UrlVideoExterno { get; set; }
        
        [MaxLength(255)]
        public string? NombreArchivoVideoSubido { get; set; }
        
        [MaxLength(500)]
        public string? PathVideoSubido { get; set; }
        
        [MaxLength(20)]
        public string? Duracion { get; set; } // Format: "mm:ss" or "hh:mm:ss"
        
        [MaxLength(200)]
        public string? MetaTituloSEO { get; set; }
        
        [MaxLength(500)]
        public string? MetaDescripcionSEO { get; set; }
        
        [MaxLength(500)]
        public string? PalabrasClaveSEO { get; set; } // Comma-separated keywords
        
        public bool EsDestacado { get; set; } = false;
        
        [MaxLength(50)]
        public string EstadoVideo { get; set; } = "Borrador"; // Publicado, Borrador, Archivado
        
        public DateTime FechaSubida { get; set; } = DateTime.UtcNow;
        
        public int? UsuarioCreadorId { get; set; }
        public virtual User? UsuarioCreador { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}