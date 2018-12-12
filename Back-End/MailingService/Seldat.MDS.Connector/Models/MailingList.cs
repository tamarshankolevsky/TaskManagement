using System.Collections.Generic;

namespace Seldat.MDS.Connector
{
    public class MailingList : BaseModel, IContact
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Contact> Contacts { get; set; }
        public ServerConfiguration ServerConfiguration { get; set; }
        public int AccountId { get; set; }

        public RecipientType Type
        {
            get
            {
                return RecipientType.MailingList;
            }
        }
    }
}
