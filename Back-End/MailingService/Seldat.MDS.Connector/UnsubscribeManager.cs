using System.Net.Http;

namespace Seldat.MDS.Connector
{
    public class UnsubscribeManager
    {
        public static string Unsubscribe(int accountId, int mailingListId, string email)
        {
            HttpResponseMessage response = Base.Delete("unsubscribe?accountId={0}&mailingListId={1}&email={2}", accountId, mailingListId, email);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
