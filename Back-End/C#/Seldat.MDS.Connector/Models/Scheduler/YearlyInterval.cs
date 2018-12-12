using System;

namespace Seldat.MDS.Connector
{
    public class YearlyInterval : IntervalPattern, IConditionBased
    {
        public int Month { get; set; }
        public ICondition Condition { get; set; }
        public override RecurrenceType RecurrencyType
        {
            get
            {
                return RecurrenceType.Yearly;
            }
        }
    }
}