using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.AdminPages
{
    public partial class Support : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check permissions
            Classes.Admins a = new Classes.Admins();
            if (!a.permissions(Convert.ToInt32(Session["UserId"]), "Support"))
            {
                Response.Redirect("~/Error/404");
            }
        }
    }
}