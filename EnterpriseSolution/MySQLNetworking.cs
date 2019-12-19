using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EnterpriseSolution
{
    public static class MySQLNetworking
    {
        public static bool login(string username, string password)
        {
            bool loginStatus = false;
            //Line 99 will mess up: Charset not found in key
            string connstring = @"server=localhost;uid=root;pwd=password;database=enterprisesolution;CharSet=utf8";
            MySqlConnection conn = null;
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = connstring;
                conn.Open();

                //login to MySQL
                MySqlCommand cmd = new MySqlCommand("SELECT username, password FROM login l WHERE l.username = '" + username + "' AND l.password = '" + password + "'", conn);
                MySqlDataReader dr = cmd.ExecuteReader(); //This line messes up if your charset in MySQL isn't set to UTF8

                if (dr.Read() == true)
                {
                    loginStatus = true;
                }
                else if (dr.Read() == false)
                {
                    loginStatus = false;
                }

/*                while (dr.Read())
                {
                    string usernameFromMySQL = (string)dr["username"];
                    string passwordFromMySQL = (string)dr["password"];
                }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                
            }
            return loginStatus; //this needs to be here and notinside the checks, early returns
        }
        public static void Registration(string username, string password, string email)
        {
            string connstring = @"server=localhost;uid=root;pwd=password;database=enterprisesolution;Charset=utf8";
            MySqlConnection conn = null;
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = connstring;
                conn.Open();
                string dt = DateTime.Now.ToString("yyyy-MM-dd");

                MySqlCommand cmd = new MySqlCommand("INSERT INTO login VALUES ('" + username + "', '" + email + "', '" + password + "', '" + dt +"', null)", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public static void UpdateLoginDate(string username, string password)
        {
            string connstring = @"server=localhost;uid=root;pwd=password;database=enterprisesolution;Charset=utf8";
            MySqlConnection conn = null;
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = connstring;
                conn.Open();

                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                MySqlCommand cmd = new MySqlCommand("UPDATE login SET last_logged_on = '" + dt + "' WHERE username = '" + username + "' AND password = '" + password + "'", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        
    }
}
