using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;
using ToolBox.Interfaces;

namespace ToolBox.Controllers
{
    public class HabitTrackerController : BaseController
    {
        private readonly IHabitoService _habitoService;
        private readonly ILogger<HabitTrackerController> _logger;

        public HabitTrackerController(IHabitoService habitoService, ILogger<HabitTrackerController> logger)
        {
            _habitoService = habitoService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var currentWeekStart = GetStartOfWeek(DateTime.Now);
            var currentWeekEnd = currentWeekStart.AddDays(6);

            // Obtener hábitos con registros para la semana actual
            var habitosConRegistros = await _habitoService.GetHabitosConRegistrosAsync(userId, currentWeekStart, currentWeekEnd);

            // Convertir a formato compatible con la vista existente
            var habits = habitosConRegistros.Habitos.Select(h => 
            {
                var habitViewModel = new HabitViewModel
                {
                    HabitId = h.HabitId,
                    HabitName = h.HabitName,
                    Description = h.Description,
                    IconOrColor = h.IconOrColor,
                    CreatedAt = h.CreatedAt,
                    IsActive = h.IsActive,
                    DailyStatuses = new List<DailyStatusViewModel>()
                };

                // Generar estados diarios para la semana actual
                for (var date = currentWeekStart; date <= currentWeekEnd; date = date.AddDays(1))
                {
                    var registro = habitosConRegistros.RegistrosPorHabito.ContainsKey(h.HabitId)
                        ? habitosConRegistros.RegistrosPorHabito[h.HabitId].FirstOrDefault(r => r.Fecha.Date == date.Date)
                        : null;

                    habitViewModel.DailyStatuses.Add(new DailyStatusViewModel
                    {
                        Date = date,
                        IsCompleted = registro?.Cumplido ?? false
                    });
                }

                // Calcular métricas
                habitViewModel.DaysMet = habitViewModel.DailyStatuses.Count(d => d.IsCompleted);
                var totalDays = habitViewModel.DailyStatuses.Count;
                habitViewModel.PercentageMet = totalDays > 0 ? Math.Round((decimal)habitViewModel.DaysMet / totalDays * 100, 1) : 0;

                return habitViewModel;
            }).ToList();

            var viewModel = new HabitTrackerPageViewModel
            {
                Habits = habits,
                WeekStartDate = currentWeekStart,
                WeekEndDate = currentWeekEnd,
                CurrentWeekStart = currentWeekStart,
                CurrentWeekEnd = currentWeekEnd,
                CurrentPeriod = "last7days",
                OverallSuccessRate = habits.Any() ? Math.Round(habits.Average(h => h.PercentageMet), 1) : 0,
                TotalHabits = habits.Count,
                ActiveDays = 7
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddHabit([FromBody] HabitoCreateEditViewModel habitData)
        {
            try
            {
                var userId = GetCurrentUserId();
                
                var nuevoHabito = new Habito
                {
                    UsuarioId = userId,
                    Nombre = habitData.Nombre?.Trim() ?? "",
                    Descripcion = habitData.Descripcion?.Trim(),
                    Color = habitData.Color,
                    CategoriaHabitoId = habitData.CategoriaHabitoId,
                    FrecuenciaHabitoId = habitData.FrecuenciaHabitoId,
                    HabilitarRecordatorios = habitData.HabilitarRecordatorios
                };

                var habitoCreado = await _habitoService.CreateHabitoAsync(nuevoHabito);

                var habitViewModel = new HabitViewModel
                {
                    HabitId = habitoCreado.Id,
                    HabitName = habitoCreado.Nombre,
                    Description = habitoCreado.Descripcion ?? string.Empty,
                    IconOrColor = habitoCreado.Color,
                    CreatedAt = habitoCreado.FechaCreacion,
                    IsActive = habitoCreado.IsActive
                };

                return Json(new HabitTrackerResponseViewModel
                {
                    Success = true,
                    Message = "Hábito agregado exitosamente",
                    Habit = habitViewModel,
                    ActionDate = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding habit");
                return Json(new HabitTrackerResponseViewModel
                {
                    Success = false,
                    Message = $"Error al agregar el hábito: {ex.Message}",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateHabit([FromBody] HabitoCreateEditViewModel habitData)
        {
            try
            {
                var userId = GetCurrentUserId();
                
                var habitoToUpdate = new Habito
                {
                    Id = habitData.Id,
                    UsuarioId = userId,
                    Nombre = habitData.Nombre?.Trim() ?? "",
                    Descripcion = habitData.Descripcion?.Trim(),
                    Color = habitData.Color,
                    CategoriaHabitoId = habitData.CategoriaHabitoId,
                    FrecuenciaHabitoId = habitData.FrecuenciaHabitoId,
                    HabilitarRecordatorios = habitData.HabilitarRecordatorios
                };

                var success = await _habitoService.UpdateHabitoAsync(habitoToUpdate);

                if (!success)
                {
                    return Json(new HabitTrackerResponseViewModel
                    {
                        Success = false,
                        Message = "Hábito no encontrado"
                    });
                }

                return Json(new HabitTrackerResponseViewModel
                {
                    Success = true,
                    Message = "Hábito actualizado exitosamente",
                    ActionDate = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating habit {HabitId}", habitData.Id);
                return Json(new HabitTrackerResponseViewModel
                {
                    Success = false,
                    Message = $"Error al actualizar el hábito: {ex.Message}",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteHabit(int habitId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _habitoService.DeleteHabitoAsync(habitId, userId);

                if (!success)
                {
                    return Json(new HabitTrackerResponseViewModel
                    {
                        Success = false,
                        Message = "Hábito no encontrado"
                    });
                }

                return Json(new HabitTrackerResponseViewModel
                {
                    Success = true,
                    Message = "Hábito eliminado exitosamente",
                    ActionDate = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting habit {HabitId}", habitId);
                return Json(new HabitTrackerResponseViewModel
                {
                    Success = false,
                    Message = "Error al eliminar el hábito"
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveHabitLog([FromBody] GuardarRegistrosViewModel logData)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _habitoService.GuardarRegistrosCumplimientoAsync(userId, logData.Registros);

                if (!success)
                {
                    return Json(new HabitTrackerResponseViewModel
                    {
                        Success = false,
                        Message = "Error al guardar los registros"
                    });
                }

                // Calcular estadísticas actualizadas
                var fechaDesde = DateTime.UtcNow.AddDays(-7);
                var fechaHasta = DateTime.UtcNow;
                var estadisticas = await _habitoService.GetEstadisticasAsync(userId, fechaDesde, fechaHasta);

                return Json(new HabitTrackerResponseViewModel
                {
                    Success = true,
                    Message = "Registros guardados exitosamente",
                    ActionDate = DateTime.UtcNow,
                    Data = new { OverallSuccessRate = estadisticas.AverageCompletion }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving habit log for user {UserId}", GetCurrentUserId());
                return Json(new HabitTrackerResponseViewModel
                {
                    Success = false,
                    Message = "Error al guardar los registros"
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetHabitsWithRecords(DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            try
            {
                var userId = GetCurrentUserId();
                var fechaDesdeReal = fechaDesde ?? DateTime.UtcNow.AddDays(-7);
                var fechaHastaReal = fechaHasta ?? DateTime.UtcNow;

                var habitosConRegistros = await _habitoService.GetHabitosConRegistrosAsync(userId, fechaDesdeReal, fechaHastaReal);

                return Json(new
                {
                    success = true,
                    data = habitosConRegistros
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting habits with records for user {UserId}", GetCurrentUserId());
                return Json(new
                {
                    success = false,
                    message = "Error al obtener los hábitos"
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetHabitChartData(string period = "last7days")
        {
            try
            {
                var userId = GetCurrentUserId();
                var (startDate, endDate) = GetDateRangeForPeriod(period);

                var datosChart = await _habitoService.GetDatosGraficoProgresoAsync(userId, startDate, endDate);
                var estadisticas = await _habitoService.GetEstadisticasAsync(userId, startDate, endDate);

                return Json(new
                {
                    labels = datosChart.Select(d => d.Nombre).ToList(),
                    completionRates = datosChart.Select(d => d.Porcentaje).ToList(),
                    colors = datosChart.Select(d => d.Color).ToList(),
                    totalHabits = estadisticas.TotalHabits,
                    averageCompletion = estadisticas.AverageCompletion,
                    bestStreak = estadisticas.BestStreak,
                    activeStreaks = estadisticas.CurrentActiveStreaks,
                    period = period,
                    success = true
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting chart data for user {UserId}", GetCurrentUserId());
                return Json(new
                {
                    success = false,
                    message = "Error al obtener datos del gráfico",
                    labels = new List<string>(),
                    completionRates = new List<decimal>(),
                    colors = new List<string>(),
                    totalHabits = 0,
                    averageCompletion = 0,
                    bestStreak = 0,
                    activeStreaks = 0,
                    period = period ?? "last7days"
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCategorias()
        {
            try
            {
                var categorias = await _habitoService.GetCategoriasHabitosAsync();
                return Json(new
                {
                    success = true,
                    data = categorias.Select(c => new
                    {
                        id = c.Id,
                        nombre = c.Nombre,
                        descripcion = c.Descripcion,
                        iconClass = c.IconClass,
                        color = c.Color
                    })
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting habit categories");
                return Json(new
                {
                    success = false,
                    message = "Error al obtener las categorías"
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetFrecuencias()
        {
            try
            {
                var frecuencias = await _habitoService.GetFrecuenciasHabitosAsync();
                return Json(new
                {
                    success = true,
                    data = frecuencias.Select(f => new
                    {
                        id = f.Id,
                        nombre = f.Nombre,
                        descripcion = f.Descripcion,
                        diasIntervalo = f.DiasIntervalo
                    })
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting habit frequencies");
                return Json(new
                {
                    success = false,
                    message = "Error al obtener las frecuencias"
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetHabitById(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var habito = await _habitoService.GetHabitoByIdAsync(id, userId);

                if (habito == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Hábito no encontrado"
                    });
                }

                return Json(new
                {
                    success = true,
                    data = new
                    {
                        id = habito.Id,
                        nombre = habito.Nombre,
                        descripcion = habito.Descripcion,
                        color = habito.Color,
                        categoriaHabitoId = habito.CategoriaHabitoId,
                        frecuenciaHabitoId = habito.FrecuenciaHabitoId,
                        habilitarRecordatorios = habito.HabilitarRecordatorios
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting habit {HabitId}", id);
                return Json(new
                {
                    success = false,
                    message = "Error al obtener el hábito"
                });
            }
        }

        private int GetCurrentUserId()
        {
            // TODO: Obtener el usuario actual desde la sesión o claims
            // Por ahora retornamos un ID hardcodeado para pruebas
            return 1;
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        private (DateTime start, DateTime end) GetDateRangeForPeriod(string period)
        {
            var today = DateTime.UtcNow.Date;

            return period.ToLower() switch
            {
                "last7days" => (today.AddDays(-6), today),
                "last30days" => (today.AddDays(-29), today),
                "thismonth" => (new DateTime(today.Year, today.Month, 1), today),
                "alltime" => (today.AddDays(-365), today), // Último año para "all time"
                _ => (today.AddDays(-6), today)
            };
        }
    }
}