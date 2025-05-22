using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class TopicViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public string Status { get; set; } = "active";

        public string? IconClass { get; set; }

        public string? IconBackground { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}