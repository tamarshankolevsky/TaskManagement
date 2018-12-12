namespace Seldat.MDS.Connector
{
    public class SmsMessageDistribution : MessageDistribution
    {
        public override MessageType Type
        {
            get
            {
                return MessageType.Sms;
            }
        }
    }
}
