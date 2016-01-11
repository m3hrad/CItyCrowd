using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Search
    {

        public DataTable searchUsername(string keyword)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_searchByUsername", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@Keyword", SqlDbType.VarChar).Value = keyword;

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
                dt2.Columns.Add(new DataColumn("Username", typeof(string)));
                dt2.Columns.Add(new DataColumn("UserId", typeof(string)));
                dt2.Columns.Add(new DataColumn("ProfilePicUrl", typeof(string)));

                string profilePicUrl;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    dr2 = dt2.NewRow();

                    //add values to each rows
                    dr2["Username"] = dt.Rows[i]["Username"].ToString();
                    dr2["UserId"] = dt.Rows[i]["UserId"].ToString();
                    if (Convert.ToBoolean(dt.Rows[i]["HasPhoto"].ToString()))
                    {
                        profilePicUrl = "Files/ProfilesPhotos/" + dt.Rows[i]["UserId"].ToString() + "-100.jpg";
                    }
                    else
                    {
                        profilePicUrl = "Images/nophoto.png";
                    }
                    dr2["ProfilePicUrl"] = profilePicUrl;

                    //add the row to DataTable
                    dt2.Rows.Add(dr2);
                }

                return dt2;
            }
        }

        public DataTable searchHashtag(string hashtag, int locationId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_searchByHashtag", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@Hashtag", SqlDbType.NVarChar).Value = hashtag;
            sda.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationId;

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
                dt2.Columns.Add(new DataColumn("EventId", typeof(string)));
                dt2.Columns.Add(new DataColumn("Name", typeof(string)));
                dt2.Columns.Add(new DataColumn("RemainedDate", typeof(string)));
                dt2.Columns.Add(new DataColumn("ParticipantsRemained", typeof(string)));
                dt2.Columns.Add(new DataColumn("TypeId", typeof(string)));

                DateTime date;
                TimeSpan span;
                string remainedDate = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    dr2 = dt2.NewRow();

                    //add values to each rows
                    dr2["EventId"] = dt.Rows[i]["EventId"].ToString();
                    dr2["Name"] = dt.Rows[i]["Name"].ToString();
                    dr2["ParticipantsRemained"] = dt.Rows[i]["ParticipantsRemained"].ToString();
                    dr2["TypeId"] = dt.Rows[i]["TypeId"].ToString();
                    // remained time
                    date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                    span = date.Subtract(DateTime.Now);

                    if (span.TotalMinutes < 60)
                    {
                        remainedDate = "in less than an hour";
                    }

                    if (span.TotalMinutes > 60 && span.TotalHours < 24)
                    {
                        remainedDate = "in " + Convert.ToInt16(span.TotalHours).ToString() + " hours";
                    }

                    if (span.TotalHours > 24)
                    {
                        remainedDate = "in " + Convert.ToInt16(span.TotalDays).ToString() + " days";
                    }

                    dr2["RemainedDate"] = remainedDate;

                    //add the row to DataTable
                    dt2.Rows.Add(dr2);
                }

                return dt2;
            }
        }

        public DataTable searchType(int typeId, int locationId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_searchByType", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@TypeId", SqlDbType.TinyInt).Value = typeId;
            sda.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationId;

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
                dt2.Columns.Add(new DataColumn("EventId", typeof(string)));
                dt2.Columns.Add(new DataColumn("Name", typeof(string)));
                dt2.Columns.Add(new DataColumn("RemainedDate", typeof(string)));
                dt2.Columns.Add(new DataColumn("ParticipantsRemained", typeof(string)));
                dt2.Columns.Add(new DataColumn("TypeId", typeof(string)));

                DateTime date;
                TimeSpan span;
                string remainedDate = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //create new row
                    dr2 = dt2.NewRow();

                    //add values to each rows
                    dr2["EventId"] = dt.Rows[i]["EventId"].ToString();
                    dr2["Name"] = dt.Rows[i]["Name"].ToString();
                    dr2["ParticipantsRemained"] = dt.Rows[i]["ParticipantsRemained"].ToString();
                    dr2["TypeId"] = dt.Rows[i]["TypeId"].ToString();
                    // remained time
                    date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                    span = date.Subtract(DateTime.Now);

                    if (span.TotalMinutes < 60)
                    {
                        remainedDate = "in less than an hour";
                    }

                    if (span.TotalMinutes > 60 && span.TotalHours < 24)
                    {
                        remainedDate = "in " + Convert.ToInt16(span.TotalHours).ToString() + " hours";
                    }

                    if (span.TotalHours > 24)
                    {
                        remainedDate = "in " + Convert.ToInt16(span.TotalDays).ToString() + " days";
                    }

                    dr2["RemainedDate"] = remainedDate;

                    //add the row to DataTable
                    dt2.Rows.Add(dr2);
                }

                return dt2;
            }
        }
    }
}