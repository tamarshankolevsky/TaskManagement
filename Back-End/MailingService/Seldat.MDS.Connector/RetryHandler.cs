using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Seldat.MDS.Connector
{
    public class RetryHandler : DelegatingHandler
    {
        // Strongly consider limiting the number of retries - "retry forever" is
        // probably not the most user friendly way you could respond to "the
        // network cable got pulled out."
        private const int MaxRetries = 3;

        public RetryHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            for (int i = 0; i < MaxRetries; i++)
            {
                response = base.SendAsync(request, cancellationToken).Result;

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var token = LoginManager.Login();

                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                else
                    return response;

            }

            return response;
        }
    }

}
