namespace Seldat.MDS.Connector
{
    public class WeeklyInterval : IntervalPattern
    {
        public WeekDays WeekDays { get; set; }

        public override RecurrenceType RecurrencyType
        {
            get
            {
                return RecurrenceType.Weekly;
            }
        }
    }
}