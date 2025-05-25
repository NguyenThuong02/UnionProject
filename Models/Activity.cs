using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouthUnionManagement.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [StringLength(100)]
        public string Location { get; set; }
        public int PointValue { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public virtual ICollection<ActivityParticipation> ActivityParticipations { get; set; }
    }
}