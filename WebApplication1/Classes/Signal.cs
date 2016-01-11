using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace WebApplication1.Classes
{
    public class Signal : Hub
    {
        public void usernotificationsNumber(int userId)
        {
            Classes.UserInfo ui = new Classes.UserInfo();
            Tuple<int, int, int> result = ui.notificationsNumber(userId);

            int messagesNumber = result.Item1;
            int notificationsNumber = result.Item2;
            int requestsNumber = result.Item3;

            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.Group(userId.ToString()).updateNotificationNumbers(messagesNumber.ToString(), notificationsNumber.ToString(), requestsNumber.ToString());
            // text, notificationType, notificationid,notificationLink, unread(boool)
            //context.Clients.Group(userId.ToString()).receiveNotification("text", 10, 10, "profile/4", true);
        }
    }
}