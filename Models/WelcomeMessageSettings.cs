namespace ToolBox.Models
{
    public class WelcomeMessageSettings
    {
        public int Id { get; set; } = 1; // Singleton - solo habrá un registro
        public string Title { get; set; } = string.Empty;
        public string DescriptionHTML { get; set; } = string.Empty;
        public string VideoType { get; set; } = "None"; // None, Upload, YouTube, Vimeo
        public string? VideoUrl { get; set; } // Para YouTube/Vimeo URLs
        public string? UploadedVideoFileName { get; set; }
        public string? UploadedVideoPath { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}