using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineJobPortal.User
{
    public class ClassProfile
    {
        private readonly string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public DataTable GetUserProfile(string username)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                string query = "SELECT UserId, Username, Name, Address, Mobile, Email, Country, Resume FROM [User] WHERE Username = @username";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt.Rows.Count > 0 ? dt : null;
                    }
                }
            }
        }
    }
}
