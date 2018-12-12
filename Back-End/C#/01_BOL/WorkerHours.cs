using System;

namespace _01_BOL
{
    public class WorkerHours
    {
        public int? Id { get; set; }
        public String Name { get; set; }
        public DateTime Date { get; set; }
        public string Hours { get; set; }
        public int? allocatedHours { get; set; }
    }
}
