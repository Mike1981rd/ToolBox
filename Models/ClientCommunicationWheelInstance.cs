using System;
using System.Collections.Generic;

namespace ToolBox.Models
{
    public class ClientCommunicationWheelInstance
    {
        public long Id { get; set; }
        
        public long CommunicationWheelTemplateId { get; set; }
        public CommunicationWheelTemplate CommunicationWheelTemplate { get; set; } = null!;
        
        public int ClientId { get; set; }
        public User Client { get; set; } = null!;
        
        public int AssignedByCoachId { get; set; }
        public User AssignedByCoach { get; set; } = null!;
        
        public WheelCompletionStatus Status { get; set; } = WheelCompletionStatus.Pending;
        
        public string? ClientNotes { get; set; }
        
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? StartedAt { get; set; }
        
        public DateTime? CompletedAt { get; set; }
        
        public ICollection<DimensionScore> Scores { get; set; } = new List<DimensionScore>();
    }
}