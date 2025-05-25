using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YouthUnionManagement.Models
{
    public class Achievement
    {
        [Key]
        public int AchievementId { get; set; }
        
        [ForeignKey("MemberId")]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public DateTime AwardDate { get; set; }
        public int PointValue { get; set; } // Giá trị điểm của khen thưởng
        [StringLength(100)]
        public string AwardedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}