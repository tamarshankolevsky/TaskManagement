using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;

namespace WebApi.Models
{
    public  class Global
    {
        public static HttpResponseMessage ErrorList(ModelStateDictionary ModelState)
        {
            List<string> ErrorList = new List<string>();

            //if the code reached this part - the project is not valid
            foreach (var item in ModelState.Values)
                foreach (var err in item.Errors)
                    ErrorList.Add(err.ErrorMessage);

            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent<List<string>>(ErrorList, new JsonMediaTypeFormatter())
            };

        }
    }
}
