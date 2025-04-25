using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineJobPortal.Admin
{
    public class JobManager
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public DataTable GetJobById(int jobId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Jobs WHERE JobId = @JobId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@JobId", jobId);
                    con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public bool SaveOrUpdateJob(int? jobId, string title, int noOfPost, string description, string qualification, string experience,
            string specialization, DateTime lastDateToApply, string salary, string jobType, string companyName, string companyImage,
            string website, string email, string address, string country, string state, DateTime createDate)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query;
                if (jobId.HasValue)
                {
                    // Update existing job
                    query = @"UPDATE Jobs SET Title=@Title, NoOfPost=@NoOfPost, Description=@Description, Qualification=@Qualification, 
                              Experience=@Experience, Specialization=@Specialization, LastDateToApply=@LastDateToApply, Salary=@Salary, 
                              JobType=@JobType, CompanyName=@CompanyName, CompanyImage=@CompanyImage, Website=@Website, Email=@Email, 
                              Address=@Address, Country=@Country, State=@State WHERE JobId=@JobId";
                }
                else
                {
                    // Insert new job
                    query = @"INSERT INTO Jobs (Title, NoOfPost, Description, Qualification, Experience, Specialization, LastDateToApply, Salary, 
                              JobType, CompanyName, CompanyImage, Website, Email, Address, Country, State, CreateDate) 
                              VALUES (@Title, @NoOfPost, @Description, @Qualification, @Experience, @Specialization, @LastDateToApply, @Salary, 
                              @JobType, @CompanyName, @CompanyImage, @Website, @Email, @Address, @Country, @State, @CreateDate)";
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@NoOfPost", noOfPost);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Qualification", qualification);
                    cmd.Parameters.AddWithValue("@Experience", experience);
                    cmd.Parameters.AddWithValue("@Specialization", specialization);
                    cmd.Parameters.AddWithValue("@LastDateToApply", lastDateToApply);
                    cmd.Parameters.AddWithValue("@Salary", salary);
                    cmd.Parameters.AddWithValue("@JobType", jobType);
                    cmd.Parameters.AddWithValue("@CompanyName", companyName);
                    cmd.Parameters.AddWithValue("@CompanyImage", companyImage);
                    cmd.Parameters.AddWithValue("@Website", website);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Parameters.AddWithValue("@State", state);
                    cmd.Parameters.AddWithValue("@CreateDate", createDate);

                    if (jobId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@JobId", jobId.Value);
                    }

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
