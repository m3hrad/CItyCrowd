using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Application["Status"]) == 2) Response.Redirect("~/Maintenance.html");

            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    Response.Redirect("~/Done/Welcome");
                }
                //reading cookies for login
                if (Request.Cookies["VC"] != null)
                {
                    string VC = Request.Cookies["VC"].Values["VC"];
                    Classes.LoginSession ls = new Classes.LoginSession();
                    int UserId = ls.getUserId(VC);
                    if (UserId != 0) //user logged before
                    {
                        Response.Redirect("~/Done/Welcome");
                    }
                }
                
                if (!Convert.ToBoolean(Application["LoginAllowed"]))
                {
                    LabelError.Text = "Login is not allowed! Please try again later!";
                    ButtonLogin.Enabled = false;
                }
            }
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (TextBoxUsername.Text.Length == 0 || TextBoxPassword.Text.Length == 0) //blank information
            {
                LabelError.Visible = true;
                LabelError.Text = "Please enter the username and password!";
            }
            else
            {
                string username = TextBoxUsername.Text;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(username);
                int mode = 0;
                bool isValid = false;

                if (match.Success) //user entered email
                {
                    mode = 1;
                    isValid = true;
                }
                else //user entered phone number
                {
                    Regex strPattern = new Regex("[0-9]*[_]*");

                    if (!strPattern.IsMatch(username))
                    {
                        mode = 2;
                        isValid = true;
                    }
                }

                if (isValid)
                {
                    Classes.LoginSession ls = new Classes.LoginSession();
                    int userId = ls.login(mode, username, TextBoxPassword.Text);

                    if (userId == 0) // user information was not valid
                    {
                        LabelError.Visible = true;
                        LabelError.Text = "You username and/or password is not valid!";
                    }
                    else if (userId == -1)
                    {
                        LabelError.Visible = true;
                        LabelError.Text = "Access to user's panel is not allowed!";
                    }
                    else // user information was valid
                    {
                        Session["UserId"] = userId.ToString();

                        int Hours;
                        string VerificationCode = Convert.ToString(Guid.NewGuid());

                        if (CheckBoxRemember.Checked) // user want the system to remember him/her
                        {
                            Hours = Convert.ToInt32(ConfigurationManager.AppSettings["LoginHoursLong"].ToString());
                        }
                        else
                        {
                            Hours = Convert.ToInt32(ConfigurationManager.AppSettings["LoginHoursShort"].ToString());
                        }

                        // set login information
                        ls.setLoginSession(Convert.ToInt32(Session["UserId"]), VerificationCode, Hours);

                        // create the cookies
                        HttpCookie _userInfoCookies = new HttpCookie("VC");
                        _userInfoCookies["VC"] = VerificationCode;
                        _userInfoCookies.Expires = DateTime.Now.AddHours(Hours);
                        Response.Cookies.Add(_userInfoCookies);

                        // redirect user
                        try
                        {
                            if (Page.RouteData.Values["ItemId"].ToString() != "") // redirect user to a page with item id
                            {
                                Response.Redirect("~/" + Page.RouteData.Values["Page"].ToString() + "/" + Page.RouteData.Values["ItemId"].ToString());
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {

                        }
                        try
                        {
                            if (Page.RouteData.Values["Page"].ToString() != "") // redirect user to a page
                            {
                                Response.Redirect("~/" + Page.RouteData.Values["Page"].ToString());
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {

                        }
                        // redirect the user to his/her panel
                        Response.Redirect("~/Done/Welcome");
                    }
                }
                else
                {
                    LabelError.Visible = true;
                    LabelError.Text = "Wrong information!";
                }
            }
        }

        //private void callFacebook(string fbId, string fbTokenId)
        //{
        //    bool fbValidate = false;

        //    //////////////////////////FB
        //    WebRequest request = WebRequest.Create("http://www.contoso.com/");


        //    Int64 FbId = Convert.ToInt64(fbId);
        //    string Email = "";
        //    string FirstName = "";
        //    string LastName = "";
        //    Int16 Gender = 0;
        //    bool HasPhoto = false;

        //    FacebookObject myFacebookObject = new FacebookObject(
        //        FbId,
        //        Email,
        //        FirstName,
        //        LastName,
        //        Gender,
        //        HasPhoto);

        //    //check if user exists in our db
        //    if (fbValidate)
        //    {
        //        int myFbId = Convert.ToInt32(fbId);
        //        DataTable dt = new DataTable();
        //        DataSet ds = new DataSet();
        //        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
        //        SqlDataAdapter sda = new SqlDataAdapter("sp_getUserIdByFbId", sqlConn);
        //        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        sda.SelectCommand.Parameters.Add("@FbId", SqlDbType.BigInt).Value = myFacebookObject.FbId;

        //        try
        //        {
        //            sda.Fill(ds);
        //            dt = ds.Tables[0];
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        finally
        //        {
        //            sqlConn.Close();
        //            sda.Dispose();
        //            sqlConn.Dispose();
        //        }

        //        if (dt.Rows.Count != 0)
        //        {
        //            //login
        //            int userId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());

        //        }
        //        else
        //        {
        //            //register
        //            int userId = registerByFacebook(myFacebookObject);
        //        }

        //    }
        //}

    }
}