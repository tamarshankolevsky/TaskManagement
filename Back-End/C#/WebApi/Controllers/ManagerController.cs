using _01_BOL;
using _02_BLL;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Collections.Generic;
using WebApi.Models;

namespace UIL.Controllers
{
    public class ManagerController : ApiController
    {
        [HttpPut]
        [Route("api/addProject")]
        public HttpResponseMessage AddProject([FromBody]Project value)
        {
            if (ModelState.IsValid)
            {
                return (ManagerLogic.AddProject(value)) ?
                   new HttpResponseMessage(HttpStatusCode.Created)
                   {
                       Content = new ObjectContent<bool>(true, new JsonMediaTypeFormatter())
                   } :
                   new HttpResponseMessage(HttpStatusCode.BadRequest)
                   {
                       Content = new ObjectContent<String>("Can not add to DB", new JsonMediaTypeFormatter())
                   };
            }
            return Global.ErrorList(ModelState);
        }

        [HttpPost]
        [Route("api/addUser")]
        public HttpResponseMessage AddWorker([FromBody]User value)
        {
            if (ModelState.IsValid || ModelState["ManagerId"] == null)
            {
                return (ManagerLogic.AddUser(value)) ?
                   new HttpResponseMessage(HttpStatusCode.Created)
                   {
                       Content = new ObjectContent<bool>(true, new JsonMediaTypeFormatter())
                   } :
                   new HttpResponseMessage(HttpStatusCode.BadRequest)
                   {
                       Content = new ObjectContent<String>("Can not add to DB", new JsonMediaTypeFormatter())
                   };
            }
            return Global.ErrorList(ModelState);
        }

        /// <summary>
        /// add workers to project
        /// </summary>
        /// <param name="ids">array of workers id</param>
        /// <param name="name">array of workers name</param>
        /// <returns>succees or not</returns>
        [HttpPost]
        [Route("api/addWorkersToProject/{name}")]
        public HttpResponseMessage addWorkersToProject([FromBody]int[] ids, [FromUri]string name)
        {
            return (ManagerLogic.addWorkersToProject(ids, name)) ?
                     new HttpResponseMessage(HttpStatusCode.OK)
                     {
                         Content = new ObjectContent<bool>(true, new JsonMediaTypeFormatter())
                     } :
                     new HttpResponseMessage(HttpStatusCode.BadRequest)
                     {
                         Content = new ObjectContent<String>("Can not update in DB", new JsonMediaTypeFormatter())
                     };
        }

        /// <summary>
        /// update worker detail
        /// </summary>
        /// <param name="value">the updated details</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/updateUser")]
        public HttpResponseMessage UpdateWorker([FromBody]User value)
        {
            if (ModelState.IsValid || ModelState["Password"] == null)
            {
                return (ManagerLogic.UpdateUser(value)) ?
                    new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ObjectContent<bool>(true, new JsonMediaTypeFormatter())
                    } :
                new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new ObjectContent<String>("Can not update in DB", new JsonMediaTypeFormatter())
                };
            };
            return Global.ErrorList(ModelState);
        }

        [HttpGet]
        [Route("api/getAllUsers")]
        public HttpResponseMessage GetAllWorkers()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<User>>(ManagerLogic.GetAllUsers(), new JsonMediaTypeFormatter())
            };
        }

        [HttpGet]
        [Route("api/getAllManagers")]
        public HttpResponseMessage GetAllManagers()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<User>>(ManagerLogic.GetAllManagers(), new JsonMediaTypeFormatter())
            };
        }

        [HttpGet]
        [Route("api/getAllTeamLeaders")]
        public HttpResponseMessage getAllTeamLeaders()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<User>>(ManagerLogic.getAllTeamLeaders(), new JsonMediaTypeFormatter())
            };
        }

        [HttpGet]
        [Route("api/getAllProjects")]
        public HttpResponseMessage getAllProjects()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<Project>>(ManagerLogic.getAllProjects(), new JsonMediaTypeFormatter())
            };
        }

        [HttpDelete]
        [Route("api/deleteUser/{id}")]
        public HttpResponseMessage DeleteWorker(int id)
        {
            return (ManagerLogic.RemoveUser(id)) ?
                    new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ObjectContent<bool>(true, new JsonMediaTypeFormatter())
                    } :
                    new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new ObjectContent<String>("Can not remove from DB", new JsonMediaTypeFormatter())
                    };
        }

        /// <summary>
        /// get presence of workers hours
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getPresence")]
        public HttpResponseMessage GetPresence()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<Object>>(ManagerLogic.GetPresence(), new JsonMediaTypeFormatter())
            };
        }

    }
}
