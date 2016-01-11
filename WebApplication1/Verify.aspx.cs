using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    public partial class Verify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mode = "";
            string code = "";

            try
            {
                mode = Page.RouteData.Values["Mode"].ToString();
                code = Page.RouteData.Values["Code"].ToString();
            }
            catch
            {

            }
            finally
            {

            }

            switch (mode)
            {
                case "Email":
                    {
                        DataTable dt = new DataTable();
                        DataSet ds = new DataSet();
                        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                        SqlDataAdapter sda = new SqlDataAdapter("sp_verifyEmail", sqlConn);

                        //try
                        //{
                            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                            sda.SelectCommand.Parameters.Add(new SqlParameter("@VCode", SqlDbType.NVarChar));
                            sda.SelectCommand.Parameters["@VCode"].Value = code;
                            sda.Fill(ds);
                            dt = ds.Tables[0];

                            if (dt.Rows.Count != 0)
                            {
                                if (dt.Rows[0]["VerificationStatus"].ToString() == "1")
                                {
                                    //successfull
                                    Response.Redirect("~/Done/EmailVerified");
                                }
                                else
                                {
                                    //unsuccessfull
                                    Response.Redirect("~/Error/EmailVerificationFailed");
                                }
                            }
                            else
                            {
                                //unsuccessfull
                                Response.Redirect("~/Error/EmailVerificationFailed");
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
                        break;
                    }
                default:
                    {
                        Response.Redirect("~/Error/404");
                        break;
                    }
            }
        }
    }
}