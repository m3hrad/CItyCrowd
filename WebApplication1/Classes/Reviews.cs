using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Reviews
    {
        public class ReviewRequest
        {
            public Int64 ReviewRequestId { get; private set; }
            public int UserId { get; private set; }
            public Int64 EventId { get; private set; }
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public Boolean HasPhoto { get; private set; }
            public Boolean EventOwnerHasPhoto { get; private set; }
            public int EventOwnerId { get; private set; }
            public string EventName { get; private set; }
            public int EventTypeId { get; private set; }
            public int EventCoverId { get; private set; }
            public Boolean Exist { get; private set; }

            public ReviewRequest(Int64 reviewrequestid, int userid, Int64 eventid, string firstname,
                string lastname, Boolean hasphoto, Boolean eventownerhasphoto, int eventownerid, string eventname, int eventtypeid, int eventcoverid, Boolean exist)
            {
                ReviewRequestId = reviewrequestid;
                UserId = userid;
                EventId = eventid;
                FirstName = firstname;
                LastName = lastname;
                HasPhoto = hasphoto;
                EventOwnerHasPhoto = eventownerhasphoto;
                EventOwnerId = eventownerid;
                EventName = eventname;
                EventTypeId = eventtypeid;
                EventCoverId = eventcoverid;
                Exist = exist;
            }
        }

        //public DataTable reviewInfo(int userId)
        //{
        //    DataTable dt = new DataTable();
        //    DataSet ds = new DataSet();
        //    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);

        //    SqlDataAdapter sda = new SqlDataAdapter("sp_reviewRead", sqlConn);
        //    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    sda.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;

        //    try
        //    {
        //        sda.Fill(ds);
        //        dt = ds.Tables[0];
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

        //    return dt;
        //}

        public ReviewRequest reviewInfo(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);

            SqlDataAdapter sda = new SqlDataAdapter("sp_reviewRead", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;

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

            if (dt.Rows.Count != 0)
            {
                ReviewRequest myReviewRequest = new ReviewRequest(
                    Convert.ToInt64(dt.Rows[0]["ReviewRequestId"].ToString()),
                    Convert.ToInt32(dt.Rows[0]["UserId"].ToString()),
                    Convert.ToInt64(dt.Rows[0]["EventId"].ToString()),
                    dt.Rows[0]["FirstName"].ToString(),
                    dt.Rows[0]["LastName"].ToString(),
                    Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString()),
                    Convert.ToBoolean(dt.Rows[0]["EventOwnerHasPhoto"].ToString()),
                    Convert.ToInt32(dt.Rows[0]["EventOwnerId"].ToString()),
                    dt.Rows[0]["EventName"].ToString(),
                    Convert.ToInt32(dt.Rows[0]["TypeId"].ToString()),
                    Convert.ToInt32(dt.Rows[0]["CoverId"].ToString()),
                    true);

                return myReviewRequest;
            }
            else
            {
                ReviewRequest myReviewRequest = new ReviewRequest(
                    0,
                    0,
                    0,
                    "",
                    "",
                    false,
                    false,
                    0,
                    "",
                    0,
                    0,
                    false);

                return myReviewRequest;
            }

            
        }
        public void reviewCancel(int userId, Int64 reviewRequestId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_reviewRequestDelete", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@ReviewRequestId", SqlDbType.BigInt).Value = reviewRequestId;
            sqlCmd.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;

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
        }

        public int reviewAdd(int userId, Int64 reviewRequestId, string comment, int rate)
        {
            int status = 0;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);

            SqlDataAdapter sda = new SqlDataAdapter("sp_reviewRequestRead", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@ReviewRequestId", SqlDbType.BigInt).Value = reviewRequestId;
            sda.SelectCommand.Parameters.Add("@OwnerId", SqlDbType.Int).Value = userId;

            //try
            //{
                sda.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0) //doesn't exist
                {
                    status = -1; //not found
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

            if (status != -1)
            {
                SqlConnection sqlConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd = new SqlCommand("sp_reviewAdd", sqlConn2);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@ReviewRequestId", SqlDbType.BigInt).Value = reviewRequestId;
                sqlCmd.Parameters.Add("@OwnerId", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                sqlCmd.Parameters.Add("@EventId", SqlDbType.BigInt).Value = Convert.ToInt64(dt.Rows[0]["EventId"].ToString());
                sqlCmd.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = comment;
                if (rate > 5)
                {
                    rate = 5;
                }
                sqlCmd.Parameters.Add("@Rate", SqlDbType.TinyInt).Value = rate;

                //try
                //{
                    sqlConn2.Open();
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

            return status;
        }

        public Tuple<int[], DataTable> reviews(int userId)
        {
            int one = 0;
            int two = 0;
            int three = 0;
            int four = 0;
            int five = 0;

            DataTable dtReviews = new DataTable();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_reviews", sqlConn);

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

            if (dt.Rows.Count > 0)
            {
                DataRow drReviews = null;

                //define the columns
                dtReviews.Columns.Add(new DataColumn("FirstName", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("LastName", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("Username", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("UserId", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("Rate", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("Comment", typeof(string)));
                dtReviews.Columns.Add(new DataColumn("SubmitDate", typeof(string)));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    drReviews = dtReviews.NewRow();

                    //add values to each rows
                    drReviews["FirstName"] = dt.Rows[i]["FirstName"].ToString();
                    drReviews["LastName"] = dt.Rows[i]["LastName"].ToString();
                    drReviews["Username"] = dt.Rows[i]["Username"].ToString();
                    drReviews["UserId"] = dt.Rows[i]["UserId"].ToString();
                    drReviews["Rate"] = dt.Rows[i]["Rate"].ToString();
                    drReviews["Comment"] = dt.Rows[i]["Comment"].ToString();
                    drReviews["SubmitDate"] = dt.Rows[i]["SubmitDate"].ToString();

                    string profilePicUrl;

                    // Show profile's photo
                    if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                    {
                        profilePicUrl = "~/Files/ProfilesPhotos/" + dt.Rows[i]["UserId"].ToString() + "-100.jpg";
                    }
                    else
                    {
                        profilePicUrl = "~/Images/nophoto.png";
                    }
                    drReviews["ProfilePicUrl"] = profilePicUrl;

                    //add the row to DataTable
                    dtReviews.Rows.Add(drReviews);

                    int rate = Convert.ToInt16(dt.Rows[i]["Rate"].ToString());
                    switch (rate)
                    {
                        case 1:
                            {
                                one = one + 1;
                                break;
                            }
                        case 2:
                            {
                                two = two + 1;
                                break;
                            }
                        case 3:
                            {
                                three = three + 1;
                                break;
                            }
                        case 4:
                            {
                                four = four + 1;
                                break;
                            }
                        case 5:
                            {
                                five = five + 1;
                                break;
                            }
                    }
                }

                //convert DataTable to DataView
                DataView dv = dtReviews.DefaultView;
                //save our newly ordered results back into our datatable
                dtReviews = dv.ToTable();
            }

            int[] ratesCount = new int[] { one, two, three, four, five };

            return new Tuple<int[], DataTable>(ratesCount, dtReviews);
        }
    }
}