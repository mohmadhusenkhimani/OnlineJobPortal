using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineJobPortal.User
{
    public partial class JobDetails : System.Web.UI.Page
    {
        ClassJobDetails cs1;
        public string jobTitle = string.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                showJobDetails();
                DataBind();
            }
            else
            {
                Response.Redirect("JobListing.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
        }

        void getcon()
        {
            cs1 = new ClassJobDetails();
            cs1.startcon();
        }

        private void showJobDetails()
        {
            getcon();
            DataTable dt1 = cs1.getJobDetails(Request.QueryString["id"]);

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                DataList1.DataSource = dt1;
                DataList1.DataBind();

                if (dt1.Columns.Contains("Title") && dt1.Rows[0]["Title"] != DBNull.Value)
                {
                    jobTitle = dt1.Rows[0]["Title"].ToString();
                }
                else
                {
                    jobTitle = "No Title Available";
                }

                // Get the applied user count
                int appliedUserCount = cs1.getAppliedUserCount(Request.QueryString["id"]);

                // Display in Label instead of alert
                lblApplicants.Text = appliedUserCount.ToString();
                lblApplicants.Visible = true;
            }
            else
            {
                displayAlert("Job not found!", "alert-danger");
                Response.Redirect("JobListing.aspx");
            }
        }


        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ApplyJob")
            {
                if (Session["user"] != null)
                {
                    getcon();
                    int result = cs1.applyJob(Request.QueryString["id"], Session["userId"].ToString());

                    if (result > 0)
                    {
                        displayAlert("Job Applied successfully!", "alert-success");
                        showJobDetails();
                    }
                    else
                    {
                        displayAlert("Cannot apply for the job. Please try again later!", "alert-danger");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (Session["user"] != null)
            {
                LinkButton btnApplyJob = e.Item.FindControl("lbApplyJob") as LinkButton;
                getcon();

                if (cs1.isApplied(Session["userId"].ToString(), Request.QueryString["id"]))
                {
                    btnApplyJob.Enabled = false;
                    btnApplyJob.Text = "Applied";
                }
                else
                {
                    btnApplyJob.Enabled = true;
                    btnApplyJob.Text = "Apply Now";
                }
            }
        }

        private void displayAlert(string message, string cssClass)
        {
            lblMsg.Visible = true;
            lblMsg.Text = message;
            lblMsg.CssClass = "alert " + cssClass;
        }

        protected string GetImageUrl(object url)
        {
            string url1 = string.IsNullOrEmpty(url?.ToString()) || url == DBNull.Value
                ? "~/Images/No_image.png"
                : string.Format("~/{0}", url);
            return ResolveUrl(url1);
        }
    }
}
