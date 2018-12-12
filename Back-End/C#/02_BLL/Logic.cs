using _00_DAL;
using _01_BOL;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace _02_BLL
{
    public class Logic
    {
        public static List<Status> GetStatuses()
        {
            string query = $"SELECT * FROM  task_managment.Statuses";

            Func<MySqlDataReader, List<Status>> func = (reader) => {
                List<Status> Statuses = new List<Status>();
                while (reader.Read())
                {
                    Statuses.Add(new Status
                    {
                        Id = reader.GetInt32(0),
                       Name = reader.GetString(1),
                    });
                }
                return Statuses;
            };
            return DBAccess.RunReader(query, func);
        }
        
    }
}
