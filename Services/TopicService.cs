using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class TopicService : ITopicService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TopicService> _logger;

        public TopicService(ApplicationDbContext context, ILogger<TopicService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Topic?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Topics
                    .Include(t => t.CreatedByUser)
                    .FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting topic by id: {Id}", id);
                throw;
            }
        }

        public async Task<Topic> CreateAsync(Topic topic)
        {
            try
            {
                topic.CreatedAt = DateTime.UtcNow;
                _context.Topics.Add(topic);
                await _context.SaveChangesAsync();
                return topic;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating topic");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Topic topic)
        {
            try
            {
                var existingTopic = await _context.Topics.FindAsync(topic.Id);
                if (existingTopic == null)
                {
                    return false;
                }

                existingTopic.Name = topic.Name;
                existingTopic.Description = topic.Description;
                existingTopic.IsActive = topic.IsActive;
                existingTopic.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating topic: {Id}", topic.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var topic = await _context.Topics.FindAsync(id);
                if (topic == null)
                {
                    return false;
                }

                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting topic: {Id}", id);
                throw;
            }
        }

        public async Task<(IEnumerable<Topic> Topics, int TotalCount)> GetPaginatedAsync(
            string? searchTerm = null,
            int page = 1,
            int pageSize = 10,
            bool includeInactive = false)
        {
            try
            {
                var query = _context.Topics.AsQueryable();

                // Filter by active status
                if (!includeInactive)
                {
                    query = query.Where(t => t.IsActive);
                }

                // Search filter
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(t => 
                        t.Name.Contains(searchTerm) || 
                        (t.Description != null && t.Description.Contains(searchTerm)));
                }

                // Get total count for pagination
                var totalCount = await query.CountAsync();

                // Apply pagination
                var topics = await query
                    .OrderByDescending(t => t.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Include(t => t.CreatedByUser)
                    .ToListAsync();

                return (topics, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated topics");
                throw;
            }
        }

        public async Task<(bool Success, string Message, bool NewState)> ToggleStatusAsync(int id)
        {
            try
            {
                var topic = await _context.Topics.FindAsync(id);
                if (topic == null)
                {
                    return (false, "Tema no encontrado", false);
                }

                topic.IsActive = !topic.IsActive;
                topic.UpdatedAt = DateTime.UtcNow;
                
                await _context.SaveChangesAsync();
                
                var statusText = topic.IsActive ? "activado" : "desactivado";
                return (true, $"Tema {statusText} exitosamente", topic.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling topic status: {Id}", id);
                return (false, "Error al cambiar el estado del tema", false);
            }
        }

        public async Task<IEnumerable<Topic>> GetAllActiveAsync()
        {
            try
            {
                return await _context.Topics
                    .Where(t => t.IsActive)
                    .OrderBy(t => t.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all active topics");
                throw;
            }
        }
    }
}