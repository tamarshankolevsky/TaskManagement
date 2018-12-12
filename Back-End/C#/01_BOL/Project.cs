using _01_BOL.validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace _01_BOL
{
    public class Project
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringValidator(MaxLength = 15, MinLength = 2)]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Project name can contain only letters")]
        [UniqeName]
        public string Name { get; set; }

        [ForeignKey("TeamLeader")]
        public int TeamLeaderId { get; set; }

        [Required]
        [MinLength(2), MaxLength(15)]
        public string Customer { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "development houres must be above 0")]
        public int DevelopHours { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "QA houres Houres must be above 0")]
        public int QAHours { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "UI_UX houres Houres must be above 0")]
        public int UiUxHours { get; set; }

        
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [RangeDate]
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }

        public bool IsComplete { get; set; } 

    }
}
