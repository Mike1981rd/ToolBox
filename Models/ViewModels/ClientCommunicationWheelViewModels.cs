using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    public class ClientWheelInstanceViewModel
    {
        public long InstanceId { get; set; }
        public string WheelTemplateName { get; set; } = string.Empty;
        public string? WheelTemplateDescription { get; set; }
        public DateTime AssignedAt { get; set; }
        public WheelCompletionStatus Status { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int DimensionCount { get; set; }
    }
    
    public class CompleteWheelViewModel
    {
        public long InstanceId { get; set; }
        
        [Display(Name = "Plantilla")]
        public string WheelTemplateName { get; set; } = string.Empty;
        
        [Display(Name = "Descripción")]
        public string? WheelTemplateDescription { get; set; }
        
        public List<DimensionToScoreViewModel> DimensionsToScore { get; set; } = new List<DimensionToScoreViewModel>();
        
        [Display(Name = "Notas/Reflexiones")]
        [DataType(DataType.MultilineText)]
        public string? ClientNotes { get; set; }
        
        // For display purposes
        public DateTime AssignedAt { get; set; }
        public bool IsCompleted { get; set; }
    }
    
    public class DimensionToScoreViewModel
    {
        public long DimensionId { get; set; }
        
        [Display(Name = "Dimensión")]
        public string DimensionName { get; set; } = string.Empty;
        
        [Display(Name = "Pregunta Guía")]
        public string? GuidingQuestion { get; set; }
        
        [Required(ErrorMessage = "La puntuación es requerida")]
        [Range(1, 10, ErrorMessage = "La puntuación debe estar entre 1 y 10")]
        [Display(Name = "Puntuación")]
        public int Score { get; set; } = 5; // Default middle value
        
        public int Order { get; set; }
    }
    
    public class ViewWheelViewModel
    {
        public long InstanceId { get; set; }
        public string WheelTemplateName { get; set; } = string.Empty;
        public string? WheelTemplateDescription { get; set; }
        public DateTime CompletedAt { get; set; }
        public string? ClientNotes { get; set; }
        
        // For the radar chart
        public List<string> DimensionLabels { get; set; } = new List<string>();
        public List<int> DimensionScores { get; set; } = new List<int>();
        
        // Additional info
        public string AssignedByCoachName { get; set; } = string.Empty;
        public DateTime AssignedAt { get; set; }
    }
    
    public class ClientWheelsIndexViewModel
    {
        public List<ClientWheelInstanceViewModel> PendingWheels { get; set; } = new List<ClientWheelInstanceViewModel>();
        public List<ClientWheelInstanceViewModel> InProgressWheels { get; set; } = new List<ClientWheelInstanceViewModel>();
        public List<ClientWheelInstanceViewModel> CompletedWheels { get; set; } = new List<ClientWheelInstanceViewModel>();
        public int TotalCount { get; set; }
    }
}