using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Nearby
    {
        public class EventObjectAll
        {
            public EventObject Object1 { get; private set; }
            public EventObject Object2 { get; private set; }
            public EventObject Object3 { get; private set; }
            public EventObject Object4 { get; private set; }
            public EventObject Object5 { get; private set; }

            public EventObjectAll(EventObject object1, EventObject object2, EventObject object3, EventObject object4, EventObject object5)
            {
                Object1 = object1;
                Object2 = object2;
                Object3 = object3;
                Object4 = object4;
                Object5 = object5;
            }

            public EventObjectAll()
            {

            }
        }
        public class EventObject
        {
            public Int64 EventId { get; private set; }
            public string EventName { get; private set; }
            public string RemainedTime { get; private set; }
            public Int16 TypeId { get; private set; }
            public Int16 CoverId { get; private set; }
            public int UserId { get; private set; }
            public string Location { get; private set; }
            public int UserRate { get; private set; }
            public string OwnerName { get; private set; }
            public int Participants { get; private set; }
            public int ParticipantsAccepted { get; private set; }
            public string ProfilePicUrl { get; private set; }

            public EventObject(Int64 eventid, string eventname, string remainedtime, Int16 typeid, Int16 coverid,
                int userid, string location,
                int userrate, string ownername, int participants, int participantsaccepted, string profilepicurl)
            {
                EventId = eventid;
                EventName = eventname;
                RemainedTime = remainedtime;
                TypeId = typeid;
                CoverId = coverid;
                UserId = userid;
                Location = location;
                UserRate = userrate;
                OwnerName = ownername;
                Participants = participants;
                ParticipantsAccepted = participantsaccepted;
                ProfilePicUrl = profilepicurl;
            }

            public EventObject()
            {

            }
        }

        public EventObject getEvent(string type, int userId, Int64 eventId)
        {
            Int64 EventId = 0;
            string EventName = "";
            string RemainedTime = "";
            Int16 TypeId = 0;
            Int16 CoverId = 0;
            int UserId = 0;
            string Location = "";
            int UserRate = 0;
            string OwnerName = "";
            int Participants = 0;
            int ParticipantsAccepted = 0;
            string ProfilePicUrl = "";

            EventObject myEventObject = new EventObject(
                EventId, EventName, RemainedTime, TypeId, CoverId,
                UserId,
                Location, UserRate, OwnerName, Participants,
                ParticipantsAccepted, ProfilePicUrl);

            return myEventObject;
    }

        public EventObjectAll eventsCity(int userId, Int64 eventId, int mode)
        {
            EventObjectAll allObjects = new EventObjectAll();

            string spName = "";
            int days = 0;

            if (eventId == 0)
            {
                eventId = 99999999;
            }

            if (mode == 1)
            {
                spName = "sp_nearbyCityAll";
            }
            else if (mode == 2)//day
            {
                spName = "sp_nearbyCityDate";
                days = 1;
            }
            else if (mode == 3)//week
            {
                spName = "sp_nearbyCityDate";
                days = 7;
            }
            else if (mode == 4)//month
            {
                spName = "sp_nearbyCityDate";
                days = 31;
            }

            Classes.UserInfo ui = new Classes.UserInfo();
            int locationId = ui.locationIdByUserId(userId);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter(spName, sqlConn);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationId;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sda.SelectCommand.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;
            sda.SelectCommand.Parameters.Add("@Days", SqlDbType.SmallInt).Value = days;

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

            if (dt.Rows.Count == 0)
            {
                return allObjects;
            }
            else
            {
                Classes.Date d = new Classes.Date();
                Classes.Nearby n = new Classes.Nearby();

                EventObject object1 = new EventObject();
                EventObject object2 = new EventObject();
                EventObject object3 = new EventObject();
                EventObject object4 = new EventObject();
                EventObject object5 = new EventObject();

                string Location = "";
                Classes.Locations l = new Classes.Locations();
                DataTable dtLocation = l.getLocationInfoByCityId(Convert.ToInt32(locationId));
                if (dtLocation.Rows.Count == 0)
                {
                    Location = "Not Available!";
                }
                else
                {
                    Location = dtLocation.Rows[0]["CountryName"].ToString() + " - " + dtLocation.Rows[0]["CityName"].ToString();
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Show profile's photo
                    string profilePicUrl;
                    if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                    {
                        profilePicUrl = "Files/ProfilesPhotos/" + dt.Rows[i]["OwnerId"].ToString() + "-100.jpg";
                    }
                    else
                    {
                        profilePicUrl = "Images/nophoto.png";
                    }

                    // Rate
                    int rate = 0;
                    int rateSufficient = Convert.ToInt32(ConfigurationManager.AppSettings["RateSufficient"].ToString());
                    int rateCount = Convert.ToInt32(dt.Rows[i]["rateCount"].ToString());
                    int rateScore = Convert.ToInt32(dt.Rows[i]["rateScore"].ToString());
                    if (rateCount >= rateSufficient)
                    {
                        rate = (20 * rateScore) / rateCount;

                    }

                    EventObject myEventObject = new EventObject(
                        Convert.ToInt64(dt.Rows[i]["EventId"].ToString()),
                        dt.Rows[i]["Name"].ToString(),
                        d.FormatRemainedDate(dt.Rows[i]["Date"].ToString()),
                        Convert.ToInt16(dt.Rows[i]["TypeId"].ToString()),
                        Convert.ToInt16(dt.Rows[i]["CoverId"].ToString()),
                        Convert.ToInt32(dt.Rows[i]["OwnerId"].ToString()),
                        Location,
                        rate,
                        dt.Rows[i]["OwnerName"].ToString(),
                        Convert.ToInt32(dt.Rows[i]["Participants"].ToString() + 1),
                        Convert.ToInt32(dt.Rows[i]["ParticipantsAccepted"].ToString() + 1),
                        profilePicUrl);

                    switch (i)
                    {
                        case 0:
                            {
                                object1 = myEventObject;
                                break;
                            }
                        case 1:
                            {
                                object2 = myEventObject;
                                break;
                            }
                        case 2:
                            {
                                object3 = myEventObject;
                                break;
                            }
                        case 3:
                            {
                                object4 = myEventObject;
                                break;
                            }
                        case 4:
                            {
                                object5 = myEventObject;
                                break;
                            }
                    }
                }
                allObjects = new EventObjectAll(
                        object1,
                        object2,
                        object3,
                        object4,
                        object5);

                return allObjects;
            }
        }

        public EventObjectAll eventsFollowing(int userId, Int64 eventId)
        {
            EventObjectAll allObjects = new EventObjectAll();

            if (eventId == 0)
            {
                eventId = 99999999;
            }

            Classes.UserInfo ui = new Classes.UserInfo();
            int locationId = ui.locationIdByUserId(userId);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_followingEvents", sqlConn);

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

            if (dt.Rows.Count == 0)
            {
                return allObjects;
            }
            else
            {
                Classes.Date d = new Classes.Date();
                Classes.Nearby n = new Classes.Nearby();

                EventObject object1 = new EventObject();
                EventObject object2 = new EventObject();
                EventObject object3 = new EventObject();
                EventObject object4 = new EventObject();
                EventObject object5 = new EventObject();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Location = "";
                    Classes.Locations l = new Classes.Locations();
                    DataTable dtLocation = l.getLocationInfoByCityId(Convert.ToInt32(dt.Rows[i]["LocationId"].ToString()));
                    if (dtLocation.Rows.Count == 0)
                    {
                        Location = "Not Available!";
                    }
                    else
                    {
                        Location = dtLocation.Rows[0]["CountryName"].ToString() + " - " + dtLocation.Rows[0]["CityName"].ToString();
                    }

                    // Show profile's photo
                    string profilePicUrl;
                    if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                    {
                        profilePicUrl = "Files/ProfilesPhotos/" + dt.Rows[i]["OwnerId"].ToString() + "-100.jpg";
                    }
                    else
                    {
                        profilePicUrl = "Images/nophoto.png";
                    }

                    // Rate
                    int rate = 0;
                    int rateSufficient = Convert.ToInt32(ConfigurationManager.AppSettings["RateSufficient"].ToString());
                    int rateCount = Convert.ToInt32(dt.Rows[i]["rateCount"].ToString());
                    int rateScore = Convert.ToInt32(dt.Rows[i]["rateScore"].ToString());
                    if (rateCount >= rateSufficient)
                    {
                        rate = (20 * rateScore) / rateCount;

                    }

                    EventObject myEventObject = new EventObject(
                        Convert.ToInt64(dt.Rows[i]["EventId"].ToString()),
                        dt.Rows[i]["Name"].ToString(),
                        d.FormatRemainedDate(dt.Rows[i]["Date"].ToString()),
                        Convert.ToInt16(dt.Rows[i]["TypeId"].ToString()),
                        Convert.ToInt16(dt.Rows[i]["CoverId"].ToString()),
                        Convert.ToInt32(dt.Rows[i]["OwnerId"].ToString()),
                        Location,
                        rate,
                        dt.Rows[i]["OwnerName"].ToString(),
                        Convert.ToInt32(dt.Rows[i]["Participants"].ToString() + 1),
                        Convert.ToInt32(dt.Rows[i]["ParticipantsAccepted"].ToString() + 1),
                        profilePicUrl);

                    switch (i)
                    {
                        case 0:
                            {
                                object1 = myEventObject;
                                break;
                            }
                        case 1:
                            {
                                object2 = myEventObject;
                                break;
                            }
                        case 2:
                            {
                                object3 = myEventObject;
                                break;
                            }
                        case 3:
                            {
                                object4 = myEventObject;
                                break;
                            }
                        case 4:
                            {
                                object5 = myEventObject;
                                break;
                            }
                    }

                    allObjects = new EventObjectAll(
                        object1,
                        object2,
                        object3,
                        object4,
                        object5);
                }

                return allObjects;
            }
        }

        public Boolean checkEvent(int userId, Int64 eventId)
        {
            Boolean status = false;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_nearbyCheckEvent", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = eventId;

            //try
            //{
                sqlConn.Open();
                status = Convert.ToBoolean(sqlCmd.ExecuteScalar());
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
    }
}