using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    public partial class Calendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_calendar", sqlConn);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
            sda.Fill(ds);
            dt = ds.Tables[0];
            dt2 = ds.Tables[1];
            DataTable dtCalendar = new DataTable();
            DataRow drCalendar = null;

            //define the columns
            dtCalendar.Columns.Add(new DataColumn("EventId", typeof(string)));
            dtCalendar.Columns.Add(new DataColumn("Name", typeof(string)));
            dtCalendar.Columns.Add(new DataColumn("Date", typeof(string)));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //create new row
                drCalendar = dtCalendar.NewRow();

                //add values to each rows
                drCalendar["EventId"] = dt.Rows[i]["EventId"].ToString();
                drCalendar["Name"] = dt.Rows[i]["Name"].ToString();
                drCalendar["Date"] = dt.Rows[i]["Date"].ToString();

                //add the row to DataTable
                dtCalendar.Rows.Add(drCalendar);
            }

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                //create new row
                drCalendar = dtCalendar.NewRow();

                //add values to each rows
                drCalendar["EventId"] = dt2.Rows[i]["EventId"].ToString();
                drCalendar["Name"] = dt2.Rows[i]["Name"].ToString();
                drCalendar["Date"] = dt2.Rows[i]["Date"].ToString();

                //add the row to DataTable
                dtCalendar.Rows.Add(drCalendar);
            }

            if (dtCalendar.Rows.Count > 0)
            {
                //convert DataTable to DataView
                DataView dv = dtCalendar.DefaultView;
                //apply the sort on CustomerSurname column
                dv.Sort = "Date";
                //save our newly ordered results back into our datatable
                dtCalendar = dv.ToTable();

                RepeaterCalendar.DataSource = dtCalendar;
                RepeaterCalendar.DataBind();
            }
            else
            {
                LabelNoRecord.Visible = true;
            }
        }
    }
}