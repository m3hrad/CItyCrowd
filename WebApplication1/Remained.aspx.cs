using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication1
{
    public partial class Remained : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check login
            int UserId = 0;

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

            Classes.UserInfo ui = new Classes.UserInfo();
            int locationId = ui.locationIdByUserId(UserId);

            Classes.Locations l = new Classes.Locations();
            Tuple<int, string> result1 = l.locationUsersCount(locationId);
            int locationUsersCount = result1.Item1;
            string locationName = result1.Item2;
            int minLocationUsers = Convert.ToInt32(ConfigurationManager.AppSettings["MinLocationUsers"].ToString());

            Page.Title = "Hi!";
            HiddenFieldTitle.Value = "Hi!";
            HiddenFieldSmiley.Value = ":)";
            HiddenFieldMessage.Value = "How are you doing today? Where can I redirect you?!";
            HiddenFieldLinksNumber.Value = "1";
            HiddenFieldLocationUsersCount.Value = locationUsersCount.ToString();
            HiddenFieldMinLocationUsers.Value = minLocationUsers.ToString();
            HiddenFieldLocation.Value = locationName;

            Classes.Done d = new Classes.Done();
            Tuple<string, string, string, string> result = d.doneItem("A", "");
            HiddenFieldLink1Text.Value = result.Item1;
            HiddenFieldLink1Url.Value = result.Item2;
            HiddenFieldLink1Image.Value = result.Item3;
            HiddenFieldLink1Color.Value = result.Item4;
        }
    }
}