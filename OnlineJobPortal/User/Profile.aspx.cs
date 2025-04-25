using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.User
{
    public partial class Profile : System.Web.UI.Page
    {
        ClassProfile cs1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                cs1 = new ClassProfile();
                ShowUserProfile();
            }
        }

        private void ShowUserProfile()
        {
            try
            {
                cs1 = new ClassProfile();
                var userProfile = cs1.GetUserProfile(Session["user"].ToString());

                if (userProfile != null)
                {
                    dlProfile.DataSource = userProfile;
                    dlProfile.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('Please log in again with your latest username');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
            }
        }

        protected void dlProfile_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "EditUserProfile")
            {
                Response.Redirect("ResumeBuild.aspx?id=" + e.CommandArgument.ToString());
            }
        }
    }
}
