using System;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class CommunicationDimension
    {
        public long Id { get; set; }
        
        public long CommunicationWheelTemplateId { get; set; }
        public CommunicationWheelTemplate CommunicationWheelTemplate { get; set; } = null!;
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string? GuidingQuestion { get; set; }
        
        public int Order { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}