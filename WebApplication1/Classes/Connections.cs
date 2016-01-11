using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Connections
    {
        public void writeConnection(int userId, string connectionId)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_connectionWrite", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@ConnectionId", SqlDbType.VarChar).Value = connectionId;

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

        public DataTable readConnection(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);

            SqlDataAdapter sda = new SqlDataAdapter("sp_connectionRead", sqlConn);
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

            return dt;
        }

    }
}