using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace OnlineJobPortal.User
{
    public class ClassJobDetails
    {
        private readonly string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter sda;
        private DataTable dt1;

        public void startcon()
        {
            con = new SqlConnection(str);
            con.Open();
        }

        public DataTable getJobDetails(string jobId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(str))
            {
                string query = @"SELECT * FROM Jobs WHERE JobId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", jobId);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public int applyJob(string jobId, string userId)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(str))
            {
                string query = @"INSERT INTO AppliedJobs (JobId, UserId) VALUES (@JobId, @UserId)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@JobId", jobId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }
        public int getAppliedUserCount(string jobId)
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(str))
            {
                string query = @"SELECT COUNT(*) FROM AppliedJobs WHERE JobId = @JobId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@JobId", jobId);
                    con.Open();
                    count = (int)cmd.ExecuteScalar();
                }
            }
            return count;
        }

        public bool isApplied(string userId, string jobId)
        {
            bool applied = false;
            using (SqlConnection con = new SqlConnection(str))
            {
                string query = @"SELECT COUNT(*) FROM AppliedJobs WHERE UserId=@UserId AND JobId = @JobId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@JobId", jobId);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    applied = count > 0;
                }
            }
            return applied;
        }
    }
}
