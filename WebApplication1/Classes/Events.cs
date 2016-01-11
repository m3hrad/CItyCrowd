using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace WebApplication1.Classes
{
    public class Events
    {
        public DataTable eventslist(int userId, string mode)
        {
            string spName = "";

            switch (mode)
            {
                case "created":
                    spName = "sp_eventsCreated";
                    break;
                case "accepted":
                    spName = "sp_eventsAccepted";
                    break;
                case "requested":
                    spName = "sp_eventsRequested";
                    break;
                case "bookmarked":
                    spName = "sp_eventsBookmarked";
                    break;
            }

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter(spName, sqlConn);

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
                sda.Dispose();
                sqlConn.Dispose();
            //}

            if (dt.Rows.Count == 0)
            {
                return dt;
            }
            else
            {
                DataTable dtEvents = new DataTable();
                DataRow drEvents = null;

                //define the columns
                dtEvents.Columns.Add(new DataColumn("EventId", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("Name", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("RemainedTime", typeof(string)));
                dtEvents.Columns.Add(new DataColumn("TypeId", typeof(string)));

                Classes.Date d = new Classes.Date();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    drEvents = dtEvents.NewRow();

                    //add values to each rows
                    drEvents["EventId"] = dt.Rows[i]["EventId"].ToString();
                    drEvents["Name"] = dt.Rows[i]["Name"].ToString();
                    drEvents["RemainedTime"] = d.FormatRemainedDate(dt.Rows[i]["Date"].ToString());
                    drEvents["TypeId"] = dt.Rows[i]["TypeId"].ToString();

                    //add the row to DataTable
                    dtEvents.Rows.Add(drEvents);
                }

                return dtEvents;
            }
        }

        public int eventAdd(int userId, string name, string date, int participants, int locationId, string address, Int16 typeId, Int16 coverId, string descriptions)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_eventAdd", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // user id
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            // name +max 50
            sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            // date +add time
            sqlCmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = date;
            // participants +limit
            sqlCmd.Parameters.Add("@Participants", SqlDbType.Int).Value = participants;
            // location +near own location or trip
            sqlCmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationId;
            // location +near own location or trip
            sqlCmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = address;
            // type id +limit
            sqlCmd.Parameters.Add("@TypeId", SqlDbType.SmallInt).Value = typeId;
            // type id +limit
            sqlCmd.Parameters.Add("@CoverId", SqlDbType.SmallInt).Value = coverId;
            // descriptions +limit to max
            sqlCmd.Parameters.Add("@Descriptions", SqlDbType.NVarChar).Value = descriptions;

            //try
            //{
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            //}
            //catch
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sqlConn.Dispose();
                sqlCmd.Dispose();
            //}

            string[] hashtags = Regex.Matches(descriptions, @"\#\w*").Cast<Match>().Select(m => m.Value).ToArray();

            if (hashtags.Length > 0)
            {
                Classes.UserInfo ui = new Classes.UserInfo();
                Int64 lastEventId = ui.getLastEvent(userId);

                DataTable dtTags = new DataTable();
                DataRow drTags = null;

                //define the columns
                dtTags.Columns.Add(new DataColumn("EventId", typeof(Int64)));
                dtTags.Columns.Add(new DataColumn("LocationId", typeof(Int32)));
                dtTags.Columns.Add(new DataColumn("Hashtag", typeof(string)));
                dtTags.Columns.Add(new DataColumn("ExpireDate", typeof(string)));

                for (int i = 0; i < hashtags.Length; i++)
                {

                    //create new row
                    drTags = dtTags.NewRow();

                    //add values to each rows
                    drTags["EventId"] = lastEventId;
                    drTags["LocationId"] = locationId;
                    drTags["Hashtag"] = hashtags[i].Replace("#", "");
                    drTags["ExpireDate"] = date;

                    //add the row to DataTable
                    dtTags.Rows.Add(drTags);
                }

                SqlConnection sqlConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd2 = new SqlCommand("sp_eventAddHashtagsAdd", sqlConn2);
                sqlCmd2.CommandType = CommandType.StoredProcedure;
                sqlCmd2.Parameters.Add("@HashtagsTable", dtTags);

                //try
                //{
                    sqlConn2.Open();
                    sqlCmd2.ExecuteNonQuery();
                //}
                //catch
                //{

                //}
                //finally
                //{
                    sqlConn2.Close();
                    sqlConn2.Dispose();
                    sqlCmd2.Dispose();
                //}
            }

            return 1;
        }

        public DataTable eventBoardMessages(Int64 eventId, int ownerId)
        {
            DataTable dt = new DataTable();
            DataTable dtCountries = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventBoardMessages", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

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

            if (dt.Rows.Count == 0)// no messages
            {
                return dt;
            }
            else
            {

                DataTable dt2 = new DataTable();
                DataRow dr2 = null;

                //define the columns
                dt2.Columns.Add(new DataColumn("MessageId", typeof(string)));
                dt2.Columns.Add(new DataColumn("IsOwner", typeof(string)));
                dt2.Columns.Add(new DataColumn("SenderName", typeof(string)));
                dt2.Columns.Add(new DataColumn("SenderId", typeof(string)));
                dt2.Columns.Add(new DataColumn("Message", typeof(string)));
                dt2.Columns.Add(new DataColumn("PassedDate", typeof(string)));
                dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));

                string profilePicUrl;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    dr2 = dt2.NewRow();

                    //add values to each rows
                    dr2["MessageId"] = dt.Rows[i]["MessageId"].ToString();
                    dr2["SenderName"] = dt.Rows[i]["FirstName"].ToString() + " " + dt.Rows[i]["LastName"].ToString();
                    dr2["SenderId"] = dt.Rows[i]["UserId"].ToString();
                    dr2["Message"] = dt.Rows[i]["Message"].ToString();
                    Classes.Date d = new Classes.Date();
                    dr2["PassedDate"] = d.FormatPassedDate(dt.Rows[i]["SubmitDate"].ToString());

                    if (ownerId == Convert.ToInt32(dt.Rows[i]["UserId"].ToString()))
                    {
                        dr2["IsOwner"] = "true";
                    }
                    else
                    {
                        dr2["IsOwner"] = "false";
                    }

                    // Show profile's photo
                    if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                    {
                        profilePicUrl = "~/Files/ProfilesPhotos/" + dt.Rows[i]["UserId"].ToString() + "-100.jpg";
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
        }

        public Int16 eventBoardMessagesAdd(Int64 eventId, int userId, string message)
        {
            Int16 status = 0;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_eventBoardMessagesAdd", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
            sqlCmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message;

            //try
            //{
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

            return status;
        }

        public DataTable eventParticipants(Int64 eventId)
        {
            DataTable dtParticipants = new DataTable();
            DataSet dsParticipants = new DataSet();
            SqlConnection sqlConnParticipants = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sdaParticipants = new SqlDataAdapter("sp_eventParticipants", sqlConnParticipants);

            sdaParticipants.SelectCommand.CommandType = CommandType.StoredProcedure;
            sdaParticipants.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

            //try
            //{
                sdaParticipants.Fill(dsParticipants);
                dtParticipants = dsParticipants.Tables[0];
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConnParticipants.Close();
                sdaParticipants.Dispose();
                sqlConnParticipants.Dispose();
            //}

            DataTable dt2 = new DataTable();
            DataRow dr2 = null;

            //define the columns
            dt2.Columns.Add(new DataColumn("UserId", typeof(string)));
            dt2.Columns.Add(new DataColumn("FirstName", typeof(string)));
            dt2.Columns.Add(new DataColumn("LastName", typeof(string)));
            dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));

            string profilePicUrl;

            for (int i = 0; i < dtParticipants.Rows.Count; i++)
            {
                //create new row
                dr2 = dt2.NewRow();

                //add values to each rows
                dr2["UserId"] = dtParticipants.Rows[i]["UserId"].ToString();
                dr2["FirstName"] = dtParticipants.Rows[i]["FirstName"].ToString();
                dr2["LastName"] = dtParticipants.Rows[i]["LastName"].ToString();

                // Show profile's photo
                if (Convert.ToBoolean(dtParticipants.Rows[i]["HasPhoto"].ToString()))
                {
                    profilePicUrl = "~/Files/ProfilesPhotos/" + dtParticipants.Rows[i]["UserId"].ToString() + "-100.jpg";
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

        public int eventBookmark(int userId, Int64 eventId)
        {
            int status = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_bookmarkEventCheckExists", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
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
                sda.Dispose();
            //}

            if (dt.Rows.Count == 0)
            {
                SqlCommand sqlCmd = new SqlCommand("sp_bookmarkEventAdd", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                //try
                //{
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                //}
                //catch (Exception ex)
                //{

                //}
                //finally
                //{
                    sqlCmd.Dispose();
                //}

                status = 1;
            }
            else
            {
                SqlCommand sqlCmd = new SqlCommand("sp_bookmarkEventDelete", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                //try
                //{
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                //}
                //catch (Exception ex)
                //{

                //}
                //finally
                //{
                    sqlCmd.Dispose();
                    sqlConn.Dispose();
                //}

                status = 2;
            }

            return status;
        }

        public bool checkBookmark(int userId, Int64 eventId)
        {
            bool status = false;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_bookmarkEventCheckExists", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
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
                sda.Dispose();
            //}

            if (dt.Rows.Count != 0)
            {
                status = true;
            }

            return status;
        }

        public int eventDelete(Int64 eventId, int userId)
        {
            int status = 0;

            DataTable dtParticipants = eventParticipants(eventId);
            for (int i = 0; i < dtParticipants.Rows.Count; i++)
            {
                int participantId = Convert.ToInt32(dtParticipants.Rows[i]["UserId"].ToString());
                if(userId == participantId)
                {
                    continue;
                }
                Classes.Notifications n = new Classes.Notifications();
                n.notificationAdd(participantId, 9, userId);
            }

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_eventDelete", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            //try
            //{
            sqlConn.Open();
            sqlCmd.ExecuteNonQuery();
            //}
            //catch
            //{

            //}
            //finally
            //{
            sqlConn.Close();
            sqlConn.Dispose();
            sqlCmd.Dispose();
            //}
            status = 1;

            return status;
        }

        public Tuple<int, string> eventModify(Int64 eventId, string name, DateTime date, string participants, int locationId, string address, int typeId, int coverId, string descriptions)
        {
            int status = 1;
            string message = "";

            if (name.Length == 0) // check name
            {
                status = -1;
                message = "Enter name<br/>";
            }
            //if (TextBoxParticipants.Text.Length == 0) // check participants +++only number
            //{
            //    status = -1;
            //    message += "Enter participants number<br/>";
            //}
            // check duration
            double num;
            string candidate = participants;
            if (!double.TryParse(candidate, out num) && participants.Length != 0)
            {
                status = -1;
                message += "participants number is not valid, enter number<br/>";
            }

            if (status != -1) // there is something wrong with the values that user entered
            {// user entered valid values
                // add to the database
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd = new SqlCommand("sp_eventModifySet", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                // event id
                sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = Convert.ToInt32(eventId);
                // name +max 50
                sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                // date +add time
                sqlCmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = date;
                // participants +limit
                sqlCmd.Parameters.Add("@Participants", SqlDbType.Int).Value = participants;
                // location +near own location or trip
                sqlCmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationId;
                // location +near own location or trip
                sqlCmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = address;
                // mood id +limit
                sqlCmd.Parameters.Add("@TypeId", SqlDbType.TinyInt).Value = typeId;
                // descriptions +limit to max
                sqlCmd.Parameters.Add("@Descriptions", SqlDbType.NVarChar).Value = descriptions;
                // repeat type +limit
                sqlCmd.Parameters.Add("@CoverId", SqlDbType.SmallInt).Value = coverId; /////////////////////////

                //try
                //{
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                //}
                //catch
                //{

                //}
                //finally
                //{
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlCmd.Dispose();
                //}

                    status = 1;
            }

            return new Tuple<int, string>(status, message);
        }

        public DataTable eventModifyInfo(Int64 eventId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventModifyGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

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

            return dt;
        }

        public DataTable eventInfo(Int64 eventId, int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventInfoGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

            int OwnerId = 0;

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

            return dt;
        }

        public string getEventNameByEventId(Int64 eventId)
        {
            string eventName = "";

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventNameByEventId", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

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

            if (dt.Rows.Count != 0) //doesn't exist
            {
                eventName = dt.Rows[0]["Name"].ToString();
            }

            return eventName;
        }

        public bool allowBoard(int userId, Int64 eventId)
        {
            bool status = false;

            if (isUserOwner(userId, eventId))
            {
                status = true;
            }
            else if (isUserParticipant(userId, eventId))
            {
                status = true;
            }

            return status;
        }

        public bool isUserOwner(int userId, Int64 eventId)
        {
            bool status = false;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_isUserOwner", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

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

            if (dt.Rows.Count != 0) //doesn't exist
            {
                status = true;
            }

            return status;
        }

        public bool isUserParticipant(int userId, Int64 eventId)
        {
            bool status = false;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_isUserParticipant", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

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

            if (dt.Rows.Count != 0) //doesn't exist
            {
                status = true;
            }

            return status;
        }

        public int eventParticipantsRemained(Int64 eventId)
        {
            int number = -1;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventParticipantsRemained", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

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

            if (dt.Rows.Count != 0) //doesn't exist
            {
                number = Convert.ToInt32(dt.Rows[0]["ParticipantsRemained"].ToString());
            }

            return number;
        }

        public int getOwnerIdByEventId(Int64 eventId)
        {
            int ownerId = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_eventOwnerIdByEventId", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

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

            if (dt.Rows.Count != 0) //doesn't exist
            {
                ownerId = Convert.ToInt32(dt.Rows[0]["OwnerId"].ToString());
            }

            return ownerId;
        }
    }
}