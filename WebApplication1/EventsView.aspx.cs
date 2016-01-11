using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace WebApplication1
{
    public partial class EventsView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check to see if the user logged in or is a guest
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
                    if (UserId == 0)
                    {
                        //Response.Redirect("~/Login");
                    }
                }
            }

            int buttonsStatus = 2; //0 guest 1 owner 2 user not requested 3 user requested 4 user participant
            int OwnerId = -1;
            if (UserId == 0)
            {
                buttonsStatus = 0;
            }

            Int64 eventId = Convert.ToInt64(Page.RouteData.Values["EventId"].ToString());

            if (!IsPostBack)
            {
                // get info
                Classes.Events ev = new Classes.Events();
                DataTable dt = ev.eventInfo(eventId, UserId);

                if (dt.Rows.Count == 0)// event doesn't exist
                {
                    Response.Redirect("~/Error/EventNotFound");
                }
                else
                {
                    int eventStatus = Convert.ToInt32(dt.Rows[0]["Status"].ToString());
                    if (eventStatus == 4)// event is banned
                    {
                        Response.Redirect("~/Error/EventNotFound");
                    }

                    //count available spots
                    int participantsAvailable = Convert.ToInt32(dt.Rows[0]["ParticipantsAccepted"].ToString());
                    int participants = Convert.ToInt32(dt.Rows[0]["Participants"].ToString());

                    HiddenFieldOwnerId.Value = dt.Rows[0]["OwnerId"].ToString();
                    HiddenFieldEventId.Value = Page.RouteData.Values["EventId"].ToString();
                    LabelName.Text = dt.Rows[0]["Name"].ToString();
                    Page.Title = dt.Rows[0]["Name"].ToString();
                    HiddenFieldDate.Value = dt.Rows[0]["Date"].ToString();
                    LabelParticipants.Text = (participants + 1).ToString();
                    LabelParticipantsAvailable.Text = (participantsAvailable + 1).ToString();
                    LabelAddress.Text = dt.Rows[0]["Address"].ToString();
                    LabelDescriptions.Text = dt.Rows[0]["Descriptions"].ToString();
                    HiddenFieldUsername.Value = dt.Rows[0]["Username"].ToString();
                    HiddenFieldOwnerFullname.Value = dt.Rows[0]["OwnerName"].ToString();
                    HiddenFieldTypeId.Value = dt.Rows[0]["TypeId"].ToString();
                    HiddenFieldCoverId.Value = dt.Rows[0]["CoverId"].ToString();
                    OwnerId = Convert.ToInt32(dt.Rows[0]["OwnerId"].ToString());

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

                    if (UserId != 0)
                    {
                        //check to see if the user logged in or is a guest
                        if (UserId.ToString() == dt.Rows[0]["OwnerId"].ToString())
                        {
                            HyperLinkModify.Visible = true;
                            HyperLinkModify.NavigateUrl = "~/Events/Modify/" + eventId.ToString();
                            buttonsStatus = 1;
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

                        HiddenFieldButtonStatus.Value = buttonsStatus.ToString();
                    }
                }

                ////////////////// participants list
                DataTable dtParticipants = ev.eventParticipants(eventId);

                RepeaterParticipants.DataSource = dtParticipants;
                RepeaterParticipants.DataBind();

                if (RepeaterParticipants.Items.Count == 0)
                {
                    LabelNoRecord.Visible = true;
                }

                /////////////////////////////////////////////board messages
                bool status = ev.allowBoard(UserId, eventId);

                if (status)
                {
                    getBoardMessages(eventId, Convert.ToInt32(dt.Rows[0]["OwnerId"].ToString()));
                    HiddenFieldBoardStatus.Value = "1";
                }
                else
                {
                    HiddenFieldBoardStatus.Value = "0";
                }

            }
            else
            {
                Page.Title = LabelName.Text;
            }

            if (UserId != 0)
            {
                if (UserId.ToString() == OwnerId.ToString())
                {
                    buttonsStatus = 1;
                }
                else
                {
                    Classes.Requests r = new Classes.Requests();
                    int requestStatus = r.checkRequest(UserId, eventId);

                    if (requestStatus == 0)
                    {
                        buttonsStatus = 2;
                    }
                    else if (requestStatus == 1)
                    {
                        buttonsStatus = 3;
                    }
                    else if (requestStatus == 2)
                    {
                        buttonsStatus = 4;
                    }
                }
            }

            HiddenFieldRequestStatus.Value = buttonsStatus.ToString();
            HiddenFieldButtonStatus.Value = buttonsStatus.ToString();
        }

        protected void ButtonBookmark_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                Classes.Events ev = new Classes.Events();
                int status = ev.eventBookmark(Convert.ToInt32(Session["UserId"]), Convert.ToInt64(Page.RouteData.Values["EventId"]));

                if (status == 1)
                {
                    HiddenFieldToastStatus.Value = "1";
                    HiddenFieldToastMode.Value = "bookmark";
                    HiddenFieldToastSmiley.Value = ":)";
                    HiddenFieldToastMessage.Value = "The event added to your bookmark list!";
                    HiddenFieldButton1Text.Value = "OK";
                    HiddenFieldButton1Color.Value = "d7432e";
                    ButtonBookmark.Text = "Remove Bookmark";
                }
                else if (status == 2)
                {
                    HiddenFieldToastStatus.Value = "1";
                    HiddenFieldToastMode.Value = "bookmark";
                    HiddenFieldToastSmiley.Value = ":)";
                    HiddenFieldToastMessage.Value = "The event removed from bookmark list!";
                    HiddenFieldButton1Text.Value = "OK";
                    HiddenFieldButton1Color.Value = "d7432e";
                    ButtonBookmark.Text = "Remove Bookmark";
                    ButtonBookmark.Text = "Add Bookmark";
                }
            }
            else
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "You have to login first.";
            }
        }

        protected void getBoardMessages(Int64 eventId, int ownerId)
        {
            //check if participant or owner

            Classes.Events e = new Classes.Events();
            DataTable dt = e.eventBoardMessages(eventId, ownerId);

            if (dt.Rows.Count != 0)
            {
                RepeaterBoardMessages.DataSource = dt;
                RepeaterBoardMessages.DataBind();
            }
        }

        protected void ButtonBoardMessageAdd_Click(object sender, EventArgs e)
        {
            Int64 eventId = Convert.ToInt64(Page.RouteData.Values["EventId"].ToString());
            int userId = Convert.ToInt32(Session["UserId"]);

            Classes.Events ev = new Classes.Events();
            bool status = ev.allowBoard(userId, eventId);

            if (status)
            {
                string message = TextBoxBoardMessageAdd.Text;
                Int16 status2 = ev.eventBoardMessagesAdd(eventId, userId, message);
                HiddenFieldBoardStatus.Value = "1";
            }
            else
            {
                HiddenFieldBoardStatus.Value = "0";
            }
        }

        protected void ButtonRequest_Click(object sender, EventArgs e)
        {
            string message = TextBoxMessage.Text;
            if (message.Length > 85)
            {
                message = message.Substring(0, 85);
            }

            Classes.Requests r = new Classes.Requests();
            int status = r.requestSend(Convert.ToInt32(Session["UserId"]), Convert.ToInt64(Page.RouteData.Values["EventId"].ToString()), message);

            switch (status)
            {
                case -1:
                    HiddenFieldToastStatus.Value = "1";
                    HiddenFieldToastMode.Value = "request";
                    HiddenFieldToastSmiley.Value = ":)";
                    HiddenFieldToastMessage.Value = "You cannot send a request to your event!";
                    HiddenFieldButton1Text.Value = "OK";
                    HiddenFieldButton1Color.Value = "d7432e";
                    break;
                case 1:
                    HiddenFieldToastStatus.Value = "1";
                    HiddenFieldToastMode.Value = "request";
                    HiddenFieldToastSmiley.Value = ":)";
                    HiddenFieldToastMessage.Value = "You canceled your participation!";
                    HiddenFieldButton1Text.Value = "OK";
                    HiddenFieldButton1Color.Value = "d7432e";
                    HiddenFieldButtonStatus.Value = "2";
                    break;
                case 2:
                    HiddenFieldToastStatus.Value = "1";
                    HiddenFieldToastMode.Value = "request";
                    HiddenFieldToastSmiley.Value = ":)";
                    HiddenFieldToastMessage.Value = "You canceled your request!";
                    HiddenFieldButton1Text.Value = "OK";
                    HiddenFieldButton1Color.Value = "d7432e";
                    HiddenFieldButtonStatus.Value = "2";
                    break;
                case 3:
                    HiddenFieldToastStatus.Value = "1";
                    HiddenFieldToastMode.Value = "request";
                    HiddenFieldToastSmiley.Value = ":)";
                    HiddenFieldToastMessage.Value = "Your request has been successfully sent!";
                    HiddenFieldButton1Text.Value = "OK";
                    HiddenFieldButton1Color.Value = "d7432e";
                    HiddenFieldButtonStatus.Value = "3";
                    break;
            }
        }

        protected void ButtonRemoveParticipant_Click(object sender, EventArgs e)
        {
            int ownerId = Convert.ToInt32(Session["UserId"]);
            int userId = Convert.ToInt32(HiddenFieldParticipantValue.Value);
            Int64 eventId = Convert.ToInt64(Page.RouteData.Values["EventId"].ToString());

            Classes.Requests r = new Classes.Requests();
            int status = r.removeParticipant(ownerId, userId, eventId);

            //0 user not owner 1 participants cancelled 2 user was not participant
            if(status == 0)
            {
                HiddenFieldToastStatus.Value = "1";
                HiddenFieldToastMode.Value = "participants";
                HiddenFieldToastSmiley.Value = ":(";
                HiddenFieldToastMessage.Value = "You are not the owner of this event!";
                HiddenFieldButton1Text.Value = "OK";
                HiddenFieldButton1Color.Value = "d7432e";
            }
            else if (status == 1)
            {
                ////////////////// participants list
                Classes.Events ev = new Classes.Events();
                DataTable dtParticipants = ev.eventParticipants(eventId);

                RepeaterParticipants.DataSource = dtParticipants;
                RepeaterParticipants.DataBind();

                if (RepeaterParticipants.Items.Count == 0)
                {
                    LabelNoRecord.Visible = true;
                }

                HiddenFieldToastStatus.Value = "1";
                HiddenFieldToastMode.Value = "participants";
                HiddenFieldToastSmiley.Value = ":)";
                HiddenFieldToastMessage.Value = "The participant got removed!";
                HiddenFieldButton1Text.Value = "OK";
                HiddenFieldButton1Color.Value = "d7432e";
            }
            else if (status == 2)
            {
                ////////////////// participants list
                Classes.Events ev = new Classes.Events();
                DataTable dtParticipants = ev.eventParticipants(eventId);

                RepeaterParticipants.DataSource = dtParticipants;
                RepeaterParticipants.DataBind();

                if (RepeaterParticipants.Items.Count == 0)
                {
                    LabelNoRecord.Visible = true;
                }

                HiddenFieldToastStatus.Value = "1";
                HiddenFieldToastMode.Value = "participants";
                HiddenFieldToastSmiley.Value = ":)";
                HiddenFieldToastMessage.Value = "The user was not a participant!";
                HiddenFieldButton1Text.Value = "OK";
                HiddenFieldButton1Color.Value = "d7432e";
            }
        }
    }
}