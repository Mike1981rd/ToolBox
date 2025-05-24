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
    public class LifeAreaService : ILifeAreaService
    {
        private readonly ApplicationDbContext _context;

        public LifeAreaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LifeArea>> GetAllLifeAreasAsync(bool includeInactive = false)
        {
            var query = _context.LifeAreas.AsQueryable();
            
            if (!includeInactive)
            {
                query = query.Where(la => la.IsActive);
            }
            
            return await query
                .OrderBy(la => la.DisplayOrder)
                .ThenBy(la => la.Title)
                .ToListAsync();
        }

        public async Task<LifeArea?> GetLifeAreaByIdAsync(int id)
        {
            return await _context.LifeAreas
                .Include(la => la.CreatedByUser)
                .FirstOrDefaultAsync(la => la.Id == id);
        }

        public async Task<bool> CreateLifeAreaAsync(LifeArea lifeArea)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                lifeArea.CreatedAt = DateTime.UtcNow;
                lifeArea.UpdatedAt = DateTime.UtcNow;
                
                if (lifeArea.DisplayOrder == 0)
                {
                    lifeArea.DisplayOrder = await GetNextDisplayOrderAsync();
                }

                _context.LifeAreas.Add(lifeArea);
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

        public async Task<bool> UpdateLifeAreaAsync(LifeArea lifeArea)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingLifeArea = await _context.LifeAreas.FindAsync(lifeArea.Id);
                if (existingLifeArea == null)
                {
                    return false;
                }

                existingLifeArea.Title = lifeArea.Title;
                existingLifeArea.Description = lifeArea.Description;
                existingLifeArea.IconClass = lifeArea.IconClass;
                existingLifeArea.IconColor = lifeArea.IconColor;
                existingLifeArea.IsActive = lifeArea.IsActive;
                existingLifeArea.DisplayOrder = lifeArea.DisplayOrder;
                existingLifeArea.UpdatedAt = DateTime.UtcNow;

                _context.LifeAreas.Update(existingLifeArea);
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

        public async Task<bool> DeleteLifeAreaAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var lifeArea = await _context.LifeAreas.FindAsync(id);
                if (lifeArea == null)
                {
                    return false;
                }

                _context.LifeAreas.Remove(lifeArea);
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

        public async Task<(bool Success, string Message, bool NewState)> ToggleLifeAreaStatusAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var lifeArea = await _context.LifeAreas.FindAsync(id);
                if (lifeArea == null)
                {
                    return (false, "Área de vida no encontrada.", false);
                }

                lifeArea.IsActive = !lifeArea.IsActive;
                lifeArea.UpdatedAt = DateTime.UtcNow;

                _context.LifeAreas.Update(lifeArea);
                int changes = await _context.SaveChangesAsync();

                if (changes > 0)
                {
                    await transaction.CommitAsync();
                    string statusMessage = lifeArea.IsActive ? "Activa" : "Inactiva";
                    return (true, statusMessage, lifeArea.IsActive);
                }
                
                await transaction.RollbackAsync();
                return (false, "No se pudo actualizar el estado del área de vida.", lifeArea.IsActive);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Error al actualizar el estado: {ex.Message}", false);
            }
        }

        public async Task<bool> IsTitleTakenAsync(string title, int? excludeId = null)
        {
            var query = _context.LifeAreas.AsQueryable();
            
            if (excludeId.HasValue)
            {
                query = query.Where(la => la.Id != excludeId.Value);
            }
            
            return await query.AnyAsync(la => la.Title.ToLower() == title.ToLower());
        }

        public async Task<int> GetNextDisplayOrderAsync()
        {
            var maxOrder = await _context.LifeAreas.MaxAsync(la => (int?)la.DisplayOrder) ?? 0;
            return maxOrder + 1;
        }

        public async Task<bool> UpdateDisplayOrderAsync(int id, int newOrder)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var lifeArea = await _context.LifeAreas.FindAsync(id);
                if (lifeArea == null)
                {
                    return false;
                }

                lifeArea.DisplayOrder = newOrder;
                lifeArea.UpdatedAt = DateTime.UtcNow;

                _context.LifeAreas.Update(lifeArea);
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
    }
}