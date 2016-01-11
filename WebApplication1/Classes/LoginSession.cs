using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class LoginSession
    {
        public Int32 getUserId(string VerificationCode)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_loginSessionRead", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@VerificationCode", SqlDbType.NVarChar).Value = VerificationCode;
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

            if (dt.Rows.Count == 0) //no user found
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
            }
        }
        public void setLoginSession(int UserId, string VerificationCode, int Hours)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_loginSessionWrite", sqlConn);

            //try
            //{
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                sqlCmd.Parameters.Add("@VerificationCode", SqlDbType.NVarChar).Value = VerificationCode;
                sqlCmd.Parameters.Add("@ExpireDate", SqlDbType.SmallDateTime).Value = DateTime.Now.AddHours(Hours);
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

        public int login(int mode, string username, string password)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter();


            string sp = "";
            if (mode == 1) //email
            {
                sp = "sp_loginEmail";
                sda = new SqlDataAdapter(sp, sqlConn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                sda.SelectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = username;

            }
            if (mode == 2) //mobile
            {
                sp = "sp_loginMobile";
                sda = new SqlDataAdapter(sp, sqlConn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                sda.SelectCommand.Parameters.Add("@Mobile", SqlDbType.Int).Value = Convert.ToInt32(username);
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

            if (dt.Rows.Count == 0) // user information was not valid
            {
                return 0;
            }
            else if (Convert.ToInt32(dt.Rows[0]["Status"].ToString()) == 2) //Access to user's panel is not allowed
            {
                return -1;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
            }
        }
    }
}