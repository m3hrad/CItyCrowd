using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.AdminPages
{
    public partial class Blog : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check permissions
            Classes.Admins a = new Classes.Admins();
            if (!a.permissions(Convert.ToInt32(Session["UserId"]), "Blog"))
            {
                Response.Redirect("~/Error/404");
            }
        }
        protected void LinkButtonAdd_Click(object sender, EventArgs e)
        {
            PanelAdd.Visible = true;
            PanelManage.Visible = false;
            LinkButtonAdd.Enabled = false;
            LinkButtonManage.Enabled = true;
        }
        protected void LinkButtonManage_Click(object sender, EventArgs e)
        {
            PanelAdd.Visible = false;
            PanelManage.Visible = true;
            LinkButtonAdd.Enabled = true;
            LinkButtonManage.Enabled = false;
        }
        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            string browserTitle = TextBoxTitle.Text;
            browserTitle.Replace(" ", "-");

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_adminBlogEntryAdd", sqlConn);

            //try
            //{
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = TextBoxTitle.Text;
                sqlCmd.Parameters.Add("@BrowserTitle", SqlDbType.NVarChar).Value = browserTitle;
                sqlCmd.Parameters.Add("@Body", SqlDbType.NVarChar).Value = TextBoxBody.Text;

                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();

                LabelAddMessage.Visible = true;
                LabelAddMessage.Text = "Entry has been added.";
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            //}
        }

        protected void GridViewBlog_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_adminBlogEntryInfo", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@EntryId", SqlDbType.Int).Value = Convert.ToInt32(GridViewBlog.SelectedDataKey.Value);
                sda.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0) //news doesn't exist
                {
                    //LabelName.Text = 
                }
                else //entry exists
                {
                    PanelEdit.Visible = true;
                    LabelEditEntryId.Text = GridViewBlog.SelectedDataKey.Value.ToString();
                    TextBoxEditTitle.Text = dt.Rows[0]["Title"].ToString();
                    TextBoxEditBody.Text = dt.Rows[0]["Body"].ToString();
                }
                sda.Dispose();
                sqlConn.Close();
                sqlConn.Dispose();
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{

            //}
        }

        protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs e)
        {
            string browserTitle = TextBoxEditTitle.Text;
            browserTitle.Replace(" ", "-");

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_adminBlogEntryEdit", sqlConn);

            //try
            //{
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@EntryId", SqlDbType.Int).Value = Convert.ToInt32(LabelEditEntryId.Text);
                sqlCmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = TextBoxEditTitle.Text;
                sqlCmd.Parameters.Add("@BrowserTitle", SqlDbType.NVarChar).Value = browserTitle;
                sqlCmd.Parameters.Add("@Body", SqlDbType.NVarChar).Value = TextBoxBody.Text;

                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();

                LabelEditMessage.Visible = true;
                LabelEditMessage.Text = "Entry has been edited.";
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            //}
        }

        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_adminBlogEntryDelete", sqlConn);

            //try
            //{
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@EntryId", SqlDbType.Int).Value = Convert.ToInt32(LabelEditEntryId.Text);

                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();

                LabelEditMessage.Visible = true;
                LabelEditMessage.Text = "Entry has been deleted.";
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            //}
        }
    }
}