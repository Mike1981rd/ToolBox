using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    public class SessionsIndexViewModel
    {
        public List<Session> Sessions { get; set; } = new List<Session>();
        public string? CurrentSearchQuery { get; set; }
        public string? CurrentStatusFilter { get; set; }
    }

    public class SessionCreateViewModel
    {
        [Required(ErrorMessage = "El cliente es requerido")]
        [Display(Name = "Cliente")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "La fecha y hora de la sesión es requerida")]
        [Display(Name = "Fecha y hora de la sesión")]
        [DataType(DataType.DateTime)]
        public DateTime SessionDateTime { get; set; } = DateTime.Now;

        [Display(Name = "Fecha y hora de la próxima sesión")]
        [DataType(DataType.DateTime)]
        public DateTime? NextSessionDateTime { get; set; }

        [Required(ErrorMessage = "Los puntos clave son requeridos")]
        [Display(Name = "Puntos clave")]
        public string KeyPoints { get; set; } = string.Empty;

        [Display(Name = "Formas de desarrollar")]
        public string? WaysToDevelop { get; set; }

        [Display(Name = "Asignaciones")]
        public string? Assignments { get; set; }

        [Display(Name = "Desafíos")]
        public string? Challenges { get; set; }

        [Display(Name = "Retroalimentación")]
        public string? Feedback { get; set; }

        [Display(Name = "Twitter")]
        [StringLength(255, ErrorMessage = "El campo Twitter no puede exceder 255 caracteres")]
        public string? Twitter { get; set; }
    }

    public class SessionEditViewModel : SessionCreateViewModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public List<SessionFile>? SessionFiles { get; set; }
    }

    public class SessionDetailViewModel
    {
        public Session Session { get; set; } = null!;
        public List<SessionFile> SessionFiles { get; set; } = new List<SessionFile>();
    }
}