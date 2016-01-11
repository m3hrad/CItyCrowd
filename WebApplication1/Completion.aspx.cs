using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ImageManager;
using System.IO;

namespace WebApplication1
{
    public partial class Completion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Application["Status"]) == 2) Response.Redirect("~/Maintenance.html");

            //check login
            int UserId = 0;
            int userStatus = 0;
            Classes.UserInfo ui = new Classes.UserInfo();
            if (Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(Session["UserId"]);
                userStatus = ui.getUserStatus(UserId);
            }
            else
            {
                if (Request.Cookies["VC"] != null)
                {
                    string VC = Request.Cookies["VC"].Values["VC"];
                    Classes.LoginSession ls = new Classes.LoginSession();
                    UserId = ls.getUserId(VC);
                    if (UserId == 0) //if user not logged in redirect to login
                    {
                        Response.Redirect("~/Login/Completion");
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                    }
                }
                else
                {
                    Response.Redirect("~/Login/Completion");
                }
            }


            if (!IsPostBack)
            {
                //check if user entered these information before
                userStatus = ui.getUserStatus(UserId);
                if (userStatus != 0)
                {
                    if (userStatus != 4) Response.Redirect("~/Done/Welcome");
                }

                if (userStatus == 4)
                {
                    Tuple<string, string, string, string> result = ui.fbCompletion(UserId);

                    TextBoxFirstName.Text = result.Item1;
                    TextBoxLastName.Text = result.Item2;
                    HiddenFieldHasPhoto.Value = result.Item3;
                    DropDownListGender.SelectedValue = result.Item4;
                    if(Convert.ToBoolean(result.Item3))
                    {
                        HiddenFieldPhotoUrl.Value = "Files/Temp/" + UserId.ToString() + "-100.jpg";
                    }
                }

                DataTable dtCountries;
                Classes.Locations l = new Classes.Locations();
                dtCountries = l.countriesList();

                List<System.Web.UI.WebControls.ListItem> countries = new List<System.Web.UI.WebControls.ListItem>();
                DropDownListCountry.Items.Add(new ListItem("Select Country", "0"));
                for (int i = 0; i < dtCountries.Rows.Count; i++)
                {
                    DropDownListCountry.Items.Add(new ListItem(dtCountries.Rows[i]["CountryName"].ToString(), dtCountries.Rows[i]["CountryCode"].ToString()));
                }
            }
        }

        protected void photoUpload()
        {
            if (FileUpload1.HasFile)
            {

                string filePath = "~/Files/Temp/" + FileUpload1.FileName;
                if (Path.GetExtension(filePath).ToLower() == ".jpg" || Path.GetExtension(filePath).ToLower() == ".jpeg")
                {
                    string fileName = Session["UserId"] + ".jpg";
                    string relativePath = @"~\Files\Temp\" + fileName;
                    FileUpload1.SaveAs(Server.MapPath(relativePath));

                    //use WebManager to get the file, and save it
                    IImageInfo img = WebManager.GetImageInfo(FileUpload1);
                    img.Path = Server.MapPath("~") + "\\Files\\Temp\\";
                    img.FileName = Session["UserId"] + "-100.jpg";

                    //now create resized versions, and save them
                    IImageInfo img220 = img.ResizeMe(100, 100);
                    img220.Save("~/Files/Temp/");

                    File.Delete(Server.MapPath("~") + "\\Files\\Temp\\" + fileName);

                    //try
                    //{
                        File.Move(Server.MapPath("~") + "\\Files\\Temp\\" + Session["UserId"] + "-100.jpg", Server.MapPath("~") + "\\Files\\ProfilesPhotos\\" + Session["UserId"] + "-100.jpg");
                    //}
                    //catch
                    //{
                    //}

                    HiddenFieldHasPhoto.Value = "true";
                }
            }
            else
            {
                if (HiddenFieldPhotoUrl.Value.Length > 1)
                {
                    //try
                    //{
                        File.Move(Server.MapPath("~") + "\\Files\\Temp\\" + Session["UserId"] + "-100.jpg", Server.MapPath("~") + "\\Files\\ProfilesPhotos\\" + Session["UserId"] + "-100.jpg");
                    //}
                    //catch
                    //{
                    //}

                    HiddenFieldHasPhoto.Value = "true";
                }
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            Int16 locationStatus = Convert.ToInt16(HiddenFieldLocationStatus.Value);
            int locationId = 0;
            int userId = Convert.ToInt32(Session["UserId"]);

            if (locationStatus == 1)
            {
                locationId = Convert.ToInt32(HiddenFieldLocationId.Value);
            }
            else
            {
                locationId = 0;

                Classes.Locations l = new Classes.Locations();
                int status2 = l.request(userId,
                    TextBoxRequestLocationCountry.Text,
                    TextBoxRequestLocationCity.Text);
            }

            //save photo
            photoUpload();

            //validate

            //save
            int status;
            Classes.UserProfileSet ups = new Classes.UserProfileSet();
            status = ups.completion(
                userId,
                TextBoxUsername.Text,
                TextBoxFirstName.Text,
                TextBoxLastName.Text,
                Convert.ToInt16(DropDownListGender.SelectedValue),
                locationId,
                HiddenFieldDOB.Value,
                Convert.ToBoolean(HiddenFieldHasPhoto.Value));

            if (status == 1)
            {

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("sp_fbFriendsList", sqlConn);

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

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

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlConnection sqlConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                    SqlCommand sqlCmd2 = new SqlCommand("sp_fbFriendsApply", sqlConn2);
                    sqlCmd2.CommandType = CommandType.StoredProcedure;
                    sqlCmd2.Parameters.Add("@FbId", SqlDbType.VarChar).Value = dt.Rows[i]["FriendFbId"].ToString();
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



                Response.Redirect("~/Introduction");
            }
            else if (status == -1)
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "Username is not available, please select another one!";
            }
            else if (status == -2)
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "Username must has at least 1 letter!";
            }
            else if (status == -3)
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "Username must not contains special characters!";
            }
    }

        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt;
            Classes.Locations l = new Classes.Locations();
            dt = l.citiesList(DropDownListCountry.SelectedValue);

            DropDownListCity.Items.Clear();
            DropDownListCity.Items.Add(new ListItem("Select City", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownListCity.Items.Add(new ListItem(dt.Rows[i]["CityName"].ToString(), dt.Rows[i]["LocationId"].ToString()));
            }
        }
    }
}