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
    public partial class Admins : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check permissions
            Classes.Admins a = new Classes.Admins();
            if (!a.permissions(Convert.ToInt32(Session["UserId"]), "Admins"))
            {
                Response.Redirect("~/Error/404");
            }
        }

        protected void GridViewAdmins_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_adminPermissions", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = GridViewAdmins.SelectedDataKey.Value;
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

            if (dt.Rows.Count == 0) //admin doesn't exist
            {
                Response.Redirect("~/Admin/Admins");
            }
            else //admin exists
            {
                PanelEdit.Visible = true;
                LabelEditUserId.Text = dt.Rows[0]["UserId"].ToString();
                CheckBoxListEditPremissions.Items[0].Selected = Convert.ToBoolean(dt.Rows[0]["PermAdmins"].ToString());
                CheckBoxListEditPremissions.Items[1].Selected = Convert.ToBoolean(dt.Rows[0]["PermBlog"].ToString());
                CheckBoxListEditPremissions.Items[2].Selected = Convert.ToBoolean(dt.Rows[0]["PermEvents"].ToString());
                CheckBoxListEditPremissions.Items[3].Selected = Convert.ToBoolean(dt.Rows[0]["PermLocations"].ToString());
                CheckBoxListEditPremissions.Items[4].Selected = Convert.ToBoolean(dt.Rows[0]["PermSettings"].ToString());
                CheckBoxListEditPremissions.Items[5].Selected = Convert.ToBoolean(dt.Rows[0]["PermStats"].ToString());
                CheckBoxListEditPremissions.Items[6].Selected = Convert.ToBoolean(dt.Rows[0]["PermUsers"].ToString());
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            int numSelected = 0;
            foreach (ListItem li in CheckBoxListPremissions.Items)
            {
                if (li.Selected)
                {
                    numSelected = numSelected + 1;
                }
            }

            if (numSelected != 0)
            {
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("sp_adminCheckExists", sqlConn);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand sqlCmd = new SqlCommand("sp_adminAdd", sqlConn);

                //try
                //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(TextBoxUserId.Text);
                sda.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0) //user doesn't exist as an admin
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@PermAdmins", SqlDbType.Bit).Value = CheckBoxListPremissions.Items[0].Selected;
                    sqlCmd.Parameters.Add("@PermBlog", SqlDbType.Bit).Value = CheckBoxListPremissions.Items[1].Selected;
                    sqlCmd.Parameters.Add("@PermEvents", SqlDbType.Bit).Value = CheckBoxListPremissions.Items[2].Selected;
                    sqlCmd.Parameters.Add("@PermLocations", SqlDbType.Bit).Value = CheckBoxListPremissions.Items[3].Selected;
                    sqlCmd.Parameters.Add("@PermSettings", SqlDbType.Bit).Value = CheckBoxListPremissions.Items[4].Selected;
                    sqlCmd.Parameters.Add("@PermStats", SqlDbType.Bit).Value = CheckBoxListPremissions.Items[5].Selected;
                    sqlCmd.Parameters.Add("@PermUsers", SqlDbType.Bit).Value = CheckBoxListPremissions.Items[6].Selected;
                    sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(TextBoxUserId.Text);
                    sqlCmd.Parameters.Add("@Status", SqlDbType.TinyInt).Value = 1;

                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();

                    GridViewAdmins.DataBind();

                    sqlCmd.Dispose();
                    sqlConn.Dispose();
                    sda.Dispose();

                    LabelAddMessage.Visible = true;
                    LabelAddMessage.Text = "User added as an admin with the selected permissions.";
                }
                else //user exists as an admin
                {
                    LabelAddMessage.Visible = true;
                    LabelAddMessage.Text = "User was already an admin!";

                    sda.Dispose();
                    sqlConn.Close();
                }
                //}
                //catch (Exception ex)
                //{

                //}
                //finally
                //{
                //    
                //}
            }
            else
            {
                LabelAddMessage.Visible = true;
                LabelAddMessage.Text = "An admin must have at least one permission to be able to be added as an admin!";
            }
        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_adminEdit", sqlConn);

            //try
            //{
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@PermAdmins", SqlDbType.Bit).Value = CheckBoxListEditPremissions.Items[0].Selected;
                sqlCmd.Parameters.Add("@PermBlog", SqlDbType.Bit).Value = CheckBoxListEditPremissions.Items[1].Selected;
                sqlCmd.Parameters.Add("@PermEvents", SqlDbType.Bit).Value = CheckBoxListEditPremissions.Items[2].Selected;
                sqlCmd.Parameters.Add("@PermLocations", SqlDbType.Bit).Value = CheckBoxListEditPremissions.Items[3].Selected;
                sqlCmd.Parameters.Add("@PermSettings", SqlDbType.Bit).Value = CheckBoxListEditPremissions.Items[4].Selected;
                sqlCmd.Parameters.Add("@PermStats", SqlDbType.Bit).Value = CheckBoxListEditPremissions.Items[5].Selected;
                sqlCmd.Parameters.Add("@PermUsers", SqlDbType.Bit).Value = CheckBoxListEditPremissions.Items[6].Selected;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(LabelEditUserId.Text);
                sqlCmd.Parameters.Add("@Status", SqlDbType.Int).Value = 1;

                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();

                GridViewAdmins.DataBind();

                LabelEditMessage.Visible = true;
                LabelEditMessage.Text = "You have successfully changed admin permissions!";
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