using _01_BOL;
using _02_BLL;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using WebApi.Models;
using System.Text;
using System.Net.Mail;
using Seldat.MDS.Connector;
using User = _01_BOL.User;

namespace UIL.Controllers
{
    public class WorkerController : ApiController
    {
        static User user = new User();
        static string body;

        /// <summary>
        /// Update employee start-up hours
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/updateStartHour")]
        public HttpResponseMessage UpdateStartHour([FromBody]JObject data)
        {
            if (ModelState.IsValid)
            {
                bool isFirst = (bool)data["isFirst"];
                string s = (string)data["hour"];
                return ((isFirst ? WorkerLogic.UpdateStartHour((int)data["idProjectWorker"], Convert.ToDateTime(s))
                    : WorkerLogic.UpdateEndHour((int)data["idProjectWorker"], Convert.ToDateTime(s))) ?
                new HttpResponseMessage(HttpStatusCode.OK) :
                    new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new ObjectContent<String>("Can not update in DB", new JsonMediaTypeFormatter())
                    });
            };
            return Global.ErrorList(ModelState);
        }

        [HttpGet]
        [Route("api/VerifyEmail/{userName}")]
        public HttpResponseMessage VerifyEmail(string userName)
        {
            List<User> users = ManagerLogic.GetAllUsers();
            user = users.FindLast(u => u.UserName == userName);
            if (user != null)
            {
                string email = "rachel.novak@seldatinc.com"; //user.EMail;
                SendEmail(email);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "error");
        }

        [HttpGet]
        [Route("api/VerifyPassword/{password}")]
        public HttpResponseMessage VerifyPassword(string password)
        {
            if (password == body)
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<User>(user, new JsonMediaTypeFormatter())
                };
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "error");
        }

        private void SendEmail(string email)
        {
            List<User> users = ManagerLogic.GetAllUsers();
            try
            {
                string subject = "Verify Password";
                body = CreatePassword(6);
                string FromMail = "rachel.novak@seldatinc.com";
                string emailTo = "rachel.novak@seldatinc.com";//email;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(FromMail);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential("rachel.novak@seldatinc.com", "0556774766");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        /// <summary>
        ///  send email
        /// </summary>
        /// <param name="data">the mail details</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/sendMsg")]
        public HttpResponseMessage SendMsg([FromBody] JObject data)
        {
            if (ModelState.IsValid)
            {
                Dictionary<string, object> information = new Dictionary<string, object>
                         {
                            { "sub",(string)data["sub"]},
                            {"body",(string)data["body"]}
                         };
                string result = MessageDistributionManager.SendEmail(1092, "rachel.novak@seldatinc.com", information);
                return result =="success" ? new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<String>("yes", new JsonMediaTypeFormatter())
                } :
                   new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new ObjectContent<String>("Can not send Email", new JsonMediaTypeFormatter())
                };
            }
            return Global.ErrorList(ModelState);
        }

        [HttpPut]
        [Route("api/Users/EditPassword")]
        public HttpResponseMessage EditPassword([FromBody]User value)
        {
            if (ModelState.IsValid)
            {
                return (ManagerLogic.UpdatePassword(value)) ?
                    new HttpResponseMessage(HttpStatusCode.OK) :
                    new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new ObjectContent<String>("Can not update in DB", new JsonMediaTypeFormatter())
                    };
            };
            List<string> ErrorList = new List<string>();
            //if the code reached this part - the user is not valid
            foreach (var item in ModelState.Values)
                foreach (var err in item.Errors)
                    ErrorList.Add(err.ErrorMessage);
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent<List<string>>(ErrorList, new JsonMediaTypeFormatter())
            };
        }

        /// <summary>
        /// Get worker's details
        /// </summary>
        /// <param name="id">worker id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getWorkerDetails/{id}")]
        public HttpResponseMessage GetWorkerDetails(int id)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<User>>(WorkerLogic.GetWorkerDetails(id), new JsonMediaTypeFormatter())
            };
        }

        /// <summary>
        /// get project according to worker
        /// </summary>
        /// <param name="id">worker id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getProject/{id}")]
        public HttpResponseMessage GetProject(int id)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<WorkerHours>>(WorkerLogic.GetProject(id), new JsonMediaTypeFormatter())
            };
        }

        /// <summary>
        /// get all worker's hours  
        /// </summary>
        /// <param name="id">worker id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getAllHours/{id}")]
        public HttpResponseMessage GetAllHours(int id)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<string>(WorkerLogic.GetAllHours(id), new JsonMediaTypeFormatter())
            };
        }

    }
}