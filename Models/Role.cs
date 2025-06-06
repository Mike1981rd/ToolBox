using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string AssignedDashboard { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Dashboard Permission Properties
        public bool PuedeVerMensajeBienvenidaDashboard { get; set; } = true;
        public bool PuedeVerVideoBienvenidaDashboard { get; set; } = true;
        public bool PuedeVerCardTotalClientesDashboard { get; set; } = true;
        public bool PuedeVerCardClientesActivosDashboard { get; set; } = true;

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
            RolePermissions = new HashSet<RolePermission>();
        }
    }
}