using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class SaveDefaultLanguageRequest
    {
        [Required]
        [StringLength(5)]
        public string Language { get; set; } = string.Empty;
    }
}