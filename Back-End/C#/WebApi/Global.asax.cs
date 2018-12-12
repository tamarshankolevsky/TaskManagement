using _02_BLL;
using Seldat.MDS.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
          //  HomeLogic.sendEmail("bsd", "trust in hashem!!!", "rachel.novak@seldatinc.com");
            Dictionary<string, object> information = new Dictionary<string, object>
                         {
                            { "sub","sub"},
                            {"body","body"}
                         };
         string s=   MessageDistributionManager.SendEmail(1092, "rachel.novak@seldatinc.com", information);
          string  rrs = s;
        }
    }
}
