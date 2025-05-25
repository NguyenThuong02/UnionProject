using System;
using System.ComponentModel.DataAnnotations;

namespace YouthUnionManagement.Models
{
    public class ActivityParticipation
    {
        public int MemberId { get; set; }
        public int ActivityId { get; set; }
        public DateTime RegisterDate { get; set; }
        [StringLength(50)]
        public string AttendanceStatus { get; set; }
        public string Notes { get; set; }
        public virtual Member Member { get; set; }
        public virtual Activity Activity { get; set; }
    }
}