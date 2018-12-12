namespace Seldat.MDS.Connector
{
    public enum ConditionType
    {
        Simple,
        Relative
    }

    public abstract class ICondition
    {
        public abstract ConditionType ConditionType { get; }
    }

    public class RelativeCondition : ICondition
    {
        public int WeekDay { get; set; }
        public int WeekNumber { get; set; }

        public override ConditionType ConditionType
        {
            get
            {
                return ConditionType.Relative;
            }
        }
    }

    public class SimpleCondition : ICondition
    {    
        public int MonthDay { get; set; }

        public override ConditionType ConditionType
        {
            get
            {
                return ConditionType.Simple;
            }
        }
    }
}
