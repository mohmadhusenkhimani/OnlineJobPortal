using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.Admin
{
    public partial class ViewResume : System.Web.UI.Page
    {
        private ResumeData resumeData = new ResumeData();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                resumeData = new ResumeData();
                LoadAppliedJobs();
            }
        }

        private void LoadAppliedJobs()
        {
            try
            {
                DataTable dt = resumeData.GetAppliedJobs();
                ViewState["JobData"] = dt;

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadAppliedJobs();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int appliedJobId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                bool isDeleted = resumeData.DeleteResume(appliedJobId);

                if (isDeleted)
                {
                    llbMsg.Text = "Resume deleted successfully!";
                    llbMsg.CssClass = "alert alert-success";
                }
                else
                {
                    llbMsg.Text = "Cannot delete this record!";
                    llbMsg.CssClass = "alert alert-danger";
                }

                LoadAppliedJobs();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to View job details";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    HiddenField jobId = (HiddenField)row.FindControl("hdnJobId");
                    Response.Redirect("JobList.aspx?id=" + jobId.Value);
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row";
                }
            }
        }
    }
}
