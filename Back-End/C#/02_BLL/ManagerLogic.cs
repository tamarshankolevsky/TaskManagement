using _00_DAL;
using _01_BOL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace _02_BLL
{
    public class ManagerLogic
    {
        public static List<User> GetAllManagers()
        {
            string query = $"SELECT * FROM task_managment.users WHERE status=1";
            Func<MySqlDataReader, List<User>> func = (reader) =>
            {
                List<User> managers = new List<User>();
                while (reader.Read())
                {
                    managers.Add(GlobalLogic.InitUser(reader));
                }
                return managers;
            };
            return DBAccess.RunReader(query, func);

        }

        public static bool AddUser(User user)
        {
            int? managerId = user.ManagerId == null ? null : user.ManagerId;
            string query;
            if (managerId != null)
                query = $"INSERT INTO task_managment.users  " +
                    $"(name,user_name,password,email,status,manager)" +
                    $" VALUES ('{user.Name}','{user.UserName}'," +
                    $"'{user.Password}','{user.EMail}',{user.StatusId},{managerId})";
            else
            {
                query = $"INSERT INTO task_managment.users  " +
                    $"(name,user_name,password,email,status,manager)" +
                    $" VALUES ('{user.Name}','{user.UserName}'," +
                    $"'{user.Password}','{user.EMail}',{user.StatusId},null)";
            }
            return DBAccess.RunNonQuery(query) == 1;
        }

        public static bool UpdateUser(User user)
        {
            string query;
            if (user.ManagerId != null)
                query = $"UPDATE task_managment.users SET name='{user.Name}', user_name='{user.UserName}' ," +
                $" email='{user.EMail}', status={user.StatusId}, manager={user.ManagerId} WHERE user_id={user.Id}";
            else
                query = $"UPDATE task_managment.users SET name='{user.Name}', user_name='{user.UserName}'" +
                           $" , email='{user.EMail}', status={user.StatusId}, manager=null WHERE user_id={user.Id}";
            return DBAccess.RunNonQuery(query) == 1;
        }

        public static bool UpdatePassword(User user)
        {
            string query = $"UPDATE task_managment.users SET password='{user.Password}'WHERE user_id={user.Id}";
            return DBAccess.RunNonQuery(query) == 1;
        }

        public static List<User> GetAllUsers()
        {
            string query = $"SELECT * FROM task_managment.users where is_active=1 && status!=1";

            Func<MySqlDataReader, List<User>> func = (reader) =>
            {
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    users.Add(GlobalLogic.InitUser(reader));
                }
                return users;
            };
            return DBAccess.RunReader(query, func);
        }

        public static bool RemoveUser(int id)
        {
            string query;
            query = $"UPDATE task_managment.users SET is_active=0 where user_id={id} AND is_active=1;";
            return DBAccess.RunNonQuery(query) == 1;
        }

        public static bool AddProject(Project project)
        {
            string query = $"INSERT INTO task_managment.projects  " +
                $" (name,customer,team_leader,develop_houres,qa_houres,ui_ux_houres,start_date,end_date)" +
                $"  VALUES ('{project.Name}','{project.Customer}'," +
                $" '{project.TeamLeaderId}',{project.DevelopHours},{project.QAHours},{project.UiUxHours}," +
                $" '{project.StartDate.Year}-{project.StartDate.Month}-{project.StartDate.Day}','{project.EndDate.Year}-{project.EndDate.Month}-{project.EndDate.Day}')";
            return DBAccess.RunNonQuery(query) == 1;
        }

        public static bool addWorkersToProject(int[] ids, string name)
        {
            string q = $"SELECT project_id FROM projects WHERE name = '{name}'";
            int idProject = (int)DBAccess.RunScalar(q);
            foreach (var item in ids)
            {
                string query = $"INSERT INTO task_managment.user_projects(user_id,project_id) VALUES({item},{idProject})";
                if (DBAccess.RunNonQuery(query) == 0)
                    return false;
            }
            return true;
        }

        public static List<User> getAllTeamLeaders()
        {
            string query = $"SELECT * FROM task_managment.users WHERE status=2";
            Func<MySqlDataReader, List<User>> func = (reader) =>
            {
                List<User> teamLeaders = new List<User>();
                while (reader.Read())
                {
                    teamLeaders.Add(GlobalLogic.InitUser(reader));
                }
                return teamLeaders;
            };
            return DBAccess.RunReader(query, func);
        }

        public static List<Project> getAllProjects()
        {
            string query = $"SELECT * FROM task_managment.projects";
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

        public static List<Object> GetPresence()
        {
            string query = $"SELECT w.name, p.name, wh.date , wh.start , wh.end" +
                   $" FROM users w JOIN user_projects pw ON w.user_id = pw.user_id" +
                   $" JOIN projects P ON pw.project_id = p.project_id JOIN daily_presence wh" +
                   $" ON wh.user_project_id = pw.user_project_id" +
                   $" ORDER BY w.name, p.name, wh.date , wh.start";

            Func<MySqlDataReader, List<Object>> func = (reader) =>
            {
                List<Object> Presence = new List<Object>();
                while (reader.Read())
                {
                    Presence.Add(new
                    {
                        WorkerName = reader.GetString(0),
                        ProjectName = reader.GetString(1),
                        Date = reader.GetDateTime(2),
                        Start = reader.GetString(3),
                        End = reader.GetString(4),
                    });
                }
                return Presence;
            };
            return DBAccess.RunReader(query, func);
        }

    }
}
