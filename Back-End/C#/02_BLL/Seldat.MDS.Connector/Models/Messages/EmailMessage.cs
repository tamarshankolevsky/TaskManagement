using System.Collections.Generic;

namespace Seldat.MDS.Connector
{
    public enum MessageType
    {
        Sms = 1,
        PushNotification = 2,
        Email = 3
    }

    public enum MessageStatus
    {
        Accepted,
        Started,
        Failed,
        Success,
        Completed
    }

    public class EmailMessage : Message
    {

        public List<string> CC { get; set; }

        public List<string> Bcc { get; set; }

        public override MessageType Type
        {
            get
            {
                return MessageType.Email;
            }
        }

        public IServerConfiguration ServerConfiguration { get; set; }

    }
}
