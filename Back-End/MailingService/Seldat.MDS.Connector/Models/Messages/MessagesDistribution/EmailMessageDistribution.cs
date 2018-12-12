using System.Collections.Generic;

namespace Seldat.MDS.Connector
{
    public enum ContentType
    {
        Html,
        Text
    }
    public class EmailMessageDistribution : MessageDistribution
    {
        public List<IContact> CC { get; set; }
        public List<IContact> Bcc { get; set; }
        public ContentType ContentType { get; set; }

        public override MessageType Type
        {
            get
            {
                return MessageType.Email;
            }
        }
    }
}
