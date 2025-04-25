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
    public partial class JobListing : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        public int jobCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showJobList();
                RBSelectedColorChange();
            }
        }
        public static string RelativeDate(DateTime theDate)
        {
            Dictionary<long, string> thresholds = new Dictionary<long, string>();
            int minute = 60;
            int hour = 60 * minute;
            int day = 24 * hour;

            thresholds.Add(60, "{0} seconds ago");
            thresholds.Add(minute * 2, "a minute ago");
            thresholds.Add(45 * minute, "{0} minutes ago");
            thresholds.Add(120 * minute, "an hour ago");
            thresholds.Add(day, "{0} hours ago");
            thresholds.Add(day * 2, "yesterday");
            thresholds.Add(day * 30, "{0} days ago");
            thresholds.Add(day * 365, "{0} months ago");
            thresholds.Add(long.MaxValue, "{0} years ago");

            long since = (DateTime.Now - theDate).TotalSeconds > 0 ? (long)(DateTime.Now - theDate).TotalSeconds : 0;

            foreach (long threshold in thresholds.Keys)
            {
                if (since < threshold)
                {
                    long value = since;

                    if (threshold == day * 365)
                        value /= 30 * day;
                    else if (threshold == long.MaxValue)
                        value /= 365 * day;
                    else if (threshold >= day)
                        value /= day;
                    else if (threshold >= hour)
                        value /= hour;
                    else if (threshold >= minute)
                        value /= minute;

                    return string.Format(thresholds[threshold], value);
                }
            }

            return "Just now";  // Fallback return
        }

        private void showJobList()
        {
            if (dt == null)
            {
                con = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,CompanyName,CompanyImage,Country,State,CreateDate,JobType from Jobs";
                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
            lbljobCount.Text = JobCount(dt.Rows.Count);
        }

        string JobCount(int count)
        {
            if (count > 1)
            {
                return "Total <b>" + count + "</b> jobs found";
            }
            else if (count == 1)
            {
                return "Total <b>" + count + "</b> job found";
            }
            else
            {
                return "No job found";
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "0")
            {
                con = new SqlConnection(str);
                string query = @"select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs where Country = '" + ddlCountry.SelectedValue + "' ";
                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
                RBSelectedColorChange();
            }
        }

        protected string GetImageUrl(Object url)
        {
            string url1 = "";
            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url1 = "~/Images/No_image.png";
            }
            else
            {
                url1 = string.Format("~/{0}", url);
            }
            return ResolveUrl(url1);
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string jobType = string.Empty;
            jobType = selectedCheckBox();
            if (jobType != "")
            {
                con = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs where JobType IN (" + jobType + ")";
                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
            }
        }

        string selectedCheckBox()
        {
            string jobType = string.Empty;
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    jobType += "'" + CheckBoxList1.Items[i].Text + "',";
                }
            }
            return jobType = jobType.TrimEnd(',');
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue != "0")
            {
                string postedDate = selectedRadioButton();
                con = new SqlConnection(str);
                string query = "SELECT JobId, Title, Salary, JobType, CompanyName, CompanyImage, Country, State, CreateDate FROM Jobs";

                if (!string.IsNullOrEmpty(postedDate))
                {
                    query += " WHERE Convert(DATE, CreateDate) " + postedDate;
                }

                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
                RBSelectedColorChange();
            }
        }

        string selectedRadioButton()
        {
            string postedDate = string.Empty;
            DateTime date = DateTime.Today;

            if (RadioButtonList1.SelectedValue == "1")
            {
                postedDate = "= Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";
            }
            else if (RadioButtonList1.SelectedValue == "2")
            {
                postedDate = "BETWEEN Convert(DATE, '" + DateTime.Now.AddDays(-2).ToString("yyyy/MM/dd") + "') AND Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";
            }
            else if (RadioButtonList1.SelectedValue == "3")
            {
                postedDate = "BETWEEN Convert(DATE, '" + DateTime.Now.AddDays(-3).ToString("yyyy/MM/dd") + "') AND Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";
            }

            return postedDate;
        }

        protected void lbFilter_Click(object sender, EventArgs e)
        {
            try
            {
                bool isCondition = false;
                string subquery = string.Empty;
                List<string> queryList = new List<string>();
                con = new SqlConnection(str);

                if (ddlCountry.SelectedValue != "0")
                {
                    queryList.Add(" Country = '" + ddlCountry.SelectedValue + "' ");
                    isCondition = true;
                }

                string jobType = selectedCheckBox();
                if (jobType != "")
                {
                    queryList.Add(" JobType IN (" + jobType + ") ");
                    isCondition = true;
                }

                if (isCondition)
                {
                    foreach (string a in queryList)
                    {
                        subquery += a + " and ";
                    }
                    subquery = subquery.Remove(subquery.LastIndexOf("and"), 3);
                    string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs where " + subquery;
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    showJobList();
                }
                else
                {
                    showJobList();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        protected void lbReset_Click(object sender, EventArgs e)
        {
            ddlCountry.ClearSelection();
            CheckBoxList1.ClearSelection();
            foreach (ListItem item in CheckBoxList2.Items)
            {
                item.Selected = false;
            }
            foreach (ListItem item in CheckBoxList3.Items)
            {
                item.Selected = false;
            }
            RadioButtonList1.SelectedValue = "0";
            RBSelectedColorChange();
            showJobList();
        }

        void RBSelectedColorChange()
        {
            if (RadioButtonList1.SelectedItem.Selected == true)
            {
                RadioButtonList1.SelectedItem.Attributes.Add("class", "selectedradio");
            }
        }
        string selectedCheckBox1()
        {
            string Title = string.Empty;
            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            {
                if (CheckBoxList2.Items[i].Selected)
                {
                    Title += "'" + CheckBoxList2.Items[i].Text + "',";
                }
            }
            return Title = Title.TrimEnd(',');
        }
        protected void CheckBoxList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Title = string.Empty;
            Title = selectedCheckBox1();
            if (Title != "")
            {
                con = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs where Title IN (" + Title + ")";
                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
            }
        }

        string selectedCheckBox2()
        {
            string CompanyName = string.Empty;
            for (int i = 0; i < CheckBoxList3.Items.Count; i++)
            {
                if (CheckBoxList3.Items[i].Selected)
                {
                    CompanyName += "'" + CheckBoxList3.Items[i].Text + "',";
                }
            }
            return CompanyName = CompanyName.TrimEnd(',');
        }
        protected void CheckBoxList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CompanyName = string.Empty;
            CompanyName = selectedCheckBox2();
            if (CompanyName != "")
            {
                con = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs where CompanyName IN (" + CompanyName + ")";
                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
            }
        }
    }
}
