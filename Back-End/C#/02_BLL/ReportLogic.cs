using _00_DAL;
using _01_BOL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace _02_BLL
{
    public class ReportLogic
    {
        static List<TreeTable> treeTables = new List<TreeTable>();
        public static List<TreeTable> GetAllTreeTable()
        {
            string query = $"SELECT p.*,user_id,user_name FROM task_managment.projects P  JOIN task_managment.users u ON u.user_id=p.team_leader ";
            Func<MySqlDataReader, List<TreeTable>> func = (reader) =>
            {
                treeTables = new List<TreeTable>();
                while (reader.Read())
                {
                    treeTables.Add(new TreeTable
                    {
                        Project = new Project()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Customer = reader.GetString(2),
                            TeamLeaderId = reader.GetInt32(3),
                            DevelopHours = reader.GetInt32(4),
                            QAHours = reader.GetInt32(5),
                            UiUxHours = reader.GetInt32(6),
                            StartDate = reader.GetDateTime(7),
                            EndDate = reader.GetDateTime(8),
                        },
                        User = new User() { Id = reader.GetInt32(9), UserName = reader.GetString(10) }
                    });
                }
                return treeTables;
            };
            DBAccess.RunReader(query, func);
            FillHoursToTreeTable();
            return treeTables;
        }

        public static void FillHoursToTreeTable()
        {
            foreach (var item in treeTables)
            {
                string query = $"select allocated_hours,s.name,u.user_name,u.user_id,us.user_name as teamLeaderName" +
                    $" from users u join users us on u.manager=us.user_id " +
                    $"join user_projects up on up.user_id = u.user_id  join statuses s on s.status_id = u.status where up.project_id ={item.Project.Id}; ";
                Func<MySqlDataReader, List<TreeTable>> func = (reader) =>
                {
                    item.DetailsWorkerInProjects = new List<DetailsWorkerInProjects>();
                    while (reader.Read())
                    {
                        item.DetailsWorkerInProjects.Add(new DetailsWorkerInProjects()
                        {
                            Hours = reader.GetInt32(0),
                            Status = reader.GetString(1),
                            Name = reader.GetString(2),
                            UserId = reader.GetInt32(3),
                            TeamLeaderName = reader.GetString(4)
                        });
                    }
                    return treeTables;
                };

                DBAccess.RunReader(query, func);
            }
            FillActualHoursToTreeTable();
        }

        public static void FillActualHoursToTreeTable()
        {
            foreach (var item in treeTables)
            {
                foreach (var item1 in item.DetailsWorkerInProjects)
                {
                    string query = $"SELECT up.user_project_id, p.name , allocated_hours , SEC_TO_TIME(SUM(TIME_TO_SEC(end) - TIME_TO_SEC(start)))" +
            $" FROM user_projects UP  join projects p on p.project_id=up.project_id" +
            $" LEFT join daily_presence DP on up.user_project_id= dp.user_project_id" +
            $" where  p.project_id={item.Project.Id} and up.user_id= {item1.UserId}" +
            $" group by up.user_project_id, p.name,allocated_hours";

                    Func<MySqlDataReader, List<TreeTable>> func = (reader) =>
                    {
                        item1.ActualHours = new List<WorkerHours>();
                        while (reader.Read())
                        {
                            string s = reader[2].ToString();
                            int.TryParse(s, out int x);
                            string s2 = reader[3].ToString();
                            item1.ActualHours.Add(new WorkerHours
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                allocatedHours = x,
                                Hours = s2
                            });
                        }
                        return treeTables;
                    };
                    DBAccess.RunReader(query, func);
                }
            }
        }
    }
}