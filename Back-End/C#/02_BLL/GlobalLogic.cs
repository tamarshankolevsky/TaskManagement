using _01_BOL;
using MySql.Data.MySqlClient;
using System;

namespace _02_BLL
{
    public class GlobalLogic
    {
        public static User InitUser(MySqlDataReader reader)
        {
            int? managerId;
            if (reader.IsDBNull(6))
                managerId = null;
            else
                managerId = reader.GetInt32(6);

            User user = new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                UserName = reader.GetString(2),
                Password = reader.GetString(3),
                EMail = reader.GetString(4),
                StatusId = reader.GetInt32(5),
                ManagerId = managerId,
            };
            return user;
        }

        public static Project InitProject(MySqlDataReader reader)
        {
            Project project = new Project
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Customer = reader.GetString(2),
                TeamLeaderId = reader.GetInt32(3),
                DevelopHours = reader.GetInt32(4),
                QAHours = reader.GetInt32(5),
                UiUxHours = reader.GetInt32(6),
                StartDate = Convert.ToDateTime(reader.GetDateTime(7)),
                EndDate = Convert.ToDateTime(reader.GetDateTime(8)),
                IsComplete = reader.GetBoolean(9)
            };
            return project;
        }

        public static WorkerHours InitWorkerHours(MySqlDataReader reader)
        {
            WorkerHours unknown = new WorkerHours
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                allocatedHours = reader.GetInt32(2),
                Hours = reader.GetString(3)
            };
            return unknown;
        }
    }
}
