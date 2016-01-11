using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.SignalR;

namespace WebApplication1
{
    public partial class Done : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mode = "";

            try
            {
                mode = Page.RouteData.Values["Mode"].ToString().ToLower();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

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
                        Response.Redirect("~/Login/Done/" + mode);
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                    }
                }
                else
                {
                    Response.Redirect("~/Login/Done/" + mode);
                }
            }



            switch (mode)
            {
                case "welcome":
                    Classes.Done done = new Classes.Done();
                    Classes.Done.DoneObject result = done.getDone("welcome", 0, 0);
                    var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                    context.Clients.All.setDone("hi!");
            break;
            }
        }
    }
}