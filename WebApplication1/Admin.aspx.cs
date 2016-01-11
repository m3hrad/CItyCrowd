using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check login
            int UserId = 0;

            if (Request.Cookies["VC"] != null)
            {
                string VC = Request.Cookies["VC"].Values["VC"];
                Classes.LoginSession ls = new Classes.LoginSession();
                UserId = ls.getUserId(VC);
                if (UserId == 0) //if user not logged in redirect to login
                {
                    Response.Redirect("~/Login/Events/Add");
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

            //check premissions
            Classes.Admins a = new Classes.Admins();
            if (!a.permissions(UserId, "Home"))
            {
                Response.Redirect("~/Error/404");
            }


            try
            {
                if (Page.RouteData.Values["Page"].ToString().Length > 0)
                {
                    string pathToUserControl = "~/AdminPages/" + Page.RouteData.Values["Page"].ToString() + ".ascx";
                    var ucSrc = LoadControl(pathToUserControl);
                    PanelPage.Controls.Add(ucSrc);

                    switch (Page.RouteData.Values["Page"].ToString().ToLower())
                    {
                        case "admins":
                            {
                                Page.Title = "Admin - Admins";
                                break;
                            }
                        case "blog":
                            {
                                Page.Title = "Admin - Blog";
                                break;
                            }
                        case "content":
                            {
                                Page.Title = "Admin - Content";
                                break;
                            }
                        case "events":
                            {
                                Page.Title = "Admin - Events";
                                break;
                            }
                        case "locations":
                            {
                                Page.Title = "Admin - Locations";
                                break;
                            }
                        case "newsletter":
                            {
                                Page.Title = "Admin - Newsletters";
                                break;
                            }
                        case "settings":
                            {
                                Page.Title = "Admin - Settings";
                                break;
                            }
                        case "statistics":
                            {
                                Page.Title = "Admin - Statistics";
                                break;
                            }
                        case "support":
                            {
                                Page.Title = "Admin - Support";
                                break;
                            }
                        case "users":
                            {
                                Page.Title = "Admin - Users";
                                break;
                            }
                        case "feedback":
                            {
                                Page.Title = "Admin - Feedback";
                                break;
                            }
                    }
                }
            }
            catch
            {
            }
        }
    }
}