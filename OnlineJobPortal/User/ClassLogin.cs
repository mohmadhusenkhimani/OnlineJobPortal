using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineJobPortal.User
{
    public class ClassLogin
    {
        private readonly string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public bool ValidateAdmin(string username, string password)
        {
            string adminUsername = ConfigurationManager.AppSettings["username"];
            string adminPassword = ConfigurationManager.AppSettings["password"];

            return adminUsername == username && adminPassword == password;
        }

        public Tuple<string, string> ValidateUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                string query = "SELECT UserId, Username FROM [User] WHERE Username = @Username AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            return new Tuple<string, string>(sdr["Username"].ToString(), sdr["UserId"].ToString());
                        }
                    }
                }
            }
            return null;
        }
    }
}
