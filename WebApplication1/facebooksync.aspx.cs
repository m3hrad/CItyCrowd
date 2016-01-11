using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

namespace WebApplication1
{
    public partial class facebooksync : System.Web.UI.Page
    {

        int inviteId = 0;
        public class fbObject
        {
            public string id { get; set; }
            public string email { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string gender { get; set; }

            public fbObject(string Id, string Email, string First_name, string Last_name, string Gender)
            {
                id = Id;
                email = Email;
                first_name = First_name;
                last_name = Last_name;
                gender = Gender;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAuthorization();
        }

        private void CheckAuthorization()
        {
            string app_id = "880736035338936";
            string app_secret = "720dfa778da7953acf9c19b68eafa064";
            string scope = "public_profile,email,user_friends";

            if (Request["code"] == null)
            {
                Response.Redirect(string.Format(
                    "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                    app_id, Request.Url.AbsoluteUri, scope));
            }
            else
            {
                Dictionary<string, string> tokens = new Dictionary<string, string>();

                string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                    app_id, Request.Url.AbsoluteUri, scope, Request["code"].ToString(), app_secret);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    string vals = reader.ReadToEnd();

                    foreach (string token in vals.Split('&'))
                    {
                        //meh.aspx?token1=steve&token2=jake&...
                        tokens.Add(token.Substring(0, token.IndexOf("=")),
                            token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                    }
                }

                string access_token = tokens["access_token"];

                var client = new FacebookClient(access_token);
                string result = client.Get("me", new { fields = "id,email,first_name,last_name,gender" }).ToString();
                string friends = client.Get("me", new { fields = "friends" }).ToString();

                JObject json = JObject.Parse(result);
                JObject jsonFriends = JObject.Parse(friends);

            string id="";
            string name = "";

            

                Int16 gender = 0;

                if (json.GetValue("gender").ToString() == "male") gender = 1;
                else if (json.GetValue("gender").ToString() == "female") gender = 2;

                int inviteId = 0;

                if (Request.Cookies["inviteid"] != null)
                {
                    string myid = Request.Cookies["inviteid"].Values["inviteid"];
                    inviteId = Convert.ToInt32(myid);
                }

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("sp_loginByFacebook", sqlConn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = json.GetValue("email").ToString();
                sda.SelectCommand.Parameters.Add("@FbId", SqlDbType.VarChar).Value = json.GetValue("id").ToString();
                sda.SelectCommand.Parameters.Add("@FbAccessToken", SqlDbType.VarChar).Value = access_token;
                sda.SelectCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = json.GetValue("first_name").ToString();
                sda.SelectCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = json.GetValue("last_name").ToString();
                sda.SelectCommand.Parameters.Add("@Gender", SqlDbType.TinyInt).Value = gender;
                sda.SelectCommand.Parameters.Add("@HasPhoto", SqlDbType.Bit).Value = false;
                sda.SelectCommand.Parameters.Add("@InviteId", SqlDbType.Int).Value = inviteId;

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

                int userId = 0;
                int status = 0;

                if (dt.Rows.Count != 0)
                {
                    userId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    status = Convert.ToInt32(dt.Rows[0]["Status"].ToString());
                }
                else
                {
                    Response.Redirect("~/Login");
                }

                if (status == 4)
                {
                    //fb friends

                    foreach (var i in jsonFriends["friends"]["data"].Children())
                    {
                        SqlConnection sqlConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                        SqlCommand sqlCmd2 = new SqlCommand("sp_fbFriends", sqlConn2);
                        sqlCmd2.CommandType = CommandType.StoredProcedure;
                        sqlCmd2.Parameters.Add("@FbId", SqlDbType.VarChar).Value = i["id"].ToString().Replace("\"", "");
                        sqlCmd2.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                        //try
                        //{
                            sqlConn2.Open();
                            sqlCmd2.ExecuteNonQuery();
                        //}
                        //catch
                        //{

                        //}
                        //finally
                        //{
                            sqlConn2.Close();
                            sqlConn2.Dispose();
                            sqlCmd2.Dispose();
                        //}
                    }

                    

                    //picture
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile("https://graph.facebook.com/" + json.GetValue("id").ToString() + "/picture?type=large",
                        Server.MapPath(@"~\Files\Temp\" + userId.ToString() + "-100.jpg"));

                    Bitmap img = new Bitmap(Server.MapPath(@"~\Files\Temp\" + userId.ToString() + "-100.jpg"));
                    int imageHeight = Convert.ToInt32(img.Height);
                    img.Dispose();

                    if (imageHeight == 126)
                    {
                        File.Delete(Server.MapPath(@"~\Files\Temp\" + userId.ToString() + "-100.jpg"));
                    }
                    else
                    {
                        SqlConnection sqlConn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                        SqlCommand sqlCmd = new SqlCommand("sp_settingsPhotoEdit", sqlConn3);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        sqlCmd.Parameters.Add("@HasPhoto", SqlDbType.Bit).Value = true;

                        //try
                        //{
                            sqlConn3.Open();
                            sqlCmd.ExecuteNonQuery();
                        //}
                        //catch (Exception ex)
                        //{

                        //}
                        //finally
                        //{
                            sqlConn3.Close();
                            sqlCmd.Dispose();
                            sqlConn3.Dispose();
                        //}
                    }
                }


                Session["UserId"] = userId.ToString();
                Session.Remove("InviteId");

                int Hours = Convert.ToInt32(ConfigurationManager.AppSettings["LoginHoursLong"].ToString());
                string VerificationCode = Convert.ToString(Guid.NewGuid());

                // set login information
                Classes.LoginSession ls = new Classes.LoginSession();
                ls.setLoginSession(userId, VerificationCode, Hours);

                // create the cookies
                HttpCookie _userInfoCookies = new HttpCookie("VC");
                _userInfoCookies["VC"] = VerificationCode;
                _userInfoCookies.Expires = DateTime.Now.AddHours(Hours);
                Response.Cookies.Add(_userInfoCookies);

                if(status == 4)
                {
                    Response.Redirect("~/Completion");
                }
                else
                {
                    Response.Redirect("~/Done/Welcome");
                }
                
            }


        }
    }
}