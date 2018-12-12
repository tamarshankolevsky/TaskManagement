using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Seldat.MDS.Connector
{
    public class MailingListManager
    {
        public static List<MailingList> Get()
        {
            HttpResponseMessage response = Base.Get("mailingList");
            return JsonConvert.DeserializeObject<List<MailingList>>(response.Content.ReadAsStringAsync().Result);
        }

        public static MailingList Get(int id)
        {
            HttpResponseMessage response = Base.Get("mailingList/{0}", id);
            return JsonConvert.DeserializeObject<MailingList>(response.Content.ReadAsStringAsync().Result);
        }

        public static int Create(MailingList mailingList)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(mailingList), Encoding.UTF8, "application/json");

            HttpResponseMessage response = Base.Post("mailingList", content);

            return int.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public static int Update(int id, MailingList mailingList)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(mailingList), Encoding.UTF8, "application/json");

            HttpResponseMessage response = Base.Put("mailingList/{0}", content, id);

            return int.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public static int Delete(int id)
        {
            HttpResponseMessage response = Base.Delete("mailingList/{0}", id);

            return int.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public static int DeleteContact(int mailingListId, int contactId)
        {
            HttpResponseMessage response = Base.Delete("mailingList/{0}/{1}", mailingListId, contactId);

            return int.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public static bool HasContacts(int mailingListId)
        {
            HttpResponseMessage response = Base.Get("mailingList/hasContacts/{0}", mailingListId);
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
