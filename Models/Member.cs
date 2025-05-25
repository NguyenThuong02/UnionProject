using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouthUnionManagement.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [StringLength(20)]
        public string StudentId { get; set; }
        public DateTime DateOfBirth { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        [StringLength(100)]
        public string Class { get; set; }
        [StringLength(100)]
        public string Faculty { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public virtual ICollection<ActivityParticipation> ActivityParticipations { get; set; }
        public virtual ICollection<TrainingPoint> TrainingPoints { get; set; }
        public virtual ICollection<Reward> Rewards { get; set; }
    }
}