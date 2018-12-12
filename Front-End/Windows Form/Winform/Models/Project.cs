using System;

namespace TaskManagment.Models
{
   public class Project
    {
        public int Id { get; set; }
    
        public string Name { get; set; }

        public int TeamLeaderId { get; set; }
       
        public string Customer { get; set; }

        public int DevelopHours { get; set; }

        public int QAHours { get; set; }
       
        public int UiUxHours { get; set; }
      
        public DateTime StartDate { get; set; } = DateTime.Today;
       
        public DateTime EndDate { get; set; }
    }
}
