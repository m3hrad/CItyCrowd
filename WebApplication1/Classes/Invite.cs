using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Invite
    {
        public int inviteSend(int userId, string email)
        {
            int status = 0;

            Classes.UserInfo ui = new Classes.UserInfo();
            string userFullName = ui.getUserFullNameByUserId(userId);

            string siteLink = ConfigurationManager.AppSettings["WebsiteLink"].ToString();
            string link = siteLink + "Register/" + userId.ToString();
            Classes.Mail m = new Classes.Mail();
            int status2 = m.sendMail("invite", email,
                "Hi, " + userFullName + " invited you to join CityCrowd.<br/><a href='"
                + link + "'>CLICK HERE</a> to join your friend and other CityCrowders.<br/><br/>CityCowd Team", userFullName);

            status = 1;

            return status;
        }
    }
}