using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public enum QuestionnaireStatus
    {
        [Display(Name = "Pendiente")]
        Pending,    // Asignado, no iniciado
        
        [Display(Name = "En Progreso")]
        InProgress, // Iniciado, guardado como borrador
        
        [Display(Name = "Completado")]
        Completed   // Enviado por el cliente
    }
}