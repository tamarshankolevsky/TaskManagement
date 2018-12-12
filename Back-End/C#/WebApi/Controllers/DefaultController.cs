using _01_BOL;
using _02_BLL;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;


namespace UIL.Controllers
{
    public class DefaultController : ApiController
    {
        /// <summary>
        /// get all statuses 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getAllStatuses")]
        public HttpResponseMessage GetAllStatuses()
        {
           return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<Status>>(Logic.GetStatuses(), new JsonMediaTypeFormatter())
            };
        }

    }
}
