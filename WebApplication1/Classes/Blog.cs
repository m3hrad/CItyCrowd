using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Blog
    {
        // blog entries
        public DataTable blogEntries()
        {
            DataTable dt1 = new DataTable();
            DataSet ds1 = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda1 = new SqlDataAdapter("sp_blog", sqlConn);

            sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda1.Fill(ds1);
            dt1 = ds1.Tables[0];
            DataTable dt2 = new DataTable();
            DataRow dr2 = null;

            //define the columns
            dt2.Columns.Add(new DataColumn("EntryId", typeof(string)));
            dt2.Columns.Add(new DataColumn("Title", typeof(string)));
            dt2.Columns.Add(new DataColumn("BrowserTitle", typeof(string)));
            dt2.Columns.Add(new DataColumn("Body", typeof(string)));
            dt2.Columns.Add(new DataColumn("SubmitDate", typeof(string)));

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                //create new row
                dr2 = dt2.NewRow();

                //add values to each rows
                dr2["EntryId"] = dt1.Rows[i]["EntryId"].ToString();
                dr2["Title"] = dt1.Rows[i]["Title"].ToString();
                dr2["BrowserTitle"] = dt1.Rows[i]["BrowserTitle"].ToString();
                dr2["Body"] = dt1.Rows[i]["Body"].ToString();
                dr2["SubmitDate"] = Convert.ToDateTime(dt1.Rows[i]["SubmitDate"].ToString()).ToShortDateString();

                //add the row to DataTable
                dt2.Rows.Add(dr2);
            }

            return dt2;
        }

        // blog entry info with specific entry id
        public Tuple<int, DataTable> blogEntryInfo(int entryId)
        {
            int status = 0;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);

            SqlDataAdapter sda = new SqlDataAdapter("sp_blogInfo", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EntryId", SqlDbType.Int).Value = entryId;

            //try
            //{
                sda.Fill(ds);
                dt = ds.Tables[0];
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            //}

            if (dt.Rows.Count == 0) //doesn't exist
            {
                status = 0;
            }
            else //blog exists
            {
                status = 1;
            }
            

            return new Tuple<int, DataTable>(status, dt);
        }
    }
}