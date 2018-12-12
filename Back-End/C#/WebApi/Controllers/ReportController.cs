using _01_BOL;
using _02_BLL;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Collections.Generic;


namespace UIL.Controllers
{
    public class ReportController : ApiController
    {
        /// <summary>
        /// get thr details for the report
        /// </summary>
        /// <returns>all the data</returns>
       [HttpGet]
       [Route("api/getTreeTable")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<TreeTable>>(ReportLogic.GetAllTreeTable(), new JsonMediaTypeFormatter())
            };
        }

    }
}
