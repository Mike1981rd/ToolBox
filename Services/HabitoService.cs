using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class HabitoService : IHabitoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HabitoService> _logger;

        public HabitoService(ApplicationDbContext context, ILogger<HabitoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // CRUD operations for Habito
        public async Task<IEnumerable<Habito>> GetAllHabitosAsync(int usuarioId, bool includeInactive = false)
        {
            var query = _context.Habitos
                .Include(h => h.CategoriaHabito)
                .Include(h => h.FrecuenciaHabito)
                .Where(h => h.UsuarioId == usuarioId);

            if (!includeInactive)
            {
                query = query.Where(h => h.IsActive);
            }

            return await query
                .OrderByDescending(h => h.FechaCreacion)
                .ToListAsync();
        }

        public async Task<Habito?> GetHabitoByIdAsync(int id, int usuarioId)
        {
            return await _context.Habitos
                .Include(h => h.CategoriaHabito)
                .Include(h => h.FrecuenciaHabito)
                .FirstOrDefaultAsync(h => h.Id == id && h.UsuarioId == usuarioId);
        }

        public async Task<Habito> CreateHabitoAsync(Habito habito)
        {
            // Verificar que el usuario existe
            var userExists = await _context.Users.AnyAsync(u => u.Id == habito.UsuarioId);
            if (!userExists)
            {
                throw new InvalidOperationException($"Usuario con ID {habito.UsuarioId} no existe");
            }

            // Verificar que la categoría existe
            var categoriaExists = await _context.CategoriasHabitos.AnyAsync(c => c.Id == habito.CategoriaHabitoId);
            if (!categoriaExists)
            {
                throw new InvalidOperationException($"Categoría con ID {habito.CategoriaHabitoId} no existe");
            }

            // Verificar que la frecuencia existe
            var frecuenciaExists = await _context.FrecuenciasHabitos.AnyAsync(f => f.Id == habito.FrecuenciaHabitoId);
            if (!frecuenciaExists)
            {
                throw new InvalidOperationException($"Frecuencia con ID {habito.FrecuenciaHabitoId} no existe");
            }

            habito.FechaCreacion = DateTime.UtcNow;
            habito.FechaActualizacion = DateTime.UtcNow;

            _context.Habitos.Add(habito);
            await _context.SaveChangesAsync();

            return await GetHabitoByIdAsync(habito.Id, habito.UsuarioId) ?? habito;
        }

        public async Task<bool> UpdateHabitoAsync(Habito habito)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingHabito = await _context.Habitos
                    .FirstOrDefaultAsync(h => h.Id == habito.Id && h.UsuarioId == habito.UsuarioId);

                if (existingHabito == null)
                    return false;

                // Verificar que la categoría existe
                var categoriaExists = await _context.CategoriasHabitos.AnyAsync(c => c.Id == habito.CategoriaHabitoId);
                if (!categoriaExists)
                {
                    throw new InvalidOperationException($"Categoría con ID {habito.CategoriaHabitoId} no existe");
                }

                // Verificar que la frecuencia existe
                var frecuenciaExists = await _context.FrecuenciasHabitos.AnyAsync(f => f.Id == habito.FrecuenciaHabitoId);
                if (!frecuenciaExists)
                {
                    throw new InvalidOperationException($"Frecuencia con ID {habito.FrecuenciaHabitoId} no existe");
                }

                existingHabito.Nombre = habito.Nombre;
                existingHabito.Descripcion = habito.Descripcion;
                existingHabito.Color = habito.Color;
                existingHabito.CategoriaHabitoId = habito.CategoriaHabitoId;
                existingHabito.FrecuenciaHabitoId = habito.FrecuenciaHabitoId;
                existingHabito.HabilitarRecordatorios = habito.HabilitarRecordatorios;
                existingHabito.FechaActualizacion = DateTime.UtcNow;

                _context.Habitos.Update(existingHabito);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating habit {HabitId}", habito.Id);
                return false;
            }
        }

        public async Task<bool> DeleteHabitoAsync(int id, int usuarioId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var habito = await GetHabitoByIdAsync(id, usuarioId);
                if (habito == null)
                    return false;

                // Eliminar todos los registros de cumplimiento asociados
                var registros = await _context.RegistrosCumplimientoHabitos
                    .Where(r => r.HabitoId == id)
                    .ToListAsync();

                _context.RegistrosCumplimientoHabitos.RemoveRange(registros);

                // Eliminar el hábito
                _context.Habitos.Remove(habito);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error deleting habit {HabitId}", id);
                return false;
            }
        }

        public async Task<(bool Success, string Message, bool NewState)> ToggleHabitoStatusAsync(int id, int usuarioId)
        {
            var habito = await GetHabitoByIdAsync(id, usuarioId);
            if (habito == null)
                return (false, "Hábito no encontrado", false);

            habito.IsActive = !habito.IsActive;
            habito.FechaActualizacion = DateTime.UtcNow;

            _context.Habitos.Update(habito);
            await _context.SaveChangesAsync();

            string message = habito.IsActive ? "Hábito activado" : "Hábito desactivado";
            return (true, message, habito.IsActive);
        }

        // Operations for RegistroCumplimientoHabito
        public async Task<HabitosConRegistrosViewModel> GetHabitosConRegistrosAsync(int usuarioId, DateTime fechaDesde, DateTime fechaHasta)
        {
            var habitos = await GetAllHabitosAsync(usuarioId, false);
            var habitosIds = habitos.Select(h => h.Id).ToList();

            // Obtener todos los registros de cumplimiento en el rango de fechas
            var registros = await _context.RegistrosCumplimientoHabitos
                .Where(r => habitosIds.Contains(r.HabitoId) && 
                           r.Fecha >= fechaDesde.Date && 
                           r.Fecha <= fechaHasta.Date)
                .ToListAsync();

            // Convertir a ViewModels
            var habitosViewModel = habitos.Select(h => new HabitViewModel
            {
                HabitId = h.Id,
                HabitName = h.Nombre,
                Description = h.Descripcion ?? string.Empty,
                IconOrColor = h.Color,
                CreatedAt = h.FechaCreacion,
                IsActive = h.IsActive
            }).ToList();

            // Agrupar registros por hábito
            var registrosPorHabito = new Dictionary<int, List<RegistroCumplimientoViewModel>>();
            foreach (var habitoId in habitosIds)
            {
                var registrosHabito = registros
                    .Where(r => r.HabitoId == habitoId)
                    .Select(r => new RegistroCumplimientoViewModel
                    {
                        HabitoId = r.HabitoId,
                        Fecha = r.Fecha,
                        Cumplido = r.Cumplido
                    })
                    .ToList();

                registrosPorHabito[habitoId] = registrosHabito;
            }

            // Calcular estadísticas
            var estadisticas = await GetEstadisticasAsync(usuarioId, fechaDesde, fechaHasta);

            // Obtener categorías y frecuencias
            var categorias = await GetCategoriasHabitosAsync();
            var frecuencias = await GetFrecuenciasHabitosAsync();

            return new HabitosConRegistrosViewModel
            {
                Habitos = habitosViewModel,
                RegistrosPorHabito = registrosPorHabito,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Estadisticas = estadisticas,
                Categorias = categorias.Select(c => new CategoriaHabitoViewModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                    IconClass = c.IconClass,
                    Color = c.Color,
                    OrdenVisualizacion = c.OrdenVisualizacion
                }).ToList(),
                Frecuencias = frecuencias.Select(f => new FrecuenciaHabitoViewModel
                {
                    Id = f.Id,
                    Nombre = f.Nombre,
                    Descripcion = f.Descripcion,
                    DiasIntervalo = f.DiasIntervalo,
                    OrdenVisualizacion = f.OrdenVisualizacion
                }).ToList()
            };
        }

        public async Task<bool> GuardarRegistrosCumplimientoAsync(int usuarioId, List<RegistroCumplimientoViewModel> registros)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Verificar que todos los hábitos pertenecen al usuario
                var habitosIds = registros.Select(r => r.HabitoId).Distinct().ToList();
                var habitosUsuario = await _context.Habitos
                    .Where(h => habitosIds.Contains(h.Id) && h.UsuarioId == usuarioId)
                    .Select(h => h.Id)
                    .ToListAsync();

                if (habitosUsuario.Count != habitosIds.Count)
                {
                    throw new InvalidOperationException("Uno o más hábitos no pertenecen al usuario");
                }

                foreach (var registro in registros)
                {
                    // Buscar si ya existe un registro para este hábito en esta fecha
                    var existingRegistro = await _context.RegistrosCumplimientoHabitos
                        .FirstOrDefaultAsync(r => r.HabitoId == registro.HabitoId && 
                                                 r.Fecha.Date == registro.Fecha.Date);

                    if (existingRegistro != null)
                    {
                        // Actualizar registro existente
                        existingRegistro.Cumplido = registro.Cumplido;
                        existingRegistro.FechaRegistro = DateTime.UtcNow;
                        _context.RegistrosCumplimientoHabitos.Update(existingRegistro);
                    }
                    else
                    {
                        // Crear nuevo registro
                        var nuevoRegistro = new RegistroCumplimientoHabito
                        {
                            HabitoId = registro.HabitoId,
                            Fecha = registro.Fecha.Date,
                            Cumplido = registro.Cumplido,
                            FechaRegistro = DateTime.UtcNow
                        };
                        _context.RegistrosCumplimientoHabitos.Add(nuevoRegistro);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error saving compliance records for user {UserId}", usuarioId);
                return false;
            }
        }

        public async Task<List<RegistroCumplimientoHabito>> GetRegistrosCumplimientoAsync(int habitoId, DateTime fechaDesde, DateTime fechaHasta)
        {
            return await _context.RegistrosCumplimientoHabitos
                .Where(r => r.HabitoId == habitoId && 
                           r.Fecha >= fechaDesde.Date && 
                           r.Fecha <= fechaHasta.Date)
                .OrderBy(r => r.Fecha)
                .ToListAsync();
        }

        // Catalog operations
        public async Task<IEnumerable<CategoriaHabito>> GetCategoriasHabitosAsync()
        {
            return await _context.CategoriasHabitos
                .Where(c => c.IsActive)
                .OrderBy(c => c.OrdenVisualizacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<FrecuenciaHabito>> GetFrecuenciasHabitosAsync()
        {
            return await _context.FrecuenciasHabitos
                .Where(f => f.IsActive)
                .OrderBy(f => f.OrdenVisualizacion)
                .ToListAsync();
        }

        // Statistics and calculations
        public async Task<HabitStatisticsViewModel> GetEstadisticasAsync(int usuarioId, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            var habitos = await GetAllHabitosAsync(usuarioId, false);
            var totalHabitos = habitos.Count();
            var habitosActivos = habitos.Where(h => h.IsActive).Count();

            if (totalHabitos == 0)
            {
                return new HabitStatisticsViewModel
                {
                    TotalHabits = 0,
                    ActiveHabits = 0,
                    AverageCompletion = 0,
                    TotalCompletions = 0,
                    BestStreak = 0,
                    CurrentActiveStreaks = 0
                };
            }

            var habitosIds = habitos.Select(h => h.Id).ToList();
            var fechaDesdeReal = fechaDesde ?? DateTime.UtcNow.AddDays(-30);
            var fechaHastaReal = fechaHasta ?? DateTime.UtcNow;

            // Obtener registros en el rango de fechas
            var registros = await _context.RegistrosCumplimientoHabitos
                .Where(r => habitosIds.Contains(r.HabitoId) && 
                           r.Fecha >= fechaDesdeReal.Date && 
                           r.Fecha <= fechaHastaReal.Date)
                .ToListAsync();

            var totalCompletions = registros.Count(r => r.Cumplido);
            var totalPossibleDays = habitosIds.Count * (int)(fechaHastaReal.Date - fechaDesdeReal.Date).TotalDays + 1;
            var averageCompletion = totalPossibleDays > 0 ? (decimal)totalCompletions / totalPossibleDays * 100 : 0;

            // Calcular mejor racha global
            var mejorRachaGlobal = 0;
            foreach (var habitoId in habitosIds)
            {
                var mejorRachaHabito = await CalcularMejorRachaAsync(habitoId);
                if (mejorRachaHabito > mejorRachaGlobal)
                    mejorRachaGlobal = mejorRachaHabito;
            }

            // Calcular rachas activas
            var rachasActivas = await CalcularRachasActivasAsync(usuarioId);
            var totalRachasActivas = rachasActivas.Values.Count(r => r > 0);

            return new HabitStatisticsViewModel
            {
                TotalHabits = totalHabitos,
                ActiveHabits = habitosActivos,
                AverageCompletion = Math.Round(averageCompletion, 1),
                TotalCompletions = totalCompletions,
                BestStreak = mejorRachaGlobal,
                CurrentActiveStreaks = totalRachasActivas
            };
        }

        public async Task<List<HabitoProgresoChartViewModel>> GetDatosGraficoProgresoAsync(int usuarioId, DateTime fechaDesde, DateTime fechaHasta)
        {
            var habitos = await GetAllHabitosAsync(usuarioId, false);
            var result = new List<HabitoProgresoChartViewModel>();

            foreach (var habito in habitos)
            {
                var registros = await GetRegistrosCumplimientoAsync(habito.Id, fechaDesde, fechaHasta);
                var totalDias = (int)(fechaHasta.Date - fechaDesde.Date).TotalDays + 1;
                var diasCumplidos = registros.Count(r => r.Cumplido);
                var porcentaje = totalDias > 0 ? (decimal)diasCumplidos / totalDias * 100 : 0;

                result.Add(new HabitoProgresoChartViewModel
                {
                    Nombre = habito.Nombre,
                    Porcentaje = Math.Round(porcentaje, 1),
                    Color = habito.Color
                });
            }

            return result.OrderByDescending(h => h.Porcentaje).ToList();
        }

        public async Task<int> CalcularRachaActualAsync(int habitoId, DateTime? fechaHasta = null)
        {
            var fecha = fechaHasta?.Date ?? DateTime.UtcNow.Date;
            var registros = await _context.RegistrosCumplimientoHabitos
                .Where(r => r.HabitoId == habitoId && r.Fecha <= fecha)
                .OrderByDescending(r => r.Fecha)
                .ToListAsync();

            var rachaActual = 0;
            foreach (var registro in registros)
            {
                if (registro.Cumplido)
                {
                    rachaActual++;
                }
                else
                {
                    break; // La racha se rompe
                }
            }

            return rachaActual;
        }

        public async Task<int> CalcularMejorRachaAsync(int habitoId)
        {
            var registros = await _context.RegistrosCumplimientoHabitos
                .Where(r => r.HabitoId == habitoId)
                .OrderBy(r => r.Fecha)
                .ToListAsync();

            var mejorRacha = 0;
            var rachaActual = 0;

            foreach (var registro in registros)
            {
                if (registro.Cumplido)
                {
                    rachaActual++;
                    if (rachaActual > mejorRacha)
                        mejorRacha = rachaActual;
                }
                else
                {
                    rachaActual = 0;
                }
            }

            return mejorRacha;
        }

        public async Task<Dictionary<int, int>> CalcularRachasActivasAsync(int usuarioId)
        {
            var habitos = await GetAllHabitosAsync(usuarioId, false);
            var result = new Dictionary<int, int>();

            foreach (var habito in habitos)
            {
                var rachaActual = await CalcularRachaActualAsync(habito.Id);
                result[habito.Id] = rachaActual;
            }

            return result;
        }
    }
}