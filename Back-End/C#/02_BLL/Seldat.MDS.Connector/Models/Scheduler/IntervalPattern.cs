namespace Seldat.MDS.Connector
{
    public enum RecurrenceType
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
    public abstract class IntervalPattern
    {
        public abstract RecurrenceType RecurrencyType { get; }
        public int Interval { get; set; }
    }
}
