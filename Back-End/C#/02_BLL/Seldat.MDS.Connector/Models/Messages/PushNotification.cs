namespace Seldat.MDS.Connector
{
    public class PushNotification : Message
    {

        public int Badge { get; set; }
        public string Sound { get; set; }
        public string OS { get; set; }

        public override MessageType Type
        {
            get
            {
                return MessageType.PushNotification;
            }
        }
    }
}
