using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ModuleName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ActionName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}