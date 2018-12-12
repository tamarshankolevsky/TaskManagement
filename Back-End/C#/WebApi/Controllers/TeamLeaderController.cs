using _01_BOL;
using _02_BLL;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;


using Newtonsoft.Json.Linq;

namespace UIL.Controllers
{
    public class TeamLeaderController : ApiController
    {
        /// <summary>
        /// get the details and status of the current project that the teamLeader manage
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getProjectDeatails/{teamLeaderId}")]
        public HttpResponseMessage GetProjectDeatails(int teamLeaderId) => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ObjectContent<List<Project>>(TeamLeaderLogic.GetProjectDeatails(teamLeaderId), new JsonMediaTypeFormatter())
        };

        /// <summary>
        /// get workers details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getWorkersDeatails/{teamLeaderId}")]
        public HttpResponseMessage GetWorkersDeatails(int teamLeaderId) => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ObjectContent<List<User>>(TeamLeaderLogic.GetWorkersDeatails(teamLeaderId), new JsonMediaTypeFormatter())
        };

        /// <summary>
        /// get workers in projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getWorkersForProject/{teamLeaderId}")]
        public HttpResponseMessage GetWorkersForProject(int teamLeaderId) => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ObjectContent<List<User>>(TeamLeaderLogic.GetWorkersForProject(teamLeaderId), new JsonMediaTypeFormatter())
        };

        /// <summary>
        /// get the workers hours in projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getWorkersHours/{projectId}")]
        public HttpResponseMessage GetWorkersHours(int projectId) => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ObjectContent<List<Object>>(TeamLeaderLogic.getWorkersHours(projectId), new JsonMediaTypeFormatter())
        };

        /// <summary>
        /// get worker hours for all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getWorkerHours/{teamLeaderId}/{workerId}")]
        public HttpResponseMessage GetWorkerHours(int teamLeaderId, int workerId) => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ObjectContent<List<WorkerHours>>(TeamLeaderLogic.getWorkerHours(teamLeaderId, workerId), new JsonMediaTypeFormatter())
        };

        /// <summary>
        ///  get all the hours that used in current month
        /// </summary>
        [HttpGet]
        [Route("api/getHours/{projectId}")]
        public HttpResponseMessage GetHours(int projectId)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<String>(TeamLeaderLogic.GetHours(projectId), new JsonMediaTypeFormatter())
            };
        }

        [HttpPut]
        [Route("api/updateWorkerHours")]
        public HttpResponseMessage UpdateWorkerHours([FromBody]JObject data)
        {
            int projectWorkerId = (int)data["projectWorkerId"];
            int numHours = (int)data["numHours"];
            return (TeamLeaderLogic.UpdateWorkerHours(projectWorkerId, numHours)) ?
                    new HttpResponseMessage(HttpStatusCode.OK) :
                    new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new ObjectContent<String>("Can not update in Data Base", new JsonMediaTypeFormatter())
                    };
        }

        [HttpPut]
        [Route("api/updateProjectDetails")]
        public HttpResponseMessage UpdateProjectDetails([FromBody]JObject data)
        {
            int projectId = (int)data["projectId"];
            int developHours = (int)data["developHours"];
            int QAHours = (int)data["QAHours"];
            int UIUXHours = (int)data["UIUXHours"];
            DateTime endDate = (DateTime)data["endDate"];
            bool isComplete = (bool)data["isComplete"];
            return (TeamLeaderLogic.UpdateProjectDetails(projectId, developHours, QAHours, UIUXHours, endDate, isComplete)) ?

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
        /// get the remaining hours
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getRemainingHours/{projectId}/{jobId}")]
        public HttpResponseMessage GetRemainingHours(int projectId, int jobId) => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ObjectContent<string>(TeamLeaderLogic.GetRemainingHours(projectId, jobId), new JsonMediaTypeFormatter())
        };

    }
}