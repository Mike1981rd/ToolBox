using ToolBox.Models;

namespace ToolBox.Interfaces
{
    public interface IWelcomeMessageService
    {
        Task<WelcomeMessageSettings> GetSettingsAsync();
        Task<bool> UpdateSettingsAsync(WelcomeMessageSettings settings);
        Task<string> SaveVideoFileAsync(IFormFile videoFile);
        void DeleteOldVideoFile(string? filePath);
    }
}