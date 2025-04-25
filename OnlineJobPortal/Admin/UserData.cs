using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineJobPortal.Admin
{
    public class UserData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public DataTable GetUserList()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT Row_Number() OVER (ORDER BY (SELECT 1)) AS [Sr.No], 
                                 UserId, Name, Email, Mobile, Country 
                                 FROM [User]";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public bool DeleteUser(int userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM [User] WHERE UserId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
