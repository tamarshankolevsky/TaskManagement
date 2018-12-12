using System.Collections.Generic;

namespace Seldat.MDS.Connector
{
    public enum OperationSystem
    {
        Android,
        iOS
    }
    public class Contact : BaseModel, IContact
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Language Language { get; set; }
        public IDictionary<string, object> Info { get; set; }
        public int AccountId { get; set; }
        public string Token { get; set; }
        public OperationSystem? OS { get; set; }

        public RecipientType Type
        {
            get
            {
                return RecipientType.Contact;
            }
        }
    }
}
