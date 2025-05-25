using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class WebsiteConfigurationService : IWebsiteConfigurationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WebsiteConfigurationService> _logger;

        public WebsiteConfigurationService(ApplicationDbContext context, ILogger<WebsiteConfigurationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<WebsiteConfiguration> GetConfigurationAsync()
        {
            var configuration = await _context.WebsiteConfiguration
                .Include(w => w.LastUpdatedByUser)
                .FirstOrDefaultAsync();

            if (configuration == null)
            {
                // Create default configuration if none exists
                configuration = new WebsiteConfiguration
                {
                    SiteName = "Toolbox",
                    SiteDescription = "Your personal development platform",
                    DefaultLanguage = "es",
                    DefaultTimeZone = "America/Mexico_City",
                    LastUpdated = DateTime.UtcNow
                };

                _context.WebsiteConfiguration.Add(configuration);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Created default website configuration");
            }

            return configuration;
        }

        public async Task<WebsiteConfiguration> UpdateConfigurationAsync(WebsiteConfiguration configuration)
        {
            var existingConfig = await _context.WebsiteConfiguration.FirstOrDefaultAsync();
            
            if (existingConfig == null)
            {
                existingConfig = new WebsiteConfiguration();
                _context.WebsiteConfiguration.Add(existingConfig);
            }

            // Update all fields
            existingConfig.SiteEmail = configuration.SiteEmail;
            existingConfig.SitePhone = configuration.SitePhone;
            existingConfig.SiteAddress = configuration.SiteAddress;
            existingConfig.FooterMessage = configuration.FooterMessage;
            
            // Social media
            existingConfig.FacebookUrl = configuration.FacebookUrl;
            existingConfig.TwitterUrl = configuration.TwitterUrl;
            existingConfig.GoogleUrl = configuration.GoogleUrl;
            existingConfig.LinkedInUrl = configuration.LinkedInUrl;
            existingConfig.YouTubeUrl = configuration.YouTubeUrl;
            existingConfig.InstagramUrl = configuration.InstagramUrl;
            existingConfig.TelegramUrl = configuration.TelegramUrl;
            existingConfig.TikTokUrl = configuration.TikTokUrl;
            existingConfig.DiscordUrl = configuration.DiscordUrl;
            existingConfig.RedditUrl = configuration.RedditUrl;
            
            // Branding (logo path is handled separately)
            existingConfig.SiteName = configuration.SiteName;
            existingConfig.SiteDescription = configuration.SiteDescription;
            
            // Additional settings
            existingConfig.DefaultTimeZone = configuration.DefaultTimeZone;
            existingConfig.DefaultLanguage = configuration.DefaultLanguage;
            
            // Audit
            existingConfig.LastUpdated = DateTime.UtcNow;
            existingConfig.LastUpdatedByUserId = configuration.LastUpdatedByUserId;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated website configuration");
            
            return existingConfig;
        }

        public async Task<bool> UpdateLogoAsync(string logoPath)
        {
            var configuration = await GetConfigurationAsync();
            configuration.LogoPath = logoPath;
            configuration.LastUpdated = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ResetLogoAsync()
        {
            var configuration = await GetConfigurationAsync();
            configuration.LogoPath = null;
            configuration.LastUpdated = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }
    }
}