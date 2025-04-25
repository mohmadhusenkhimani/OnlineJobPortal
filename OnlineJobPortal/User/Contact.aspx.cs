using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

namespace OnlineJobPortal.User
{
    public partial class Contact : System.Web.UI.Page
    {
        ClassContact cs1;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
        }
        void getcon()
        {
            cs1 = new ClassContact();
            cs1.startcon();
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string nameInput = name.Value.Trim();
                string emailInput = email.Value.Trim();
                string subjectInput = subject.Value.Trim();
                string messageInput = message.Value.Trim();

                getcon();
                ClassContact cs1 = new ClassContact();

                // Insert data into database
                int result = cs1.insert(nameInput, emailInput, subjectInput, messageInput);


                if (result > 0)
                {
                    displayAlert("Thanks for reaching out! We will look into your query.", "alert-success");
                    clear();
                }
                else
                {
                    displayAlert("Cannot save record right now, please try again later.", "alert-danger");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                cs1.startcon();
            }
        }


        private void clear()
        {
            name.Value = string.Empty;
            email.Value = string.Empty;
            subject.Value = string.Empty;
            message.Value = string.Empty;
        }
        private void displayAlert(string message, string cssClass)
        {
            lblMsg.Visible = true;
            lblMsg.Text = message;
            lblMsg.CssClass = "alert " + cssClass;
        }
    }
}