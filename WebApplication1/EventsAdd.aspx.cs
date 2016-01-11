using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class EventsAdd : System.Web.UI.Page
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
                        Response.Redirect("~/Login/Events/Add");
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                    }
                }
                else
                {
                    Response.Redirect("~/Login/Events/Add");
                }
            }

            if (!IsPostBack)
            {
                Classes.UserInfo ui = new Classes.UserInfo();
                int locationId = ui.locationIdByUserId(UserId);
                Classes.Locations l = new Classes.Locations();
                //if (!l.isApproved(locationId))
                //{
                //    //Response.Redirect("~/Remained");
                //}

                DataTable dtCountries = l.countriesList();

                List<System.Web.UI.WebControls.ListItem> countries = new List<System.Web.UI.WebControls.ListItem>();
                DropDownListCountry.Items.Add(new ListItem("Select Country", "0"));
                for (int i = 0; i < dtCountries.Rows.Count; i++)
                {
                    DropDownListCountry.Items.Add(new ListItem(dtCountries.Rows[i]["CountryName"].ToString(), dtCountries.Rows[i]["CountryCode"].ToString()));
                }

                if (locationId == 0)
                {
                    DropDownListCountry.SelectedValue = "0";
                }
                else
                {
                    string countryCode = l.locationInfoOnlyId(locationId);

                    locationCity(countryCode);
                    DropDownListCountry.SelectedValue = countryCode;
                    DropDownListCity.SelectedValue = locationId.ToString();
                    HiddenFieldUserCountry.Value = countryCode;
                    HiddenFieldUserCity.Value = locationId.ToString();
                    HiddenFieldLocationId.Value = locationId.ToString();
                }
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            HiddenFieldStep.Value = "4";
            bool check = true;
            LiteralMessage.Text = "";

            if (TextBoxName.Text.Length == 0) // check name
            {
                check = false;
                LiteralMessage.Text = "Enter name<br/>";
            }
            if(TextBoxDate.Text.Length == 0) // check date
            {
                check = false;
                LiteralMessage.Text += "Enter date<br/>";
            }
            if (TextBoxParticipants.Text.Length == 0) // check participants +++only number
            {
                check = false;
                LiteralMessage.Text += "Enter participants number<br/>";
            }
            // participants
            double num;
            string candidate = TextBoxParticipants.Text;
            if (!double.TryParse(candidate, out num) && TextBoxParticipants.Text.Length != 0)
            {
                check = false;
                LiteralMessage.Text += "Participants is not valid, enter number<br/>";
            }
            // date
            if (Convert.ToDateTime(HiddenFieldDate.Value) < DateTime.Now)
            {
                check = false;
                LiteralMessage.Text += "Date cannot be in the past<br/>";
            }

            if(!check) // there is something wrong with the values that user entered
            {
                LiteralMessage.Visible = true;
            }
            else
            { // user entered valid values
                LiteralMessage.Visible = false;

                DateTime date = Convert.ToDateTime(HiddenFieldDate.Value);

                Classes.Events ev = new Classes.Events();
                int status = ev.eventAdd(Convert.ToInt32(Session["UserId"]),
                    TextBoxName.Text,
                    HiddenFieldDate.Value,
                    Convert.ToInt32(HiddenFieldParticipants.Value),
                    Convert.ToInt32(HiddenFieldLocationId.Value),
                    TextBoxAddress.Text,
                    Convert.ToInt16(HiddenFieldTypeId.Value),
                    Convert.ToInt16(HiddenFieldCoverId.Value),
                    TextBoxDescriptions.Text);

                if (status == 1)
                {
                    Response.Redirect("~/Done/EventAdded");
                }
                else if (status == -1)
                {
                    // +show error message
                }
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
            dt = l.citiesList(countryCode);

            DropDownListCity.Items.Clear();
            DropDownListCity.Items.Add(new ListItem("Select City", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownListCity.Items.Add(new ListItem(dt.Rows[i]["CityName"].ToString(), dt.Rows[i]["LocationId"].ToString()));
            }
        }
    }
}