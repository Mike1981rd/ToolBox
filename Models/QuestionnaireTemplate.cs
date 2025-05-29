using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class QuestionnaireTemplate
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "El título no puede exceder los 255 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "TEXT")]
        public string? Description { get; set; }

        [Required]
        public int CoachId { get; set; }

        [ForeignKey("CoachId")]
        public virtual User Coach { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relación uno-a-muchos con QuestionTemplate
        public virtual ICollection<QuestionTemplate> Questions { get; set; } = new List<QuestionTemplate>();
    }
}