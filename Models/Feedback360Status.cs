namespace ToolBox.Models
{
    public enum Feedback360Status
    {
        PendingSetup,       // Coach está configurando los evaluadores
        CollectingFeedback, // Invitaciones enviadas, esperando respuestas
        GeneratingReport,   // Recolección cerrada, procesando datos (estado intermedio opcional)
        Completed           // Informe disponible
    }
}