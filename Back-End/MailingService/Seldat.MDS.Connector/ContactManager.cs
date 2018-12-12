using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Seldat.MDS.Connector
{
    public class ContactManager
    {
        public static List<Contact> Get()
        {
            HttpResponseMessage response = Base.Get("contact");
            return JsonConvert.DeserializeObject<List<Contact>>(response.Content.ReadAsStringAsync().Result);
        }

        public static Contact Get(int id)
        {
            HttpResponseMessage response = Base.Get("contact/{0}", id);
            return JsonConvert.DeserializeObject<Contact>(response.Content.ReadAsStringAsync().Result);
        }

        public static int Create(Contact contact)
        {

            StringContent content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");

            HttpResponseMessage response = Base.Post("contact", content);

            return int.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public static int Update(int id, Contact contact)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");

            HttpResponseMessage response = Base.Put("contact/{0}", content, id);

            return int.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public static int Delete(int id)
        {
            HttpResponseMessage response = Base.Delete("contact/{0}", id);

            return int.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public static int UpdateToken(Contact contact)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");

            HttpResponseMessage response = Base.Put("contact/token", content);

            return int.Parse(response.Content.ReadAsStringAsync().Result);
        }
    }
}
