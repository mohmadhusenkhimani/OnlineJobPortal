using System;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineJobPortal.User
{
    public class ClassResume
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public SqlDataReader GetUserInfo(string userId)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT * FROM [User] WHERE UserId = @UserId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            con.Open();
            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

        public bool CheckDuplicateUsername(string username, string userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string checkQuery = "SELECT COUNT(*) FROM [User] WHERE Username = @Username AND UserId != @UserId";
                SqlCommand cmd = new SqlCommand(checkQuery, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                int duplicateCount = (int)cmd.ExecuteScalar();
                return duplicateCount > 0;
            }
        }

        public bool UpdateUserDetails(string userId, string username, string name, string email, string mobile,
            string tenthGrade, string twelfthGrade, string graduationGrade, string postGraduationGrade, string phd,
            string worksOn, string experience, string address, string country, string resumePath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE [User] SET 
                                Username = @Username, 
                                Name = @Name, 
                                Email = @Email, 
                                Mobile = @Mobile, 
                                TenthGrade = @TenthGrade, 
                                TwelfthGrade = @TwelfthGrade, 
                                GraduationGrade = @GraduationGrade, 
                                PostGraduationGrade = @PostGraduationGrade, 
                                Phd = @Phd, 
                                WorksOn = @WorksOn, 
                                Experience = @Experience, 
                                Address = @Address, 
                                Country = @Country" +
                                (resumePath != null ? ", Resume = @Resume" : "") +
                                " WHERE UserId = @UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Mobile", mobile);
                cmd.Parameters.AddWithValue("@TenthGrade", string.IsNullOrEmpty(tenthGrade) ? (object)DBNull.Value : tenthGrade);
                cmd.Parameters.AddWithValue("@TwelfthGrade", twelfthGrade);
                cmd.Parameters.AddWithValue("@GraduationGrade", graduationGrade);
                cmd.Parameters.AddWithValue("@PostGraduationGrade", postGraduationGrade);
                cmd.Parameters.AddWithValue("@Phd", phd);
                cmd.Parameters.AddWithValue("@WorksOn", worksOn);
                cmd.Parameters.AddWithValue("@Experience", experience);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Country", country);
                cmd.Parameters.AddWithValue("@UserId", userId);

                if (resumePath != null)
                {
                    cmd.Parameters.AddWithValue("@Resume", resumePath);
                }

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
