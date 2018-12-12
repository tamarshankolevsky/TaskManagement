using Newtonsoft.Json;
using System.Collections;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace Seldat.MDS.Connector
{
    public class LoginManager
    {
        public static string Login()
        {
            

            string url = "grant_type=password&username=" + ConfigMembers.Username + "&password=" + ConfigMembers.Password;

            StringContent content = new StringContent(url, Encoding.UTF8, "application/json");

            HttpResponseMessage response = Base.PostNonAuthorize("login", content);

            dynamic result = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

            string token = result.access_token;

            Base.AddAuthorizationHeader(token);

            return token;
        }

        public static void Logout()
        {
            Base.Logout();
        }
    }
}
