using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class VideoListViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? AutorNombre { get; set; }
        public string? TemaNombre { get; set; }
        public string TipoFuenteVideo { get; set; } = string.Empty;
        public string? Duracion { get; set; }
        public bool EsDestacado { get; set; }
        public string EstadoVideo { get; set; } = string.Empty;
        public DateTime FechaSubida { get; set; }
    }

    public class VideoCreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200)]
        public string Titulo { get; set; } = string.Empty;

        public string? DescripcionHTML { get; set; }

        public int? AutorId { get; set; }

        public int? TemaId { get; set; }

        [Required]
        public string TipoFuenteVideo { get; set; } = "YouTube"; // YouTube, Vimeo, Upload

        public string? UrlVideoExterno { get; set; }

        public string? NombreArchivoVideoSubido { get; set; }
        
        public string? PathVideoSubido { get; set; }

        public string? Duracion { get; set; }

        public string? MetaTituloSEO { get; set; }

        public string? MetaDescripcionSEO { get; set; }

        public string? PalabrasClaveSEO { get; set; }

        public bool EsDestacado { get; set; }

        [Required]
        public string EstadoVideo { get; set; } = "Borrador"; // Publicado, Borrador, Archivado
    }

    public class VideoPaginatedResponseViewModel
    {
        public List<VideoListViewModel> Videos { get; set; } = new();
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

    public class VideoAuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class VideoTopicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class VideoSaveRequest
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? DescripcionHTML { get; set; }
        public int? AutorId { get; set; }
        public int? TemaId { get; set; }
        public string TipoFuenteVideo { get; set; } = "YouTube";
        public string? UrlVideoExterno { get; set; }
        public string? NombreArchivoVideoSubido { get; set; }
        public string? PathVideoSubido { get; set; }
        public string? Duracion { get; set; }
        public string? MetaTituloSEO { get; set; }
        public string? MetaDescripcionSEO { get; set; }
        public string? PalabrasClaveSEO { get; set; }
        public bool EsDestacado { get; set; }
        public string EstadoVideo { get; set; } = "Borrador";
    }
}