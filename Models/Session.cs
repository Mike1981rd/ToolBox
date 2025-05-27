using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El cliente es requerido")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "La fecha y hora de la sesión es requerida")]
        public DateTime SessionDateTime { get; set; }

        public DateTime? NextSessionDateTime { get; set; }

        [Required(ErrorMessage = "Los puntos clave son requeridos")]
        [Column(TypeName = "TEXT")]
        public string KeyPoints { get; set; } = string.Empty;

        [Column(TypeName = "TEXT")]
        public string? WaysToDevelop { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Assignments { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Challenges { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Feedback { get; set; }

        [StringLength(255)]
        public string? Twitter { get; set; }

        public bool Status { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("ClientId")]
        public virtual Customer Client { get; set; } = null!;

        public virtual ICollection<SessionFile> SessionFiles { get; set; } = new List<SessionFile>();
    }
}