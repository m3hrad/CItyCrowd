using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Application["Status"]) == 2) Response.Redirect("~/Maintenance.html");

            int UserId = 0;
            if (Session["UserId"] != null)
            {
                Response.Redirect("~/Done/Welcome");
            }
            if (Request.Cookies["VC"] != null)
            {
                string VC = Request.Cookies["VC"].Values["VC"];
                Classes.LoginSession ls = new Classes.LoginSession();
                UserId = ls.getUserId(VC);
                if (UserId != 0)
                {
                    Response.Redirect("~/Done/Welcome");
                }
            }

            if (!IsPostBack)
            {
                switch (Page.RouteData.Values["Mode"].ToString())
                {
                    case "Request":
                        {
                            PanelRequest.Visible = true;
                            break;
                        }
                    case "Recover":
                        {
                            Classes.ForgotPassword fp = new Classes.ForgotPassword();
                            string email = fp.recoverEmailGet(Page.RouteData.Values["VCode"].ToString());

                            if (email == "")
                            {
                                Response.Redirect("~/Error/PRequestNotFound");
                            }
                            else
                            {
                                LabelRecoverEmail.Text = email;
                                PanelRecover.Visible = true;
                            }
                            break;
                        }
                }
            }
        }

        protected void ButtonRequest_Click(object sender, EventArgs e)
        {
            Classes.ForgotPassword fp = new Classes.ForgotPassword();
            int status = fp.request(TextBoxEmail.Text);

            if (status == 1)
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "We sent you an email";
                HiddenFieldStatus.Value = "1";
            }
            else if (status == 2)
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "Email address was not found.";
                HiddenFieldStatus.Value = "0";
            }
        }

        protected void ButtonRecover_Click(object sender, EventArgs e)
        {
            if (TextBoxPassword1.Text.Length < 4)
            {
                LabelMessage.Text = "New password must be atleast 4 characters";
                LabelMessage.Visible = true;
            }
            else
            {
                if (TextBoxPassword1.Text != TextBoxPassword2.Text)
                {
                    LabelMessage.Text = "New password and re-type new password must be the same";
                    LabelMessage.Visible = true;
                }
                else
                {
                    Classes.ForgotPassword fp = new Classes.ForgotPassword();
                    int status = fp.recover(Page.RouteData.Values["VCode"].ToString(), LabelRecoverEmail.Text, TextBoxPassword1.Text);

                    if (status == 1)
                    {
                        Response.Redirect("~/Login");
                    }
                }
            }
        }
    }
}