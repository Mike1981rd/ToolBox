using System;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class WebsiteConfiguration
    {
        public int Id { get; set; }
        
        // General Site Information
        [MaxLength(100)]
        public string? SiteEmail { get; set; }
        
        [MaxLength(50)]
        public string? SitePhone { get; set; }
        
        [MaxLength(500)]
        public string? SiteAddress { get; set; }
        
        [MaxLength(1000)]
        public string? FooterMessage { get; set; }
        
        // Social Media Links
        [MaxLength(200)]
        public string? FacebookUrl { get; set; }
        
        [MaxLength(200)]
        public string? TwitterUrl { get; set; }
        
        [MaxLength(200)]
        public string? GoogleUrl { get; set; }
        
        [MaxLength(200)]
        public string? LinkedInUrl { get; set; }
        
        [MaxLength(200)]
        public string? YouTubeUrl { get; set; }
        
        [MaxLength(200)]
        public string? InstagramUrl { get; set; }
        
        [MaxLength(200)]
        public string? TelegramUrl { get; set; }
        
        [MaxLength(200)]
        public string? TikTokUrl { get; set; }
        
        [MaxLength(200)]
        public string? DiscordUrl { get; set; }
        
        [MaxLength(200)]
        public string? RedditUrl { get; set; }
        
        // Site Branding
        [MaxLength(500)]
        public string? LogoPath { get; set; }
        
        [MaxLength(200)]
        public string? SiteName { get; set; }
        
        [MaxLength(1000)]
        public string? SiteDescription { get; set; }
        
        // Additional Settings
        [MaxLength(100)]
        public string? DefaultTimeZone { get; set; }
        
        [MaxLength(10)]
        public string? DefaultLanguage { get; set; }
        
        // Audit fields
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        
        public int? LastUpdatedByUserId { get; set; }
        public virtual User? LastUpdatedByUser { get; set; }
    }
}