using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToolBox.Models.ViewModels
{
    public class AssignedWheelsListViewModel
    {
        public List<AssignedWheelListItem> Wheels { get; set; } = new List<AssignedWheelListItem>();
        
        // Filters
        public long? SelectedTemplateId { get; set; }
        public int? SelectedClientId { get; set; }
        public string? SelectedStatus { get; set; }
        
        // Filter options
        public List<SelectListItem> TemplateOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ClientOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> StatusOptions { get; set; } = new List<SelectListItem>();
        
        // Pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
    }
    
    public class AssignedWheelListItem
    {
        public long InstanceId { get; set; }
        public string WheelTemplateName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        public DateTime AssignedAt { get; set; }
        public WheelCompletionStatus Status { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int DimensionCount { get; set; }
    }
    
    public class ViewClientWheelViewModel
    {
        public long InstanceId { get; set; }
        
        [Display(Name = "Plantilla")]
        public string WheelTemplateName { get; set; } = string.Empty;
        
        [Display(Name = "Descripción")]
        public string? WheelTemplateDescription { get; set; }
        
        [Display(Name = "Cliente")]
        public string ClientName { get; set; } = string.Empty;
        
        [Display(Name = "Email del Cliente")]
        public string ClientEmail { get; set; } = string.Empty;
        
        [Display(Name = "Fecha de Asignación")]
        public DateTime AssignedAt { get; set; }
        
        [Display(Name = "Fecha de Completado")]
        public DateTime? CompletedAt { get; set; }
        
        [Display(Name = "Notas del Cliente")]
        public string? ClientNotes { get; set; }
        
        // For the radar chart
        public List<string> DimensionLabelsForChart { get; set; } = new List<string>();
        public List<WheelChartSeries> SeriesForChart { get; set; } = new List<WheelChartSeries>();
        
        // For comparison selection
        public List<ClientWheelInstanceSelectionViewModel> AvailableInstancesForComparison { get; set; } = new List<ClientWheelInstanceSelectionViewModel>();
        public List<long> CurrentlyComparedInstanceIds { get; set; } = new List<long>();
        
        // Detailed dimension information
        public List<DimensionScoreDetailViewModel> DimensionDetails { get; set; } = new List<DimensionScoreDetailViewModel>();
        
        // Statistics
        public double AverageScore { get; set; }
        public int MaxScore { get; set; }
        public int MinScore { get; set; }
        public string? HighestDimension { get; set; }
        public string? LowestDimension { get; set; }
    }
    
    public class DimensionScoreDetailViewModel
    {
        public string DimensionName { get; set; } = string.Empty;
        public string? GuidingQuestion { get; set; }
        public int Score { get; set; }
        public int Order { get; set; }
        
        // For visual indicators
        public string ScoreColorClass
        {
            get
            {
                if (Score <= 3) return "text-danger";
                if (Score <= 6) return "text-warning";
                if (Score <= 8) return "text-info";
                return "text-success";
            }
        }
        
        public string ScoreBadgeClass
        {
            get
            {
                if (Score <= 3) return "bg-danger";
                if (Score <= 6) return "bg-warning";
                if (Score <= 8) return "bg-info";
                return "bg-success";
            }
        }
    }
    
    public class WheelChartSeries
    {
        public string SeriesLabel { get; set; } = string.Empty; // Ex: "Evaluación - 15 Ene 2023"
        public List<int> Scores { get; set; } = new List<int>(); // Puntuaciones para esta instancia
        public string BorderColor { get; set; } = string.Empty; // Color para la línea de esta serie
        public string BackgroundColor { get; set; } = string.Empty; // Color para el relleno (opcional)
        public DateTime CompletedAt { get; set; }
        public bool IsPrimary { get; set; } // To identify the main instance
    }
    
    public class ClientWheelInstanceSelectionViewModel
    {
        public long InstanceId { get; set; }
        public DateTime CompletedAt { get; set; }
        public string DisplayLabel => $"Evaluación del {CompletedAt:dd MMM yyyy}";
        public bool IsCurrentlyCompared { get; set; } // Para marcar en la UI si está seleccionada
        public double AverageScore { get; set; } // To show in the selection list
    }
}