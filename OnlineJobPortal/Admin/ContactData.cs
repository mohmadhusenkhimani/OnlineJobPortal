using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineJobPortal.Admin
{
    public class ContactData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public DataTable GetContacts()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS [Sr.No], 
                    ContactId, Name, Email, Subject, Message 
                FROM Contact";

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

        public bool DeleteContact(int contactId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Contact WHERE ContactId = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", contactId);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
