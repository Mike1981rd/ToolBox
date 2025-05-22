using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class VideoViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Description { get; set; }

        [StringLength(500)]
        public string? VideoUrl { get; set; }

        [StringLength(500)]
        public string? VideoFilePath { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public string AuthorName { get; set; } = string.Empty;

        [Required]
        public int TopicId { get; set; }

        public string TopicName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string MediaType { get; set; } = "youtube"; // youtube, vimeo, uploadedfile

        public bool IsFeatured { get; set; } = false;

        [StringLength(10)]
        public string? Duration { get; set; } // Format: mm:ss

        public DateTime UploadDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        // SEO Fields
        [StringLength(200)]
        public string? MetaTitle { get; set; }

        [StringLength(500)]
        public string? MetaDescription { get; set; }

        [StringLength(500)]
        public string? MetaKeywords { get; set; }

        // Additional properties for display
        public string? ThumbnailUrl { get; set; }

        public int ViewCount { get; set; } = 0;

        public string Status { get; set; } = "published"; // draft, published, archived

        // Properties for file upload simulation
        public string? UploadedFileName { get; set; }

        public long? FileSizeBytes { get; set; }
    }

    public class VideoAuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class VideoTopicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}