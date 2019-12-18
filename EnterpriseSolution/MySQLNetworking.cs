using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

//Possibly return some type of value to check if the person is able to login (if record exists), or if the person logged in wrong\
//Could also break this down even farther to specify the connstring

namespace EnterpriseSolution
{
    public static class MySQLNetworking
    {
        public static void login(string username, string password)
        {
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
                while (dr.Read())
                {
                    string usernameFromMySQL = (string)dr["username"];
                    string passwordFromMySQL = (string)dr["password"];
                    string secondUsernameFromMySQL = (string)dr["username"];
                }
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
        //Need to make a "Confirm email" or confirmation screen to show successfully registered
        public static void Registration(string username, string password, string email)
        {
            string connstring = @"server=localhost;uid=root;pwd=password;database=enterprisesolution;Charset=utf8";
            MySqlConnection conn = null;
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = connstring;
                conn.Open();

                //INSERT mysql command here to register
                MySqlCommand cmd = new MySqlCommand("INSERT INTO login VALUES ('" + username + "', '" + email + "', '" + password + "')", conn);
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
