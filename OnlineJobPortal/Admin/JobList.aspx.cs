using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace OnlineJobPortal.Admin
{
    public partial class JobList : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter da;
        DataSet ds;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        static string Crypath = "";

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }

            if (!IsPostBack)
            {
                ShowJob();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowJob();
            }
        }

        private void ShowJob()
        {
            using (con = new SqlConnection(str))
            {
                string query = @"SELECT Row_Number() OVER (ORDER BY (SELECT 1)) AS [Sr.No], JobId, Title, NoOfPost, Qualification, Experience, 
                                 LastDateToApply, CompanyName, Country, State, CreateDate FROM Jobs";
                cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                ViewState["JobData"] = dt;

                GridView1.DataSource = dt;
                GridView1.DataBind();
                if (Request.QueryString["id"] != null)
                {
                    linkBack.Visible = true;
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (ViewState["JobData"] != null)
            {
                GridView1.DataSource = ViewState["JobData"];
                GridView1.DataBind();
            }
            else
            {
                ShowJob();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int jobId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

                using (con = new SqlConnection(str))
                {
                    cmd = new SqlCommand("DELETE FROM Jobs WHERE JobId=@id", con);
                    cmd.Parameters.AddWithValue("@id", jobId);
                    con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        llbMsg.Text = "Job deleted successfully!";
                        llbMsg.CssClass = "alert alert-success";
                    }
                    else
                    {
                        llbMsg.Text = "Cannot delete this record!";
                        llbMsg.CssClass = "alert alert-danger";
                    }

                    ShowJob();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditJob")
            {
                Response.Redirect("NewJob.aspx?id=" + e.CommandArgument.ToString());
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.ID = e.Row.RowIndex.ToString();
                if (Request.QueryString["id"] != null)
                {
                    int jobId = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Values[0]);
                    if (jobId == Convert.ToInt32(Request.QueryString["id"]))
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(str);
            da = new SqlDataAdapter("select * from Jobs", con);
            ds = new DataSet();
            da.Fill(ds);
            string xml = @"D:\OnlineJobPortal\OnlineJobPortal\data.xml";
            ds.WriteXmlSchema(xml);

            Crypath = @"D:\OnlineJobPortal\OnlineJobPortal\Admin\CrystalReport1.rpt";
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            cr.Database.Tables[0].SetDataSource(ds);
            cr.Refresh();
            CrystalReportViewer1.ReportSource = cr;
            cr.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Test");
        }
    }
}