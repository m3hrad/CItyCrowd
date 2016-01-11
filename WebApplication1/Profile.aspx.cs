using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Profile : System.Web.UI.Page
    {
        int profileId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //check to see if the user logged in or is a guest
            int buttonStatus = 0; //0 guest 1 user 2 owner
            bool userLogin = false;
            int UserId = 0;

            if (Request.Cookies["VC"] != null)
            {
                string VC = Request.Cookies["VC"].Values["VC"];
                Classes.LoginSession ls = new Classes.LoginSession();
                UserId = ls.getUserId(VC);
                if (UserId == 0)
                {

                }
                else
                {
                    userLogin = true;
                    Session["UserId"] = UserId.ToString();
                    buttonStatus = 1;
                }
            }

            if (UserId != 0) profileId = UserId;

            try
            {
                profileId = Convert.ToInt32(Page.RouteData.Values["Id"].ToString());
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
                

            Classes.UserInfo ui = new Classes.UserInfo();
            Tuple<int, DataTable> result = ui.profileInfo(UserId, profileId.ToString());

            int status = result.Item1;
            DataTable dt = result.Item2;
            

            if (status == -1)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfile");
            }
            else
            {
                // Profile is redistrected
                if (status != 1)
                {
                    Response.Redirect("~/Error/RestrictedProfile");
                }
                else
                {
                    
                    HiddenFieldProfilePhoto.Value = dt.Rows[0]["ProfilePicUrl"].ToString();

                    // Show action buttons
                    if (Convert.ToBoolean(dt.Rows[0]["isOwner"].ToString())) //user is the profile owner
                    {
                        buttonStatus = 2;
                        ButtonFollow.Visible = false;
                    }
                    else
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["isFollower"].ToString()))
                        {
                            HiddenFieldFollowText.Value = "UNFOLLOW";
                        }
                        else
                        {
                            HiddenFieldFollowText.Value = "FOLLOW";
                        }
                    }
                }
 
                // Profile info
                Page.Title = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString() + " (@" + dt.Rows[0]["Username"].ToString() + ") " + " Profile";
                LabelName.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                LabelUsername.Text = dt.Rows[0]["Username"].ToString();
                LabelFollowers.Text = dt.Rows[0]["FollowersCount"].ToString();
                LabelFollowing.Text = dt.Rows[0]["FollowingCount"].ToString();
                LabelAbout.Text = dt.Rows[0]["About"].ToString();
                HiddenFieldUserId.Value = dt.Rows[0]["UserId"].ToString();
                HiddenFieldProfileVerified.Value = dt.Rows[0]["ProfileVerified"].ToString();
                LabelRate.Text = dt.Rows[0]["RatePercent"].ToString();
                LabelRatePercent.Text = dt.Rows[0]["RatePercent"].ToString();
                LabelRateCount.Text = dt.Rows[0]["RateCount"].ToString();
                LabelCountry.Text = dt.Rows[0]["CountryName"].ToString();
                LabelCity.Text = dt.Rows[0]["CityName"].ToString();
                HiddenFieldFlagId.Value = dt.Rows[0]["CountryCode"].ToString();
                HiddenFieldCoverUrl.Value = dt.Rows[0]["CoverUrl"].ToString();
                HiddenFieldButtonStatus.Value = buttonStatus.ToString();
            }

            getEvents();
            getReviews();
            getFollowers();
            getFollowing();
        }

        protected void getEvents()
        {
            Classes.Events ev = new Classes.Events();
            DataTable dt = ev.eventslist(profileId, "created");

            if (dt.Rows.Count > 0)
            {
                RepeaterEvents.DataSource = dt;
                RepeaterEvents.DataBind();
                HiddenFieldEventsStatus.Value = "1";
            }
            else
            {
                HiddenFieldEventsStatus.Value = "0";
            }

        }

        protected void getReviews()
        {
            Classes.Reviews r = new Classes.Reviews();
            Tuple<int[], DataTable> result = r.reviews(profileId);

            int[] rateCount = result.Item1;
            DataTable dt = result.Item2;

            if (dt.Rows.Count != 0)
            {
                LabelRateOne.Text = rateCount[0].ToString();
                LabelRateTwo.Text = rateCount[1].ToString();
                LabelRateThree.Text = rateCount[2].ToString();
                LabelRateFour.Text = rateCount[3].ToString();
                LabelRateFive.Text = rateCount[4].ToString();

                RepeaterReviews.DataSource = dt;
                RepeaterReviews.DataBind();
                HiddenFieldReviewsStatus.Value = "1";
            }
            else
            {
                HiddenFieldReviewsStatus.Value = "0";
            }
        }

        protected void getFollowers()
        {
            Classes.UserInfo ui = new Classes.UserInfo();
            DataTable dt = ui.followersList(profileId);

            if (dt.Rows.Count != 0)
            {
                RepeaterFollowers.DataSource = dt;
                RepeaterFollowers.DataBind();
                HiddenFieldFollowersStatus.Value = "1";
            }
            else
            {
                HiddenFieldFollowersStatus.Value = "0";
            }
        }

        protected void getFollowing()
        {
            Classes.UserInfo ui = new Classes.UserInfo();
            DataTable dt = ui.followingList(profileId);

            if (dt.Rows.Count != 0)
            {
                RepeaterFollowing.DataSource = dt;
                RepeaterFollowing.DataBind();
                HiddenFieldFollowingStatus.Value = "1";
            }
            else
            {
                LabelFollowingNoRecord.Visible = true;
                LabelFollowingNoRecord.Text = "There is no following!";
                HiddenFieldFollowingStatus.Value = "0";
            }
        }

        protected void ButtonFollow_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            int profileId = Convert.ToInt32(HiddenFieldUserId.Value);

            if (userId == 0)
            {
                Response.Redirect("~/Login/Profile/" + profileId.ToString());
            }
            else
            {
                // is visitor follower
                Classes.UserActions ua = new Classes.UserActions();
                int followStatus = ua.followAction(userId, profileId);


                    if (followStatus == 1)
                    {
                        //user followed
                        HiddenFieldFollowText.Value = "UNFOLLOW";
                        HiddenFieldToastStatus.Value = "1";
                        HiddenFieldToastMode.Value = "follow";
                        HiddenFieldToastSmiley.Value = ":)";
                        HiddenFieldToastMessage.Value = "You followed this user!";
                        HiddenFieldButton1Text.Value = "OK";
                        HiddenFieldButton1Color.Value = "d7432e";
                    }
                    else if (followStatus == 2)
                    {
                        //user unfollowed
                        HiddenFieldFollowText.Value = "FOLLOW";
                        HiddenFieldToastStatus.Value = "1";
                        HiddenFieldToastMode.Value = "follow";
                        HiddenFieldToastSmiley.Value = ":)";
                        HiddenFieldToastMessage.Value = "You unfollowed this user!";
                        HiddenFieldButton1Text.Value = "OK";
                        HiddenFieldButton1Color.Value = "d7432e";
                    }
                    else if (followStatus == 0)
                    {
                        //error

                        HiddenFieldToastStatus.Value = "1";
                        HiddenFieldToastMode.Value = "follow";
                        HiddenFieldToastSmiley.Value = ":)";
                        HiddenFieldToastMessage.Value = "Something went wrong! Please try later!";
                        HiddenFieldButton1Text.Value = "OK";
                        HiddenFieldButton1Color.Value = "d7432e";
                    }
            }
        }
    }
}