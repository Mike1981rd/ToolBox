using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToolBox.Models.ViewModels
{
    public class CommunicationWheelTemplateViewModel
    {
        public long Id { get; set; }
        
        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombre de la Plantilla")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder los 200 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        
        public List<CommunicationDimensionViewModel> Dimensions { get; set; } = new List<CommunicationDimensionViewModel>();
        
        // For display purposes
        public int DimensionCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class CommunicationDimensionViewModel
    {
        public long Id { get; set; }
        public long CommunicationWheelTemplateId { get; set; }
        
        [Required(ErrorMessage = "El nombre de la dimensión es requerido")]
        [Display(Name = "Nombre de la Dimensión")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Pregunta Guía")]
        [DataType(DataType.MultilineText)]
        public string? GuidingQuestion { get; set; }
        
        [Required(ErrorMessage = "El orden es requerido")]
        [Display(Name = "Orden")]
        [Range(1, 100, ErrorMessage = "El orden debe estar entre 1 y 100")]
        public int Order { get; set; }
    }
    
    public class CommunicationWheelTemplateListViewModel
    {
        public List<CommunicationWheelTemplateListItem> Templates { get; set; } = new List<CommunicationWheelTemplateListItem>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
    }
    
    public class CommunicationWheelTemplateListItem
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DimensionCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class DimensionReorderViewModel
    {
        public List<long> DimensionIds { get; set; } = new List<long>();
    }
    
    public class ReorderDimensionsRequest
    {
        public long TemplateId { get; set; }
        public List<long> DimensionIds { get; set; } = new List<long>();
    }
    
    public class AssignCommunicationWheelViewModel
    {
        public long TemplateId { get; set; }
        
        [Display(Name = "Plantilla")]
        public string TemplateName { get; set; } = string.Empty;
        
        [Display(Name = "Descripción")]
        public string? TemplateDescription { get; set; }
        
        [Required(ErrorMessage = "Por favor selecciona al menos un cliente")]
        [Display(Name = "Seleccionar Clientes")]
        public List<int> SelectedClientIds { get; set; } = new List<int>();
        
        public List<SelectListItem> PotentialClients { get; set; } = new List<SelectListItem>();
        
        // For displaying already assigned clients
        public List<AssignedClientInfo> AlreadyAssignedClients { get; set; } = new List<AssignedClientInfo>();
    }
    
    public class AssignedClientInfo
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public WheelCompletionStatus Status { get; set; }
        public DateTime AssignedAt { get; set; }
    }
    
    public class CreateDimensionRequest
    {
        public long TemplateId { get; set; }
        
        [Required(ErrorMessage = "El nombre de la dimensión es requerido")]
        [Display(Name = "Nombre de la Dimensión")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Pregunta Guía")]
        [DataType(DataType.MultilineText)]
        public string? GuidingQuestion { get; set; }
    }
    
    public class UpdateDimensionRequest
    {
        public long DimensionId { get; set; }
        
        [Required(ErrorMessage = "El nombre de la dimensión es requerido")]
        [Display(Name = "Nombre de la Dimensión")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Pregunta Guía")]
        [DataType(DataType.MultilineText)]
        public string? GuidingQuestion { get; set; }
    }
    
    public class DeleteDimensionRequest
    {
        public long DimensionId { get; set; }
    }
}