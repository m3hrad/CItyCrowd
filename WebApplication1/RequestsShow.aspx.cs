using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class RequestsShow : System.Web.UI.Page
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

            
            Int64 eventId = 0;

            try
            {
                eventId = Convert.ToInt64(Page.RouteData.Values["EventId"].ToString());
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            if (eventId == 0)
            {
                Response.Redirect("~/Requests");
            }

            int actionCode = 0;
            Int64 requestId = 0;

            try
            {
                actionCode = Convert.ToInt32(Page.RouteData.Values["ActionCode"].ToString());
                requestId = Convert.ToInt64(Page.RouteData.Values["RequestId"].ToString());
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            if (actionCode != 0 && requestId != 0)
            {
                Classes.Requests r1 = new Classes.Requests();
                if(actionCode == 1)
                {
                    r1.requestAccept(UserId, requestId);
                }
                else if (actionCode == 2)
                {
                    r1.requestReject(UserId, requestId);
                }
                Response.Redirect("~/Requests/" + eventId.ToString());
            }


            // no action, just show the requests
            Classes.Requests r2 = new Classes.Requests();
            DataTable dt = r2.eventRequestsList(UserId, eventId);

            if (dt.Rows.Count == 0)
            {
                Response.Redirect("~/Requests");
            }
            else
            {
                RepeaterRequests.DataSource = dt;
                RepeaterRequests.DataBind();
            }
        }
    }
}