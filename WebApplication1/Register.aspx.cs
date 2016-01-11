using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication1
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Application["Status"]) == 2) Response.Redirect("~/Maintenance.html");

            //reading cookies for loginint UserId = 0;
            if (Session["UserId"] != null)
            {
                Response.Redirect("~/Done/Welcome");
            }
            else
            {
                if (Request.Cookies["VC"] != null)
                {
                    string VC = Request.Cookies["VC"].Values["VC"];
                    Classes.LoginSession ls = new Classes.LoginSession();
                    int UserId = ls.getUserId(VC);
                    if (UserId != 0) //user logged before
                    {
                        Response.Redirect("~/Done/Welcome");
                    }
                }
            }

            if (!Convert.ToBoolean(Application["RegisterAllowed"]))
            {
                LabelError.Text = "Login is not allowed! Please try again later!";
                ButtonRegister.Enabled = false;
            }

            //check if visitor has been invited
            int inviteId = 0;
            try
            {
                inviteId = Convert.ToInt32(Page.RouteData.Values["InviteId"].ToString());

                // create the cookies
                HttpCookie _inviteIdCookies = new HttpCookie("inviteid");
                _inviteIdCookies["inviteid"] = Page.RouteData.Values["InviteId"].ToString();
                _inviteIdCookies.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(_inviteIdCookies);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            if(inviteId != 0)
            {
                HiddenFieldInvite.Value = inviteId.ToString();
                Classes.UserInfo ui = new Classes.UserInfo();
                Tuple<int, string, string> result = ui.getFirstNamePhotoUrlByUserId(inviteId);
                HiddenFieldInviteStatus.Value = result.Item1.ToString(); 
                HiddenFieldInviteName.Value = result.Item2;
                HiddenFieldInvitePhotoUrl.Value = result.Item3;
            }
        }

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            string email = TextBoxEmail.Text;
            string password1 = TextBoxPassword1.Text;
            string password2 = TextBoxPassword2.Text;
            int inviteId = Convert.ToInt32(HiddenFieldInvite.Value);

            Classes.UserProfileSet ups = new Classes.UserProfileSet();
            Tuple<int, string, int> result = ups.register(email, password1, password2, inviteId);

            if (result.Item1 == -1)
            {
                LabelError.Visible = true;
                LabelError.Text = result.Item2;
            }
            else if (result.Item1 == 1)
            {
                Classes.UserInfo ui = new Classes.UserInfo();
                int userId = ui.getUserIdByEmail(email);
                Session["UserId"] = userId.ToString();

                int Hours = Convert.ToInt32(ConfigurationManager.AppSettings["LoginHoursShort"].ToString());
                string VerificationCode = Convert.ToString(Guid.NewGuid());

                // set login information
                Classes.LoginSession ls = new Classes.LoginSession();
                ls.setLoginSession(userId, VerificationCode, Hours);

                // create the cookies
                HttpCookie _userInfoCookies = new HttpCookie("VC");
                _userInfoCookies["VC"] = VerificationCode;
                _userInfoCookies.Expires = DateTime.Now.AddHours(Hours);
                Response.Cookies.Add(_userInfoCookies);

                //check if user got invited
                int invitorId = 0;

                try
                {
                    invitorId = Convert.ToInt32(Page.RouteData.Values["UserId"]);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    
                }

                if (invitorId != 0)
                {
                    Classes.Notifications n = new Classes.Notifications();
                    n.notificationAdd(invitorId, 5, userId);
                }

                // redirect the user
                Response.Redirect("~/Completion");
            }
        }
    }
}