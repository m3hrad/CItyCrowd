using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class UserActions
    {
        public int followAction(int userId, int profileId)
        {
            // is visitor follower
            int status = 0;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_isUserFollower", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = profileId;
            sda.SelectCommand.Parameters.Add("@FollowerId", SqlDbType.Int).Value = userId;

            //try
            //{
                sda.Fill(ds);
                dt = ds.Tables[0];
                SqlCommand sqlCmd = new SqlCommand("sp_followUser", sqlConn);

                if (dt.Rows.Count == 0)
                {
                    sqlCmd = new SqlCommand("sp_followUser", sqlConn);
                    status = 1;
                    // add notification
                    Classes.Notifications n = new Classes.Notifications();
                    n.notificationAdd(profileId, 4, Convert.ToInt64(userId));
                }
                else
                {
                    sqlCmd = new SqlCommand("sp_unfollowUser", sqlConn);
                    status = 2;
                }

                //try
                //{
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = profileId;
                    sqlCmd.Parameters.Add("@FollowerId", SqlDbType.Int).Value = userId;
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
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sda.Dispose();
            //}

            return status;
        }
    }
}