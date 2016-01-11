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
    public partial class MasterPageUser : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Application["Status"]) == 2) Response.Redirect("~/Maintenance.html");

            //check to see if the user logged in or is a guest
            bool userLogin = false;
            int UserId = 0;
            if (Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(Session["UserId"]);
                HyperLinkProfile.NavigateUrl = "~/Profile/" + UserId.ToString();
                userLogin = true;
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
                        //guest
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                        HyperLinkProfile.NavigateUrl = "~/Profile/" + UserId.ToString();
                        userLogin = true;
                    }
                }
                else
                {
                    userLogin = false;
                }
            }

            if (Request.Cookies["VC"] != null)
            {
                string VC = Request.Cookies["VC"].Values["VC"];
                Classes.LoginSession ls = new Classes.LoginSession();
                UserId = ls.getUserId(VC);
                if (UserId == 0)
                {
                    HiddenFieldLoginStatus.Value = "0";
                }
                else
                {
                    Session["UserId"] = UserId.ToString();
                    HyperLinkProfile.NavigateUrl = "~/Profile/" + UserId.ToString();
                }
            }

            if (userLogin)
            {
                //check user status
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

                HiddenFieldLoginStatus.Value = "1";

                DataTable dt;
                Classes.UserInfo ui2 = new Classes.UserInfo();
                dt = ui2.masterPageInfo(UserId);

                if (dt.Rows.Count == 0)// Profile doesn't exist OR user didn't logged in
                {
                    Response.Redirect("~/Error/NoProfileForSettings");
                }
                else
                {
                    LabelFullName.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                    HyperLinkProfile.NavigateUrl = "~/Profile/" + dt.Rows[0]["Username"].ToString();
                    HiddenFieldMessages.Value = dt.Rows[0]["MessagesCount"].ToString();
                    HiddenFieldRequests.Value = dt.Rows[0]["RequestsCount"].ToString();
                    HiddenFieldNotifications.Value = dt.Rows[0]["NotificationsCount"].ToString();
                    HiddenFieldUsername.Value = dt.Rows[0]["Username"].ToString();
                    HiddenFieldUserId.Value = Session["UserId"].ToString();

                    string photoUrl = "Images/NoPhoto.png";
                    if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString()))
                    {
                        photoUrl = "Files/ProfilesPhotos/" + UserId.ToString() + "-100.jpg";
                    }
                    HiddenFieldPhotoUrl.Value = photoUrl;
                }
            }
            else
            {
                HiddenFieldLoginStatus.Value = "0";
            }
        }
    }
}