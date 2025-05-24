using System;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class Question
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(500)]
        public string QuestionText { get; set; } = string.Empty;
        
        // Relación con LifeArea
        public int LifeAreaId { get; set; }
        public virtual LifeArea? LifeArea { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}