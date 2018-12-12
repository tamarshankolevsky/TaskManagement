namespace Seldat.MDS.Connector
{
    public class DailyInterval : IntervalPattern
    {
        public bool WeekDaysOnly { get; set; }
        public override RecurrenceType RecurrencyType
        {
            get
            {
                return RecurrenceType.Daily;
            }
        }
    }
}
