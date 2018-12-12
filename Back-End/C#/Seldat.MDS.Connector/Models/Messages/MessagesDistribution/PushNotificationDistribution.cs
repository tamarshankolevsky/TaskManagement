namespace Seldat.MDS.Connector
{
    public class PushNotificationDistribution : MessageDistribution
    {
        public override MessageType Type
        {
            get
            {
                return MessageType.PushNotification;
            }
        }
    }
}
