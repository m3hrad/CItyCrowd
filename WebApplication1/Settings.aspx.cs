using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using ImageManager;

namespace WebApplication1
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check login
            int UserId = 0;
            if (Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(Session["UserId"]);
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
                        Response.Redirect("~/Login/Settings");
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                    }
                }
                else
                {
                    Response.Redirect("~/Login/Settings");
                }
            }

            //check user status
            string completionValue = Session["DoneCompletion"] as string; if (String.IsNullOrEmpty(completionValue))
            {
                Classes.UserInfo ui = new Classes.UserInfo();
                int userStatus = ui.getUserStatus(UserId);
                switch (userStatus)
                {
                    case 1:
                        Session["DoneCompletion"] = "1";
                        break;
                    case 0:
                    case 4:
                        Response.Redirect("~/Completion");
                        break;
                    case 2:
                        Response.Redirect("~/Error/UserDisabled");
                        break;
                    case 3:
                        Response.Redirect("~/Error/UserDeactivated");
                        break;
                }
            }


            if (!IsPostBack)
            {
                // pre show a tab
                try
                {
                    switch (Page.RouteData.Values["Section"].ToString().ToLower())
                    {
                        case "information":
                            PanelInformation.Visible = true;
                            getDataInformation();
                            break;
                        case "photo":
                            PanelPhoto.Visible = true;
                            getDataPhoto();
                            break;
                        case "location":
                            PanelLocation.Visible = true;
                            getDataLocation();
                            break;
                        case "preferences":
                            PanelPreferences.Visible = true;
                            getDataPreferences();
                            break;
                        case "account":
                            PanelAccount.Visible = true;
                            getDataAccount();
                            break;
                        case "privacy":
                            PanelPrivacy.Visible = true;
                            getDataPrivacy();
                            break;
                        default:
                            break;
                    }
                }
                catch
                {

                }
                finally
                {

                }
            }
        }

        protected void hidePanels() // hide all the panels
        {
            PanelMenu.Visible = false;
            PanelInformation.Visible = false;
            PanelPhoto.Visible = false;
            PanelLocation.Visible = false;
            PanelPreferences.Visible = false;
            PanelAccount.Visible = false;
            PanelPrivacy.Visible = false;
        }

        protected void LinkButtonInfo_Click(object sender, EventArgs e)
        {
            getDataInformation();
        }

        protected void LinkButtonPhoto_Click(object sender, EventArgs e)
        {
            getDataPhoto();
        }

        protected void LinkButtonLocation_Click(object sender, EventArgs e)
        {
            getDataLocation();
        }

        protected void LinkButtonPreferences_Click(object sender, EventArgs e)
        {
            getDataPreferences();
        }

        protected void LinkButtonAccount_Click(object sender, EventArgs e)
        {
            getDataAccount();
        }

        protected void LinkButtonPrivacy_Click(object sender, EventArgs e)
        {
            getDataPrivacy();
        }

        protected void ButtonLocation_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsLocationEdit", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
            sqlCmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = Convert.ToInt32(DropDownListCity.SelectedValue);

            //try
            //{
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

            LabelLocationMessage.Visible = true;
            LabelLocationMessage.Text = "You have succesfully changed your location settings!";
        }

        protected void LinkButtonActivate_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsDeactivate", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

            //try
            //{
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

            LabelDeactivateMessage.Visible = true;
            LabelDeactivateMessage.Text = "You have succesfully deactivated your account!";
        }

        protected void getDataInformation()
        {
            hidePanels();
            PanelInformation.Visible = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsInformationGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

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

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                TextBoxFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                TextBoxLastName.Text = dt.Rows[0]["LastName"].ToString();
                TextBoxUsername.Text = dt.Rows[0]["Username"].ToString();
                TextBoxAbout.Text = dt.Rows[0]["About"].ToString();
                //Gender
                if (dt.Rows[0]["Gender"].ToString() == "")
                {
                    DropDownListGender.SelectedValue = "0";
                }
                else
                {
                    DropDownListGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                }
                //Birthdate
                if (dt.Rows[0]["BirthDate"].ToString() != "")
                {
                    HiddenFieldDOB.Value = Convert.ToDateTime(dt.Rows[0]["BirthDate"].ToString()).ToString("yyyy-MM-dd");
                }
                else
                {
                    HiddenFieldDOB.Value = "0";
                }
            }
        }

        protected void getDataPhoto()
        {
            hidePanels();
            PanelPhoto.Visible = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsPhotoGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

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

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString())) // user has photo
                {
                    HiddenFieldPhotoUrl.Value = "Files/ProfilesPhotos/" + Session["UserId"].ToString() + "-100.jpg";
                    HiddenFieldStatus.Value = "1";
                }
                else // user doesnt have photo
                {
                    HiddenFieldPhotoUrl.Value = "Images/NoPhoto.png";
                    HiddenFieldStatus.Value = "0";
                }
                HiddenFieldCoverId.Value = dt.Rows[0]["CoverId"].ToString();
            }
        }

        protected void getDataLocation()
        {
            hidePanels();
            PanelLocation.Visible = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsLocationGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

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

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                if (!IsPostBack)
                {
                    Classes.Locations l = new Classes.Locations();
                    DataTable dtCountries = l.countriesList();

                    List<System.Web.UI.WebControls.ListItem> countries = new List<System.Web.UI.WebControls.ListItem>();
                    DropDownListCountry.Items.Add(new ListItem("Select Country", "0"));
                    for (int i = 0; i < dtCountries.Rows.Count; i++)
                    {
                        DropDownListCountry.Items.Add(new ListItem(dtCountries.Rows[i]["CountryName"].ToString(), dtCountries.Rows[i]["CountryCode"].ToString()));
                    }

                    Classes.UserInfo ui = new Classes.UserInfo();
                    int locationId = ui.locationIdByUserId(Convert.ToInt32(Session["UserId"]));

                    if (locationId == 0)
                    {
                        DropDownListCountry.SelectedValue = "0";
                    }
                    else
                    {
                        string countryCode = l.locationInfoOnlyId(locationId);

                        locationCity(countryCode);
                        DropDownListCountry.SelectedValue = countryCode;
                        DropDownListCity.SelectedValue = locationId.ToString();
                    }

                    DataTable dtLocation = l.getLocationInfoByCityId(Convert.ToInt32(dt.Rows[0]["LocationId"].ToString()));
                    if (dtLocation.Rows.Count == 0)
                    {
                        LabelLocation.Text = "Not Available!";
                    }
                    else
                    {
                        LabelLocation.Text = dtLocation.Rows[0]["CityName"].ToString();
                    }
                }

                if (dt.Rows[0]["LocationId"].ToString() == "0")
                {
                    DropDownListCountry.SelectedValue = "0";
                }
                else
                {
                    int cityId = Convert.ToInt32(dt.Rows[0]["LocationId"].ToString());
                    DataTable dtLocation = new DataTable();
                    DataSet dsLocation = new DataSet();
                    SqlConnection sqlConnLocation = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                    SqlDataAdapter sdaLocation = new SqlDataAdapter("sp_locationInfoOnlyId", sqlConnLocation);
                    sdaLocation.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sdaLocation.SelectCommand.Parameters.Add("@CityId", SqlDbType.Int).Value = cityId;

                    //try
                    //{
                        sdaLocation.Fill(dsLocation);
                        dtLocation = dsLocation.Tables[0];
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                    //finally
                    //{
                        sqlConnLocation.Close();
                        sdaLocation.Dispose();
                        sqlConnLocation.Dispose();
                    //}

                    locationCity(dtLocation.Rows[0]["CountryCode"].ToString());
                    DropDownListCountry.SelectedValue = dtLocation.Rows[0]["CountryCode"].ToString();
                    DropDownListCity.SelectedValue = dt.Rows[0]["LocationId"].ToString();
                }

            }
        }

        protected void getDataPreferences()
        {
            hidePanels();
            PanelPreferences.Visible = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsPreferences", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

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

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                HiddenFieldNotifications.Value = dt.Rows[0]["Notifications"].ToString();
                HiddenFieldSound.Value = dt.Rows[0]["Sound"].ToString();
            }
        }

        protected void getDataAccount()
        {
            hidePanels();
            PanelAccount.Visible = true;

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsAccountGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

            //try
            //{
                sda.Fill(ds);
                dt = ds.Tables[0];
                dt2 = ds.Tables[0];
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

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                LabelEmail.Text = dt.Rows[0]["Email"].ToString();
                ImageEmailStatus.ImageUrl = "~/Images/Verified" + dt.Rows[0]["EmailVerified"].ToString() + ".png";
                HiddenFieldEmailVerify.Value = dt.Rows[0]["EmailVerified"].ToString();
            }

            if (dt.Rows[0]["PStatus"].ToString() == "0")
            {
                HiddenFieldFStatus.Value = "0";
            }
        }

        protected void getDataPrivacy()
        {
            hidePanels();
            PanelPrivacy.Visible = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsPrivacyGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

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

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                //DropDownListFriends.SelectedValue = dt.Rows[0]["FriendsOnly"].ToString(); ;
            }
        }

        protected void ButtonPassword_Click(object sender, EventArgs e)
        {
            Int16 FStatus = Convert.ToInt16(HiddenFieldFStatus.Value);
            if (TextBoxPasswordOld.Text == "" && FStatus == 1)
            {
                HiddenFieldPStatus.Value = "0";
                LabelPasswordMessage.Text = "Please enter your current password";
                LabelPasswordMessage.Visible = true;
            }
            else
            {
                if (TextBoxPasswordOld.Text.Length < 4)
                {
                    HiddenFieldPStatus.Value = "0";
                    LabelPasswordMessage.Text = "Current password is not currect";
                    LabelPasswordMessage.Visible = true;
                }
                else
                {
                    if (TextBoxPasswordNew.Text.Length < 4)
                    {
                        HiddenFieldPStatus.Value = "0";
                        LabelPasswordMessage.Text = "New password must be atleast 4 characters";
                        LabelPasswordMessage.Visible = true;
                    }
                    else
                    {
                        if (TextBoxPasswordNew.Text != TextBoxPasswordNew2.Text)
                        {
                            HiddenFieldPStatus.Value = "0";
                            LabelPasswordMessage.Text = "New password and re-type new password must be the same";
                            LabelPasswordMessage.Visible = true;
                        }
                        else
                        {
                            if (FStatus == 1)
                            {
                                string currentPassword = "";
                                DataTable dt = new DataTable();
                                DataSet ds = new DataSet();
                                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                                SqlDataAdapter sda = new SqlDataAdapter("sp_settingsAccountPassword", sqlConn);
                                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                                sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

                                //try
                                //{
                                    sda.Fill(ds);
                                    dt = ds.Tables[0];
                                    currentPassword = dt.Rows[0]["Password"].ToString();
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

                                if (currentPassword != TextBoxPasswordOld.Text)
                                {
                                    HiddenFieldPStatus.Value = "0";
                                    LabelPasswordMessage.Text = "Your current password is not correct";
                                    LabelPasswordMessage.Visible = true;
                                }
                                else
                                {
                                    sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                                    SqlCommand sqlCmd = new SqlCommand("sp_settingsAccountPasswordEdit", sqlConn);
                                    sqlCmd.CommandType = CommandType.StoredProcedure;
                                    sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
                                    sqlCmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = TextBoxPasswordNew.Text;

                                    //try
                                    //{
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
                                    HiddenFieldPStatus.Value = "1";
                                    LabelPasswordMessage.Text = "You have successfully changed your password";
                                    LabelPasswordMessage.Visible = true;
                                }
                            }
                            else
                            {
                                SqlConnection sqlConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                                SqlCommand sqlCmd = new SqlCommand("sp_settingsAccountPasswordAdd", sqlConn2);
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
                                sqlCmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = TextBoxPasswordNew.Text;

                                //try
                                //{
                                    sqlConn2.Open();
                                    sqlCmd.ExecuteNonQuery();
                                //}
                                //catch (Exception ex)
                                //{

                                //}
                                //finally
                                //{
                                    sqlConn2.Close();
                                    sqlCmd.Dispose();
                                    sqlConn2.Dispose();
                                //}
                                Response.Redirect("~/Settings/Account");
                            }
                        }
                    }
                }
            }
        }

        protected void ButtonPereferences_Click(object sender, EventArgs e)
        {
            bool notifications = false;
            bool sound = false;
            if (HiddenFieldNotifications.Value == "True")
            {
                notifications = true;
            }
            if (HiddenFieldSound.Value == "True")
            {
                sound = true;
            }

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsPereferencesEdit", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
            sqlCmd.Parameters.Add("@Notifications", SqlDbType.Bit).Value = notifications;
            sqlCmd.Parameters.Add("@Sound", SqlDbType.Bit).Value = sound;

            //try
            //{
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

            LabelPereferencesMessage.Visible = true;
            LabelPereferencesMessage.Text = "You have succesfully changed your pereferences settings!";
        }

        protected void ButtonPhoto_Click(object sender, EventArgs e)
        {
            if (FileUploadPicture.HasFile)
            {

                string filePath = "~/Files/Temp/" + FileUploadPicture.FileName;
                if (Path.GetExtension(filePath).ToLower() == ".jpg" || Path.GetExtension(filePath).ToLower() == ".jpeg")
                {
                    try
                    {
                        File.Delete(Server.MapPath("~") + "\\Files\\ProfilesPhotos\\" + Session["UserId"] + "-100.jpg");
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                    string fileName = Session["UserId"] + ".jpg";
                    string relativePath = @"~\Files\Temp\" + fileName;
                    FileUploadPicture.SaveAs(Server.MapPath(relativePath));

                    //use WebManager to get the file, and save it
                    IImageInfo img = WebManager.GetImageInfo(FileUploadPicture);
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

                    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                    SqlCommand sqlCmd = new SqlCommand("sp_settingsPhotoEdit", sqlConn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
                    sqlCmd.Parameters.Add("@HasPhoto", SqlDbType.Bit).Value = true;

                    //try
                    //{
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

                    LabelPhotoMessage.Visible = true;
                    LabelPhotoMessage.Text = "You have successfully uploaded your picture.";

                    HiddenFieldPhotoUrl.Value = "Files/ProfilesPhotos/" + Session["UserId"].ToString() + "-100.jpg";
                    HiddenFieldStatus.Value = "1";
                }
                else
                {
                    LabelPhotoMessage.Visible = true;
                    LabelPhotoMessage.Text = "Select a picture with JPG extenstion.";
                }
            }
            else
            {
                LabelPhotoMessage.Visible = true;
                LabelPhotoMessage.Text = "Select a picture file.";
            }

        }

        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            locationCity(DropDownListCountry.SelectedValue);
            HiddenFieldStep.Value = "Location";
        }

        void locationCity(string countryCode)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationCities", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@CountryCode", SqlDbType.Char).Value = countryCode;

            sda.Fill(ds);
            dt = ds.Tables[0];

            //try
            //{
                
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

            DropDownListCity.Items.Clear();
            DropDownListCity.Items.Add(new ListItem("Select City", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownListCity.Items.Add(new ListItem(dt.Rows[i]["CityName"].ToString(), dt.Rows[i]["LocationId"].ToString()));
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            Classes.UserProfileSet ups = new Classes.UserProfileSet();
            int status = 0;
            switch (HiddenFieldMode.Value.ToLower())
              {
                    case "firstname":
                        status = ups.firstName(userId, TextBoxFirstName.Text);
                        break;
                    case "lastname":
                        status = ups.lastName(userId, TextBoxLastName.Text);
                        break;
                    case "username":
                        //-1 exists before -2 username not valid -3 no special char -4 enter username 1 successful
                        status = ups.username(userId, TextBoxUsername.Text);
                        HiddenFieldStatus.Value = status.ToString();
                        if (status != 1)
                        {
                            Classes.UserInfo ui = new Classes.UserInfo();
                            TextBoxUsername.Text = ui.getUsernameByUserId(userId);
                        }
                        break;
                    case "gender":
                        status = ups.gender(userId, Convert.ToInt16(DropDownListGender.SelectedValue));
                        break;
                    case "dob":
                        status = ups.birthDate(userId, HiddenFieldDOB.Value);
                        break;
                    case "about":
                        status = ups.about(userId, TextBoxAbout.Text);
                        break;
                    case "photo":
                        bool hasPhoto = Convert.ToBoolean(HiddenFieldValue.Value);
                        status = ups.hasPhoto(userId, hasPhoto);
                        break;
                    case "cover":
                        status = ups.cover(userId, Convert.ToInt16(HiddenFieldValue.Value));
                        break;
                    case "location":
                        status = ups.location(userId, Convert.ToInt32(HiddenFieldLocationId.Value));

                        Classes.Locations l = new Classes.Locations();
                        DataTable dtLocation = l.getLocationInfoByCityId(Convert.ToInt32(DropDownListCity.SelectedValue));
                        if (dtLocation.Rows.Count == 0)
                        {
                            LabelLocation.Text = "Not Available!";
                        }
                        else
                        {
                            LabelLocation.Text = dtLocation.Rows[0]["CityName"].ToString();
                        }
                        break;
                    case "notifications":
                        bool getNotifications = Convert.ToBoolean(HiddenFieldNotifications.Value);
                        status = ups.notifications(userId, getNotifications);
                        break;
                    case "mobile":
                        status = ups.mobile(userId, Convert.ToInt64(TextBoxMobile.Text));
                        break;

               }
        }

        protected void ButtonPhotoRemove_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsPhotoEdit", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@HasPhoto", SqlDbType.Bit).Value = 0;

            //try
            //{
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

            //try
            //{
                File.Delete(Server.MapPath("~") + "\\Files\\ProfilesPhotos\\" + userId.ToString() + "-100.jpg");
            //}
            //catch
            //{
            //}

            HiddenFieldPhotoUrl.Value = "Images/nophoto.png";
            LabelPhotoMessage.Visible = true;
            LabelPhotoMessage.Text = "You have succesfully removed your profile photo!";
            HiddenFieldStatus.Value = "0";
        }
    }
}