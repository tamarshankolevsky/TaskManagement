namespace Seldat.MDS.Connector
{
    public class MonthlyInterval : IntervalPattern, IConditionBased
    {
        public ICondition Condition { get; set; }

        public override RecurrenceType RecurrencyType
        {
            get
            {
                return RecurrenceType.Monthly;
            }
        }
    }
}
