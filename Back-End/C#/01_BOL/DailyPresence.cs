using _01_BOL.validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_BOL
{
    class DailyPresence
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("UserProject")]
        public int UserProjectId { get; set; }

        public DateTime Date { get; set; }

        public DateTime Start { get; set; } = DateTime.Now;

        [RangeDate]
        public DateTime End { get; set; }

    }
}
