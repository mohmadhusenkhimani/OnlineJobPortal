using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace OnlineJobPortal.User
{
    public class ClassRegister
    {
        private readonly string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        private SqlConnection con;
        private SqlCommand cmd;

        public void startcon()
        {
            con = new SqlConnection(str);
            con.Open();
        }

        public bool checkUsername(string username)
        {
            bool exists = false;
            using (SqlConnection con = new SqlConnection(str))
            {
                string checkQuery = "SELECT COUNT(*) FROM [User] WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(checkQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    exists = count > 0;
                }
            }
            return exists;
        }

        public int insert(string username, string password, string name, string email, string mobile, string address, string country)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(str))
            {
                string insertQuery = @"
                INSERT INTO [User] 
                    (Username, Password, Name, Email, Mobile, Address, Country) 
                VALUES 
                    (@Username, @Password, @Name, @Email, @Mobile, @Address, @Country)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Mobile", mobile);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Country", country);

                    con.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }
    }
}
