using System;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class LifeArea
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        [MaxLength(100)]
        public string IconClass { get; set; } = "fas fa-circle"; // Font Awesome icon class
        
        [MaxLength(7)]
        public string IconColor { get; set; } = "#6c757d"; // Hex color for the icon
        
        public bool IsActive { get; set; } = true;
        
        public int DisplayOrder { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        // Opcionalmente, si queremos trackear quién creó el área
        public int? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
    }
}