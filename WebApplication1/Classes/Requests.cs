using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Requests
    {
        public void allRead(int userId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_requestsAllRead", sqlConn);

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

        public Tuple<int, DataTable> requestsList(int userId)
        {
            //status codes: 0 no event 1 requests 2 no request
            int status = 0;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_requestsUserEvents", sqlConn);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }

            if (dt.Rows.Count == 0)
            {
                return new Tuple<int, DataTable>(status, dt);
            }
            else
            {
                DataTable dt2 = new DataTable();
                DataRow dr = null;
                string profilePicUrl = "";

                //define the columns
                dt2.Columns.Add(new DataColumn("EventId", typeof(string)));
                dt2.Columns.Add(new DataColumn("Name", typeof(string)));
                dt2.Columns.Add(new DataColumn("TypeId", typeof(string)));
                dt2.Columns.Add(new DataColumn("RemainedTime", typeof(string)));
                dt2.Columns.Add(new DataColumn("RequestsCount", typeof(string)));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Status"].ToString() != "3" && dt.Rows[i]["RequestsCount"].ToString() != "0")
                    {
                        //create new row
                        dr = dt2.NewRow();

                        //add values to each rows
                        dr["EventId"] = dt.Rows[i]["EventId"].ToString();
                        dr["Name"] = dt.Rows[i]["Name"].ToString();
                        dr["TypeId"] = dt.Rows[i]["TypeId"].ToString();
                        dr["RequestsCount"] = dt.Rows[i]["RequestsCount"].ToString();
                        // time
                        Classes.Date d = new Classes.Date();
                        dr["RemainedTime"] = d.FormatRemainedDate(dt.Rows[i]["Date"].ToString());

                        //add the row to DataTable
                        dt2.Rows.Add(dr);
                    }
                }

                if (dt2.Rows.Count == 0)
                {
                    status = 2;
                    return new Tuple<int, DataTable>(status, dt2);
                }
                else
                {
                    status = 1;
                    return new Tuple<int, DataTable>(status, dt2);
                }
            }

        }

        public DataTable eventRequestsList(int userId, Int64 eventId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_requestsInfo", sqlConn);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }

            if (dt.Rows.Count == 0)
            {
                return dt;
            }
            else
            {
                DataTable dt2 = new DataTable();
                DataRow dr = null;
                string profilePicUrl = "";

                //define the columns
                dt2.Columns.Add(new DataColumn("RequestId", typeof(string)));
                dt2.Columns.Add(new DataColumn("EventId", typeof(string)));
                dt2.Columns.Add(new DataColumn("SenderId", typeof(string)));
                dt2.Columns.Add(new DataColumn("FullName", typeof(string)));
                dt2.Columns.Add(new DataColumn("Message", typeof(string)));
                dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));
                dt2.Columns.Add(new DataColumn("PassedTime", typeof(string)));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    dr = dt2.NewRow();

                    //add values to each rows
                    dr["RequestId"] = dt.Rows[i]["RequestId"].ToString();
                    dr["EventId"] = dt.Rows[i]["EventId"].ToString();
                    dr["SenderId"] = dt.Rows[i]["UserId"].ToString();
                    dr["FullName"] = dt.Rows[i]["FirstName"].ToString() + " " + dt.Rows[i]["LastName"].ToString();
                    dr["Message"] = dt.Rows[i]["Message"].ToString();
                    // time
                    Classes.Date d = new Classes.Date();
                    dr["PassedTime"] = d.FormatPassedDate(dt.Rows[i]["SubmitDate"].ToString());

                    // Show profile's photo
                    if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                    {
                        profilePicUrl = "~/Files/ProfilesPhotos/" + dt.Rows[i]["UserId"].ToString() + "-100.jpg";
                    }
                    else
                    {
                        profilePicUrl = "~/Images/nophoto.png";
                    }
                    dr["ProfilePicUrl"] = profilePicUrl;

                    //add the row to DataTable
                    dt2.Rows.Add(dr);
                }

                return dt2;
            }
            
        }

        public int requestSend(int userId, Int64 eventId, string message)
        {
            int totalStatus = 0;

            Classes.Events ev = new Classes.Events();
            if (ev.isUserOwner(userId, eventId))
            {
                totalStatus = -1;
            }
            else if (ev.isUserParticipant(userId, eventId))
            {
                //cancel participation
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd = new SqlCommand("sp_eventCancelParticipation", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                try
                {
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    sqlConn.Close();
                    sqlCmd.Dispose();
                    sqlConn.Dispose();
                }

                Classes.Events e = new Classes.Events();
                int ownerId = e.getOwnerIdByEventId(eventId);

                Classes.Notifications n = new Classes.Notifications();
                n.notificationAdd(ownerId, 7, Convert.ToInt64(userId));

                totalStatus = 1;
            }
            else
            {
                int status = checkRequest(userId, eventId);

                if (status == 0)
                {
                    //send request
                    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                    SqlCommand sqlCmd = new SqlCommand("sp_eventRequestAdd", sqlConn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
                    sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    sqlCmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message;

                    try
                    {
                        sqlConn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        sqlConn.Close();
                        sqlCmd.Dispose();
                        sqlConn.Dispose();
                    }

                    Classes.Events e1 = new Classes.Events();
                    int personId = e1.getOwnerIdByEventId(eventId);
                    Classes.Signal s = new Classes.Signal();
                    s.usernotificationsNumber(personId);

                    totalStatus = 3;
                }
                else if (status == 1)
                {
                    //cancel request
                    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                    SqlCommand sqlCmd = new SqlCommand("sp_eventRequestDelete", sqlConn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
                    sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                    try
                    {
                        sqlConn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        sqlConn.Close();
                        sqlCmd.Dispose();
                        sqlConn.Dispose();
                    }

                    Classes.Events e1 = new Classes.Events();
                    int personId = e1.getOwnerIdByEventId(eventId);
                    Classes.Signal s = new Classes.Signal();
                    s.usernotificationsNumber(personId);

                    totalStatus = 2;
                }
            }

            return totalStatus;
        }

        public int requestAccept(int userId, Int64 requestId)
        {
            int status = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_requestInfo", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;
            sda.SelectCommand.Parameters.Add("@RequestId", SqlDbType.BigInt).Value = requestId;

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }



            if (dt.Rows.Count != 0)// request exists
            {
                int userId1 = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                Int64 eventId = Convert.ToInt64(dt.Rows[0]["EventId"].ToString());

                SqlConnection sqlConn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd1 = new SqlCommand("sp_requestAccept", sqlConn1);
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;
                sqlCmd1.Parameters.Add("@RequestId", SqlDbType.BigInt).Value = requestId;
                sqlCmd1.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
                sqlCmd1.Parameters.Add("@UserId", SqlDbType.Int).Value = userId1;
                try
                {
                    sqlConn1.Open();
                    sqlCmd1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    sqlConn1.Close();
                    sqlCmd1.Dispose();
                    sqlConn1.Dispose();
                }

                // add notification
                Classes.Notifications n = new Classes.Notifications();
                n.notificationAdd(userId1, 2, eventId);

                Classes.Events ev = new Classes.Events();
                int remained = ev.eventParticipantsRemained(eventId);

                if (remained == 0)
                {
                    n.notificationAdd(userId, 3, eventId);
                }

                status = 1;
            }
            else
            {
                status = -1;
            }

            return status;
        }

        public void requestReject(int userId, Int64 requestId)
        {
            int status = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_requestInfo", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;
            sda.SelectCommand.Parameters.Add("@RequestId", SqlDbType.BigInt).Value = requestId;

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }



            if (dt.Rows.Count != 0)// request exists
            {

                SqlConnection sqlConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd = new SqlCommand("sp_requestReject", sqlConn2);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@RequestId", SqlDbType.BigInt).Value = requestId;
                try
                {
                    sqlConn2.Open();
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    sqlConn2.Close();
                    sqlCmd.Dispose();
                    sqlConn2.Dispose();
                }
            }
        }

        public int checkRequest(int userId, Int64 eventId)
        {
            int status = 0; //0 user has no request 1 user has requested before 2 user is a participant

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_requestCheck", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }


            if (dt.Rows.Count == 0)// request exists
            {
                Classes.Events ev = new Classes.Events();
                bool participantStatus = ev.isUserParticipant(userId, eventId);

                if (participantStatus)// user is a participant
                {
                    status = 2;
                }
            }
            else
            {
                status = 1;
            }

            return status;
        }

        public int removeParticipant(int ownerId, int userId, Int64 eventId)
        {
            int status = 0; //0 user not owner 1 participants cancelled 2 user was not participant

            Classes.Events ev = new Classes.Events();
            if (!ev.isUserOwner(ownerId, eventId))
            {
                status = 0;
            }
            else
            {
                bool participantStatus = ev.isUserParticipant(userId, eventId);
                if(!participantStatus)
                {
                    status = 2;
                }
                else
                {
                    status = 1;
                    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                    SqlCommand sqlCmd = new SqlCommand("sp_eventRemoveParticipant", sqlConn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
                    sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                    try
                    {
                        sqlConn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        sqlConn.Close();
                        sqlCmd.Dispose();
                        sqlConn.Dispose();
                    }

                    Classes.Notifications n = new Classes.Notifications();
                    n.notificationAdd(userId, 8, eventId);
                }
            }

            return status;
        }
    }
}