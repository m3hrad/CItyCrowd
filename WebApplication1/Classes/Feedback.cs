using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Feedback
    {
        public int feedbackAdd(int userId, int rate, string message)
        {
            int status = 0;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_feedbackAdd", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@Rate", SqlDbType.TinyInt).Value = rate;
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
    }
}