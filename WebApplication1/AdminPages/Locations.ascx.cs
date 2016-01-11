using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.AdminPages
{
    public partial class Locations : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check permissions
            Classes.Admins a = new Classes.Admins();
            if (!a.permissions(Convert.ToInt32(Session["UserId"]), "Locations"))
            {
                Response.Redirect("~/Error/404");
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_adminLocationAdd", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = TextBoxCountryCode.Text;
            sqlCmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = TextBoxCountryName.Text;
            sqlCmd.Parameters.Add("@CityName", SqlDbType.VarChar).Value = TextBoxCityName.Text;
            sqlCmd.Parameters.Add("@Population", SqlDbType.Int).Value = Convert.ToInt32(TextBoxPopulation.Text);

            int locationId = 0;
            //try
            //{
                sqlConn.Open();
                locationId = Convert.ToInt32(sqlCmd.ExecuteScalar());

                LabelAddMessage.Visible = true;
                LabelAddMessage.Text = "Location has been added. LocationId: " + locationId.ToString();
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

        protected void ButtonNotification_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(TextBoxUserId.Text);
            int locationId = Convert.ToInt32(TextBoxLocationId.Text);
            Classes.Notifications n = new Classes.Notifications();
            n.notificationAdd(userId, 14, locationId);
        }
    }
}