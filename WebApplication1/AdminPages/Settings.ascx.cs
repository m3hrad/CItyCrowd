using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.AdminPages
{
    public partial class Settings : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check permissions
            Classes.Admins a = new Classes.Admins();
            if (!a.permissions(Convert.ToInt32(Session["UserId"]), "Settings"))
            {
                Response.Redirect("~/Error/404");
            }

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_adminSettings", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;

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

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/AdminSettingsNotFound");
            }
            else
            {
                DropDownListLogin.SelectedValue = dt.Rows[0]["LoginAllow"].ToString();
                DropDownListRegister.SelectedValue = dt.Rows[0]["RegisterAllow"].ToString();
                DropDownListActivities.SelectedValue = dt.Rows[0]["ActivitiesAllow"].ToString();
                DropDownListStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_adminSettingsEdit", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@LoginAllow", SqlDbType.Bit).Value = DropDownListLogin.SelectedValue;
            sqlCmd.Parameters.Add("@RegisterAllow", SqlDbType.Bit).Value = DropDownListRegister.SelectedValue;
            sqlCmd.Parameters.Add("@ActivitiesAllow", SqlDbType.Bit).Value = DropDownListActivities.SelectedValue;
            sqlCmd.Parameters.Add("@Status", SqlDbType.SmallInt).Value = DropDownListStatus.SelectedValue;

            //try
            //{
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
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

            LabelMessage.Visible = true;
            LabelMessage.Text = "You have succesfully saved settings!";
        }
    }
}