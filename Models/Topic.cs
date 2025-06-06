using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class Topic
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Opcional: Si se requiere rastrear quién creó el tema
        public int? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
    }
}