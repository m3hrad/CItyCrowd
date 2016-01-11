using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
namespace WebApplication1
{
    public class ChatHub : Hub<IClient>
    {
        string vc;
        int UserId;
        string conid;

        //public DoneObject(Boolean donestatus, string smiley, string message, Int16 number,
        //string item1text, string item1url, string item1image, string item1color,
        //string item2text, string item2url, string item2image, string item2color,
        //string item3text, string item3url, string item3image, string item3color,
        //string item4text, string item4url, string item4image, string item4color)



        /*Int64 MessageId 
        bool Sender 
        string Message 
        string PassedDate 
        bool Unread 
        int UserId*/

        public Boolean usernameValidator(string username) {
            Classes.UserInfo ui = new Classes.UserInfo();
            bool usernameExists = ui.checkUsernameExists(username);
            return usernameExists;
        }
        public int verifyEmail(int userId) {
            Classes.UserProfileSet ups = new Classes.UserProfileSet();
            int status = ups.verifyEmailSend(userId);
            return status;
        }

            
        public Classes.Messages.MessageObjectAll getTenMessages(int userId, int otherId, Int64 messageId)
        {
            Classes.Messages m = new Classes.Messages();
            //int userId, int otherId, Int64 messageId
            
            Classes.Messages.MessageObjectAll result = m.getMessages(userId, otherId, messageId);
            return result;
        }

        public bool login(string userId, string accessToken) {
            return false;
        }


        public bool sendBoardMessage(long eventId, Int32 userId, string message) {
            Classes.Events ev = new Classes.Events();
            Int16 status2 = ev.eventBoardMessagesAdd(eventId, userId, message);
            return true;
        }

        public string getCities(string countryCode) {
            Classes.Locations l = new Classes.Locations();
            string result = l.citiesListValues(countryCode);
            return result;
        }
        public int sendRequest(int userId, Int64 eventId)
        {
            Classes.Requests request = new Classes.Requests();
            int result = request.requestSend(userId, eventId, "");
            return result;
        }

        public Classes.Nearby.EventObjectAll getFollowingEvents(int userId, Int64 eventId)
        {
            Classes.Nearby n = new Classes.Nearby();
            WebApplication1.Classes.Nearby.EventObjectAll objectAll = n.eventsFollowing(userId, eventId);
            return objectAll;
        }
        //mode 1 all 2 day 3 week 4 month
        public Classes.Nearby.EventObjectAll getNearbyEvents(int userId, Int64 eventId, int mode)
        {
            Classes.Nearby n = new Classes.Nearby();
            Classes.Nearby.EventObjectAll objectAll = n.eventsCity(userId, eventId, mode);
            return objectAll;
        }
        public void requestResponse(int userId, Int64 requestId, bool response)
        {
            Classes.Requests r = new Classes.Requests();
            var id = Convert.ToInt16(userId);
            if (response)
            {
                r.requestAccept(id, requestId);
            }
            else
            {
                r.requestReject(id, requestId);
            }

        }
        public Classes.Done.DoneObject done(string type, int userId, int typeId)
        {
            Classes.Done done = new Classes.Done();
            //string type, int userId, Int64 eventId)
            //types: reviewsEmpty
            Classes.Done.DoneObject result = done.getDone(type, userId, typeId);
            return result;
        }

        //reviewCancel(int userId, Int64 reviewRequestId)
        //public int reviewAdd(int userId, Int64 reviewRequestId, string comment, int rate(5))
        public bool reviewSend(bool reviewed, int userId, Int64 reviewRequestId, string comment, int rate)
        {
            Classes.Reviews reviews = new Classes.Reviews();
            if (reviewed)
            {
                reviews.reviewAdd(userId, reviewRequestId, comment, rate);
            }
            else
            {
                reviews.reviewCancel(userId, reviewRequestId);
            }
            return true;
        }

        public Classes.Reviews.ReviewRequest getReviewInfo(int userId)
        {
            Classes.Reviews reviews = new Classes.Reviews();
            Classes.Reviews.ReviewRequest result = reviews.reviewInfo(userId);
            return result;
        }

        public bool deleteNotification(int userId, int notificationId)
        {
            Classes.Notifications n = new Classes.Notifications();
            n.notificationDelete(userId, notificationId);
            return true;
        }

        public void Hop(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
        public void gimmeTheInfo()
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.printInfo("user ID: and connection id: " + UserId.ToString() + " , " + conid);
            //Clients.All.setDone("hello");
        }


        public bool sendMessage(int userId, int targetId, string message)
        {
            //add send message function and return true if it was successfull
            Classes.Messages m = new Classes.Messages();
            int status = m.addMessage(userId, targetId, message);
            Clients.Group(targetId.ToString()).receiveMessage(userId.ToString(), message);
            Clients.Group(targetId.ToString()).setDone("this is done message");
            return false;
        }
        //userId: who presses the button, profileId: target
        public int follow(int userId, int profileId)
        {
            Classes.UserActions ua = new Classes.UserActions();
            int followStatus = ua.followAction(userId, profileId);
            /*if 1:
                //user followed
                2:
                //user unfollowed
                0:
                //error
            }*/
            return followStatus;

        }

        public async Task<string> setId(string vc)
        {
            this.vc = vc;
            conid = Context.ConnectionId;
            Classes.LoginSession ls = new Classes.LoginSession();
            await Task.Delay(1000);
            UserId = ls.getUserId(vc);
            await Groups.Add(Context.ConnectionId, UserId.ToString());
            //Clients.Group(UserId.ToString()).printInfo("user ID: and connection id: " + UserId.ToString() + " , " + conid);
            //joinGroup(UserId.ToString());
            //Task task = new Task(new Action(JoinRoom));
            //gimmeTheInfo();
            return "Job complete!";
        }

        /*public async Task JoinRoom(string roomName)
        {
            await Groups.Add(Context.ConnectionId, roomName);
            Clients.Group(roomName).printInfo("user ID: and connection id: " + UserId.ToString() + " , " + conid);
        }
        */
        //group name is users id
        /*public void joinGroup(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
            conid = Context.ConnectionId;
            gimmeTheInfo();
            //Clients.Group(roomName).addChatMessage(Context.User.Identity.Name + " joined.");
            //Clients.Group(groupName).checkMehrad("you are the damn Mehrad");
            //Clients.All.printInfo("you are the damn Mehrad");
            
        }
        */
        public void checkGroup()
        {
            Clients.Group("9").checkMehrad("you are the damn Mehrad");
        }
    }
    public interface IClient
    {
        void setDone(string hi);
        void receiveMessage(string sender, string message);
        void broadcastMessage(string name, string message);
        void printInfo(string hi);
        void checkMehrad(string text);
    }
}