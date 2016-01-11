using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class Explore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //check login
            int UserId = 0;
            if (Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(Session["UserId"]);
            }
            else
            {
                if (Request.Cookies["VC"] != null)
                {
                    string VC = Request.Cookies["VC"].Values["VC"];
                    Classes.LoginSession ls = new Classes.LoginSession();
                    UserId = ls.getUserId(VC);
                    if (UserId == 0) //if user not logged in redirect to login
                    {
                        Response.Redirect("~/Login/Explore");
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                    }
                }
                else
                {
                    Response.Redirect("~/Login/Explore");
                }
            }

            //check user status
            string completionValue = Session["DoneCompletion"] as string; if (String.IsNullOrEmpty(completionValue))
            {
                Classes.UserInfo ui = new Classes.UserInfo();
                int userStatus = ui.getUserStatus(UserId);
                switch (userStatus)
                {
                    case 1:
                        Session["DoneCompletion"] = "1";
                        break;
                    case 0:
                    case 4:
                        Response.Redirect("~/Completion");
                        break;
                    case 2:
                        Response.Redirect("~/Error/UserDisabled");
                        break;
                    case 3:
                        Response.Redirect("~/Error/UserDeactivated");
                        break;
                }
            }

            if (!IsPostBack)
            {
                Classes.Explore ex = new Classes.Explore();
                Tuple<int, Int64> result = ex.startRecommending(UserId);

                int status = result.Item1;
                Int64 eventId = result.Item2;

                if (status == 1)
                {
                    Classes.Events ev = new Classes.Events();
                    DataTable dt = ev.eventInfo(eventId, UserId);

                    HiddenFieldStatus.Value = "1";

                    int participantsAvailable = Convert.ToInt32(dt.Rows[0]["ParticipantsAccepted"].ToString());
                    int participants = Convert.ToInt32(dt.Rows[0]["Participants"].ToString());

                    HiddenFieldOwnerId.Value = dt.Rows[0]["OwnerId"].ToString();
                    HiddenFieldEventId.Value = dt.Rows[0]["EventId"].ToString();
                    LabelEventName.Text = dt.Rows[0]["Name"].ToString();
                    Page.Title = dt.Rows[0]["Name"].ToString();
                    HiddenFieldDate.Value = dt.Rows[0]["Date"].ToString();
                    LabelParticipants.Text = (participants + 1).ToString();
                    LabelParticipantsAvailable.Text = (participantsAvailable + 1).ToString();
                    LabelDescriptions.Text = dt.Rows[0]["Descriptions"].ToString();
                    LabelAddress.Text = dt.Rows[0]["Address"].ToString();
                    HiddenFieldUsername.Value = dt.Rows[0]["Username"].ToString();
                    HiddenFieldOwnerFullname.Value = dt.Rows[0]["OwnerName"].ToString();
                    HiddenFieldTypeId.Value = dt.Rows[0]["TypeId"].ToString();
                    HiddenFieldCoverId.Value = dt.Rows[0]["CoverId"].ToString();
                    int OwnerId = Convert.ToInt32(dt.Rows[0]["OwnerId"].ToString());

                    Classes.Locations l = new Classes.Locations();
                    DataTable dtLocation = l.getLocationInfoByCityId(Convert.ToInt32(dt.Rows[0]["LocationId"].ToString()));
                    if (dtLocation.Rows.Count == 0)
                    {
                        LabelLocation.Text = "Not Available!";
                    }
                    else
                    {
                        LabelLocation.Text = dtLocation.Rows[0]["CountryName"].ToString() + " - " + dtLocation.Rows[0]["CityName"].ToString();
                    }

                    //Owner photo url
                    if (Convert.ToBoolean(dt.Rows[0]["OwnerHasPhoto"].ToString()))
                    {
                        HiddenFieldOwnerPhotoUrl.Value = "Files/ProfilesPhotos/" + dt.Rows[0]["OwnerId"].ToString() + "-100.jpg";
                    }
                    else
                    {
                        HiddenFieldOwnerPhotoUrl.Value = "Images/nophoto.png";
                    }
                    //owner rate
                    int RateCount = Convert.ToInt32(dt.Rows[0]["RateCount"].ToString());
                    int RateScore = Convert.ToInt32(dt.Rows[0]["RateScore"].ToString());
                    int RateSufficient = Convert.ToInt32(ConfigurationManager.AppSettings["RateSufficient"].ToString());

                    if (RateCount >= RateSufficient)
                    {
                        int RatePercent = (20 * RateScore / RateCount);
                        HiddenFieldOwnerRateScore.Value = RatePercent.ToString();
                        HiddenFieldOwnerRateCount.Value = RateCount.ToString();
                    }
                    else
                    {
                        HiddenFieldOwnerRateScore.Value = "0";
                        HiddenFieldOwnerRateCount.Value = "0";
                    }

                    switch (dt.Rows[0]["Status"].ToString())
                    {
                        case "1":
                            LabelStatus.Text = "Available";
                            break;
                        case "2":
                            LabelStatus.Text = "Full";
                            break;
                        case "3":
                            LabelStatus.Text = "Passed";
                            break;
                    }

                    //bookmark
                    bool bookmarkStatus = ev.checkBookmark(UserId, eventId);

                    if (bookmarkStatus == true)
                    {
                        ButtonBookmark.Text = "Remove Bookmark";
                    }
                    else
                    {
                        ButtonBookmark.Text = "Add Bookmark";
                    }
                }
                else
                {
                    HiddenFieldStatus.Value = "0";
                    HiddenFieldSmiley.Value = ":)";
                    HiddenFieldMessage.Value = "Unfortunatly there is no event to show now!";
                    HiddenFieldLinksNumber.Value = "4";

                    Classes.Done d = new Classes.Done();
                    Tuple<string, string, string, string> result2 = d.doneItem("A", "");
                    HiddenFieldLink1Text.Value = result2.Item1;
                    HiddenFieldLink1Url.Value = result2.Item2;
                    HiddenFieldLink1Image.Value = result2.Item3;
                    HiddenFieldLink1Color.Value = result2.Item4;

                    result2 = d.doneItem("B", "");
                    HiddenFieldLink2Text.Value = result2.Item1;
                    HiddenFieldLink2Url.Value = result2.Item2;
                    HiddenFieldLink2Image.Value = result2.Item3;
                    HiddenFieldLink2Color.Value = result2.Item4;

                    result2 = d.doneItem("C", "");
                    HiddenFieldLink3Text.Value = result2.Item1;
                    HiddenFieldLink3Url.Value = result2.Item2;
                    HiddenFieldLink3Image.Value = result2.Item3;
                    HiddenFieldLink3Color.Value = result2.Item4;

                    result2 = d.doneItem("E", "");
                    HiddenFieldLink4Text.Value = result2.Item1;
                    HiddenFieldLink4Url.Value = result2.Item2;
                    HiddenFieldLink4Image.Value = result2.Item3;
                    HiddenFieldLink4Color.Value = result2.Item4;
                }
            }
        }

        protected void ButtonBookmark_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                Classes.Events ev = new Classes.Events();
                int status = ev.eventBookmark(Convert.ToInt32(Session["UserId"]), Convert.ToInt64(Page.RouteData.Values["EventId"]));

                if (status == 1)
                {
                    LabelMessage.Visible = true;
                    LabelMessage.Text = "You have successfully added this event to your bookmark list";
                    ButtonBookmark.Text = "Remove Bookmark";
                }
                else if (status == 2)
                {
                    LabelMessage.Visible = true;
                    LabelMessage.Text = "You have successfully removed this event from your bookmark list";
                    ButtonBookmark.Text = "Add Bookmark";
                }
            }
            else
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "You have to login first.";
            }
        }

        protected void ButtonActionNo_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_exploreStatusSet", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

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

            Response.Redirect("~/Explore");
        }

        protected void ButtonActionYes_Click(object sender, EventArgs e)
        {
            string message = TextBoxMessage.Text;
            if (message.Length > 85)
            {
                message = message.Substring(0, 85);
            }

            Classes.Explore ex = new Classes.Explore();
            int status = ex.actionYes(Convert.ToInt32(Session["UserId"]), Convert.ToInt64(HiddenFieldEventId.Value), message);

            Response.Redirect("~/Explore");
        }
    }
}