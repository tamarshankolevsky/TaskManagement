using _00_DAL;
using _01_BOL;
using MySql.Data.MySqlClient;
using Seldat.MDS.Connector;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace _02_BLL
{
    public static class HomeLogic
    {
        public static _01_BOL.User Login(string userName, string password)
        {
            string query = $"SELECT * FROM task_managment.users WHERE user_name='{userName}' and password='{password}' and is_active=1 ";
            Func<MySqlDataReader, List<_01_BOL.User>> func = (reader) =>
            {
                List<_01_BOL.User> userList = new List<_01_BOL.User>();

                while (reader.Read())
                {
                    userList.Add(new _01_BOL.User
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        UserName = reader.GetString(2),
                        Password = reader.GetString(3),
                        EMail = reader.GetString(4),
                        StatusId = reader.GetInt32(5),
                        ManagerId = reader[6] as int?
                    });
                }
                return userList;
            };
            List<_01_BOL.User> Users = DBAccess.RunReader(query, func);
            if (Users != null && Users.Count > 0)
                return Users[0];
            return null;
        }

        public static bool sendEmail(string sub, string body, string email)
        {
            Dictionary<string, object> information = new Dictionary<string, object>
                         {
                            { "sub","sub"},
                            {"body","body"}
                         };
           MessageDistributionManager.SendEmail(1091, "rachel.novak@seldatinc.com",null);
            return true;
        }
        
        public static List<Email> GetEmailDetailsQuery()
        {
            string teamLeaderMail = null;
            List<Email> emails = new List<Email>();
            string query = $"SELECT * FROM projects;";

            List<Project> projectList = new List<Project>();
            Func<MySqlDataReader, List<Project>> func = (reader) =>
            {
                List<Project> projects = new List<Project>();
                while (reader.Read())
                {
                    projects.Add(GlobalLogic.InitProject(reader));
                }
                return projects;
            };
            projectList = DBAccess.RunReader(query, func);

            projectList.ForEach(project =>
            {
                teamLeaderMail = null;
                var q = $"SELECT u.user_id,u.email FROM users u join user_projects up ON u.user_id=up.user_id" +
                $" LEFT JOIN daily_presence dp ON dp.user_project_id=up.user_project_id" +
                $" WHERE up.project_id={project.Id} AND u.status>2 " +
                $" AND p.end_date <= NOW() + INTERVAL 1 DAY" +
                $" GROUP BY u.email, up.allocated_hours, u.user_id"+
                $" HAVING up.allocated_hours  >" +
                $" HOUR(SEC_TO_TIME(SUM(TIME_TO_SEC(dp.end) - TIME_TO_SEC(dp.start)))) + (MINUTE(SEC_TO_TIME(SUM(TIME_TO_SEC(dp.end) - TIME_TO_SEC(dp.start))))) / 100";
                List<userEmail> userList = new List<userEmail>();
                userEmail u = new userEmail() { email = "dsfsaf", userId = 1 };
                Func<MySqlDataReader, List<userEmail>> f = (reader) =>
                {
                    List<userEmail> users = new List<userEmail>();
                    while (reader.Read())
                    {
                        users.Add(new userEmail
                        {
                            userId = reader.GetInt32(0),
                            email = reader.GetString(1),
                           
                        });
                    }
                    return users;
                };
                userList = DBAccess.RunReader(q, f);

                if (userList != null && userList.Count > 0)
                {
                    if (teamLeaderMail == null)
                    {
                        q = $"SELECT us.email FROM users us" +
                        $" join users u on us.user_id=u.manager " +
                        $"where u.user_id={userList[0].userId};";
                        teamLeaderMail = (string)DBAccess.RunScalar(q);
                    }

                    List<string> emailList = new List<string>();
                    userList.ForEach(user =>
                    {
                        emailList.Add(user.email);
                    });
                    emails.Add(new Email()
                    {
                        projectName = project.Name,
                        endDate = project.EndDate,
                        teamLeaderEmail = teamLeaderMail,
                        employeesEmail = emailList,
                    });
                }
            });
            return emails;
        }
    }
}
