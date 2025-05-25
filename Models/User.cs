using System;
using System.ComponentModel.DataAnnotations;

namespace YouthUnionManagement.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}