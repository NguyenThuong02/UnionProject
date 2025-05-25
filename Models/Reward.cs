using System;
using System.ComponentModel.DataAnnotations;

namespace YouthUnionManagement.Models
{
    public class Reward
    {
        [Key]
        public int Id { get; set; }
        public int MemberId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [StringLength(50)]
        public string Level { get; set; }
        public virtual Member Member { get; set; }
    }
}