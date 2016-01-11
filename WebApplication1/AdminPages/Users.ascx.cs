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
    public partial class Users : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check permissions
            Classes.Admins a = new Classes.Admins();
            if (!a.permissions(Convert.ToInt32(Session["UserId"]), "Users"))
            {
                Response.Redirect("~/Error/404");
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_adminUsersInfoByUsername", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = TextBoxUser.Text;

            //check if query string is user id or username
            double num;
            string userCode = TextBoxUser.Text;
            if (double.TryParse(userCode, out num))
            {
                sda = new SqlDataAdapter("sp_adminUsersInfoByUserId", sqlConn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = TextBoxUser.Text;
            }

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
                PanelUserInfo.Visible = false;
                LabelMessage.Text = "User doesn't exist!";
            }
            else
            {
                LabelMessage.Visible = false;
                PanelUserInfo.Visible = true;

                LabelUserId.Text = dt.Rows[0]["UserId"].ToString();
                LabelEmail.Text = dt.Rows[0]["Email"].ToString();
                LabelMobile.Text = dt.Rows[0]["Mobile"].ToString();
                LabelUsername.Text = dt.Rows[0]["Username"].ToString();
                LabelFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                LabelLastName.Text = dt.Rows[0]["LastName"].ToString();
                LabelDOB.Text = dt.Rows[0]["UserId"].ToString(); //////////////
                LabelMemberSince.Text = dt.Rows[0]["MemberSince"].ToString();
                LabelLastLogin.Text = dt.Rows[0]["LastLogin"].ToString();
                LabelLanguage.Text = dt.Rows[0]["Language"].ToString();
                LabelLocation.Text = dt.Rows[0]["LocationId"].ToString();
                LabelFollowingCount.Text = dt.Rows[0]["FollowingCount"].ToString();
                LabelFollowersCount.Text = dt.Rows[0]["FollowersCount"].ToString();
                LabelAbout.Text = dt.Rows[0]["About"].ToString();
                LabelEventsCreated.Text = dt.Rows[0]["EventsCreated"].ToString();
                LabelEventsRequested.Text = dt.Rows[0]["EventsRequested"].ToString();
                LabelEventsAccepted.Text = dt.Rows[0]["EventsAccepted"].ToString();
                DropDownListStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                ImageMobileStatus.ImageUrl = "~/Images/Verified" + dt.Rows[0]["MobileVerified"].ToString() + ".png";
                ImageEmailStatus.ImageUrl = "~/Images/Verified" + dt.Rows[0]["EmailVerified"].ToString() + ".png";
                // gender
                if (dt.Rows[0]["Gender"].ToString() == "1")
                {
                    LabelGender.Text = "Male";
                }
                else if (dt.Rows[0]["Gender"].ToString() == "2")
                {
                    LabelGender.Text = "Female";
                }
                // Rate
                int rateSufficient = Convert.ToInt32(ConfigurationManager.AppSettings["RateSufficient"].ToString());
                int rateCount = Convert.ToInt32(dt.Rows[0]["rateCount"].ToString());
                int rateScore = Convert.ToInt32(dt.Rows[0]["rateScore"].ToString());
                if (rateCount >= rateSufficient)
                {
                    int popularity = (20 * rateScore) / rateCount;
                    LabelRate.Text = popularity.ToString() + "%";
                }
                else
                {
                    LabelRate.Text = "NA";
                }
                LabelRateCount.Text = "(" + dt.Rows[0]["RateCount"].ToString() + " rate)";
                // Show profile's photo
                if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString()))
                {
                    ImagePhoto.ImageUrl = "~/Files/ProfilesPhotos/" + dt.Rows[0]["UserId"].ToString() + "-100.jpg";
                }
                else
                {
                    ImagePhoto.ImageUrl = "~/Images/nophoto.png";
                }
            }
        }

        protected void DropDownListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_adminUsersStatusEdit", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = LabelUserId.Text;
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
            LabelMessage.Text = "You have succesfully edited user's status!";
        }

        protected void LinkButtonLog_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            PanelReports.Visible = true;
        }
    }
}