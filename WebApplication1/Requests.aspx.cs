using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Requests : System.Web.UI.Page
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
                        Response.Redirect("~/Login/Requests");
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                    }
                }
                else
                {
                    Response.Redirect("~/Login/Requests");
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

            //all read
            Classes.Requests r = new Classes.Requests();
            r.allRead(UserId);

            Classes.Requests re = new Classes.Requests();
            Tuple<int, DataTable> result = re.requestsList(UserId);

            //status codes: 0 no event 1 requests 2 no request
            int status = result.Item1;

            HiddenFieldRequestsStatus.Value = status.ToString();
            HiddenFieldTitle.Value = "Requests";

            if (status == 1)
            {
                DataTable dt = result.Item2;
                RepeaterRequests.DataSource = dt;
                RepeaterRequests.DataBind();
            }
        }
    }
}