using System;

namespace Seldat.MDS.Connector
{
    public enum RangeType
    {
        NoEndDate,
        EndBy,
        AfterOccurrences
    }
    public class Range
    {
        public RangeType Type { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
        public int Occurrences { get; set; }
    }
}
