using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    public class LifeEventViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "El título del evento es requerido")]
        [Display(Name = "Título del Evento")]
        [StringLength(200, ErrorMessage = "El título no puede exceder los 200 caracteres")]
        public string EventTitle { get; set; } = string.Empty;

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El año del evento es requerido")]
        [Display(Name = "Año del Evento")]
        [Range(1900, 2100, ErrorMessage = "El año debe estar entre 1900 y 2100")]
        public int EventYear { get; set; } = DateTime.Now.Year;

        [Required(ErrorMessage = "El impacto es requerido")]
        [Display(Name = "Impacto Emocional")]
        [Range(-3, 3, ErrorMessage = "El impacto debe estar entre -3 y +3")]
        public int ImpactScore { get; set; }

        // Helper property for displaying impact score text
        public string ImpactScoreText => GetImpactScoreText(ImpactScore);

        // Helper property for CSS class
        public string ImpactScoreCssClass => GetImpactScoreCssClass(ImpactScore);

        public static string GetImpactScoreText(int score)
        {
            return score switch
            {
                3 => "Muy Positivo",
                2 => "Positivo",
                1 => "Ligeramente Positivo",
                0 => "Neutral",
                -1 => "Ligeramente Negativo",
                -2 => "Negativo",
                -3 => "Muy Negativo",
                _ => "Desconocido"
            };
        }

        public static string GetImpactScoreCssClass(int score)
        {
            return score switch
            {
                3 => "bg-success text-white",
                2 => "bg-success bg-opacity-75 text-white",
                1 => "bg-success bg-opacity-50",
                0 => "bg-secondary bg-opacity-50",
                -1 => "bg-danger bg-opacity-25",
                -2 => "bg-danger bg-opacity-50 text-white",
                -3 => "bg-danger text-white",
                _ => "bg-secondary"
            };
        }
    }

    public class LifeEventIndexViewModel
    {
        public List<LifeEventViewModel> Events { get; set; } = new List<LifeEventViewModel>();
        public int TotalEvents { get; set; }
        public int PositiveEvents { get; set; }
        public int NegativeEvents { get; set; }
        public int NeutralEvents { get; set; }
        
        // Data for the chart
        public string ChartLabels { get; set; } = string.Empty; // JSON array of years
        public string ChartData { get; set; } = string.Empty; // JSON array of impact scores
    }
}