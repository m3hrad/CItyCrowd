using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace WebApplication1.Classes
{
    public class ForgotPassword
    {
        public string recoverEmailGet(string vCode)
        {
            string email = "";
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_forgotPasswordEmailByVCode", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add(new SqlParameter("@VCode", SqlDbType.NVarChar));
                sda.SelectCommand.Parameters["@VCode"].Value = vCode;
                sda.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count != 0)
                {
                    email = dt.Rows[0]["Email"].ToString();
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

            return email;
        }

        public int recover(string vCode, string email, string password)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_forgotPasswordRecover", sqlConn);

            //try
            //{
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                sqlCmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                sqlCmd.Parameters.Add("@VCode", SqlDbType.NVarChar).Value = vCode;

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

            return 1;
        }

        public int request(string email)
        {
            int status = 0;
            string VCode = "";

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_getUserIdByEmail", sqlConn);
            SqlCommand sqlCmd = new SqlCommand("sp_forgotPasswordRequest", sqlConn);

            //try
            //{
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@Email"].Value = email;
            sda.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                //email address was bit dounf
                status = 2;
                
            }
            else
            {
                DataTable dt2 = new DataTable();
                DataSet ds2 = new DataSet();
                SqlDataAdapter sda2 = new SqlDataAdapter("sp_forgotPasswordExists", sqlConn);
                sda2.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda2.SelectCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
                sda2.SelectCommand.Parameters["@Email"].Value = email;
                sda2.Fill(ds2);
                dt2 = ds2.Tables[0];

                if (dt2.Rows.Count == 0)
                {
                    VCode = Convert.ToString(Guid.NewGuid());

                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                    sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    sqlCmd.Parameters.Add("@VCode", SqlDbType.NVarChar).Value = VCode;

                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();

                    status = 1;
                }
                else
                {
                    VCode = dt2.Rows[0]["VerificationCode"].ToString();
                }

                status = 1;

                string siteLink = ConfigurationManager.AppSettings["WebsiteLink"].ToString();
                string link = siteLink + "ForgotPassword/Recover/" + VCode;
                Classes.Mail m = new Classes.Mail();
                int status2 = m.sendMail("password", email, 
                    "Hi,<br/>You requested to recover your CityCrowd password.<br/>To recover your password please <a href='"
                    + link + "'>CLICK HERE</a><br/><br/>CityCowd Team", "");

                sda2.Dispose();
            }
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
                sqlConn.Close();
                sda.Dispose();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            //}

            return status;
        }
    }
}