using _00_DAL;
using _01_BOL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace _02_BLL
{
    public class WorkerLogic
    {
        //Update employee start-up hours
        public static bool UpdateStartHour(int idProjectWorker, DateTime hour)
        {
            string query = $"INSERT INTO task_managment.daily_presence (user_project_id,date,start)" +
                $" VALUES({idProjectWorker}, '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}', '{hour.TimeOfDay}')";
            return DBAccess.RunNonQuery(query) == 1;
        }

        //Update employee time-out hours
        public static bool UpdateEndHour(int idProjectWorker, DateTime hour)
        {
            string query = $"UPDATE task_managment.daily_presence  SET end='{hour.TimeOfDay}'  WHERE user_project_id={idProjectWorker}" +
                $" AND end IS NULL";
            return DBAccess.RunNonQuery(query) == 1;
        }

        // send email from the worker to his team leader
        public static bool SendMsg(string sub, string body, int id)
        {
            string query = $"SELECT manager FROM task_managment.users WHERE user_id = {id}";
            int managerId = (int)DBAccess.RunScalar(query);
            query = $"SELECT email FROM task_managment.users WHERE user_id = {managerId}";
            string email = (string)DBAccess.RunScalar(query);
            return HomeLogic.sendEmail(sub, body, email);
        }

        public static List<User> GetWorkerDetails(int id)
        {
            string query = $"SELECT * FROM task_managment.users WHERE user_id={id}";

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

        //get presence of spesific user projects
        public static List<WorkerHours> GetProject(int id)
        {
            string query = $"SELECT up.user_project_id, p.name,allocated_hours, SEC_TO_TIME(SUM(TIME_TO_SEC(end) - TIME_TO_SEC(start))) AS Time" +
        $" FROM user_projects UP JOIN projects P ON P.project_id = up.project_id LEFT JOIN" +
        $" daily_presence dp ON up.user_project_id = dp.user_project_id" +
        $" WHERE up.user_id = {id} and p.is_complete=0" +
        $" GROUP BY up.user_project_id,p.name,allocated_hours " +
        $" ORDER BY dp.user_project_id";

            Func<MySqlDataReader, List<WorkerHours>> func = (reader) =>
            {
                List<WorkerHours> unknowns = new List<WorkerHours>();
                while (reader.Read())
                {
                    string s = reader[2].ToString();
                    int.TryParse(s, out int x);
                    string s2;
                    try
                    {
                        TimeSpan t = reader.GetTimeSpan(3);
                        s2 = (t.Hours + t.Days * 24) + ":" + t.Minutes;
                    }
                    catch { s2 = 0 + ":" + 0; };
                    s2 = reader[3].ToString();
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

        // Get all hours of a particular worker by id
        public static string GetAllHours(int id)
        {
            string query = $"SELECT SEC_TO_TIME(SUM(TIME_TO_SEC(end) - TIME_TO_SEC(start))) FROM task_managment.daily_presence" +
                $" WHERE user_project_id IN(SELECT user_project_id" +
                $" FROM task_managment.user_projects WHERE user_id = {id})";
            return DBAccess.RunScalar(query).ToString();

        }
    }

}
