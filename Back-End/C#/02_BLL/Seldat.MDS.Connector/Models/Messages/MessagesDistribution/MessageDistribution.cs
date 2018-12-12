using Seldat.MDS.Connector.Models.Messages.MessagesDistribution;
using System.Collections.Generic;

namespace Seldat.MDS.Connector
{
    public enum MethodType
    {
        To = 1,
        CC = 2,
        Bcc = 3
    }

    public enum RecipientType
    {
        MailingList = 1,
        Contact = 2
    }

    public abstract class MessageDistribution : BaseModel
    {
        public int AccountId { get; set; }
        public List<IContact> To { get; set; }
        public Template Template { get; set; }
        public abstract MessageType Type { get; }
        public MessageStatus Status { get; set; }
        public List<Attachment> Attachments { get; set; }
        public int TotalMessages { get; set; }
        public Scheduling Scheduling { get; set; }
        public BNRequest bnRequst { get; set; }
    }
}
