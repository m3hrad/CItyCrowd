using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace WebApplication1.Classes
{
    public class UserProfileSet
    {
        public Int16 completion(int userId, string username, string firstName, string lastName, Int16 gender, int locationId, string birthDate, bool hasPhoto)
        {
            Int16 status = 0;

            //check username valid
            Regex RgxUrl = new Regex("[^a-zA-Z]");
            if (Regex.Matches(username, @"[a-zA-Z]").Count == 0)
            {
                status = -2;
            }
            else
            {
                //special symbols
                RgxUrl = new Regex("[^a-zA-Z0-9]");

                if (RgxUrl.IsMatch(username))
                {
                    status = -3;
                }
                else
                {

                    //check username exists
                    Classes.UserInfo ui = new Classes.UserInfo();
                    bool usernameExists = ui.checkUsernameExists(username);

                    if (usernameExists)
                    {
                        status = -1;
                    }
                    else
                    {
                        status = 1;

                        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                        SqlCommand sqlCmd = new SqlCommand("sp_completion", sqlConn);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        sqlCmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;
                        sqlCmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = firstName;
                        sqlCmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastName;
                        sqlCmd.Parameters.Add("@Gender", SqlDbType.TinyInt).Value = gender;
                        sqlCmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationId;
                        sqlCmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = Convert.ToDateTime(birthDate);//Convert.ToDateTime(dob);
                        sqlCmd.Parameters.Add("@HasPhoto", SqlDbType.Bit).Value = hasPhoto;

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

                        Classes.Notifications n = new Classes.Notifications();
                        n.notificationAdd(userId, 1, Convert.ToInt64(userId));

                        string email = ui.getEmailByUserId(userId);
                        string siteLink = ConfigurationManager.AppSettings["WebsiteLink"].ToString();
                        string link = siteLink;
                        string message = "<br/>Welcome to CityCrowd.<br/>You have successfully registered.<br/><a href='"
                            + link + "'>CLICK HERE</a> to visit the website.<br/><br/>CityCowd Team";

                        if(!ui.isUserEmailVerified(userId))
                        {
                            string VCode = Convert.ToString(Guid.NewGuid());
                            SqlConnection sqlConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                            SqlCommand sqlCmd2 = new SqlCommand("sp_verifyEmailAdd", sqlConn2);
                            sqlCmd2.CommandType = CommandType.StoredProcedure;
                            sqlCmd2.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                            sqlCmd2.Parameters.Add("@VCode", SqlDbType.NVarChar).Value = VCode;

                            sqlConn2.Open();
                            sqlCmd2.ExecuteNonQuery();

                            link = siteLink + "Verify/Email/" + VCode;
                            message = "<br/>Welcome to CityCrowd.<br/>You have successfully registered.<br/><a href='"
                            + link + "'>CLICK HERE</a> to verify your email address and visit the website.<br/><br/>CityCowd Team";
                        }
                        
                        Classes.Mail m = new Classes.Mail();
                        int status2 = m.sendMail("register", email,
                            "Hi, " + firstName + message, "");
                    }
                }
            }

            return status;
        }

        public Tuple<int, string, int> register(string email, string password1, string password2, int inviteId)
        {
            int status = 0;
            string message = "";
            int userId = 0;

            if (password1.Length == 0) // check if email entered blank
            {
                status = -1;
                message = "Enter email address!";
            }
            else // check if email is correct
            {
                string expression = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" + @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" + @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

                Match match = Regex.Match(email, expression, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    status = -1;
                    message = "Email address is not correct!";
                }
                else
                {
                    if (password1.Length == 0) // check if password is blank
                    {
                        status = -1;
                        message = "Enter password!";
                    }
                    else
                    {
                        if (password1.Length < 4) // check if password is less than 4 characters
                        {
                            status = -1;
                            message = "Password must be at least 4 characters!";
                        }
                        else
                        {
                            if (password1 != password2) // check if password and retype password are the same
                            {
                                status = -1;
                                message = "Password and retype password must be the same!";
                            }
                            else
                            {
                                Classes.UserInfo ui = new Classes.UserInfo();
                                int UserId = ui.getUserIdByEmail(email);
                                if (UserId != 0) //user registered before
                                {
                                    status = -1;
                                    message = "Email address already registered before!";
                                }
                                else
                                {
                                    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                                    SqlCommand sqlCmd = new SqlCommand("sp_register", sqlConn);
                                    sqlCmd.CommandType = CommandType.StoredProcedure;
                                    sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                                    sqlCmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password1;
                                    sqlCmd.Parameters.Add("@VCode", SqlDbType.NVarChar).Value = Convert.ToString(Guid.NewGuid());
                                    sqlCmd.Parameters.Add("@InviteId", SqlDbType.Int).Value = inviteId;

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
                                }
                            }
                        }
                    }
                }
            }

            return new Tuple<int, string, int>(status, message, userId);
        }

        public Int16 firstName(int userId, string firstName)
        {
            Int16 status = 0;

            //check username valid
            Regex RgxUrl = new Regex("[^a-zA-Z]");
            if (Regex.Matches(firstName, @"[a-zA-Z]").Count == 0)
            {
                status = -2;
            }
            else
            {
                //special symbols
                RgxUrl = new Regex("[^a-zA-Z0-9]");

                if (RgxUrl.IsMatch(firstName))
                {
                    status = -3;
                }
                else
                {
                    //empty
                    if (firstName.Length == 0)
                    {
                        status = -4;
                    }
                    else
                    {
                        //ok
                        status = 1;

                        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                        SqlCommand sqlCmd = new SqlCommand("sp_settingsFirstnameSet", sqlConn);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        sqlCmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = firstName;

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
                    }
                }
            }

            return status;
        }

        public Int16 lastName(int userId, string lastName)
        {
            Int16 status = 0;

            //check username valid
            Regex RgxUrl = new Regex("[^a-zA-Z]");
            if (Regex.Matches(lastName, @"[a-zA-Z]").Count == 0)
            {
                status = -2;
            }
            else
            {
                //special symbols
                RgxUrl = new Regex("[^a-zA-Z0-9]");

                if (RgxUrl.IsMatch(lastName))
                {
                    status = -3;
                }
                else
                {
                    //empty
                    if (lastName.Length == 0)
                    {
                        status = -4;
                    }
                    else
                    {
                        status = 1;

                        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                        SqlCommand sqlCmd = new SqlCommand("sp_settingsLastnameSet", sqlConn);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        sqlCmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastName;

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
                    }
                }
            }

            return status;
        }
        
        public Int16 username(int userId, string username)
        {
            Int16 status = 0; //0 user entered it's own username -1 exists before -2 username not valid -3 no special char -4 enter username 1 successful

            //check username valid
            Regex RgxUrl = new Regex("[^a-zA-Z]");
            if (Regex.Matches(username, @"[a-zA-Z]").Count == 0)
            {
                status = -2;
            }
            else
            {
                //special symbols
                RgxUrl = new Regex("[^a-zA-Z0-9]");

                if (RgxUrl.IsMatch(username))
                {
                    status = -3;
                }
                else
                {
                    
                    //empty
                    if (username.Length == 0)
                    {
                        status = -4;
                    }
                    else
                    {
                        //check if it's own username
                        Classes.UserInfo ui = new Classes.UserInfo();
                        string currentUsername = ui.getUsernameByUserId(userId);

                        if (currentUsername == username)
                        {
                            status = 0;
                        }
                        else
                        {
                            //check username exists
                            bool usernameExists = ui.checkUsernameExists(username);

                            if (usernameExists)
                            {
                                status = -1;
                            }
                            else
                            {
                                status = 1;

                                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                                SqlCommand sqlCmd = new SqlCommand("sp_settingsUsernameSet", sqlConn);
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                                sqlCmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;

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
                            }
                        }
                    }
                }
            }

            return status;
        }

        public Int16 gender(int userId, Int16 gender)
        {
            Int16 status = 0;

            if (gender < 0 || gender > 3)
            {
                //out of range
                status = -1;
            }
            else
            {
                //ok
                status = 1;

                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd = new SqlCommand("sp_settingsGenderSet", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@Gender", SqlDbType.TinyInt).Value = gender;

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
            }

            return status;
        }

        public Int16 birthDate(int userId, string birthDate)
        {
            Int16 status = 0;
            DateTime birthDateValue = Convert.ToDateTime(birthDate);
            TimeSpan span = DateTime.Now.Subtract(birthDateValue);

            if (span.TotalDays < 6570)
            {
                //younger than 18
                status = -1;
            }
            else
            {
                //ok
                status = 1;

                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd = new SqlCommand("sp_settingsBirthdateSet", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@Birthdate", SqlDbType.SmallDateTime).Value = birthDateValue;

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
            }

            return status;
        }

        public Int16 about(int userId, string about)
        {
            Int16 status = 0;


            status = 1;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsAboutSet", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@About", SqlDbType.NVarChar).Value = about;

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

            return status;
        }

        public Int16 hasPhoto(int userId, bool hasPhoto)
        {
            Int16 status = 0;


            status = 1;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingPhotoSet", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@HasPhoto", SqlDbType.Bit).Value = hasPhoto;

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

            return status;
        }

        public Int16 cover(int userId, Int16 coverId)
        {
            Int16 status = 0;


            status = 1;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsCoverSet", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@CoverId", SqlDbType.SmallInt).Value = coverId;

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

            return status;
        }

        public Int16 location(int userId, int locatonId)
        {
            Int16 status = 0;

            status = 1;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsLocationSet", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = locatonId;

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

            return status;
        }

        public Int16 notifications(int userId, bool notifications)
        {
            Int16 status = 0;


            status = 1;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsNotificationsSet", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@Notifications", SqlDbType.Bit).Value = notifications;

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

            return status;
        }

        public Int16 mobile(int userId, Int64 mobile)
        {
            Int16 status = 0;


            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsMobileSet", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            sqlCmd.Parameters.Add("@Mobile", SqlDbType.BigInt).Value = mobile;

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

            return status;
        }

        public int verifyEmailSend(int userId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_verifyEmailGetCode", sqlConn);
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

            if (dt.Rows.Count == 0)
            {
                return 0; //no user found
            }
            else
            {
                string siteLink = ConfigurationManager.AppSettings["WebsiteLink"].ToString();
                string link = siteLink + "Verify/Email/" + (dt.Rows[0]["VerificationCode"].ToString());
                Classes.Mail m = new Classes.Mail();
                int status2 = m.sendMail("verifyemail", dt.Rows[0]["Email"].ToString(),
                    "Hi,<br/>To verify your email addrss please <a href='"
                    + link + "'>CLICK HERE</a><br/><br/>CityCowd Team", "");

                return 1;
            }
        }
    }


}