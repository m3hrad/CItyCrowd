using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    public partial class Invite : System.Web.UI.Page
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
                        Response.Redirect("~/Login/Invite");
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                    }
                }
                else
                {
                    Response.Redirect("~/Login/Invite");
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

            HiddenFieldUserId.Value = UserId.ToString();
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            string email = TextBoxEmail.Text;
            int userId = Convert.ToInt32(Session["UserId"]);

            Classes.Invite i = new Classes.Invite();
            int status = i.inviteSend(userId, email);

            if (status == 1)
            {
                HiddenFieldToastStatus.Value = "1";
                HiddenFieldToastMode.Value = "invite";
                HiddenFieldToastSmiley.Value = ":)";
                HiddenFieldToastMessage.Value = "You have successfully sent your invitation!";
                HiddenFieldButton1Text.Value = "OK";
                HiddenFieldButton1Color.Value = "d7432e";
            }
            else
            {
                HiddenFieldToastStatus.Value = "1";
                HiddenFieldToastMode.Value = "invite";
                HiddenFieldToastSmiley.Value = ":(";
                HiddenFieldToastMessage.Value = "Something went wrong! Please try later!";
                HiddenFieldButton1Text.Value = "OK";
                HiddenFieldButton1Color.Value = "d7432e";
            }
        }
    }
}