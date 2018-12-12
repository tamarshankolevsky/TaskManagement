using System.Collections.Generic;

namespace _01_BOL
{
    public class DetailsWorkerInProjects
    {
        public int UserId { get; set; }
        public string TeamLeaderName { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int Hours { get; set; }
        public List<WorkerHours> ActualHours { get; set; }
    }
}
