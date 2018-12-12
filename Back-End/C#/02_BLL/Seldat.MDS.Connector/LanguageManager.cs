using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace Seldat.MDS.Connector
{
    public class LanguageManager
    {
        public static List<Language> Get()
        {
            HttpResponseMessage response = Base.Get("language");
            return JsonConvert.DeserializeObject<List<Language>>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
