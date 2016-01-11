using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);

            Classes.SiteSettings ss = new Classes.SiteSettings();
            Tuple<bool, bool, bool, int> result = ss.getAllSettings();
            bool loginAllowed = result.Item1;
            bool registerAllowed = result.Item2;
            bool activitiesAllowed = result.Item3;
            int status = result.Item4;

            Application["LoginAllowed"] = loginAllowed.ToString();
            Application["RegisterAllowed"] = registerAllowed.ToString();
            Application["ActivitiesAllowed"] = activitiesAllowed.ToString();
            Application["Status"] = status.ToString();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        void RegisterRoutes(RouteCollection routes)
        {
            //RouteTable.Routes.Ignore("{folder}/{*pathInfo}", new { folder = "Images" });
            /////////// User Pages

            routes.MapPageRoute(
               "ExplorePage",
               "Explore",
               "~/Explore.aspx"
            );
            routes.MapPageRoute(
               "LogoutPage",
               "Logout",
               "~/Logout.aspx"
            );
            routes.MapPageRoute(
               "RegisterPage",
               "Register",
               "~/Register.aspx"
            );
            routes.MapPageRoute(
               "RegisterinvitePage",
               "Register/{InviteId}",
               "~/Register.aspx"
            );
            routes.MapPageRoute(
               "NotificationsPage",
               "Notifications",
               "~/Notifications.aspx"
            );
            routes.MapPageRoute(
               "SearchPage",
               "Search",
               "~/Search.aspx"
            );
            routes.MapPageRoute(
               "SearchPageTag",
               "Search/Tag/{Keyword}",
               "~/Search.aspx"
            );
            routes.MapPageRoute(
               "CalendarPage",
               "Calendar",
               "~/Calendar.aspx"
            );
            routes.MapPageRoute(
               "IntroductionPage",
               "Introduction",
               "~/Introduction.aspx"
            );
            routes.MapPageRoute(
               "CompletionPage",
               "Completion",
               "~/Completion.aspx"
            );
            routes.MapPageRoute(
               "RemainedPage",
               "Remained",
               "~/Remained.aspx"
            );
            routes.MapPageRoute(
               "InvitePage",
               "Invite",
               "~/Invite.aspx"
            );
            routes.MapPageRoute(
               "VerifyCodePage",
               "Verify/{Mode}/{Code}",
               "~/Verify.aspx"
            );
            routes.MapPageRoute(
               "ReportPage",
               "Report/{Mode}/{ItemId}",
               "~/Report.aspx"
            );
            routes.MapPageRoute(
               "DonePage",
               "Done/{Mode}",
               "~/Done.aspx"
            );
            routes.MapPageRoute(
               "AboutPageModes",
               "About/{Page}",
               "~/About.aspx"
            );
            routes.MapPageRoute(
               "AboutPage",
               "About",
               "~/About.aspx"
            );
            routes.MapPageRoute(
               "HelpPage",
               "Help",
               "~/Help.aspx"
            );
            routes.MapPageRoute(
               "ReviewPage",
               "Review",
               "~/Review.aspx"
            );
            routes.MapPageRoute(
               "NearbyPage",
               "Nearby",
               "~/Nearby.aspx"
            );
            routes.MapPageRoute(
               "FollowingPage",
               "Following",
               "~/Following.aspx"
            );
            routes.MapPageRoute(
               "FeedbackPage",
               "Feedback",
               "~/Feedback.aspx"
            );
            routes.MapPageRoute(
               "FbFollowPage",
               "FbFollow",
               "~/FbFollow.aspx"
            );

            /////////// Requests

            routes.MapPageRoute(
               "RequestsPage",
               "Requests",
               "~/Requests.aspx"
            );
            routes.MapPageRoute(
               "RequestsShowPage",
               "Requests/{EventId}",
               "~/RequestsShow.aspx"
            );
            routes.MapPageRoute(
               "RequestsPageActions",
               "Requests/{EventId}/{RequestId}/{ActionCode}",
               "~/RequestsShow.aspx"
            );

            /////////// Settings

            routes.MapPageRoute(
               "SettingsPage",
               "Settings",
               "~/Settings.aspx"
            );
            routes.MapPageRoute(
               "SettingsTabs",
               "Settings/{Section}",
               "~/Settings.aspx"
            );

            /////////// Messages

            routes.MapPageRoute(
               "MessagesPage",
               "Messages",
               "~/Messages.aspx"
            );
            routes.MapPageRoute(
               "MessagesShowPage",
               "Messages/{ProfileId}",
               "~/MessagesShow.aspx"
            );
            routes.MapPageRoute(
               "NewMessagePage",
               "Messages/New",
               "~/MessagesShow.aspx"
            );

            /////////// Events

            routes.MapPageRoute(
               "Events",
               "Events",
               "~/Events.aspx"
            );
            routes.MapPageRoute(
               "EventsMode",
               "Events/Mode/{Mode}",
               "~/Events.aspx"
            ); 
            routes.MapPageRoute(
               "EventsAdd",
               "Events/Add",
               "~/EventsAdd.aspx"
            );
            routes.MapPageRoute(
               "EventsModify",
               "Events/Modify/{EventId}",
               "~/EventsModify.aspx"
            );
            routes.MapPageRoute(
               "EventsView",
               "Events/{EventId}",
               "~/EventsView.aspx"
            );
            
            /////////// Login
            
            routes.MapPageRoute(
               "LoginPage",
               "Login",
               "~/Login.aspx"
            );
            routes.MapPageRoute(
               "LoginPageWithPath",
               "Login/{Page}/{ItemId}",
               "~/Login.aspx"
            );
            routes.MapPageRoute(
               "LoginRedirect",
               "Login/{Page}",
               "~/Login.aspx"
            );

            /////////// Other pages

            routes.MapPageRoute(
               "Error",
               "Error",
               "~/Error.aspx"
            ); 
            routes.MapPageRoute(
               "ErrorCode",
               "Error/{Code}",
               "~/Error.aspx"
            );
            routes.MapPageRoute(
               "ForgotPasswordPage",
               "ForgotPassword/{Mode}",
               "~/ForgotPassword.aspx"
            );
            routes.MapPageRoute(
               "ForgotPasswordRecover",
               "ForgotPassword/{Mode}/{VCode}",
               "~/ForgotPassword.aspx"
            );

            /////////// Profile Pages

            routes.MapPageRoute(
               "Profile",
               "Profile/{Id}",
               "~/Profile.aspx"
            );
            routes.MapPageRoute(
               "ProfileSingle",
               "Profile",
               "~/Profile.aspx"
            );

            /////////// FB Login

            routes.MapPageRoute(
               "Facebooksync",
               "Facebooksync",
               "~/Facebooksync.aspx"
            );

            /////////// Blog
            routes.MapPageRoute(
               "Blog",
               "Blog",
               "~/Blog.aspx"
            );
            routes.MapPageRoute(
                "BlogEntries",
                "Blog/{EntryId}",
                "~/BlogEntries.aspx"
            );

            /////////// Admin pages
            routes.MapPageRoute(
               "Admin",
               "Admin",
               "~/Admin.aspx"
            );
            routes.MapPageRoute(
                "AdminPages",
                "Admin/{Page}",
                "~/Admin.aspx"
            );
        }
    }
}