using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Nearby : System.Web.UI.Page
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
                        Response.Redirect("~/Login/Nearby");
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                    }
                }
                else
                {
                    Response.Redirect("~/Login/Nearby");
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

            Page.Title = "Nearby";
            feedCity(4, 99999999);
        }

        protected void feedCity(int mode, Int64 eventId)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            Classes.UserInfo ui = new Classes.UserInfo();
            int locationId = ui.locationIdByUserId(userId);

            //{
            //    HiddenFieldStatus.Value = "0";
            //    HiddenFieldSmiley.Value = ":)";
            //    HiddenFieldMessage.Value = "Unfortunatly there is no event in your city to show now!";
            //    HiddenFieldLinksNumber.Value = "4";

            //    Classes.Done d = new Classes.Done();
            //    Tuple<string, string, string, string> result2 = d.doneItem("A", "");
            //    HiddenFieldLink1Text.Value = result2.Item1;
            //    HiddenFieldLink1Url.Value = result2.Item2;
            //    HiddenFieldLink1Image.Value = result2.Item3;
            //    HiddenFieldLink1Color.Value = result2.Item4;

            //    result2 = d.doneItem("C", "");
            //    HiddenFieldLink2Text.Value = result2.Item1;
            //    HiddenFieldLink2Url.Value = result2.Item2;
            //    HiddenFieldLink2Image.Value = result2.Item3;
            //    HiddenFieldLink2Color.Value = result2.Item4;

            //    result2 = d.doneItem("E", "");
            //    HiddenFieldLink3Text.Value = result2.Item1;
            //    HiddenFieldLink3Url.Value = result2.Item2;
            //    HiddenFieldLink3Image.Value = result2.Item3;
            //    HiddenFieldLink3Color.Value = result2.Item4;

            //    result2 = d.doneItem("F", "");
            //    HiddenFieldLink4Text.Value = result2.Item1;
            //    HiddenFieldLink4Url.Value = result2.Item2;
            //    HiddenFieldLink4Image.Value = result2.Item3;
            //    HiddenFieldLink4Color.Value = result2.Item4;
            //}
        }
    }
}