using Newtonsoft.Json;
using Quartz;
using Seldat.MDS.Connector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace MailingService
{
    public class EmailsManager : IJob
    {
        public const string url = "http://localhost:53728/api/";
        public Task Execute(IJobExecutionContext context)
        {

            // get the details of the mail to sent
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url + "sendEmail");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"sendEmail").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                List<Email> emailList = JsonConvert.DeserializeObject<List<Email>>(result);
                if (emailList != null && emailList.Count > 0)
                {
                    //send the email by mds
                    emailList.ForEach(email =>
                    {
                        Dictionary<string, object> information = new Dictionary<string, object>
                         {
                            { "projectName",email.projectName},
                            {"endDate",email.endDate.Date }
                         };
                       
                        MessageDistributionManager.SendEmail(1091, email.employeesEmail, information);
                    }
                   );
                }

            }
            return null;
        }

    }
}
