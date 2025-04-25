using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.User
{
    public partial class Login : System.Web.UI.Page
    {
        ClassLogin cs1;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cs1 = new ClassLogin();
                string loginType = ddlLoginType.SelectedValue;
                string username = txtUserName.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (loginType == "Admin")
                {
                    bool isAdminValid = cs1.ValidateAdmin(username, password);
                    if (isAdminValid)
                    {
                        Session["admin"] = username;
                        Response.Redirect("../Admin/Dashboard.aspx", false);
                    }
                    else
                    {
                        showErrorMsg("Admin");
                    }
                }
                else
                {
                    var userDetails = cs1.ValidateUser(username, password);
                    if (userDetails != null)
                    {
                        Session["user"] = userDetails.Item1;
                        Session["userId"] = userDetails.Item2;
                        Response.Redirect("Default.aspx", false);
                    }
                    else
                    {
                        showErrorMsg("User");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void showErrorMsg(string userType)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "<b>" + userType + "</b> credentials are incorrect..!";
            lblMsg.CssClass = "alert alert-danger";
        }
    }
}
