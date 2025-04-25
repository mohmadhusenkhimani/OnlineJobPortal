using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace OnlineJobPortal.User
{
    public partial class Register : System.Web.UI.Page
    {
        ClassRegister cs1;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
        }

        void getcon()
        {
            cs1 = new ClassRegister();
            cs1.startcon();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string usernameInput = txtUserName.Text.Trim();
                string passwordInput = txtConfirmPassword.Text.Trim();
                string nameInput = txtFullName.Text.Trim();
                string emailInput = txtEmail.Text.Trim();
                string mobileInput = txtMobile.Text.Trim();
                string addressInput = txtAddress.Text.Trim();
                string countryInput = ddlCountry.SelectedValue;

                getcon();
                ClassRegister cs1 = new ClassRegister();

                // Check if username exists
                bool userExists = cs1.checkUsername(usernameInput);
                if (userExists)
                {
                    displayAlert($"<b>{usernameInput}</b> username already exists, try a new one..!", "alert-danger");
                    return;
                }

                // Insert data into database
                int result = cs1.insert(usernameInput, passwordInput, nameInput, emailInput, mobileInput, addressInput, countryInput);

                if (result > 0)
                {
                    displayAlert("Registration Successful!", "alert-success");
                    clear();
                }
                else
                {
                    displayAlert("Cannot save record right now, please try again later.", "alert-danger");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("UNIQUE KEY constraint"))
                {
                    displayAlert($"<b>{txtUserName.Text.Trim()}</b> username already exists, try a new one..!", "alert-danger");
                }
                else
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
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
            txtUserName.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtAddress.Text = string.Empty;
            ddlCountry.ClearSelection();
        }

        private void displayAlert(string message, string cssClass)
        {
            lblMsg.Visible = true;
            lblMsg.Text = message;
            lblMsg.CssClass = "alert " + cssClass;
        }
    }
}
