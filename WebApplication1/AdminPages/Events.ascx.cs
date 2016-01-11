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
    public partial class Events : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check permissions
            Classes.Admins a = new Classes.Admins();
            if (!a.permissions(Convert.ToInt32(Session["UserId"]), "Events"))
            {
                Response.Redirect("~/Error/404");
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_adminEventInfo", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = Convert.ToInt64(TextBoxEventId.Text);

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
                LabelMessage.Visible = true;
                PanelInfo.Visible = false;
                LabelMessage.Text = "Event doesn't exist!";
            }
            else
            {
                LabelMessage.Visible = false;
                PanelInfo.Visible = true;

                LabelEventId.Text = dt.Rows[0]["EventId"].ToString();
                LabelOwnerId.Text = dt.Rows[0]["OwnerId"].ToString();
                LabelName.Text = dt.Rows[0]["Name"].ToString();
                LabelDate.Text = dt.Rows[0]["Date"].ToString();
                LabelParticipants.Text = dt.Rows[0]["Participants"].ToString();
                LabelParticipantsAccepted.Text = dt.Rows[0]["ParticipantsAccepted"].ToString();
                LabelParticipantsRemained.Text = dt.Rows[0]["ParticipantsRemained"].ToString(); //////////////
                LabelLocationId.Text = dt.Rows[0]["LocationId"].ToString();
                LabelTypeId.Text = dt.Rows[0]["TypeId"].ToString();
                LabelDescriptions.Text = dt.Rows[0]["Descriptions"].ToString();
                LabelViewCount.Text = dt.Rows[0]["ViewCount"].ToString(); /////////
                LabelRequestsCount.Text = dt.Rows[0]["RequestsCount"].ToString();
                LabelReportCount.Text = dt.Rows[0]["ReportCount"].ToString();
                DropDownListStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
            }
        }

        protected void DropDownListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_adminEventStatusEdit", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = LabelEventId.Text;
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
            //    sqlConn.Close();
            //    sqlCmd.Dispose();
            //    sqlConn.Dispose();
            //}

            LabelMessage.Visible = true;
            LabelMessage.Text = "You have succesfully edited event's status!";
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_admineventDelete", sqlConn);

            //try
            //{
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@EventId", SqlDbType.Int).Value = Convert.ToInt32(LabelEventId.Text);

                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();

                LabelMessage.Visible = true;
                LabelMessage.Text = "Entry has been deleted.";
                PanelInfo.Visible = false;
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    sqlConn.Close();
            //    sqlCmd.Dispose();
            //    sqlConn.Dispose();
            //}
        }

        protected void LinkButtonReports_Click(object sender, EventArgs e)
        {
            PanelReports.Visible = true;
        }
    }
}