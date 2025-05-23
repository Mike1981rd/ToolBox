namespace ToolBox.Models.ViewModels
{
    public class UserListItemViewModel
    {
        public int Id { get; set; }
        public string? AvatarUrl { get; set; } // Para la imagen del avatar
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? RoleName { get; set; } // Nombre del Rol
        // public string PlanName { get; set; } // Omitido según discusión previa
        public string? BillingMethod { get; set; }
        public bool IsActive { get; set; }
        public string? StatusDetail { get; set; } // Para "Pendiente", etc.
    }
}