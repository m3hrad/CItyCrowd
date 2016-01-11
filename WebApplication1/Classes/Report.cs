using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Report
    {
        public int reportUser(int userId, int itemId, int reportType, string message)
        {
            int status = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_reportUser", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = itemId;
            sqlCmd.Parameters.Add("@ReportType", SqlDbType.SmallInt).Value = reportType;
            sqlCmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message;

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

        public int reportEvent(int userId, Int64 itemId, int reportType, string message)
        {
            int status = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_reportEvent", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@ItemId", SqlDbType.BigInt).Value = itemId;
            sqlCmd.Parameters.Add("@ReportType", SqlDbType.SmallInt).Value = reportType;
            sqlCmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message;

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

        public int reportError(int userId, string page, string message)
        {
            int status = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_reportError", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = page + ":" + message;

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
    }
}