namespace Seldat.MDS.Connector
{
    public class Scheduling
    {
        public int Id { get; set; }
        public string JobKey { get; set; }
        public Time Time { get; set; }
        public IntervalPattern Interval { get; set; }
        public Range Range { get; set; }
    }
}
