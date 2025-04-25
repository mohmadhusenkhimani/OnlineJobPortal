using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace OnlineJobPortal.User
{
    public class ClassContact 
    {
        private readonly string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        private SqlConnection con;
        private SqlCommand cmd;

        
        public void startcon()
        {
            con = new SqlConnection(str);
            con.Open();
        }
        public int insert(string nm, string eml, string sub, string msg)
        {
            int rowsAffected = 0;

            cmd = new SqlCommand("insert into Contact(Name,Email,Subject,Message)values('" + nm + "','" + eml + "','" + sub + "','" + msg + "')", con);
            rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }
    }
}