using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Events : System.Web.UI.Page
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
                        Response.Redirect("~/Login/Events");
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                    }
                }
                else
                {
                    Response.Redirect("~/Login/Events");
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


            DataTable dt;
            string mode = "";
            string message = "";

            try
            {
                mode = Page.RouteData.Values["Mode"].ToString().ToLower();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            switch (mode)
            {
                case "created":
                    message = "Well! You don’t have any events now!";
                    break;
                case "accepted":
                    message = "Well! You haven’t been accepted in any upcoming events!";
                    break;
                case "requested":
                    message = "You haveen't requested to participate in any upcoming events!";
                    break;
                case "bookmarked":
                    message = "You haven't bookmarked any events!";
                    break;
                default:
                    Response.Redirect("~/Events/Mode/Created");
                    break;
            }

            Classes.Events ev = new Classes.Events();
            dt = ev.eventslist(Convert.ToInt32(Session["UserId"]), mode);

            if (dt.Rows.Count > 0)
            {
                RepeaterEvents.DataSource = dt;
                RepeaterEvents.DataBind();
                LabelNoRecord.Visible = false;
            }
            else
            {
                HiddenFieldStatus.Value = "0";
                HiddenFieldSmiley.Value = ":|";
                HiddenFieldMessage.Value = message;
                HiddenFieldLinksNumber.Value = "1";
                Classes.Done d = new Classes.Done();
                Tuple<string, string, string, string> result = d.doneItem("A", "");
                HiddenFieldLink1Text.Value = result.Item1;
                HiddenFieldLink1Url.Value = result.Item2;
                HiddenFieldLink1Image.Value = result.Item3;
                HiddenFieldLink1Color.Value = result.Item4;
            }
        }
    }
}