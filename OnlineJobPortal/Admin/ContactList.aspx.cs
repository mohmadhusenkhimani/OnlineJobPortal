using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace OnlineJobPortal.Admin
{
    public partial class ContactList : System.Web.UI.Page
    {
        private ContactData contactData;

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;

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
            contactData = new ContactData(); 
            if (!IsPostBack)
            {
                contactData = new ContactData();
                LoadContacts();
            }
        }

        private void LoadContacts()
        {
            try
            {
                DataTable dt = contactData.GetContacts();
                ViewState["ContactData"] = dt;

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
            LoadContacts();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int contactId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                bool isDeleted = contactData.DeleteContact(contactId);

                if (isDeleted)
                {
                    llbMsg.Text = "Contact deleted successfully!";
                    llbMsg.CssClass = "alert alert-success";
                }
                else
                {
                    llbMsg.Text = "Cannot delete this record!";
                    llbMsg.CssClass = "alert alert-danger";
                }

                LoadContacts();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(str);
            da = new SqlDataAdapter("select * from Contact", con);
            ds = new DataSet();
            da.Fill(ds);
            string xml = @"D:\OnlineJobPortal\OnlineJobPortal\data.xml";
            ds.WriteXmlSchema(xml);

            Crypath = @"D:\OnlineJobPortal\OnlineJobPortal\Admin\CrystalReport3.rpt";
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            cr.Database.Tables[0].SetDataSource(ds);
            cr.Refresh();
            CrystalReportViewer1.ReportSource = cr;
            cr.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Test");
        }
    }
}