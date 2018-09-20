using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace MobiOcean.MDM.DAL
{
    public class Search
    {
        private SqlConnection con;        

        string constr= ConfigurationManager.ConnectionStrings["MobiOcean.MDM.DAL.Properties.Settings.MySql"].ConnectionString;
        public DataSet SearchRecord(String query)
        {
            //fetch record without error message
            DataSet ds = new DataSet();
            try
            {
                GetConnection();
                SqlDataAdapter Ada = new SqlDataAdapter(query, con);
                Ada.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;

        }
        public int notifysrc(string query)
        {

            GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            return cmd.ExecuteNonQuery();
            
        }
        public void GetConnection()
        {
            // checking that the connection if made or not.... 
            if (con == null)
            {
                string conn = Properties.Settings.Default.MobiOceanConnectionString;
                con = new SqlConnection(conn);
            }

        }
        public void OpenConnection()
        {
            try
            {

                GetConnection();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }

        }
        public void CloseConnection()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }

        }
        //MySql
        public DataTable GetRecordsMySql(string Query)
        {
            //DataTable dt = new DataTable();
            //return dt;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand(Query))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
    }
}
