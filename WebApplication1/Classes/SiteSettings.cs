using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class SiteSettings
    {
        public string getSettings(string item)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settings", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
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

            switch (item.ToLower())
            {
                case "loginallow":
                    {
                        return dt.Rows[0]["LoginAllow"].ToString();
                        break;
                    }
                case "registerallow":
                    {
                        return dt.Rows[0]["RegisterAllow"].ToString();
                        break;
                    }
                case "status":
                    {
                        return dt.Rows[0]["Status"].ToString();
                        break;
                    }
                default:
                    {
                        return "0";
                        break;
                    }
            }
            
        }

        public Tuple<bool, bool, bool, int> getAllSettings()
        {
            bool loginAllowed = false;
            bool registerAllowed = false;
            bool activitiesAllowed = false;
            int status = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settings", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;

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
                loginAllowed = Convert.ToBoolean(dt.Rows[0]["LoginAllow"].ToString());
                registerAllowed = Convert.ToBoolean(dt.Rows[0]["RegisterAllow"].ToString());
                activitiesAllowed = Convert.ToBoolean(dt.Rows[0]["ActivitiesAllow"].ToString());
                status = Convert.ToInt32(dt.Rows[0]["Status"].ToString());
            }

            return new Tuple<bool, bool, bool, int>(
                loginAllowed,
                registerAllowed,
                activitiesAllowed,
                status);
        }
    }
}