using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToolBox.Models.ViewModels
{
    // Para la lista en Index
    public class Feedback360InstanceSummaryViewModel
    {
        public long Id { get; set; }
        public string InstanceTitle { get; set; } = string.Empty;
        public string SubjectUserName { get; set; } = string.Empty;
        public Feedback360Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FeedbackDeadline { get; set; }
        public int TotalRaters { get; set; }
        public int CompletedRaters { get; set; }
        
        // Calculated property for progress percentage
        public int ProgressPercentage => TotalRaters > 0 ? (CompletedRaters * 100) / TotalRaters : 0;
        
        // Helper for status badge color
        public string StatusBadgeClass
        {
            get
            {
                return Status switch
                {
                    Feedback360Status.PendingSetup => "bg-warning",
                    Feedback360Status.CollectingFeedback => "bg-info",
                    Feedback360Status.GeneratingReport => "bg-secondary",
                    Feedback360Status.Completed => "bg-success",
                    _ => "bg-secondary"
                };
            }
        }
    }

    // Para iniciar un nuevo proceso
    public class InitiateFeedback360ViewModel
    {
        [Required(ErrorMessage = "Please select a client")]
        [Display(Name = "Client (Subject)")]
        public int SelectedSubjectUserId { get; set; }
        
        public List<SelectListItem> PotentialSubjects { get; set; } = new List<SelectListItem>();
        
        [Required(ErrorMessage = "Please enter a title for this feedback process")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        [Display(Name = "Instance Title")]
        public string InstanceTitle { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please select a deadline")]
        [Display(Name = "Feedback Deadline")]
        [DataType(DataType.Date)]
        public DateTime FeedbackDeadline { get; set; } = DateTime.Now.AddDays(14);
    }

    // Para gestionar evaluadores
    public class ManageRatersViewModel
    {
        public long InstanceId { get; set; }
        public string InstanceTitle { get; set; } = string.Empty;
        public string SubjectUserName { get; set; } = string.Empty;
        public DateTime? FeedbackDeadline { get; set; }
        public Feedback360Status Status { get; set; }
        
        // Para el formulario de agregar evaluador
        public AddRaterViewModel NewRater { get; set; } = new AddRaterViewModel();
        
        // Lista de evaluadores actuales
        public List<Feedback360RaterViewModel> CurrentRaters { get; set; } = new List<Feedback360RaterViewModel>();
        
        // Para el dropdown de categorías
        public List<SelectListItem> RaterCategories { get; set; } = new List<SelectListItem>();
        
        // Para el dropdown de usuarios existentes (opcional)
        public List<SelectListItem> ExistingUsers { get; set; } = new List<SelectListItem>();
        
        // Helper properties
        public bool CanAddRaters => Status == Feedback360Status.PendingSetup;
        public bool CanSendInvitations => Status == Feedback360Status.PendingSetup && CurrentRaters.Count >= 3;
        public bool HasSelfEvaluation => CurrentRaters.Any(r => r.Category == RaterCategory.Self);
    }

    // Para agregar un evaluador
    public class AddRaterViewModel
    {
        public long InstanceId { get; set; }
        
        [Display(Name = "Rater Name")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string? RaterName { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string RaterEmail { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please select a category")]
        [Display(Name = "Rater Category")]
        public RaterCategory Category { get; set; }
        
        // Optional: for selecting existing user
        [Display(Name = "Or Select Existing User")]
        public int? ExistingUserId { get; set; }
    }

    // Para mostrar un evaluador en la lista
    public class Feedback360RaterViewModel
    {
        public long Id { get; set; }
        public string RaterNameOrEmail { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RaterCategory Category { get; set; }
        public RaterStatus Status { get; set; }
        public DateTime? InvitedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool CanRemove { get; set; }
        
        // Helper for category display
        public string CategoryDisplay
        {
            get
            {
                return Category switch
                {
                    RaterCategory.Self => "Self-Evaluation",
                    RaterCategory.Manager => "Manager",
                    RaterCategory.Peer => "Peer/Colleague",
                    RaterCategory.DirectReport => "Direct Report",
                    RaterCategory.Client => "Client",
                    RaterCategory.Other => "Other",
                    _ => Category.ToString()
                };
            }
        }
        
        // Helper for status badge
        public string StatusBadgeClass
        {
            get
            {
                return Status switch
                {
                    RaterStatus.PendingInvitation => "bg-secondary",
                    RaterStatus.Invited => "bg-info",
                    RaterStatus.InProgress => "bg-warning",
                    RaterStatus.Completed => "bg-success",
                    _ => "bg-secondary"
                };
            }
        }
    }

    // Response models for AJAX
    public class AddRaterResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Feedback360RaterViewModel? Rater { get; set; }
    }

    public class RemoveRaterResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}