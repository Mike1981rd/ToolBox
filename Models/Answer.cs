using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class Answer
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long QuestionnaireInstanceId { get; set; }

        [ForeignKey("QuestionnaireInstanceId")]
        public virtual QuestionnaireInstance QuestionnaireInstance { get; set; } = null!;

        [Required]
        public long QuestionTemplateId { get; set; }

        [ForeignKey("QuestionTemplateId")]
        public virtual QuestionTemplate QuestionTemplate { get; set; } = null!;

        [Column(TypeName = "TEXT")]
        public string? ResponseText { get; set; }

        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
    }
}