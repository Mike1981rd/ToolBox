using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ToolBox.Data;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    [Authorize]
    public class LifeEventsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public LifeEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LifeEvents
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            
            var events = await _context.LifeEvents
                .Where(le => le.UserId == userId)
                .OrderBy(le => le.EventYear)
                .ToListAsync();

            var viewModels = events.Select(e => new LifeEventViewModel
            {
                Id = e.Id,
                EventTitle = e.EventTitle,
                Description = e.Description,
                EventYear = e.EventYear,
                ImpactScore = e.ImpactScore
            }).ToList();

            // Prepare chart data
            var chartLabels = events.Select(e => e.EventYear).ToArray();
            var chartData = events.Select(e => e.ImpactScore).ToArray();

            var indexViewModel = new LifeEventIndexViewModel
            {
                Events = viewModels,
                TotalEvents = events.Count,
                PositiveEvents = events.Count(e => e.ImpactScore > 0),
                NegativeEvents = events.Count(e => e.ImpactScore < 0),
                NeutralEvents = events.Count(e => e.ImpactScore == 0),
                ChartLabels = JsonSerializer.Serialize(chartLabels),
                ChartData = JsonSerializer.Serialize(chartData)
            };

            return View(indexViewModel);
        }

        // GET: LifeEvents/Create
        public IActionResult Create()
        {
            var viewModel = new LifeEventViewModel
            {
                EventYear = DateTime.Now.Year
            };
            return View(viewModel);
        }

        // POST: LifeEvents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LifeEventViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = GetCurrentUserId();
                
                var lifeEvent = new LifeEvent
                {
                    UserId = userId,
                    EventTitle = viewModel.EventTitle,
                    Description = viewModel.Description,
                    EventYear = viewModel.EventYear,
                    ImpactScore = viewModel.ImpactScore,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.LifeEvents.Add(lifeEvent);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Evento agregado exitosamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: LifeEvents/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var userId = GetCurrentUserId();
            
            var lifeEvent = await _context.LifeEvents
                .FirstOrDefaultAsync(le => le.Id == id && le.UserId == userId);

            if (lifeEvent == null)
            {
                return NotFound();
            }

            var viewModel = new LifeEventViewModel
            {
                Id = lifeEvent.Id,
                EventTitle = lifeEvent.EventTitle,
                Description = lifeEvent.Description,
                EventYear = lifeEvent.EventYear,
                ImpactScore = lifeEvent.ImpactScore
            };

            return View(viewModel);
        }

        // POST: LifeEvents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LifeEventViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = GetCurrentUserId();
                
                var lifeEvent = await _context.LifeEvents
                    .FirstOrDefaultAsync(le => le.Id == viewModel.Id && le.UserId == userId);

                if (lifeEvent == null)
                {
                    return NotFound();
                }

                lifeEvent.EventTitle = viewModel.EventTitle;
                lifeEvent.Description = viewModel.Description;
                lifeEvent.EventYear = viewModel.EventYear;
                lifeEvent.ImpactScore = viewModel.ImpactScore;
                lifeEvent.UpdatedAt = DateTime.UtcNow;

                _context.Update(lifeEvent);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Evento actualizado exitosamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: LifeEvents/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var userId = GetCurrentUserId();
            
            var lifeEvent = await _context.LifeEvents
                .FirstOrDefaultAsync(le => le.Id == id && le.UserId == userId);

            if (lifeEvent == null)
            {
                return NotFound();
            }

            var viewModel = new LifeEventViewModel
            {
                Id = lifeEvent.Id,
                EventTitle = lifeEvent.EventTitle,
                Description = lifeEvent.Description,
                EventYear = lifeEvent.EventYear,
                ImpactScore = lifeEvent.ImpactScore
            };

            return View(viewModel);
        }

        // POST: LifeEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var userId = GetCurrentUserId();
            
            var lifeEvent = await _context.LifeEvents
                .FirstOrDefaultAsync(le => le.Id == id && le.UserId == userId);

            if (lifeEvent == null)
            {
                return NotFound();
            }

            _context.LifeEvents.Remove(lifeEvent);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Evento eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

    }
}