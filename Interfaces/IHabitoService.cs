using ToolBox.Models;

namespace ToolBox.Interfaces
{
    public interface IHabitoService
    {
        // CRUD operations for Habito
        Task<IEnumerable<Habito>> GetAllHabitosAsync(int usuarioId, bool includeInactive = false);
        Task<Habito?> GetHabitoByIdAsync(int id, int usuarioId);
        Task<Habito> CreateHabitoAsync(Habito habito);
        Task<bool> UpdateHabitoAsync(Habito habito);
        Task<bool> DeleteHabitoAsync(int id, int usuarioId);
        Task<(bool Success, string Message, bool NewState)> ToggleHabitoStatusAsync(int id, int usuarioId);

        // Operations for RegistroCumplimientoHabito
        Task<HabitosConRegistrosViewModel> GetHabitosConRegistrosAsync(int usuarioId, DateTime fechaDesde, DateTime fechaHasta);
        Task<bool> GuardarRegistrosCumplimientoAsync(int usuarioId, List<RegistroCumplimientoViewModel> registros);
        Task<List<RegistroCumplimientoHabito>> GetRegistrosCumplimientoAsync(int habitoId, DateTime fechaDesde, DateTime fechaHasta);

        // Catalog operations
        Task<IEnumerable<CategoriaHabito>> GetCategoriasHabitosAsync();
        Task<IEnumerable<FrecuenciaHabito>> GetFrecuenciasHabitosAsync();

        // Statistics and calculations
        Task<HabitStatisticsViewModel> GetEstadisticasAsync(int usuarioId, DateTime? fechaDesde = null, DateTime? fechaHasta = null);
        Task<List<HabitoProgresoChartViewModel>> GetDatosGraficoProgresoAsync(int usuarioId, DateTime fechaDesde, DateTime fechaHasta);
        Task<int> CalcularRachaActualAsync(int habitoId, DateTime? fechaHasta = null);
        Task<int> CalcularMejorRachaAsync(int habitoId);
        Task<Dictionary<int, int>> CalcularRachasActivasAsync(int usuarioId);
    }
}