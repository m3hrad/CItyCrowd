using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class EventsModify : System.Web.UI.Page
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
                        Response.Redirect("~/Login");
                    }
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
                // get info
                Classes.Events ev = new Classes.Events();
                DataTable dt = ev.eventModifyInfo(Convert.ToInt64(Page.RouteData.Values["EventId"].ToString()));

                if (dt.Rows.Count == 0)// event doesn't exist
                {
                    Response.Redirect("~/Error/EventNotFound");
                }
                else
                {
                    //check if user is the owner
                    if (UserId.ToString() != dt.Rows[0]["OwnerId"].ToString())
                    {
                        Response.Redirect("~/Error/UserNotEventOwner");
                    }

                    //count available spots
                    int participantsAvailable = Convert.ToInt32(dt.Rows[0]["Participants"].ToString()) - Convert.ToInt32(dt.Rows[0]["ParticipantsAccepted"].ToString());
                    //get time
                    string timeHour = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).Hour.ToString();
                    string timeMinute = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).Minute.ToString();
                    DropDownListTimeHour.SelectedValue = timeHour;
                    DropDownListTimeMinute.SelectedValue = timeMinute;

                    TextBoxName.Text = dt.Rows[0]["Name"].ToString();
                    HiddenFieldTypeId.Value = dt.Rows[0]["TypeId"].ToString();
                    TextBoxDescriptions.Text = dt.Rows[0]["Descriptions"].ToString();
                    TextBoxAddress.Text = dt.Rows[0]["Address"].ToString();
                    HiddenFieldParticipants.Value = dt.Rows[0]["Participants"].ToString();
                    HiddenFieldParticipantsAccepted.Value = dt.Rows[0]["ParticipantsAccepted"].ToString();
                    HiddenFieldDate.Value = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToString("d/M/yyyy hh:mm:ss tt");
                    HiddenFieldCoverId.Value = dt.Rows[0]["CoverId"].ToString();

                    /////////////////// event photo
                    Classes.Locations l = new Classes.Locations();
                    string countryCode = l.locationInfoOnlyId(Convert.ToInt32(dt.Rows[0]["LocationId"].ToString()));
                    int cityId = Convert.ToInt32(dt.Rows[0]["LocationId"].ToString());

                    locationCountry();
                    DropDownListCountry.SelectedValue = countryCode;
                    locationCity(countryCode);
                    DropDownListCity.SelectedValue = dt.Rows[0]["LocationId"].ToString();
                }
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            HiddenFieldStep.Value = "4";
            DateTime date = Convert.ToDateTime(HiddenFieldDate.Value);

            Classes.Events ev = new Classes.Events();
            Tuple<int, string> result = ev.eventModify(
                Convert.ToInt64(Page.RouteData.Values["EventId"].ToString()),
                TextBoxName.Text,
                date,
                TextBoxParticipants.Text,
                Convert.ToInt32(HiddenFieldLocationId.Value),
                TextBoxAddress.Text,
                Convert.ToInt16(HiddenFieldTypeId.Value),
                Convert.ToInt32(HiddenFieldCoverId.Value),
                TextBoxDescriptions.Text
                );

            int status = result.Item1;
            string message = result.Item2;

            if (status == -1)
            {
                LiteralMessage.Visible = true;
                LiteralMessage.Text = message;
            }
            else
            {
                // show success message
                Response.Redirect("~/Done/EventModified");
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            Int64 eventId = Convert.ToInt64(Page.RouteData.Values["EventId"].ToString());
            Classes.Events ev = new Classes.Events();
            if (ev.isUserOwner(userId, eventId))
            {
                int status = ev.eventDelete(eventId, userId);
                if (status == 1)
                {
                    Response.Redirect("~/Done/EventDeleted");
                }
            }
        }

        protected void LinkButtonAdvance_Click(object sender, EventArgs e)
        {
            if (HiddenFieldAdvance.Value == "0")
            {
                PanelAdvance.Visible = true;
                HiddenFieldAdvance.Value = "1";
                LinkButtonAdvance.Text = "Hide Advance";
            }
            else
            {
                PanelAdvance.Visible = false;
                HiddenFieldAdvance.Value = "0";
                LinkButtonAdvance.Text = "Show Advance";
            }
        }

        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            HiddenFieldStep.Value = "3";

            locationCity(DropDownListCountry.SelectedValue);
        }

        void locationCity(string countryCode)
        {
            DataTable dt;
            Classes.Locations l = new Classes.Locations();
            dt = l.citiesList(DropDownListCountry.SelectedValue);

            DropDownListCity.Items.Clear();
            DropDownListCity.Items.Add(new ListItem("Select City", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownListCity.Items.Add(new ListItem(dt.Rows[i]["CityName"].ToString(), dt.Rows[i]["LocationId"].ToString()));
            }
        }

        void locationCountry()
        {
            DataTable dt;
            Classes.Locations l = new Classes.Locations();
            dt = l.countriesList();

            DropDownListCountry.Items.Clear();
            DropDownListCountry.Items.Add(new ListItem("Select Country", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownListCountry.Items.Add(new ListItem(dt.Rows[i]["CountryName"].ToString(), dt.Rows[i]["CountryCode"].ToString()));
            }
        }
    }
}