using System;
using System.Data;
using System.Web.UI;

namespace OnlineJobPortal.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private DashboardData dashboardData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                dashboardData = new DashboardData();
                LoadDashboardStats();
            }
        }

        private void LoadDashboardStats()
        {
            try
            {
                Session["Users"] = dashboardData.GetUsersCount();
                Session["Jobs"] = dashboardData.GetJobsCount();
                Session["AppliedJobs"] = dashboardData.GetAppliedJobsCount();
                Session["Contact"] = dashboardData.GetContactCount();
            }
            catch (Exception ex)
            {
                Session["Error"] = "Error loading dashboard: " + ex.Message;
            }
        }
    }
}
