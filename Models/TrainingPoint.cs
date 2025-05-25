using System;
using System.ComponentModel.DataAnnotations;

namespace YouthUnionManagement.Models
{
    public class TrainingPoint
    {
        [Key]
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public int Points { get; set; }
        public string Evaluation { get; set; }
        public virtual Member Member { get; set; }
    }
}