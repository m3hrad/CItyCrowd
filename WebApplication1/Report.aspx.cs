using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Report : System.Web.UI.Page
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
                    Response.Redirect("~/Login");
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


            if (!IsPostBack)
            {

                string mode = "";
                Int64 itemId = 0;
                bool modeCheck = false;

                try
                {
                    mode = Page.RouteData.Values["Mode"].ToString().ToLower();
                    itemId = Convert.ToInt64(Page.RouteData.Values["ItemId"].ToString());
                }
                catch (Exception ex)
                {

                }
                finally
                {

                }

                if (mode == "event" || mode == "user")
                {
                    modeCheck = true;
                }

                if (!modeCheck || itemId == 0)
                {
                    Response.Redirect("~/Error/PageNotFound");
                }

                if (mode == "event")
                {
                    DropDownListReportType.Items.Add(new ListItem("Select a reason", "0"));
                    DropDownListReportType.Items.Add(new ListItem("Spam", "50"));
                    DropDownListReportType.Items.Add(new ListItem("Inappropriate", "51"));
                    DropDownListReportType.Items.Add(new ListItem("Hateful or racism", "52"));
                    DropDownListReportType.Items.Add(new ListItem("Abusive", "53"));
                    DropDownListReportType.Items.Add(new ListItem("Other", "59"));
                }
                else if (mode == "user")
                {
                    DropDownListReportType.Items.Add(new ListItem("Select a reason", "0"));
                    DropDownListReportType.Items.Add(new ListItem("Fake account", "10"));
                    DropDownListReportType.Items.Add(new ListItem("Inappropriate", "11"));
                    DropDownListReportType.Items.Add(new ListItem("Hateful or racism", "12"));
                    DropDownListReportType.Items.Add(new ListItem("Abusive", "13"));
                    DropDownListReportType.Items.Add(new ListItem("Other", "19"));
                }
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            string mode = "";
            Int64 itemId = 0;
            bool modeCheck = false;

            try
            {
                mode = Page.RouteData.Values["Mode"].ToString().ToLower();
                itemId = Convert.ToInt64(Page.RouteData.Values["ItemId"].ToString());
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            if (mode == "event" || mode == "user" || mode == "error")
            {
                modeCheck = true;
            }

            if (mode == "event" || mode == "user")
            {
                if (itemId == 0)
                {
                    modeCheck = false;
                }
            }

            if (!modeCheck)
            {
                Response.Redirect("~/Error/PageNotFound");
            }

            int reportType = Convert.ToInt32(DropDownListReportType.SelectedValue);

            if (reportType == 0)
            {
                // say select why
            }
            else
            {
                string message = TextBoxMessage.Text;
                int status = 0;
                int userId = Convert.ToInt32(Session["UserId"]);

                if (mode == "event")
                {
                    Classes.Report r = new Classes.Report();
                    status = r.reportEvent(userId, itemId, reportType, message);
                }
                else if (mode == "user")
                {
                    int itemId2 = Convert.ToInt32(itemId);
                    Classes.Report r = new Classes.Report();
                    status = r.reportUser(userId, itemId2, reportType, message);
                }
                else if (mode == "error")
                {
                    HiddenFieldErrorMode.Value = "1";
                    string page = TextBoxPage.Text;
                    Classes.Report r = new Classes.Report();
                    status = r.reportError(userId, page, message);
                }

                if (status == 1)
                {
                    Response.Redirect("~/Done/ReportSent");
                }
                else if (status == 0)
                {
                    //notsuccess
                }
            }
        }
    }
}