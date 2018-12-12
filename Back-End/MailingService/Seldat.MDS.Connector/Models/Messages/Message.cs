using System.Collections.Generic;

namespace Seldat.MDS.Connector
{
    public abstract class Message
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public abstract MessageType Type { get; }

        public MessageStatus Status { get; set; }

        public List<Attachment> Attachments { get; set; }

        public int DistributionId { get; set; }

    }
}
