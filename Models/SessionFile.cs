using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class SessionFile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SessionId { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "El nombre del archivo no puede exceder los 255 caracteres")]
        public string OriginalName { get; set; } = string.Empty;

        [Required]
        [StringLength(255, ErrorMessage = "La ruta del archivo no puede exceder los 255 caracteres")]
        public string FilePath { get; set; } = string.Empty;

        [StringLength(100)]
        public string? MimeType { get; set; }

        public int? Size { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("SessionId")]
        public virtual Session Session { get; set; } = null!;
    }
}