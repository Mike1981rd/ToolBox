using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class VideoService : IVideoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VideoService> _logger;

        public VideoService(ApplicationDbContext context, ILogger<VideoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(IEnumerable<Video> Videos, int TotalCount)> GetPaginatedAsync(string? searchTerm = null, int page = 1, int pageSize = 10, string? statusFilter = null, string? typeFilter = null, string? featuredFilter = null)
        {
            var query = _context.Videos
                .Include(v => v.Autor)
                .Include(v => v.Tema)
                .Include(v => v.UsuarioCreador)
                .AsQueryable();

            // Apply search filter (case-insensitive)
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(v => 
                    v.Titulo.ToLower().Contains(searchTerm) || 
                    (v.DescripcionHTML != null && v.DescripcionHTML.ToLower().Contains(searchTerm)) ||
                    (v.Autor != null && v.Autor.FullName.ToLower().Contains(searchTerm)) ||
                    (v.Tema != null && v.Tema.Name.ToLower().Contains(searchTerm)));
            }

            // Apply status filter
            if (!string.IsNullOrWhiteSpace(statusFilter) && statusFilter != "all")
            {
                query = query.Where(v => v.EstadoVideo == statusFilter);
            }

            // Apply type filter
            if (!string.IsNullOrWhiteSpace(typeFilter))
            {
                query = query.Where(v => v.TipoFuenteVideo == typeFilter);
            }

            // Apply featured filter
            if (!string.IsNullOrWhiteSpace(featuredFilter))
            {
                bool isFeatured = featuredFilter.ToLower() == "true";
                query = query.Where(v => v.EsDestacado == isFeatured);
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var videos = await query
                .OrderByDescending(v => v.FechaSubida)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (videos, totalCount);
        }

        public async Task<Video?> GetByIdAsync(int id)
        {
            return await _context.Videos
                .Include(v => v.Autor)
                .Include(v => v.Tema)
                .Include(v => v.UsuarioCreador)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Video> CreateAsync(Video video)
        {
            video.CreatedAt = DateTime.UtcNow;
            video.UpdatedAt = DateTime.UtcNow;
            video.FechaSubida = DateTime.UtcNow;

            _context.Videos.Add(video);
            await _context.SaveChangesAsync();
            
            return video;
        }

        public async Task<bool> UpdateAsync(Video video)
        {
            var existingVideo = await _context.Videos.FindAsync(video.Id);
            if (existingVideo == null)
            {
                return false;
            }

            // Update properties
            existingVideo.Titulo = video.Titulo;
            existingVideo.DescripcionHTML = video.DescripcionHTML;
            existingVideo.AutorId = video.AutorId;
            existingVideo.TemaId = video.TemaId;
            existingVideo.TipoFuenteVideo = video.TipoFuenteVideo;
            existingVideo.UrlVideoExterno = video.UrlVideoExterno;
            existingVideo.NombreArchivoVideoSubido = video.NombreArchivoVideoSubido;
            existingVideo.PathVideoSubido = video.PathVideoSubido;
            existingVideo.Duracion = video.Duracion;
            existingVideo.MetaTituloSEO = video.MetaTituloSEO;
            existingVideo.MetaDescripcionSEO = video.MetaDescripcionSEO;
            existingVideo.PalabrasClaveSEO = video.PalabrasClaveSEO;
            existingVideo.EsDestacado = video.EsDestacado;
            existingVideo.EstadoVideo = video.EstadoVideo;
            existingVideo.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating video {VideoId}", video.Id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return false;
            }

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<IEnumerable<Video>> GetFeaturedVideosAsync()
        {
            return await _context.Videos
                .Include(v => v.Autor)
                .Include(v => v.Tema)
                .Where(v => v.EsDestacado && v.EstadoVideo == "Publicado")
                .OrderByDescending(v => v.FechaSubida)
                .ToListAsync();
        }

        public async Task<bool> ToggleFeaturedAsync(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return false;
            }

            video.EsDestacado = !video.EsDestacado;
            video.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }
    }
}