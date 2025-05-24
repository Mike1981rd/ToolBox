using System;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        
        [Required]
        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }
        
        [Required]
        [Range(1, 10, ErrorMessage = "La puntuación debe estar entre 1 y 10")]
        public int Score { get; set; }
        
        public DateTime AnsweredAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}