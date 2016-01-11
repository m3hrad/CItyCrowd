using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                switch (Page.RouteData.Values["Page"].ToString().ToLower())
                {
                    case "about":
                        HiddenFieldMode.Value = "about";
                        Page.Title = "About";
                        break;
                    case "contact":
                        HiddenFieldMode.Value = "contact";
                        Page.Title = "Contact";
                        break;
                    case "terms":
                        HiddenFieldMode.Value = "terms";
                        Page.Title = "Terms of Use";
                        break;
                    case "privacy":
                        HiddenFieldMode.Value = "privacy";
                        Page.Title = "Privacy Policy";
                        break;
                    case "safety":
                        HiddenFieldMode.Value = "safety";
                        Page.Title = "Safety";
                        break;
                    default:
                        HiddenFieldMode.Value = "about";
                        Page.Title = "About";
                        break;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }
    }
}