using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Admins
    {
        public bool permissions(int UserId, string Section)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_adminPermissions", sqlConn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

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

            if (dt.Rows.Count == 0) //user doesn't exist as an admin
            {
                return false;
            }
            else
            {
                switch (Section)
                {
                    case "Admins":
                        {
                            return Convert.ToBoolean(dt.Rows[0]["PermAdmins"].ToString());
                            break;
                        }
                    case "Blog":
                        {
                            return Convert.ToBoolean(dt.Rows[0]["PermBlog"].ToString());
                            break;
                        }
                    case "Content":
                        {
                            return Convert.ToBoolean(dt.Rows[0]["PermContent"].ToString());
                            break;
                        }
                    case "Events":
                        {
                            return Convert.ToBoolean(dt.Rows[0]["PermEvents"].ToString());
                            break;
                        }
                    case "Locations":
                        {
                            return Convert.ToBoolean(dt.Rows[0]["PermLocations"].ToString());
                            break;
                        }
                    case "Newsletter":
                        {
                            return Convert.ToBoolean(dt.Rows[0]["PermNewsletter"].ToString());
                            break;
                        }
                    case "Settings":
                        {
                            return Convert.ToBoolean(dt.Rows[0]["PermSettings"].ToString());
                            break;
                        }
                    case "Statistics":
                        {
                            return Convert.ToBoolean(dt.Rows[0]["PermStats"].ToString());
                            break;
                        }
                    case "Support":
                        {
                            return Convert.ToBoolean(dt.Rows[0]["PermSupport"].ToString());
                            break;
                        }
                    case "Users":
                        {
                            return Convert.ToBoolean(dt.Rows[0]["PermUsers"].ToString());
                            break;
                        }
                    case "Home":
                        {
                            return true;
                            break;
                        }
                    default:
                        {
                            return false;
                            break;
                        }
                }
            }
        }
    }
}