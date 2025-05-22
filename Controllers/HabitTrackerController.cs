using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class HabitTrackerController : Controller
    {
        private static List<HabitViewModel> _mockHabits = new List<HabitViewModel>();
        private static List<HabitLogEntryViewModel> _mockLogEntries = new List<HabitLogEntryViewModel>();
        private static int _nextHabitId = 1;

        public async Task<IActionResult> Index()
        {
            // Initialize with some mock data if empty
            if (!_mockHabits.Any())
            {
                InitializeMockData();
            }

            var currentWeekStart = GetStartOfWeek(DateTime.Now);
            var currentWeekEnd = currentWeekStart.AddDays(6);

            // Generate daily statuses for current week
            foreach (var habit in _mockHabits)
            {
                habit.DailyStatuses = GenerateDailyStatuses(habit.HabitId, currentWeekStart, currentWeekEnd);
                CalculateHabitMetrics(habit);
            }

            var viewModel = new HabitTrackerPageViewModel
            {
                Habits = _mockHabits.Where(h => h.IsActive).ToList(),
                WeekStartDate = currentWeekStart,
                WeekEndDate = currentWeekEnd,
                CurrentWeekStart = currentWeekStart,
                CurrentWeekEnd = currentWeekEnd,
                CurrentPeriod = "last7days"
            };

            // Calculate overall success rate
            viewModel.OverallSuccessRate = CalculateOverallSuccessRate(viewModel.Habits);
            viewModel.TotalHabits = viewModel.Habits.Count;
            viewModel.ActiveDays = 7; // Current week

            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> AddHabit([FromBody] HabitViewModel habitData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(habitData.HabitName))
                {
                    return Json(new HabitTrackerResponseViewModel
                    {
                        Success = false,
                        Message = "Habit name is required",
                        Errors = new List<string> { "Please provide a habit name" }
                    });
                }

                var newHabit = new HabitViewModel
                {
                    HabitId = _nextHabitId++,
                    HabitName = habitData.HabitName.Trim(),
                    IconOrColor = habitData.IconOrColor ?? GetRandomColor(),
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    CurrentStreak = 0,
                    BestStreak = 0
                };

                // Generate daily statuses for current week
                var weekStart = GetStartOfWeek(DateTime.Now);
                var weekEnd = weekStart.AddDays(6);
                newHabit.DailyStatuses = GenerateDailyStatuses(newHabit.HabitId, weekStart, weekEnd);
                CalculateHabitMetrics(newHabit);

                await Task.Delay(300); // Simulate async operation

                _mockHabits.Add(newHabit);

                return Json(new HabitTrackerResponseViewModel
                {
                    Success = true,
                    Message = "Habit added successfully!",
                    Habit = newHabit,
                    ActionDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Json(new HabitTrackerResponseViewModel
                {
                    Success = false,
                    Message = "An error occurred while adding the habit",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteHabit(int habitId)
        {
            try
            {
                var habit = _mockHabits.FirstOrDefault(h => h.HabitId == habitId);
                
                if (habit == null)
                {
                    return Json(new HabitTrackerResponseViewModel
                    {
                        Success = false,
                        Message = "Habit not found"
                    });
                }

                // Mark as inactive instead of deleting to preserve historical data
                habit.IsActive = false;

                // Remove related log entries
                _mockLogEntries.RemoveAll(entry => entry.HabitId == habitId);

                await Task.Delay(200); // Simulate async operation

                return Json(new HabitTrackerResponseViewModel
                {
                    Success = true,
                    Message = "Habit deleted successfully!",
                    ActionDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Json(new HabitTrackerResponseViewModel
                {
                    Success = false,
                    Message = "An error occurred while deleting the habit",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveHabitLog([FromBody] List<HabitLogEntryViewModel> logEntries)
        {
            try
            {
                foreach (var entry in logEntries)
                {
                    // Remove existing entry for this habit and date
                    _mockLogEntries.RemoveAll(e => e.HabitId == entry.HabitId && e.Date.Date == entry.Date.Date);
                    
                    // Add new entry
                    _mockLogEntries.Add(new HabitLogEntryViewModel
                    {
                        HabitId = entry.HabitId,
                        Date = entry.Date.Date,
                        IsCompleted = entry.IsCompleted
                    });
                }

                // Update habit metrics
                foreach (var habit in _mockHabits.Where(h => h.IsActive))
                {
                    var weekStart = GetStartOfWeek(DateTime.Now);
                    var weekEnd = weekStart.AddDays(6);
                    habit.DailyStatuses = GenerateDailyStatuses(habit.HabitId, weekStart, weekEnd);
                    CalculateHabitMetrics(habit);
                }

                await Task.Delay(400); // Simulate async operation

                var overallSuccessRate = CalculateOverallSuccessRate(_mockHabits.Where(h => h.IsActive).ToList());

                return Json(new HabitTrackerResponseViewModel
                {
                    Success = true,
                    Message = "Habit log saved successfully!",
                    ActionDate = DateTime.Now,
                    Data = new { OverallSuccessRate = overallSuccessRate }
                });
            }
            catch (Exception ex)
            {
                return Json(new HabitTrackerResponseViewModel
                {
                    Success = false,
                    Message = "An error occurred while saving the habit log",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetHabitChartData(string period = "last7days")
        {
            try
            {
                var activeHabits = _mockHabits.Where(h => h.IsActive).ToList();
                var chartData = new HabitChartDataViewModel
                {
                    Period = period,
                    ChartTitle = GetChartTitle(period)
                };

                if (!activeHabits.Any())
                {
                    return Json(new {
                        labels = new List<string>(),
                        completionRates = new List<decimal>(),
                        colors = new List<string>(),
                        totalHabits = 0,
                        averageCompletion = 0,
                        bestStreak = 0,
                        activeStreaks = 0,
                        period = period,
                        success = true
                    });
                }

                // Calculate date range based on period
                var (startDate, endDate) = GetDateRangeForPeriod(period);
                
                // Set labels (habit names)
                chartData.Labels = activeHabits.Select(h => h.HabitName).ToList();

                // Calculate completion rates for each habit in the period
                var completionRates = new List<decimal>();
                
                foreach (var habit in activeHabits)
                {
                    var periodEntries = _mockLogEntries
                        .Where(e => e.HabitId == habit.HabitId && 
                                   e.Date >= startDate && 
                                   e.Date <= endDate)
                        .ToList();

                    var totalDays = (endDate - startDate).Days + 1;
                    var completedDays = periodEntries.Count(e => e.IsCompleted);
                    var completionRate = totalDays > 0 ? (decimal)completedDays / totalDays * 100 : 0;
                    
                    completionRates.Add(Math.Round(completionRate, 1));
                }

                // Calculate additional statistics
                var totalHabits = activeHabits.Count();
                var averageCompletion = totalHabits > 0 ? Math.Round(completionRates.Average(), 1) : 0;
                var bestStreak = activeHabits.Any() ? activeHabits.Max(h => h.CurrentStreak) : 0;
                var activeStreaks = activeHabits.Count(h => h.CurrentStreak > 0);

                await Task.Delay(200); // Simulate async operation

                // Return data in the format expected by JavaScript
                return Json(new {
                    labels = chartData.Labels,
                    completionRates = completionRates,
                    colors = activeHabits.Select(h => h.IconOrColor).ToList(),
                    totalHabits = totalHabits,
                    averageCompletion = averageCompletion,
                    bestStreak = bestStreak,
                    activeStreaks = activeStreaks,
                    period = period,
                    success = true
                });
            }
            catch (Exception ex)
            {
                return Json(new {
                    success = false,
                    message = "An error occurred while getting chart data: " + ex.Message,
                    labels = new List<string>(),
                    completionRates = new List<decimal>(),
                    colors = new List<string>(),
                    totalHabits = 0,
                    averageCompletion = 0,
                    bestStreak = 0,
                    activeStreaks = 0,
                    period = period ?? "Last7Days"
                });
            }
        }

        private void InitializeMockData()
        {
            var baseDate = GetStartOfWeek(DateTime.Now);
            
            _mockHabits.AddRange(new List<HabitViewModel>
            {
                new HabitViewModel
                {
                    HabitId = _nextHabitId++,
                    HabitName = "Read for 30 minutes",
                    IconOrColor = "#4f46e5",
                    CreatedAt = DateTime.Now.AddDays(-30),
                    IsActive = true
                },
                new HabitViewModel
                {
                    HabitId = _nextHabitId++,
                    HabitName = "Morning exercise",
                    IconOrColor = "#059669",
                    CreatedAt = DateTime.Now.AddDays(-25),
                    IsActive = true
                },
                new HabitViewModel
                {
                    HabitId = _nextHabitId++,
                    HabitName = "Meditate",
                    IconOrColor = "#7c3aed",
                    CreatedAt = DateTime.Now.AddDays(-20),
                    IsActive = true
                },
                new HabitViewModel
                {
                    HabitId = _nextHabitId++,
                    HabitName = "Drink 8 glasses of water",
                    IconOrColor = "#0891b2",
                    CreatedAt = DateTime.Now.AddDays(-15),
                    IsActive = true
                }
            });

            // Generate some mock log entries
            var random = new Random();
            foreach (var habit in _mockHabits)
            {
                for (int i = 0; i < 14; i++) // Last 2 weeks
                {
                    var date = baseDate.AddDays(i - 7);
                    if (random.NextDouble() > 0.3) // 70% completion rate
                    {
                        _mockLogEntries.Add(new HabitLogEntryViewModel
                        {
                            HabitId = habit.HabitId,
                            Date = date,
                            IsCompleted = true
                        });
                    }
                }
            }
        }

        private List<DailyStatusViewModel> GenerateDailyStatuses(int habitId, DateTime startDate, DateTime endDate)
        {
            var statuses = new List<DailyStatusViewModel>();
            
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var logEntry = _mockLogEntries.FirstOrDefault(e => 
                    e.HabitId == habitId && e.Date.Date == date.Date);
                
                statuses.Add(new DailyStatusViewModel
                {
                    Date = date,
                    IsCompleted = logEntry?.IsCompleted ?? false
                });
            }
            
            return statuses;
        }

        private void CalculateHabitMetrics(HabitViewModel habit)
        {
            habit.DaysMet = habit.DailyStatuses.Count(d => d.IsCompleted);
            var totalDays = habit.DailyStatuses.Count;
            habit.PercentageMet = totalDays > 0 ? Math.Round((decimal)habit.DaysMet / totalDays * 100, 1) : 0;

            // Calculate current streak
            habit.CurrentStreak = CalculateCurrentStreak(habit.HabitId);
            habit.BestStreak = CalculateBestStreak(habit.HabitId);
        }

        private int CalculateCurrentStreak(int habitId)
        {
            var recentEntries = _mockLogEntries
                .Where(e => e.HabitId == habitId)
                .OrderByDescending(e => e.Date)
                .Take(30)
                .ToList();

            int streak = 0;
            var currentDate = DateTime.Now.Date;

            foreach (var entry in recentEntries.OrderByDescending(e => e.Date))
            {
                if (entry.Date.Date == currentDate && entry.IsCompleted)
                {
                    streak++;
                    currentDate = currentDate.AddDays(-1);
                }
                else if (entry.Date.Date < currentDate)
                {
                    break;
                }
            }

            return streak;
        }

        private int CalculateBestStreak(int habitId)
        {
            var allEntries = _mockLogEntries
                .Where(e => e.HabitId == habitId && e.IsCompleted)
                .OrderBy(e => e.Date)
                .ToList();

            if (!allEntries.Any()) return 0;

            int bestStreak = 0;
            int currentStreak = 1;
            DateTime lastDate = allEntries.First().Date;

            for (int i = 1; i < allEntries.Count; i++)
            {
                if (allEntries[i].Date == lastDate.AddDays(1))
                {
                    currentStreak++;
                }
                else
                {
                    bestStreak = Math.Max(bestStreak, currentStreak);
                    currentStreak = 1;
                }
                lastDate = allEntries[i].Date;
            }

            return Math.Max(bestStreak, currentStreak);
        }

        private decimal CalculateOverallSuccessRate(List<HabitViewModel> habits)
        {
            if (!habits.Any()) return 0;
            return Math.Round(habits.Average(h => h.PercentageMet), 1);
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        [HttpPost]
        public async Task<JsonResult> SaveAllHabitLogs(string logEntries)
        {
            try
            {
                if (string.IsNullOrEmpty(logEntries))
                {
                    return Json(new HabitTrackerResponseViewModel
                    {
                        Success = false,
                        Message = "No log entries provided"
                    });
                }

                var entries = System.Text.Json.JsonSerializer.Deserialize<List<HabitLogEntryViewModel>>(logEntries);

                if (entries == null || !entries.Any())
                {
                    return Json(new HabitTrackerResponseViewModel
                    {
                        Success = false,
                        Message = "Invalid log entries data"
                    });
                }

                foreach (var entry in entries)
                {
                    // Remove existing entry if it exists
                    _mockLogEntries.RemoveAll(e => e.HabitId == entry.HabitId && e.Date.Date == entry.Date.Date);
                    
                    // Add new entry
                    _mockLogEntries.Add(new HabitLogEntryViewModel
                    {
                        HabitId = entry.HabitId,
                        Date = entry.Date,
                        IsCompleted = entry.IsCompleted
                    });
                }

                await Task.Delay(200); // Simulate async operation

                // Recalculate metrics for all habits
                var currentWeekStart = GetStartOfWeek(DateTime.Now);
                var currentWeekEnd = currentWeekStart.AddDays(6);

                foreach (var habit in _mockHabits.Where(h => h.IsActive))
                {
                    habit.DailyStatuses = GenerateDailyStatuses(habit.HabitId, currentWeekStart, currentWeekEnd);
                    CalculateHabitMetrics(habit);
                }

                return Json(new HabitTrackerResponseViewModel
                {
                    Success = true,
                    Message = "All habit logs saved successfully!",
                    ActionDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Json(new HabitTrackerResponseViewModel
                {
                    Success = false,
                    Message = "An error occurred while saving habit logs",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        private (DateTime start, DateTime end) GetDateRangeForPeriod(string period)
        {
            var today = DateTime.Now.Date;
            
            return period.ToLower() switch
            {
                "last7days" => (today.AddDays(-6), today),
                "last30days" => (today.AddDays(-29), today),
                "thismonth" => (new DateTime(today.Year, today.Month, 1), today),
                "alltime" => (_mockLogEntries.Any() ? _mockLogEntries.Min(e => e.Date) : today.AddDays(-30), today),
                _ => (today.AddDays(-6), today)
            };
        }

        private string GetChartTitle(string period)
        {
            return period.ToLower() switch
            {
                "last7days" => "Últimos 7 Días",
                "last30days" => "Últimos 30 Días", 
                "thismonth" => "Este Mes",
                "alltime" => "Todo el Tiempo",
                _ => "Progreso de Hábitos"
            };
        }

        private string GetRandomColor()
        {
            var colors = new[]
            {
                "#4f46e5", "#059669", "#7c3aed", "#0891b2", "#dc2626", 
                "#ea580c", "#ca8a04", "#16a34a", "#2563eb", "#9333ea"
            };
            
            var random = new Random();
            return colors[random.Next(colors.Length)];
        }
    }
}