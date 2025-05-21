using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using ToolBox.Data;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        
        // Acciones para Exportar
        public async Task<IActionResult> ExportToExcel()
        {
            var users = await _context.Users.ToListAsync();
            
            // En una implementación real, utilizaremos EPPlus o ClosedXML para generar Excel
            // Por ahora, crearemos un archivo CSV con formato simple
            var builder = new StringBuilder();
            builder.AppendLine("ID,Name,Email,Role,Status,Created Date");
            
            foreach (var user in users)
            {
                builder.AppendLine($"{user.Id},\"{user.Name}\",\"{user.Email}\",\"{user.Role}\",\"{(user.IsActive ? "Active" : "Inactive")}\",\"{user.CreatedAt.ToShortDateString()}\"");
            }
            
            byte[] fileBytes = Encoding.UTF8.GetBytes(builder.ToString());
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
        }

        public async Task<IActionResult> ExportToPdf()
        {
            var users = await _context.Users.ToListAsync();
            
            // En una implementación real, utilizaríamos QuestPDF o similar para generar PDF
            // Por ahora, mostraremos un mensaje indicando que se requiere la biblioteca
            return Content("Para implementar esta funcionalidad, instalar el paquete NuGet QuestPDF");
        }

        public async Task<IActionResult> ExportToCsv()
        {
            var users = await _context.Users.ToListAsync();
            
            var builder = new StringBuilder();
            builder.AppendLine("ID,Name,Email,Role,Status,Created Date");
            
            foreach (var user in users)
            {
                builder.AppendLine($"{user.Id},\"{user.Name}\",\"{user.Email}\",\"{user.Role}\",\"{(user.IsActive ? "Active" : "Inactive")}\",\"{user.CreatedAt.ToShortDateString()}\"");
            }
            
            byte[] fileBytes = Encoding.UTF8.GetBytes(builder.ToString());
            return File(fileBytes, "text/csv", "users.csv");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Role,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedAt = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Role,IsActive,CreatedAt")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}