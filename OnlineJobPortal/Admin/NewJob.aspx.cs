using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.Admin
{
    public partial class NewJob : System.Web.UI.Page
    {
        JobManager jobManager = new JobManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            Session["title"] = "Add Job";

            if (!IsPostBack)
            {
                LoadJobDetails();
            }
        }

        private void LoadJobDetails()
        {
            if (Request.QueryString["id"] != null)
            {
                int jobId;
                if (int.TryParse(Request.QueryString["id"], out jobId))
                {
                    DataTable dt = jobManager.GetJobById(jobId);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        txtJobTitle.Text = row["Title"].ToString();
                        txtNoOfPost.Text = row["NoOfPost"].ToString();
                        txtDescription.Text = row["Description"].ToString();
                        txtQualification.Text = row["Qualification"].ToString();
                        txtExperience.Text = row["Experience"].ToString();
                        txtSpecialization.Text = row["Specialization"]?.ToString() ?? "";
                        txtLastDate.Text = Convert.ToDateTime(row["LastDateToApply"]).ToString("yyyy-MM-dd");
                        txtSalary.Text = row["Salary"].ToString();
                        ddlJobType.SelectedValue = row["JobType"].ToString();
                        txtCompany.Text = row["CompanyName"].ToString();
                        txtWebsite.Text = row["Website"].ToString();
                        txtEmail.Text = row["Email"].ToString();
                        txtAddress.Text = row["Address"].ToString();
                        ddlCountry.SelectedValue = row["Country"].ToString();
                        txtState.Text = row["State"].ToString();

                        btnAdd.Text = "Update";
                        linkBack.Visible = true;
                        Session["title"] = "Edit Job";
                    }
                    else
                    {
                        lblMsg.Text = "Job not found..!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string imagePath = string.Empty;
                bool isValid = false;
                int? jobId = Request.QueryString["id"] != null ? (int?)Convert.ToInt32(Request.QueryString["id"]) : null;

                if (fuCompanyLogo.HasFile && Utils.IsValidExtension(fuCompanyLogo.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    imagePath = "Images/" + obj.ToString() + fuCompanyLogo.FileName;
                    fuCompanyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/") + obj.ToString() + fuCompanyLogo.FileName);
                    isValid = true;
                }
                else if (!fuCompanyLogo.HasFile)
                {
                    isValid = true;
                }
                else
                {
                    lblMsg.Text = "Please select a valid .jpg, .jpeg, or .png file for Logo";
                    lblMsg.CssClass = "alert alert-danger";
                }

                if (isValid)
                {
                    bool result = jobManager.SaveOrUpdateJob(
                        jobId,
                        txtJobTitle.Text.Trim(),
                        Convert.ToInt32(txtNoOfPost.Text.Trim()),
                        txtDescription.Text.Trim(),
                        txtQualification.Text.Trim(),
                        txtExperience.Text.Trim(),
                        txtSpecialization.Text.Trim(),
                        Convert.ToDateTime(txtLastDate.Text.Trim()),
                        txtSalary.Text.Trim(),
                        ddlJobType.SelectedValue,
                        txtCompany.Text.Trim(),
                        imagePath,
                        txtWebsite.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtAddress.Text.Trim(),
                        ddlCountry.SelectedValue,
                        txtState.Text.Trim(),
                        DateTime.Now
                    );

                    if (result)
                    {
                        lblMsg.Text = jobId.HasValue ? "Job updated successfully..!" : "Job saved successfully..!";
                        lblMsg.CssClass = "alert alert-success";
                        ClearFields();
                    }
                    else
                    {
                        lblMsg.Text = "Cannot save the record, please try again later.";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void ClearFields()
        {
            txtJobTitle.Text = "";
            txtNoOfPost.Text = "";
            txtDescription.Text = "";
            txtQualification.Text = "";
            txtExperience.Text = "";
            txtSpecialization.Text = "";
            txtLastDate.Text = "";
            txtSalary.Text = "";
            txtCompany.Text = "";
            txtWebsite.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtState.Text = "";
            ddlJobType.SelectedIndex = 0;
            ddlCountry.SelectedIndex = 0;
        }
    }
}
