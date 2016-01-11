using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNet.SignalR;

namespace WebApplication1.Classes
{
    public class Notifications
    {
        public void allRead(int userId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_notificationsAllRead", sqlConn);

            //try
            //{
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            //}
        }

        public DataTable notifications(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_notifications", sqlConn2);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sda.Fill(ds);
            dt = ds.Tables[0];
            DataTable dt2 = new DataTable();
            DataRow dr2 = null;

            //define the columns
            dt2.Columns.Add(new DataColumn("NotificationId", typeof(string)));
            dt2.Columns.Add(new DataColumn("NotificationType", typeof(string)));
            dt2.Columns.Add(new DataColumn("Text", typeof(string)));
            dt2.Columns.Add(new DataColumn("Unread", typeof(string)));
            dt2.Columns.Add(new DataColumn("PassedTime", typeof(string)));
            dt2.Columns.Add(new DataColumn("Link", typeof(string)));

            Classes.Date d = new Classes.Date();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //create new row
                dr2 = dt2.NewRow();

                //add values to each rows
                dr2["NotificationId"] = dt.Rows[i]["NotificationId"].ToString();
                dr2["NotificationType"] = dt.Rows[i]["NotificationType"].ToString();
                dr2["Unread"] = dt.Rows[i]["Unread"].ToString();
                dr2["PassedTime"] = d.FormatPassedDate(dt.Rows[i]["SubmitDate"].ToString());
                //link
                dr2["Link"] = notificationLink(Convert.ToInt32(dt.Rows[i]["NotificationType"].ToString()), Convert.ToInt64(dt.Rows[i]["ItemId"].ToString()));
                
                //text
                dr2["Text"] = notificationText(Convert.ToInt32(dt.Rows[i]["NotificationType"].ToString()), Convert.ToInt64(dt.Rows[i]["ItemId"].ToString()));

                //add the row to DataTable
                dt2.Rows.Add(dr2);
            }

            return dt2;
        }
        
        public void notificationAdd(int UserId, int NotificationType, Int64 itemId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_notificationAdd", sqlConn);
            Int64 notificationId = 0;
            //try
            //{
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                sqlCmd.Parameters.Add("@NotificationType", SqlDbType.Int).Value = NotificationType;
                sqlCmd.Parameters.Add("@ItemId", SqlDbType.BigInt).Value = itemId;

                sqlConn.Open();
                notificationId = Convert.ToInt64(sqlCmd.ExecuteScalar());
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            //}

            Classes.Signal s = new Classes.Signal();
            s.usernotificationsNumber(UserId);

            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            // text, notificationType, notificationid,notificationLink
            context.Clients.Group(UserId.ToString()).receiveNotification(notificationText(NotificationType, itemId),
                NotificationType,
                notificationId,
                notificationLink(NotificationType, itemId));
            
            //context.Clients.Group(UserId.ToString()).setDone("hi!");


            //context.Clients.All.setDone("hi!");
        }

        public string notificationText(int NotificationType, Int64 itemId)
        {
            switch (NotificationType)
            {
                case 1:
                    {
                        return "Congratulations! You have successfully registered!";
                        break;
                    }
                case 2:
                    {
                        Classes.Events e = new Classes.Events();
                        string eventName = e.getEventNameByEventId(itemId);
                        return "Guess what?! You have been accepted to participate in this event: " + eventName;
                        break;
                    }
                case 3:
                    {
                        Classes.Events e = new Classes.Events();
                        string eventName = e.getEventNameByEventId(itemId);
                        return "Your event, " + eventName + " is full now!";
                        break;
                    }
                case 4:
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        string userFullName = ui.getUserFullNameByUserId(Convert.ToInt32(itemId));
                        return userFullName + " started following you.";
                        break;
                    }
                case 5:
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        string userFullName = ui.getUserFullNameByUserId(Convert.ToInt32(itemId));
                        return userFullName + " registered in the app with your invitation.";
                        break;
                    }
                case 6:
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        string userFullName = ui.getUserFullNameByUserId(Convert.ToInt32(itemId));
                        return userFullName + " just reviewed you!";
                        break;
                    }
                case 7:
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        string userFullName = ui.getUserFullNameByUserId(Convert.ToInt32(itemId));
                        return userFullName + " canceled participation in your event.";
                        break;
                    }
                case 8:
                    {
                        Classes.Events e = new Classes.Events();
                        string eventName = e.getEventNameByEventId(itemId);
                        return  "You just got removed from " + eventName + ".";
                        break;
                    }
                case 9:
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        string userFullName = ui.getUserFullNameByUserId(Convert.ToInt32(itemId));
                        return userFullName + " removed an event that you were a participant in it.";
                        break;
                    }
                case 10:
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        string userFullName = ui.getUserFullNameByUserId(Convert.ToInt32(itemId));
                        return userFullName + " is waiting for your review.";
                        break;
                    }
                case 11:
                    {
                        return "You have successfully verified your email address.";
                        break;
                    }
                case 12:
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        string userFullName = ui.getUserFullNameByUserId(Convert.ToInt32(itemId));
                        return userFullName + " from your Facebook friends just became a CityCrowder.";
                        break;
                    }
                case 13:
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        string userFullName = ui.getUserFullNameByUserId(Convert.ToInt32(itemId));
                        return userFullName + " became a CityCrowder with your invitation.";
                        break;
                    }
                case 14:
                    {
                        Classes.Locations l = new Classes.Locations();
                        string locationName = l.getLocationInfoById(Convert.ToInt32(itemId));
                        return "Good news! The location you requested to be added is now available! Locaion: " + locationName;
                        break;
                    }
                default:
                    {
                        return "";
                        break;
                    }
            }
        }

        public string notificationLink(int NotificationType, Int64 itemId)
        {
            switch (NotificationType)
                {
                    case 1:
                        {
                            return "Profile/" + itemId.ToString();
                            break;
                        }
                    case 2:
                        {
                            return "Events/" + itemId.ToString();
                            break;
                        }
                    case 3:
                        {
                            return "Events/" + itemId.ToString();
                            break;
                        }
                    case 4:
                        {
                            return "Profile/" + itemId.ToString();
                            break;
                        }
                    case 5:
                        {
                            return "Profile/" + itemId.ToString();
                            break;
                        }
                    case 6:
                        {
                            return "Reviews/" + itemId.ToString(); ////////////////////////////////////////
                            break;
                        }
                    case 7:
                        {
                            return "Profile/" + itemId.ToString();
                            break;
                        }
                    case 8:
                        {
                            return "Events/" + itemId.ToString();
                            break;
                        }
                    case 9:
                        {
                            return "Profile/" + itemId.ToString();
                            break;
                        }
                    case 12:
                        {
                            return "Profile/" + itemId.ToString();
                            break;
                        }
                default:
                        {
                            return "";
                            break;
                        }
                }
        }

        public void notificationDelete(int UserId, Int64 NotificationId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_notificationDelete", sqlConn);

            //try
            //{
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@NotificationId", SqlDbType.BigInt).Value = NotificationId;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            //}
        }
    }
}