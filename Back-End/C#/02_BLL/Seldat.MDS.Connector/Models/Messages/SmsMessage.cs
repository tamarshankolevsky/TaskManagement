namespace Seldat.MDS.Connector
{
    public class SmsMessage : Message
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
