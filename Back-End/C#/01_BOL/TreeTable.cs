using System.Collections.Generic;


namespace _01_BOL
{
    public class TreeTable
    {
        public Project Project { get; set; }
        public User User { get; set; }
        public List<DetailsWorkerInProjects> DetailsWorkerInProjects { get; set; }
    }
}
