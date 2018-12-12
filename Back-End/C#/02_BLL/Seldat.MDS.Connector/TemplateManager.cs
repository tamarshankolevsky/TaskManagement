using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Seldat.MDS.Connector
{
    public class TemplateManager
    {
        public static List<Template> Get()
        {
            HttpResponseMessage response = Base.Get("template");
            return JsonConvert.DeserializeObject<List<Template>>(response.Content.ReadAsStringAsync().Result);
        }

        public static Template Get(int id)
        {
            HttpResponseMessage response = Base.Get("template/{0}", id);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Template>(response.Content.ReadAsStringAsync().Result);
        }

        public static List<Template> GetByType(TemplateType type)
        {
            HttpResponseMessage response = Base.Get("template/type/{0}", type);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<Template>>(response.Content.ReadAsStringAsync().Result);
        }

        public static List<Template> GetAllMaster(TemplateType type)
        {
            HttpResponseMessage response = Base.Get("template/master/{0}", type);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<Template>>(response.Content.ReadAsStringAsync().Result);
        }

        public static int Create(Template template)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(template), Encoding.UTF8, "application/json");

            HttpResponseMessage response = Base.Post("template", content);

            return int.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public static int Update(int id, Template template)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(template), Encoding.UTF8, "application/json");

            HttpResponseMessage response = Base.Put("contact/{0}", content, id);

            return int.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public static int Delete(int id)
        {
            HttpResponseMessage response = Base.Delete("contact/{0}", id);

            return int.Parse(response.Content.ReadAsStringAsync().Result);
        }
    }
}