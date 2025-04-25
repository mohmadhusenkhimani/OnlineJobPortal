using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineJobPortal.Admin
{
    public class DashboardData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public int GetUsersCount()
        {
            return GetCountFromDatabase("SELECT COUNT(*) FROM [User]");
        }

        public int GetJobsCount()
        {
            return GetCountFromDatabase("SELECT COUNT(*) FROM [Jobs]");
        }

        public int GetAppliedJobsCount()
        {
            return GetCountFromDatabase("SELECT COUNT(*) FROM [AppliedJobs]");
        }

        public int GetContactCount()
        {
            return GetCountFromDatabase("SELECT COUNT(*) FROM [Contact]");
        }

        private int GetCountFromDatabase(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
