using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Services
{
    public class WheelOfLifeService : IWheelOfLifeService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WheelOfLifeService> _logger;
        private readonly ILifeAreaService _lifeAreaService;

        public WheelOfLifeService(
            ApplicationDbContext context, 
            ILogger<WheelOfLifeService> logger,
            ILifeAreaService lifeAreaService)
        {
            _context = context;
            _logger = logger;
            _lifeAreaService = lifeAreaService;
        }

        public async Task<WheelOfLifeDataViewModel> GetWheelDataForUserAsync(int userId)
        {
            try
            {
                _logger.LogInformation("Getting wheel data for user {UserId}", userId);

                // Get all active life areas
                var lifeAreas = await _lifeAreaService.GetAllLifeAreasAsync(includeInactive: false);
                var lifeAreasList = lifeAreas.ToList();

                // Get user's existing scores
                var userScores = await _context.WheelOfLifeScores
                    .Where(s => s.UserId == userId)
                    .ToDictionaryAsync(s => s.LifeAreaId, s => s.Score);

                // Build the view model
                var areas = new List<WheelOfLifeAreaScoreViewModel>();
                int totalScore = 0;
                int scoredAreas = 0;

                foreach (var area in lifeAreasList)
                {
                    var areaScore = new WheelOfLifeAreaScoreViewModel
                    {
                        LifeAreaId = area.Id,
                        AreaName = area.Title,
                        IconClass = area.IconClass,
                        IconColor = area.IconColor,
                        CurrentScore = userScores.ContainsKey(area.Id) ? userScores[area.Id] : null
                    };

                    areas.Add(areaScore);

                    if (areaScore.CurrentScore.HasValue)
                    {
                        totalScore += areaScore.CurrentScore.Value;
                        scoredAreas++;
                    }
                }

                var result = new WheelOfLifeDataViewModel
                {
                    Areas = areas,
                    TotalScore = totalScore,
                    AreasCount = lifeAreasList.Count,
                    AverageScore = scoredAreas > 0 ? Math.Round((decimal)totalScore / scoredAreas, 1) : 0
                };

                _logger.LogInformation("Retrieved {AreasCount} areas with {ScoredAreas} scored for user {UserId}", 
                    lifeAreasList.Count, scoredAreas, userId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting wheel data for user {UserId}", userId);
                throw;
            }
        }

        public async Task<(bool Success, int SavedCount, int UpdatedCount)> SaveUserScoresAsync(int userId, List<SaveWheelScoreViewModel> scores)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _logger.LogInformation("Starting SaveUserScoresAsync for User {UserId}, Scores count: {Count}", 
                    userId, scores?.Count ?? 0);

                int savedCount = 0;
                int updatedCount = 0;
                var now = DateTime.UtcNow;

                // Get all valid life area IDs
                var validLifeAreaIds = await _context.LifeAreas
                    .Where(la => la.IsActive)
                    .Select(la => la.Id)
                    .ToListAsync();

                _logger.LogInformation("Valid life area IDs: {LifeAreaIds}", 
                    string.Join(", ", validLifeAreaIds));

                foreach (var score in scores)
                {
                    // Validate that the life area is valid
                    if (!validLifeAreaIds.Contains(score.LifeAreaId))
                    {
                        _logger.LogWarning("Skipping score for invalid life area {LifeAreaId}", score.LifeAreaId);
                        continue;
                    }

                    // Check if score already exists
                    var existingScore = await _context.WheelOfLifeScores
                        .FirstOrDefaultAsync(s => s.UserId == userId && s.LifeAreaId == score.LifeAreaId);

                    if (existingScore != null)
                    {
                        // Only update if the score has changed
                        if (existingScore.Score != score.Score)
                        {
                            _logger.LogDebug("Updating score for User {UserId}, LifeArea {LifeAreaId}: {OldScore} -> {NewScore}", 
                                userId, score.LifeAreaId, existingScore.Score, score.Score);
                            
                            existingScore.Score = score.Score;
                            existingScore.UpdatedAt = now;
                            _context.WheelOfLifeScores.Update(existingScore);
                            updatedCount++;
                        }
                        else
                        {
                            _logger.LogDebug("Score for User {UserId}, LifeArea {LifeAreaId} unchanged (score: {Score})", 
                                userId, score.LifeAreaId, score.Score);
                        }
                    }
                    else
                    {
                        // Create new score
                        _logger.LogDebug("Creating new score for User {UserId}, LifeArea {LifeAreaId}, Score {Score}", 
                            userId, score.LifeAreaId, score.Score);
                            
                        var newScore = new WheelOfLifeScore
                        {
                            UserId = userId,
                            LifeAreaId = score.LifeAreaId,
                            Score = score.Score,
                            UpdatedAt = now
                        };
                        _context.WheelOfLifeScores.Add(newScore);
                        savedCount++;
                    }
                }

                int changes = await _context.SaveChangesAsync();
                _logger.LogInformation("SaveChangesAsync returned {Changes} changes", changes);
                
                await transaction.CommitAsync();
                _logger.LogInformation("Transaction committed successfully. Saved: {SavedCount}, Updated: {UpdatedCount}", 
                    savedCount, updatedCount);
                
                return (true, savedCount, updatedCount);
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                _logger.LogError(dbEx, "Database update error in SaveUserScoresAsync for User {UserId}", userId);
                
                if (dbEx.InnerException != null)
                {
                    _logger.LogError("Inner exception: {InnerException}", dbEx.InnerException.Message);
                }
                
                return (false, 0, 0);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Unexpected error in SaveUserScoresAsync for User {UserId}", userId);
                return (false, 0, 0);
            }
        }

        public async Task<WheelOfLifeScore?> GetUserScoreForAreaAsync(int userId, int lifeAreaId)
        {
            return await _context.WheelOfLifeScores
                .FirstOrDefaultAsync(s => s.UserId == userId && s.LifeAreaId == lifeAreaId);
        }

        public async Task<bool> DeleteUserScoreAsync(int userId, int lifeAreaId)
        {
            var score = await _context.WheelOfLifeScores
                .FirstOrDefaultAsync(s => s.UserId == userId && s.LifeAreaId == lifeAreaId);
                
            if (score == null)
                return false;
                
            _context.WheelOfLifeScores.Remove(score);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}