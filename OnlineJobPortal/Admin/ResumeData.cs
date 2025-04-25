using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineJobPortal.Admin
{
    public class ResumeData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public DataTable GetAppliedJobs()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS [Sr.No],
                    aj.AppliedJobId,
                    aj.JobId,
                    j.Title AS Title,
                    j.CompanyName,
                    u.Mobile,
                    u.Name,
                    u.Email,
                    u.Resume
                FROM Appliedjobs aj
                INNER JOIN [User] u ON aj.UserId = u.UserId
                INNER JOIN Jobs j ON aj.JobId = j.JobId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public bool DeleteResume(int appliedJobId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Appliedjobs WHERE AppliedJobId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", appliedJobId);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
