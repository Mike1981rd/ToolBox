using System;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}