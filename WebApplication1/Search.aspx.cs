using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Search : System.Web.UI.Page
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
                        Response.Redirect("~/Login/Search");
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                    }
                }
                else
                {
                    Response.Redirect("~/Login/Search");
                }
            }


            if (!IsPostBack)
            {
                Classes.UserInfo ui = new Classes.UserInfo();
                LabelLocation.Text = ui.getUserLocationInfoByUserId(UserId);

                string keyword = "";

                try
                {
                    keyword = Page.RouteData.Values["Keyword"].ToString();
                }
                catch
                {

                }
                finally
                {
                   
                }

                if (keyword.Length > 0)
                {
                    int locationId = ui.locationIdByUserId(Convert.ToInt32(Session["UserId"]));

                    Classes.Search s = new Classes.Search();
                    DataTable dt = s.searchHashtag(keyword, locationId);
                    TextBoxTag.Text = keyword;
                    HiddenFieldSearchType.Value = "2";

                    if (dt.Rows.Count == 0)
                    {
                        PanelResult.Visible = false;
                        HiddenFieldSearchStatus.Value = "0";
                    }
                    else
                    {
                        RepeaterResult.DataSource = dt;
                        RepeaterResult.DataBind();

                        PanelUsername.Visible = false;
                        PanelResult.Visible = true;
                        HiddenFieldSearchStatus.Value = "1";
                    }
                }
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            switch (HiddenFieldSearchType.Value)
            {
                case "1": //username
                    {
                        Classes.Search s = new Classes.Search();
                        DataTable dt = s.searchUsername(TextBoxUsername.Text);

                        if (dt.Rows.Count == 0)
                        {
                            PanelUsername.Visible = false;
                            PanelResult.Visible = false;
                            HiddenFieldSearchStatus.Value = "0";
                        }
                        else
                        {
                            RepeaterUsername.DataSource = dt;
                            RepeaterUsername.DataBind();

                            PanelUsername.Visible = true;
                            PanelResult.Visible = false;
                            HiddenFieldSearchStatus.Value = "1";
                        }
                        break;
                    }
                case "2": //hashtag
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        int locationId = ui.locationIdByUserId(Convert.ToInt32(Session["UserId"]));

                        Classes.Search s = new Classes.Search();
                        DataTable dt = s.searchHashtag(TextBoxTag.Text, locationId);

                        if (dt.Rows.Count == 0)
                        {
                            PanelResult.Visible = false;
                            HiddenFieldSearchStatus.Value = "0";
                        }
                        else
                        {
                            RepeaterResult.DataSource = dt;
                            RepeaterResult.DataBind();

                            PanelUsername.Visible = false;
                            PanelResult.Visible = true;
                            HiddenFieldSearchStatus.Value = "1";
                        }
                        break;
                    }
                case "3": //type
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        int locationId = ui.locationIdByUserId(Convert.ToInt32(Session["UserId"]));

                        Classes.Search s = new Classes.Search();
                        DataTable dt = s.searchType(Convert.ToInt32(HiddenFieldTypeId.Value), locationId);

                        if (dt.Rows.Count == 0)
                        {
                            PanelResult.Visible = false;
                            HiddenFieldSearchStatus.Value = "0";
                        }
                        else
                        {
                            RepeaterResult.DataSource = dt;
                            RepeaterResult.DataBind();

                            PanelUsername.Visible = false;
                            PanelResult.Visible = true;
                            HiddenFieldSearchStatus.Value = "1";
                        }
                        break;
                    }
            }
        }
    }
}