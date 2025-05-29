using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class QuestionnaireInstance
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long QuestionnaireTemplateId { get; set; }

        [ForeignKey("QuestionnaireTemplateId")]
        public virtual QuestionnaireTemplate QuestionnaireTemplate { get; set; } = null!;

        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual User Client { get; set; } = null!;

        [Required]
        public int AssignedByCoachId { get; set; }

        [ForeignKey("AssignedByCoachId")]
        public virtual User AssignedByCoach { get; set; } = null!;

        public QuestionnaireStatus Status { get; set; } = QuestionnaireStatus.Pending;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        // Relación uno-a-muchos con Answer
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}