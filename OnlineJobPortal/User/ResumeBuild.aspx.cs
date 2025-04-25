using System;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineJobPortal.User
{
    public partial class ResumeBuild : System.Web.UI.Page
    {
        ClassResume csResume;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    ShowUserInfo();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        void ShowUserInfo()
        {
            try
            {
                csResume = new ClassResume();
                SqlDataReader sdr = csResume.GetUserInfo(Request.QueryString["id"]);

                if (sdr.HasRows)
                {
                    sdr.Read();
                    txtUserName.Text = sdr["Username"].ToString();
                    txtFullName.Text = sdr["Name"].ToString();
                    txtEmail.Text = sdr["Email"].ToString();
                    txtMobile.Text = sdr["Mobile"].ToString();
                    txtTenth.Text = sdr["TenthGrade"].ToString();
                    txtTwelfth.Text = sdr["TwelfthGrade"].ToString();
                    txtGraduation.Text = sdr["GraduationGrade"].ToString();
                    txtPostGraduation.Text = sdr["PostGraduationGrade"].ToString();
                    txtPhd.Text = sdr["Phd"].ToString();
                    txtWork.Text = sdr["WorksOn"].ToString();
                    txtExperience.Text = sdr["Experience"].ToString();
                    txtAddress.Text = sdr["Address"].ToString();
                    ddlCountry.SelectedValue = sdr["Country"].ToString();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "User not found!";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    string userId = Request.QueryString["id"];
                    string username = txtUserName.Text.Trim();
                    string resumePath = null;

                    csResume = new ClassResume();
                    bool isDuplicate = csResume.CheckDuplicateUsername(username, userId);

                    if (isDuplicate)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "The username already exists. Please choose a different username.";
                        lblMsg.CssClass = "alert alert-danger";
                        return;
                    }

                    if (fuResume.HasFile)
                    {
                        if (Utils.IsValidExtensionResume(fuResume.FileName))
                        {
                            Guid fileId = Guid.NewGuid();
                            resumePath = "Resumes/" + fileId + fuResume.FileName;
                            fuResume.PostedFile.SaveAs(Server.MapPath("~/Resumes/") + fileId + fuResume.FileName);
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Please select a valid file (.doc, .docx, .pdf).";
                            lblMsg.CssClass = "alert alert-danger";
                            return;
                        }
                    }

                    bool isUpdated = csResume.UpdateUserDetails(
                        userId, username, txtFullName.Text.Trim(), txtEmail.Text.Trim(),
                        txtMobile.Text.Trim(), txtTenth.Text.Trim(), txtTwelfth.Text.Trim(),
                        txtGraduation.Text.Trim(), txtPostGraduation.Text.Trim(), txtPhd.Text.Trim(),
                        txtWork.Text.Trim(), txtExperience.Text.Trim(), txtAddress.Text.Trim(),
                        ddlCountry.SelectedValue, resumePath);

                    lblMsg.Visible = true;
                    if (isUpdated)
                    {
                        lblMsg.Text = "Profile updated successfully!";
                        lblMsg.CssClass = "alert alert-success";
                    }
                    else
                    {
                        lblMsg.Text = "Failed to update profile. Try again later.";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Invalid user ID. Please re-login.";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }
    }
}
