using _00_DAL;
using _01_BOL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace _02_BLL
{
    public static class TeamLeaderLogic
    {
        public static List<Project> GetProjectDeatails(int teamLeaderId)
        {
            string query = $"SELECT * FROM task_managment.projects WHERE team_leader={teamLeaderId}";

            Func<MySqlDataReader, List<Project>> func = (reader) =>
            {
                List<Project> projects = new List<Project>();
                while (reader.Read())
                {
                    projects.Add(GlobalLogic.InitProject(reader));
                }
                return projects;
            };
            return DBAccess.RunReader(query, func);
        }

        public static List<User> GetWorkersDeatails(int teamLeaderId)
        {
            string query = $"SELECT * FROM task_managment.users WHERE manager={teamLeaderId} and is_active=1";

            Func<MySqlDataReader, List<User>> func = (reader) =>
            {
                List<User> workers = new List<User>();
                while (reader.Read())
                {
                    workers.Add(GlobalLogic.InitUser(reader));
                }
                return workers;
            };
            return DBAccess.RunReader(query, func);
        }

        public static List<User> GetWorkersForProject(int teamLeaderId)
        {
            string query = $"SELECT u.* FROM users u JOIN  user_projects up ON u.user_id= up.user_id" +
                $" WHERE project_id IN (SELECT project_id from projects WHERE team_leader= {teamLeaderId})" +
                $" GRUP BY u.user_name; ";

            Func<MySqlDataReader, List<User>> func = (reader) =>
            {
                List<User> workers = new List<User>();
                while (reader.Read())
                {
                    workers.Add(GlobalLogic.InitUser(reader));
                }
                return workers;
            };
            return DBAccess.RunReader(query, func);
        }

        //get worker presence
        public static List<Object> getWorkersHours(int projectId)
        {
            string query = $"SELECT name,SEC_TO_TIME(SUM(TIME_TO_SEC(end) - TIME_TO_SEC(start))) AS Time,allocated_hours  " +
        $" FROM users u JOIN user_projects up ON u.user_id=up.user_id LEFT JOIN daily_presence dp ON up.user_project_id= dp.user_project_id" +
        $" WHERE up.project_id= {projectId}" +
        $" GROUP BY name,allocated_hours" +
        $" ORDER BY name";

            Func<MySqlDataReader, List<Object>> func = (reader) =>
            {
                List<Object> unknowns = new List<Object>();
                while (reader.Read())
                {
                    string s = reader[2].ToString();
                    int.TryParse(s, out int x);
                    string s2;
                    try
                    {
                        TimeSpan t = reader.GetTimeSpan(1);
                        s2 = (t.Hours + t.Days * 24) + ":" + t.Minutes;
                    }
                    catch { s2 = 0 + ":" + 0; };
                    s2 = reader[1].ToString();

                    unknowns.Add(new
                    {
                        Name = reader.GetString(0),
                        Hours = s2,
                        AllocatedHours = x
                    });
                }
                return unknowns;
            };
            return DBAccess.RunReader(query, func);
        }

        //get daily presence for worker
        public static List<WorkerHours> getWorkerHours(int teamLeaderId, int workerId)
        {
            string query = $"SELECT up.user_project_id, p.name , allocated_hours , SEC_TO_TIME(SUM(TIME_TO_SEC(end) - TIME_TO_SEC(start)))" +
            $" FROM user_projects UP  JOIN projects p ON p.project_id=up.project_id" +
            $" LEFT JOIN daily_presence DP ON up.user_project_id= dp.user_project_id" +
            $" WHERE p.team_leader={teamLeaderId} AND up.user_id= {workerId}" +
            $" GROUP BY up.user_project_id, p.name,allocated_hours";

            Func<MySqlDataReader, List<WorkerHours>> func = (reader) =>
            {
                List<WorkerHours> unknowns = new List<WorkerHours>();
                while (reader.Read())
                {
                    string s = reader[2].ToString();
                    int.TryParse(s, out int x);
                    string s2 = reader[3].ToString();
                    unknowns.Add(new WorkerHours
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        allocatedHours = x,
                        Hours = s2
                    });
                }
                return unknowns;
            };
            return DBAccess.RunReader(query, func);
        }

        //get worker's allocated hours 
        public static bool UpdateWorkerHours(int projectWorkerId, int numHours)
        {
            string query = $"UPDATE task_managment.user_projects SET allocated_hours={numHours} WHERE user_project_id={projectWorkerId}";
            return DBAccess.RunNonQuery(query) == 1;
        }

        //get presence of project
        public static string GetHours(int projectId)
        {
            string query = $"SELECT SEC_TO_TIME(SUM(TIME_TO_SEC(end) - TIME_TO_SEC(start))) " +
                $" FROM task_managment.daily_presence " +
                $" WHERE user_project_id IN" +
                $" (SELECT user_project_id FROM task_managment.user_projects" +
                $" WHERE project_id={projectId});";
            return DBAccess.RunScalar(query) != null ? DBAccess.RunScalar(query).ToString() : "null";
        }

        public static bool UpdateProjectDetails(int projectId, int developHours, int QAHours, int UiUxHours, DateTime endDate, bool isComplete)
        {
            string query = $"UPDATE task_managment.projects SET develop_houres={developHours}," +
                $"qa_houres={QAHours},ui_ux_houres={UiUxHours},end_date='{endDate.Year}-{endDate.Month}-{endDate.Day}',is_complete={isComplete} WHERE project_id={projectId}";
            return DBAccess.RunNonQuery(query) == 1;
        }

        public static string GetRemainingHours(int projectId, int jobId)
        {
            string jobName = Logic.GetStatuses().FindLast(s => s.Id == jobId).Name;
            switch (jobName)
            {
                case "developer":
                    {
                        jobName = "develop_houres";
                        break;
                    }
                case "QA":
                    {
                        jobName = "qa_houres";
                        break;
                    }
                case "UxUi":
                    {
                        jobName = "ui_ux_houres";
                        break;
                    }
            }
            string query = $" SELECT {jobName} - SUM(allocated_hours)" +
                           $" FROM projects P JOIN  user_projects PW ON P.project_id = PW.project_id JOIN users W ON W.user_id = PW.user_id" +
                           $" WHERE PW.user_project_id = {projectId} AND W.status = {jobId}";
            return DBAccess.RunScalar(query).ToString();
        }

    }
}
