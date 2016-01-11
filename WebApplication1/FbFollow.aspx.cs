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
    public partial class FbFollow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_fbFollow", sqlConn);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

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

            if (dt.Rows.Count == 0)
            {
                Response.Redirect("~/Introduction");
            }

            DataTable dt2 = new DataTable();
            DataRow dr2 = null;

            //define the columns
            dt2.Columns.Add(new DataColumn("UserId", typeof(string)));
            dt2.Columns.Add(new DataColumn("FirstName", typeof(string)));
            dt2.Columns.Add(new DataColumn("LastName", typeof(string)));
            dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));

            string profilePicUrl;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //create new row
                dr2 = dt2.NewRow();

                //add values to each rows
                dr2["UserId"] = dt.Rows[i]["UserId"].ToString();
                dr2["FirstName"] = dt.Rows[i]["FirstName"].ToString();
                dr2["LastName"] = dt.Rows[i]["LastName"].ToString();

                // Show profile's photo
                if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                {
                    profilePicUrl = "~/Files/ProfilesPhotos/" + dt.Rows[i]["UserId"].ToString() + "-100.jpg";
                }
                else
                {
                    profilePicUrl = "~/Images/nophoto.png";
                }
                dr2["ProfilePicUrl"] = profilePicUrl;

                //add the row to DataTable
                dt2.Rows.Add(dr2);
            }

            RepeaterFriends.DataSource = dt2;
            RepeaterFriends.DataBind();









            //SqlConnection sqlConn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            //SqlCommand sqlCmd3 = new SqlCommand("sp_fbFriendsDelete", sqlConn3);
            //sqlCmd3.CommandType = CommandType.StoredProcedure;
            //sqlCmd3.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

            //try
            //{
            //    sqlConn3.Open();
            //    sqlCmd3.ExecuteNonQuery();
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    sqlConn3.Close();
            //    sqlConn3.Dispose();
            //    sqlCmd3.Dispose();
            //}
        }
    }
}