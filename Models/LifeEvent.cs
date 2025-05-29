using System;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class LifeEvent
    {
        public long Id { get; set; }

        [Required]
        public int UserId { get; set; }
        
        public User User { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string EventTitle { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int EventYear { get; set; }

        [Required]
        [Range(-3, 3)]
        public int ImpactScore { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Helper method to get impact score text
        public string GetImpactScoreText()
        {
            return ImpactScore switch
            {
                3 => "Very Positive",
                2 => "Positive", 
                1 => "Slightly Positive",
                0 => "Neutral",
                -1 => "Slightly Negative",
                -2 => "Negative",
                -3 => "Very Negative",
                _ => "Unknown"
            };
        }

        // Helper method to get impact score CSS class
        public string GetImpactScoreCssClass()
        {
            return ImpactScore switch
            {
                3 => "bg-success text-white",
                2 => "bg-success bg-opacity-75 text-white",
                1 => "bg-success bg-opacity-50",
                0 => "bg-secondary bg-opacity-50",
                -1 => "bg-danger bg-opacity-25",
                -2 => "bg-danger bg-opacity-50 text-white",
                -3 => "bg-danger text-white",
                _ => "bg-secondary"
            };
        }
    }
}