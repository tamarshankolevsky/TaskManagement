using System;
using System.ComponentModel.DataAnnotations;

namespace _01_BOL
{
    public class ActualHours
    {
        [Required]
        public int ActualHoursId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public double CountHours { get; set; }
        [Required]
        public DateTime date { get; set; }
    }
}
