using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class QuestionTemplate
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long QuestionnaireTemplateId { get; set; }

        [ForeignKey("QuestionnaireTemplateId")]
        public virtual QuestionnaireTemplate QuestionnaireTemplate { get; set; } = null!;

        [Required]
        [Column(TypeName = "TEXT")]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        public QuestionType Type { get; set; }

        [Column(TypeName = "TEXT")]
        public string? OptionsJson { get; set; }

        public bool IsRequired { get; set; } = true;

        public int Order { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}