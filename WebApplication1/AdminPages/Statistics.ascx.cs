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
    public partial class Statistics : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check permissions
            Classes.Admins a = new Classes.Admins();
            if (!a.permissions(Convert.ToInt32(Session["UserId"]), "Statistics"))
            {
                Response.Redirect("~/Error/404");
            }
        }
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_adminStatsDelete", sqlConn);

            //try
            //{
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@StatisticId", SqlDbType.Int).Value = Convert.ToInt32(DropDownListStats.SelectedValue);

                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();

                DropDownListStats.DataBind();
                PanelStats.Visible = false;
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

        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_adminStatsAdd", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;

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

            DropDownListStats.DataBind();
        }

        protected void DropDownListStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            showStats(Convert.ToInt32(DropDownListStats.SelectedValue));
        }

        protected void showStats(int statisticsId)
        {
            PanelStats.Visible = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_adminStatsInfo", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@StatisticId", SqlDbType.Int).Value = statisticsId;

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

            LabelAdmins.Text = dt.Rows[0]["Admins"].ToString();
            LabelBookmarks.Text = dt.Rows[0]["Bookmarks"].ToString();
            LabelBookmarkDistinctUsers.Text = dt.Rows[0]["BookmarkDistinctUsers"].ToString();
            LabelUEvents.Text = dt.Rows[0]["Events"].ToString();
            LabelEventsAvailable.Text = dt.Rows[0]["EventsAvailable"].ToString();
            LabelEventsDistinctOwners.Text = dt.Rows[0]["EventsDistinctOwners"].ToString();
            LabelEventsParticipantsAverage.Text = dt.Rows[0]["EventsParticipantsAverage"].ToString();
            LabelEventsParticipantsAcceptedAverage.Text = dt.Rows[0]["EventsParticipantsAcceptedAverage"].ToString();
            LabelEventsDistinctLocations.Text = dt.Rows[0]["EventsDistinctLocations"].ToString();
            LabelEventsType1.Text = dt.Rows[0]["EventsType1"].ToString();
            LabelEventsType2.Text = dt.Rows[0]["EventsType2"].ToString();
            LabelEventsType3.Text = dt.Rows[0]["EventsType3"].ToString();
            LabelEventsType4.Text = dt.Rows[0]["EventsType4"].ToString();
            LabelEventsType5.Text = dt.Rows[0]["EventsType5"].ToString();
            LabelEventsType6.Text = dt.Rows[0]["EventsType6"].ToString();
            LabelEventsType7.Text = dt.Rows[0]["EventsType7"].ToString();
            LabelEventsType8.Text = dt.Rows[0]["EventsType8"].ToString();
            LabelEventsView.Text = dt.Rows[0]["EventsView"].ToString();
            LabelEventsRequests.Text = dt.Rows[0]["EventsRequests"].ToString();
            LabelEventsBoardsMessages.Text = dt.Rows[0]["EventsBoardsMessages"].ToString();
            LabelFollowers.Text = dt.Rows[0]["Followers"].ToString();
            LabelReviews.Text = dt.Rows[0]["Reviews"].ToString();
            LabelReviewsRateAverage.Text = dt.Rows[0]["ReviewsRateAverage"].ToString();
            LabelUsers.Text = dt.Rows[0]["Users"].ToString();
            LabelUsersGenderMale.Text = dt.Rows[0]["UsersGenderMale"].ToString();
            LabelUsersGenderFemale.Text = dt.Rows[0]["UsersGenderFemale"].ToString();
            LabelUsersGenderUnknown.Text = dt.Rows[0]["UsersGenderUnknown"].ToString();
            LabelUsersHasPhoto.Text = dt.Rows[0]["UsersHasPhoto"].ToString();
            LabelUsersMessages.Text = dt.Rows[0]["UsersMessages"].ToString();
        }
    }
}