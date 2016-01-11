using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class UserInfo
    {
        public Int16 getUserStatus(int UserId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserStatusById", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

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
                return 10; //no user found
            }
            else
            {
                Int16 status = Convert.ToInt16(dt.Rows[0]["Status"].ToString());
                return status;
            }
        }

        public bool checkUsernameExists(string username)
        {
            bool status = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_usernameCheckExists", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;
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
                status = false;
            }

            return status;
        }

        public Int32 getUserIdByEmail(string Email)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserIdByEmail", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email;
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
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
            }
        }

        public string getEmailByUserId(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getEmailByUserId", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
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
                return "";
            }
            else
            {
                return dt.Rows[0]["Email"].ToString();
            }
        }

        public bool isUserEmailVerified(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_isUserEmailVerified", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
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
                return false;
            }
            else
            {
                return Convert.ToBoolean(dt.Rows[0]["EmailVerified"].ToString());
            }
        }

        public int getUserNotificationsCount(int userId)
        {
            int number = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserNotificationsCount", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
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

            if (dt.Rows.Count != 0)
            {
                number = Convert.ToInt32(dt.Rows[0]["NotificationsCount"].ToString());
            }

            number = 5;
            return number;
        }

        public string getUserFullNameByUserId(int UserId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserFullNameByUserId", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
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

            sda.Dispose();
            sqlConn.Dispose();

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
            }
        }

        public string getUsernameByUserId(int UserId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUsernameByUserId", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
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
                return "0";
            }
            else
            {
                return dt.Rows[0]["Username"].ToString();
            }
        }

        public string getUserLocationInfoByUserId(int UserId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserLocationInfoByUserId", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
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
                return "NA";
            }
            else
            {
                return dt.Rows[0]["CountryName"].ToString() + " - " + dt.Rows[0]["CityName"].ToString();
            }
        }

        public int locationIdByUserId(int UserId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserLocationByUserId", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
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
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0]["LocationId"].ToString());
            }
        }

        public DataTable masterPageInfo(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_userInfoMaster", sqlConn);
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
            return dt;
        }

        public DataTable followersList(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_followers", sqlConn);

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

            if (dt.Rows.Count == 0)
            {
                return dt;
            }
            else
            {
                DataTable dt2 = new DataTable();
                DataRow dr2 = null;

                //define the columns
                dt2.Columns.Add(new DataColumn("UserId", typeof(string)));
                dt2.Columns.Add(new DataColumn("Username", typeof(string)));
                dt2.Columns.Add(new DataColumn("FirstName", typeof(string)));
                dt2.Columns.Add(new DataColumn("LastName", typeof(string)));
                dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));

                string profilePicUrl;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    dr2 = dt2.NewRow();

                    //add values to each rows
                    dr2["UserId"] = dt.Rows[i]["FollowerId"].ToString();
                    dr2["Username"] = dt.Rows[i]["Username"].ToString();
                    dr2["Username"] = dt.Rows[i]["Username"].ToString();
                    dr2["FirstName"] = dt.Rows[i]["FirstName"].ToString();
                    dr2["LastName"] = dt.Rows[i]["LastName"].ToString();

                    // Show profile's photo
                    if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                    {
                        profilePicUrl = "~/Files/ProfilesPhotos/" + dt.Rows[i]["FollowerId"].ToString() + "-100.jpg";
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

        public DataTable followingList(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_following", sqlConn);

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

            if (dt.Rows.Count == 0)
            {
                return dt;
            }
            else
            {
                DataTable dt2 = new DataTable();
                DataRow dr2 = null;

                //define the columns
                dt2.Columns.Add(new DataColumn("UserId", typeof(string)));
                dt2.Columns.Add(new DataColumn("Username", typeof(string)));
                dt2.Columns.Add(new DataColumn("FirstName", typeof(string)));
                dt2.Columns.Add(new DataColumn("LastName", typeof(string)));
                dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));

                string profilePicUrl;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    dr2 = dt2.NewRow();

                    //add values to each rows
                    dr2["UserId"] = dt.Rows[i]["UserId"].ToString();
                    dr2["Username"] = dt.Rows[i]["Username"].ToString();
                    dr2["Username"] = dt.Rows[i]["Username"].ToString();
                    dr2["FirstName"] = dt.Rows[i]["FirstName"].ToString();
                    dr2["LastName"] = dt.Rows[i]["LastName"].ToString();

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

            //public Array getMenuInfo(int UserId)
            //{
            //    DataTable dt = new DataTable();
            //    DataSet ds = new DataSet();
            //    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            //    SqlDataAdapter sda = new SqlDataAdapter("sp_getMenuInfo", sqlConn);

            //    try
            //    {
            //        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            //        sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            //        sda.Fill(ds);
            //        dt = ds.Tables[0];

            //        string photoUrl = "Images/NoPhoto.png";
            //        if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString()))
            //        {
            //            photoUrl = "Files/ProfilesPhotos/" + UserId.ToString() + "-100.jpg";
            //        }

            //        string[] values = new string[]{
            //        dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString(),
            //        dt.Rows[0]["Username"].ToString(),
            //        photoUrl};
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //    finally
            //    {
            //        sqlConn.Close();
            //        sda.Dispose();
            //        sqlConn.Dispose();
            //    }

            //    return values[];
            //}
        }

        public bool isUserFollower(int userId, int followerId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_isUserFollower", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sda.SelectCommand.Parameters.Add("@FollowerId", SqlDbType.Int).Value = followerId;
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

            if (dt.Rows.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Tuple<int, DataTable> profileInfo(int userId, string profileId)
        {
            int status = 0;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getProfileInfoByUsername", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = profileId;

            //check if query string is user id or username
            double num;
            if (double.TryParse(profileId, out num))
            {
                sda = new SqlDataAdapter("sp_getProfileInfoByUserId", sqlConn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(profileId);
            }

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

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                status = -1;
            }
            else
            {
                // Profile is redistrected
                if (Convert.ToInt32(dt.Rows[0]["Status"].ToString()) != 1)
                {
                    status = -2;
                }
                else
                {
                    status = 1;
                    // generate the info

                    DataRow dr2 = null;

                    //define the columns
                    dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));
                    dt2.Columns.Add(new DataColumn("isOwner", typeof(string)));
                    dt2.Columns.Add(new DataColumn("isFollower", typeof(string)));
                    dt2.Columns.Add(new DataColumn("FirstName", typeof(string)));
                    dt2.Columns.Add(new DataColumn("LastName", typeof(string)));
                    dt2.Columns.Add(new DataColumn("Username", typeof(string)));
                    dt2.Columns.Add(new DataColumn("FollowersCount", typeof(string)));
                    dt2.Columns.Add(new DataColumn("FollowingCount", typeof(string)));
                    dt2.Columns.Add(new DataColumn("About", typeof(string)));
                    dt2.Columns.Add(new DataColumn("UserId", typeof(string)));
                    dt2.Columns.Add(new DataColumn("ProfileVerified", typeof(string)));
                    dt2.Columns.Add(new DataColumn("RateCount", typeof(string)));
                    dt2.Columns.Add(new DataColumn("RatePercent", typeof(string)));
                    dt2.Columns.Add(new DataColumn("CountryCode", typeof(string)));
                    dt2.Columns.Add(new DataColumn("CountryName", typeof(string)));
                    dt2.Columns.Add(new DataColumn("CityName", typeof(string)));
                    dt2.Columns.Add(new DataColumn("CoverUrl", typeof(string)));


                    //create new row
                    dr2 = dt2.NewRow();

                    //add values to each rows
                    if (userId.ToString() == dt.Rows[0]["UserId"].ToString()) //user is the profile owner
                    {
                        dr2["isOwner"] = "true";
                    }
                    else
                    {
                        dr2["isOwner"] = "false";
                    }

                    // is visitor follower
                    bool isFollower = false;
                    if (userId != 0)
                    {
                        isFollower = isUserFollower(Convert.ToInt32(dt.Rows[0]["UserId"].ToString()), userId);
                    }
                    if (isFollower)
                    {
                        dr2["isFollower"] = "true";
                    }
                    else
                    {
                        dr2["isFollower"] = "false";
                    }

                    // Profile info
                    dr2["FirstName"] = dt.Rows[0]["FirstName"].ToString();
                    dr2["LastName"] = dt.Rows[0]["LastName"].ToString();
                    dr2["Username"] = dt.Rows[0]["Username"].ToString();
                    dr2["FollowersCount"] = dt.Rows[0]["FollowersCount"].ToString();
                    dr2["FollowingCount"] = dt.Rows[0]["FollowingCount"].ToString();
                    dr2["About"] = dt.Rows[0]["About"].ToString();
                    dr2["UserId"] = dt.Rows[0]["UserId"].ToString();
                    dr2["ProfileVerified"] = dt.Rows[0]["ProfileVerified"].ToString();
                    dr2["RateCount"] = dt.Rows[0]["RateCount"].ToString();
                    dr2["CoverUrl"] = "Images/Covers/" + dt.Rows[0]["CoverId"].ToString() + ".jpg";

                    Classes.Locations l = new Classes.Locations();
                    DataTable dtLocation = l.getLocationInfoByCityId(Convert.ToInt32(dt.Rows[0]["LocationId"].ToString()));
                    if (dtLocation.Rows.Count == 0)
                    {
                        
                    }
                    else
                    {
                        dr2["CountryCode"] = dtLocation.Rows[0]["CountryCode"].ToString();
                        dr2["CountryName"] = dtLocation.Rows[0]["CountryName"].ToString();
                        dr2["CityName"] = dtLocation.Rows[0]["CityName"].ToString();
                    }

                    // Rate
                    int rateSufficient = Convert.ToInt32(ConfigurationManager.AppSettings["RateSufficient"].ToString());
                    int rateCount = Convert.ToInt32(dt.Rows[0]["rateCount"].ToString());
                    int rateScore = Convert.ToInt32(dt.Rows[0]["rateScore"].ToString());
                    if (rateCount >= rateSufficient)
                    {
                        int popularity = (20 * rateScore) / rateCount;
                        dr2["RatePercent"] = popularity.ToString();
                    }
                    else
                    {
                        dr2["RatePercent"] = "NA";
                    }

                    // Show profile's photo
                    if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString()))
                    {
                        dr2["ProfilePicUrl"] = "Files/ProfilesPhotos/" + dt.Rows[0]["UserId"].ToString() + "-100.jpg";
                    }
                    else
                    {
                        dr2["ProfilePicUrl"] = "Images/nophoto.png";
                    }

                    //add the row to DataTable
                    dt2.Rows.Add(dr2);
                }
            }

            return new Tuple<int, DataTable>(status, dt2);
        }

        public Int64 getLastEvent(int UserId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserLastEvent", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

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
                return 0;
            }
            else
            {
                return Convert.ToInt64(dt.Rows[0]["EventId"].ToString());
            }
        }

        public Tuple<int, int, int> notificationsNumber(int userId)
        {
            int messagesNumber = 0;
            int notificationsNumber = 0;
            int requestsNumber = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getnotificationsNumberByUserId", sqlConn);
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

            if (dt.Rows.Count != 0)// Profile doesn't exist
            {
                messagesNumber = Convert.ToInt32(dt.Rows[0]["MessagesCount"].ToString());
                notificationsNumber = Convert.ToInt32(dt.Rows[0]["NotificationsCount"].ToString());
                requestsNumber = Convert.ToInt32(dt.Rows[0]["RequestsCount"].ToString());
            }

            return new Tuple<int, int, int>(messagesNumber, notificationsNumber, requestsNumber);
        }

        public Tuple<string, string, string, string> fbCompletion(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_fbCompletion", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.VarChar).Value = userId;

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

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                return new Tuple<string, string, string, string>("", "", "", "");
            }

            return new Tuple<string, string, string, string>(dt.Rows[0]["FirstName"].ToString(), dt.Rows[0]["LastName"].ToString(), dt.Rows[0]["HasPhoto"].ToString(), dt.Rows[0]["Gender"].ToString());
        }

        public Tuple<int, string, string> getFirstNamePhotoUrlByUserId(int userId)
        {
            int status = 0;
            string firstName = "";
            string photoUrl = "Images/nophoto.png";
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getFirstNamePhotoUrlByUserId", sqlConn);
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

            if (dt.Rows.Count != 0)// Profile doesn't exist
            {
                status = 1;
                firstName = dt.Rows[0]["FirstName"].ToString();
                    if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString()))
                    {
                        photoUrl = "Files/ProfilesPhotos/" + userId.ToString() + "-100.jpg";
                    }
            }

            return new Tuple<int, string, string>(status, firstName, photoUrl);
        }
    }
}