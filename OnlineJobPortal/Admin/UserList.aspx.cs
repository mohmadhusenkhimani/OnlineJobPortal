using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

namespace OnlineJobPortal.Admin
{
    public partial class UserList : System.Web.UI.Page
    {
        private UserData userData = new UserData();
        SqlDataAdapter da;
        DataSet ds;
        SqlConnection con;
        DataTable dt;
        SqlCommand cmd;

        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        static string Crypath = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                userData = new UserData();
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            try
            {
                DataTable dt = userData.GetUserList();
                ViewState["UserData"] = dt;

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
            if (ViewState["UserData"] != null)
            {
                GridView1.DataSource = ViewState["UserData"];
                GridView1.DataBind();
            }
            else
            {
                LoadUsers();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                bool isDeleted = userData.DeleteUser(userId);

                if (isDeleted)
                {
                    llbMsg.Text = "User deleted successfully!";
                    llbMsg.CssClass = "alert alert-success";
                }
                else
                {
                    llbMsg.Text = "Cannot delete this record!";
                    llbMsg.CssClass = "alert alert-danger";
                }

                LoadUsers();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(str);
            da = new SqlDataAdapter("select * from [User]", con);
            ds = new DataSet();
            da.Fill(ds);
            string xml = @"D:\OnlineJobPortal\OnlineJobPortal\data.xml";
            ds.WriteXmlSchema(xml);

            Crypath = @"D:\OnlineJobPortal\OnlineJobPortal\Admin\CrystalReport2.rpt";
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            cr.Database.Tables[0].SetDataSource(ds);
            cr.Refresh();
            CrystalReportViewer1.ReportSource = cr;
            cr.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Test");
        }
    }
}
