using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    // ViewModel para el endpoint GET - estructura completa con datos del usuario
    public class WheelOfProgressViewModel
    {
        public List<AreaProgresoViewModel> Areas { get; set; } = new();
        public WheelOfProgressStatsViewModel Stats { get; set; } = new();
    }

    public class AreaProgresoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string IconClass { get; set; } = string.Empty;
        public string IconColor { get; set; } = string.Empty;
        public int OrdenVisualizacion { get; set; }
        public List<CategoriaProgresoViewModel> Categorias { get; set; } = new();
    }

    public class CategoriaProgresoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int AreaProgresoId { get; set; }
        public int OrdenVisualizacion { get; set; }
        
        // Datos del progreso del usuario (si existen)
        public string? Meta { get; set; }
        public int PorcentajeProgreso { get; set; } = 0;
        public string? SiguienteAccion { get; set; }
        public DateTime? FechaLimite { get; set; }
        public DateTime? UltimaActualizacion { get; set; }
    }

    public class WheelOfProgressStatsViewModel
    {
        public decimal ProgresoGlobal { get; set; }
        public int TotalMetas { get; set; }
        public int MetasCompletadas { get; set; }
        public int MetasEnProgreso { get; set; }
        public DateTime? UltimaActualizacion { get; set; }
        public int MetasVencidas { get; set; }
        public int MetasPorVencer { get; set; } // En próximos 7 días
    }

    // ViewModel para el endpoint POST - guardar progreso
    public class SaveProgressRequestViewModel
    {
        public List<ProgressCategoryUpdateViewModel> Categories { get; set; } = new();
    }

    public class ProgressCategoryUpdateViewModel
    {
        public int CategoriaProgresoId { get; set; }
        
        [StringLength(1000)]
        public string? Meta { get; set; }
        
        [Range(0, 100)]
        public int PorcentajeProgreso { get; set; } = 0;
        
        [StringLength(1000)]
        public string? SiguienteAccion { get; set; }
        
        public DateTime? FechaLimite { get; set; }
    }

    // ViewModel para respuesta del guardado
    public class SaveProgressResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public WheelOfProgressStatsViewModel? UpdatedStats { get; set; }
    }
}