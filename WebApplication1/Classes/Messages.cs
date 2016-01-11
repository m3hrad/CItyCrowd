using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Messages
    {
        public class MessageObjectAll
        {
            public MessageObject Object1 { get; private set; }
            public MessageObject Object2 { get; private set; }
            public MessageObject Object3 { get; private set; }
            public MessageObject Object4 { get; private set; }
            public MessageObject Object5 { get; private set; }
            public MessageObject Object6 { get; private set; }
            public MessageObject Object7 { get; private set; }
            public MessageObject Object8 { get; private set; }
            public MessageObject Object9 { get; private set; }
            public MessageObject Object10 { get; private set; }

            public MessageObjectAll(MessageObject object1, MessageObject object2, MessageObject object3, MessageObject object4, MessageObject object5,
                MessageObject object6, MessageObject object7, MessageObject object8, MessageObject object9, MessageObject object10)
            {
                Object1 = object1;
                Object2 = object2;
                Object3 = object3;
                Object4 = object4;
                Object5 = object5;
                Object6 = object6;
                Object7 = object7;
                Object8 = object8;
                Object9 = object9;
                Object10 = object10;
            }

            public MessageObjectAll()
            {

            }
        }
        public class MessageObject
        {
            public Int64 MessageId { get; private set; }
            public bool Sender { get; private set; }
            public string Message { get; private set; }
            public string PassedDate { get; private set; }
            public bool Unread { get; private set; }
            public int UserId { get; private set; }

            public MessageObject(Int64 messageid, bool sender, string message, string passeddate, bool unread,
                int userid)
            {
                MessageId = messageid;
                Sender = sender;
                Message = message;
                PassedDate = passeddate;
                Unread = unread;
                UserId = userid;
            }

            public MessageObject()
            {

            }
        }
        public void allRead(int userId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_messagesAllRead", sqlConn);

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

        public DataTable messageLists(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_messageLists", sqlConn);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            //try
            //{
                sda.Fill(ds);
                dt = ds.Tables[0];
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            //}

            DataTable dt2 = new DataTable();
            DataRow dr2 = null;

            //define the columns
            dt2.Columns.Add(new DataColumn("MessageListId", typeof(string)));
            dt2.Columns.Add(new DataColumn("OtherId", typeof(string)));
            dt2.Columns.Add(new DataColumn("FirstName", typeof(string)));
            dt2.Columns.Add(new DataColumn("LastName", typeof(string)));
            dt2.Columns.Add(new DataColumn("PassedDate", typeof(string)));
            dt2.Columns.Add(new DataColumn("Brief", typeof(string)));
            dt2.Columns.Add(new DataColumn("Unread", typeof(string)));
            dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));
            dt2.Columns.Add(new DataColumn("NewCount", typeof(string)));

            string profilePicUrl;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //create new row
                dr2 = dt2.NewRow();

                //add values to each rows
                dr2["MessageListId"] = dt.Rows[i]["MessageListId"].ToString();
                dr2["OtherId"] = dt.Rows[i]["OtherId"].ToString();
                dr2["FirstName"] = dt.Rows[i]["FirstName"].ToString();
                dr2["LastName"] = dt.Rows[i]["LastName"].ToString();
                dr2["Brief"] = dt.Rows[i]["Brief"].ToString();
                dr2["Unread"] = dt.Rows[i]["Unread"].ToString();
                dr2["NewCount"] = dt.Rows[i]["NewCount"].ToString();
                Classes.Date d = new Classes.Date();
                dr2["PassedDate"] = d.FormatPassedDate(dt.Rows[i]["SubmitDate"].ToString());

                // Show profile's photo
                if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                {
                    profilePicUrl = "~/Files/ProfilesPhotos/" + dt.Rows[i]["OtherId"].ToString() + "-100.jpg";
                }
                else
                {
                    profilePicUrl = "~/Images/nophoto.png";
                }
                dr2["ProfilePicUrl"] = profilePicUrl;

                //add the row to DataTable
                dt2.Rows.Add(dr2);
            }
            return dt2;
        }

        public Tuple<int, DataTable, DataTable, string, string> showMessages(int userId, int otherId, Int64 number)
        {
            string userPhotoUrl = "Images/nophoto.png";
            string otherPhotoUrl = "Images/nophoto.png";
            int status = 0;
            DataTable dtUserName = new DataTable();
            DataTable dtOtherName = new DataTable();
            DataTable dtMessages = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_messagesSenderReceiverInfo", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;
            sda.SelectCommand.Parameters.Add("@OtherId", SqlDbType.Int).Value = otherId;

            //try
            //{
                sda.Fill(ds);
                dtUserName = ds.Tables[0];
                dtOtherName = ds.Tables[1];

                if (dtUserName.Rows.Count == 0 || dtOtherName.Rows.Count == 0)
                {
                    status = -1;
                }
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            //}

            if (Convert.ToBoolean(dtUserName.Rows[0]["HasPhoto"].ToString()))
            {
                userPhotoUrl = "Files/ProfilesPhotos/" + userId.ToString() + "-100.jpg";
            }

            if (Convert.ToBoolean(dtUserName.Rows[0]["HasPhoto"].ToString()))
            {
                otherPhotoUrl = "Files/ProfilesPhotos/" + otherId.ToString() + "-100.jpg";
            }

            return new Tuple<int, DataTable, DataTable, string, string>(status, dtUserName, dtOtherName, userPhotoUrl, otherPhotoUrl);
        }

        public MessageObjectAll getMessages(int userId, int otherId, Int64 messageId)
        {
            MessageObjectAll allObjects = new MessageObjectAll();

            if (messageId == 0)
            {
                messageId = 999999999;
            }

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda1 = new SqlDataAdapter("sp_messages", sqlConn);

            sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda1.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;
            sda1.SelectCommand.Parameters.Add("@OtherId", SqlDbType.Int).Value = otherId;
            sda1.SelectCommand.Parameters.Add("@MessageId", SqlDbType.Int).Value = messageId;

            //try
            //{
                sda1.Fill(ds);
                dt = ds.Tables[0];
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sda1.Dispose();
                sqlConn.Dispose();
            //}

            if (dt.Rows.Count == 0)
            {
                return allObjects;
            }
            else
            {
                MessageObject object1 = new MessageObject();
                MessageObject object2 = new MessageObject();
                MessageObject object3 = new MessageObject();
                MessageObject object4 = new MessageObject();
                MessageObject object5 = new MessageObject();
                MessageObject object6 = new MessageObject();
                MessageObject object7 = new MessageObject();
                MessageObject object8 = new MessageObject();
                MessageObject object9 = new MessageObject();
                MessageObject object10 = new MessageObject();

                // profile pic url
                int userIdString = 0;
                Classes.Date d = new Classes.Date();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["Sender"].ToString()))
                    {
                        userIdString = Convert.ToInt32(dt.Rows[i]["OwnerId"].ToString());
                    }
                    else
                    {
                        userIdString = Convert.ToInt32(dt.Rows[i]["OtherId"].ToString());
                    }

                    MessageObject myMessageObject = new MessageObject(
                        Convert.ToInt32(dt.Rows[i]["MessageId"].ToString()),
                        Convert.ToBoolean(dt.Rows[i]["Sender"].ToString()),
                        dt.Rows[i]["Message"].ToString(),
                        d.FormatPassedDate(dt.Rows[i]["SubmitDate"].ToString()),
                        Convert.ToBoolean(dt.Rows[i]["Unread"].ToString()),
                        userIdString);

                    switch (i)
                    {
                        case 0:
                            {
                                object10 = myMessageObject;
                                break;
                            }
                        case 1:
                            {
                                object9 = myMessageObject;
                                break;
                            }
                        case 2:
                            {
                                object8 = myMessageObject;
                                break;
                            }
                        case 3:
                            {
                                object7 = myMessageObject;
                                break;
                            }
                        case 4:
                            {
                                object6 = myMessageObject;
                                break;
                            }
                        case 5:
                            {
                                object5 = myMessageObject;
                                break;
                            }
                        case 6:
                            {
                                object4 = myMessageObject;
                                break;
                            }
                        case 7:
                            {
                                object3 = myMessageObject;
                                break;
                            }
                        case 8:
                            {
                                object2 = myMessageObject;
                                break;
                            }
                        case 9:
                            {
                                object1 = myMessageObject;
                                break;
                            }
                    }

                    allObjects = new MessageObjectAll(
                        object1,
                        object2,
                        object3,
                        object4,
                        object5,
                        object6,
                        object7,
                        object8,
                        object9,
                        object10);
                }

                return allObjects;
            }
        }

        public int addMessage(int userId, int receiverId, string message)
        {
            int status = 0;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_messagesSendAdd", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SenderId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@ReceiverId", SqlDbType.Int).Value = receiverId;
            sqlCmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message;
            if (message.Length < 100)
            {
                sqlCmd.Parameters.Add("@Brief", SqlDbType.NVarChar).Value = message;
            }
            else
            {
                sqlCmd.Parameters.Add("@Brief", SqlDbType.NVarChar).Value = message.Substring(0, 100);
            }

            //try
            //{
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
                status = 1;
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sqlConn.Dispose();
            //}

            Classes.Signal s = new Classes.Signal();
            s.usernotificationsNumber(receiverId);

            return status;
        }

        public int deleteMessage(int userId, Int64 messageId)
        {
            int status = 0;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_messageDelete", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MessageId", SqlDbType.BigInt).Value = messageId;
            sqlCmd.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;
            
            //try
            //{
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
                status = 1;
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sqlConn.Dispose();
            //}

            return status;
        }

        public int deleteMessageList(int userId, Int64 messageListId)
        {
            int status = 0;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_messageListDelete", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MessageListId", SqlDbType.BigInt).Value = messageListId;
            sqlCmd.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;

            //try
            //{
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
                status = 1;
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sqlConn.Dispose();
            //}

            return status;
        }
    }
}