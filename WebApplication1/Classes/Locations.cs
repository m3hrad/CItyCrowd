using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Classes
{
    public class Locations
    {
        public DataTable getLocationInfoByCityId(int LocationId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationInfoByCityId", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;

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

            return dt;
        }

        public string getLocationInfoById(int LocationId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationInfoByCityId", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;

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

            return dt.Rows[0]["CountryName"].ToString() + " - " + dt.Rows[0]["CityName"].ToString();
        }

        public string locationInfoOnlyId(int LocationId)
        {
            string countryCode;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationInfoOnlyId", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;

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

            countryCode = dt.Rows[0]["CountryCode"].ToString();
            return countryCode;
        }

        public DataTable countriesList()
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_countries", sqlConn);
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

            return dt;
        }

        public DataTable citiesList(string countryCode)
        {
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationCities", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = countryCode;

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

            return dt;
        }

        public string citiesListValues(string countryCode)
        {
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationCities", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = countryCode;

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

            string result = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0) result = "<option value=" + dt.Rows[i]["LocationId"].ToString() + " selected>" + dt.Rows[i]["CityName"].ToString() + "</option>";
                if (i != 0) result = result + "<option value=" + dt.Rows[i]["LocationId"].ToString() + ">" + dt.Rows[i]["CityName"].ToString() + "</option>";
            }

            return result;
        }

        public Int16 request(int UserId, string Country, string City)
        {
            Int16 status = 1;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_locationRequestsAdd", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            sqlCmd.Parameters.Add("@Country", SqlDbType.NVarChar).Value = Country;
            sqlCmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = City;

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
                sqlConn.Dispose();
            //}

            return status;
        }

        public bool isApproved(int LocationId)
        {
            bool status = false;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationUsersCount", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
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
                int minLocationUsers = Convert.ToInt32(ConfigurationManager.AppSettings["MinLocationUsers"].ToString());
                int locationUsersCount = Convert.ToInt32(dt.Rows[0]["UsersCount"].ToString());

                if (locationUsersCount > minLocationUsers)
                {
                    status = true;
                }
            }

            return status;
        }

        public Tuple<int, string> locationUsersCount(int LocationId)
        {
            int usersCount = 0;
            string locationName = "";

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationUsersCount", sqlConn);

            //try
            //{
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
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
                usersCount = -1;
            }
            else
            {
                usersCount = Convert.ToInt32(dt.Rows[0]["UsersCount"].ToString());
                locationName = dt.Rows[0]["CountryName"].ToString() + " " + dt.Rows[0]["CityName"].ToString();
            }

            return new Tuple<int, string>(usersCount, locationName);
        }
    }
}