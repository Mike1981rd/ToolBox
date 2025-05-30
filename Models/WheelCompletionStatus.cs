namespace ToolBox.Models
{
    public enum WheelCompletionStatus
    {
        Pending,    // Asignada, no iniciada
        InProgress, // Iniciada, guardada como borrador (si implementamos guardado parcial)
        Completed   // Finalizada por el cliente
    }
}