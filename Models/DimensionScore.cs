using System;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class DimensionScore
    {
        public long Id { get; set; }
        
        public long ClientCommunicationWheelInstanceId { get; set; }
        public ClientCommunicationWheelInstance ClientCommunicationWheelInstance { get; set; } = null!;
        
        public long CommunicationDimensionId { get; set; }
        public CommunicationDimension CommunicationDimension { get; set; } = null!;
        
        [Range(1, 10)]
        public int Score { get; set; }
        
        public DateTime ScoredAt { get; set; } = DateTime.UtcNow;
    }
}