using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _context;

        public QuestionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _context.Questions
                .Include(q => q.LifeArea)
                .OrderBy(q => q.LifeAreaId)
                .ThenBy(q => q.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsByLifeAreaAsync(int lifeAreaId)
        {
            return await _context.Questions
                .Include(q => q.LifeArea)
                .Where(q => q.LifeAreaId == lifeAreaId)
                .OrderBy(q => q.Id)
                .ToListAsync();
        }

        public async Task<Question?> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions
                .Include(q => q.LifeArea)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> CreateQuestionAsync(Question question)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                question.CreatedAt = DateTime.UtcNow;
                question.UpdatedAt = DateTime.UtcNow;
                
                _context.Questions.Add(question);
                int changes = await _context.SaveChangesAsync();
                
                if (changes > 0)
                {
                    await transaction.CommitAsync();
                    return true;
                }
                
                await transaction.RollbackAsync();
                return false;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingQuestion = await _context.Questions.FindAsync(question.Id);
                if (existingQuestion == null)
                    return false;
                
                existingQuestion.QuestionText = question.QuestionText;
                existingQuestion.LifeAreaId = question.LifeAreaId;
                existingQuestion.UpdatedAt = DateTime.UtcNow;
                
                _context.Questions.Update(existingQuestion);
                int changes = await _context.SaveChangesAsync();
                
                if (changes > 0)
                {
                    await transaction.CommitAsync();
                    return true;
                }
                
                await transaction.RollbackAsync();
                return false;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var question = await _context.Questions.FindAsync(id);
                if (question == null)
                    return false;
                
                _context.Questions.Remove(question);
                int changes = await _context.SaveChangesAsync();
                
                if (changes > 0)
                {
                    await transaction.CommitAsync();
                    return true;
                }
                
                await transaction.RollbackAsync();
                return false;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<IEnumerable<Question>> SearchQuestionsAsync(string searchTerm, int? lifeAreaId = null)
        {
            var query = _context.Questions.Include(q => q.LifeArea).AsQueryable();
            
            if (lifeAreaId.HasValue)
            {
                query = query.Where(q => q.LifeAreaId == lifeAreaId.Value);
            }
            
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(q => q.QuestionText.ToLower().Contains(searchTerm));
            }
            
            return await query.OrderBy(q => q.LifeAreaId).ThenBy(q => q.Id).ToListAsync();
        }
    }
}