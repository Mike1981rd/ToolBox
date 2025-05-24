using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Services
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserAnswerService> _logger;

        public UserAnswerService(ApplicationDbContext context, ILogger<UserAnswerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<UserAnswer>> GetUserAnswersByAreaAsync(int userId, int lifeAreaId)
        {
            return await _context.UserAnswers
                .Include(ua => ua.Question)
                .Where(ua => ua.UserId == userId && ua.Question!.LifeAreaId == lifeAreaId)
                .OrderBy(ua => ua.Question!.Id)
                .ToListAsync();
        }

        public async Task<UserAnswer?> GetUserAnswerAsync(int userId, int questionId)
        {
            return await _context.UserAnswers
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.QuestionId == questionId);
        }

        public async Task<(bool Success, int SavedCount, int UpdatedCount)> SaveUserAnswersAsync(int userId, int areaId, List<UserAnswerViewModel> answers)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _logger.LogInformation("Starting SaveUserAnswersAsync for User {UserId}, Area {AreaId}, Answer count: {Count}", 
                    userId, areaId, answers?.Count ?? 0);

                int savedCount = 0;
                int updatedCount = 0;
                int skippedCount = 0;
                var now = DateTime.UtcNow;

                // Get all question IDs for validation
                var validQuestionIds = await _context.Questions
                    .Where(q => q.LifeAreaId == areaId)
                    .Select(q => q.Id)
                    .ToListAsync();

                _logger.LogInformation("Valid question IDs for area {AreaId}: {QuestionIds}", 
                    areaId, string.Join(", ", validQuestionIds));
                    
                if (!validQuestionIds.Any())
                {
                    _logger.LogWarning("No questions found for area {AreaId}", areaId);
                }

                foreach (var answer in answers)
                {
                    // Validate that the question belongs to the specified area
                    if (!validQuestionIds.Contains(answer.QuestionId))
                    {
                        _logger.LogWarning("Skipping answer for question {QuestionId} - not in area {AreaId}", 
                            answer.QuestionId, areaId);
                        skippedCount++;
                        continue;
                    }

                    // Check if answer already exists
                    var existingAnswer = await _context.UserAnswers
                        .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.QuestionId == answer.QuestionId);

                    if (existingAnswer != null)
                    {
                        // Only update if the score has changed
                        if (existingAnswer.Score != answer.Score)
                        {
                            _logger.LogDebug("Updating answer for User {UserId}, Question {QuestionId}: {OldScore} -> {NewScore}", 
                                userId, answer.QuestionId, existingAnswer.Score, answer.Score);
                            
                            existingAnswer.Score = answer.Score;
                            existingAnswer.UpdatedAt = now;
                            _context.UserAnswers.Update(existingAnswer);
                            updatedCount++;
                        }
                        else
                        {
                            _logger.LogDebug("Answer for User {UserId}, Question {QuestionId} unchanged (score: {Score})", 
                                userId, answer.QuestionId, answer.Score);
                        }
                    }
                    else
                    {
                        // Create new answer
                        _logger.LogDebug("Creating new answer for User {UserId}, Question {QuestionId}, Score {Score}", 
                            userId, answer.QuestionId, answer.Score);
                            
                        var newAnswer = new UserAnswer
                        {
                            UserId = userId,
                            QuestionId = answer.QuestionId,
                            Score = answer.Score,
                            AnsweredAt = now,
                            UpdatedAt = now
                        };
                        _context.UserAnswers.Add(newAnswer);
                        savedCount++;
                    }
                }

                if (skippedCount > 0)
                {
                    _logger.LogWarning("Skipped {SkippedCount} answers due to invalid question IDs", skippedCount);
                }

                // Check if all answers were skipped
                if (savedCount == 0 && updatedCount == 0 && skippedCount > 0)
                {
                    _logger.LogError("All answers were skipped - no valid questions found for the provided IDs");
                    await transaction.RollbackAsync();
                    return (false, 0, 0);
                }

                int changes = await _context.SaveChangesAsync();
                _logger.LogInformation("SaveChangesAsync returned {Changes} changes", changes);
                
                await transaction.CommitAsync();
                _logger.LogInformation("Transaction committed successfully. Saved: {SavedCount}, Updated: {UpdatedCount}", 
                    savedCount, updatedCount);
                
                // Return true even if no changes were made (e.g., same values were submitted)
                // This is a valid scenario and not an error
                return (true, savedCount, updatedCount);
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                _logger.LogError(dbEx, "Database update error in SaveUserAnswersAsync for User {UserId}, Area {AreaId}", 
                    userId, areaId);
                
                // Log inner exception details
                if (dbEx.InnerException != null)
                {
                    _logger.LogError("Inner exception: {InnerException}", dbEx.InnerException.Message);
                }
                
                return (false, 0, 0);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Unexpected error in SaveUserAnswersAsync for User {UserId}, Area {AreaId}", 
                    userId, areaId);
                return (false, 0, 0);
            }
        }

        public async Task<bool> DeleteUserAnswerAsync(int userId, int questionId)
        {
            var answer = await _context.UserAnswers
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.QuestionId == questionId);
                
            if (answer == null)
                return false;
                
            _context.UserAnswers.Remove(answer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Dictionary<int, int>> GetUserAnswersForQuestionsAsync(int userId, List<int> questionIds)
        {
            return await _context.UserAnswers
                .Where(ua => ua.UserId == userId && questionIds.Contains(ua.QuestionId))
                .ToDictionaryAsync(ua => ua.QuestionId, ua => ua.Score);
        }

        public async Task<(int TotalQuestions, int AnsweredQuestions)> GetAreaProgressAsync(int userId, int lifeAreaId)
        {
            var totalQuestions = await _context.Questions
                .CountAsync(q => q.LifeAreaId == lifeAreaId);
                
            var answeredQuestions = await _context.UserAnswers
                .Include(ua => ua.Question)
                .CountAsync(ua => ua.UserId == userId && ua.Question!.LifeAreaId == lifeAreaId);
                
            return (totalQuestions, answeredQuestions);
        }
    }
}