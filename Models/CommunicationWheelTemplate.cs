using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class CommunicationWheelTemplate
    {
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        public int CoachId { get; set; }
        public User Coach { get; set; } = null!;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public ICollection<CommunicationDimension> Dimensions { get; set; } = new List<CommunicationDimension>();
    }
}