using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project_B_2022___FlagsQuiz.Data.DataAccess.MySQLDAO
{
    internal class MySQLUtils
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySqlFlagsQuizDB"].ConnectionString;
        private static readonly string LOGIN = "login";
        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public static void CloseQuietly(MySqlConnection conn)
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            catch { }
        }

        public static void CloseQuietly(MySqlDataReader reader)
        {
            try
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            catch { }
        }

        public static void CloseQuietly(MySqlDataReader reader, MySqlConnection conn)
        {
            CloseQuietly(reader);
            CloseQuietly(conn);
        }

        public static (bool, int) Login(string username, string password)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = LOGIN;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters["@username"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@pass", password);
                cmd.Parameters["@pass"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@login", MySqlDbType.Int32);
                cmd.Parameters["@login"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@playerId", MySqlDbType.Int32);
                cmd.Parameters["@playerId"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                bool login = !((int)cmd.Parameters["@login"].Value == 0);
                int id = login ? (int)cmd.Parameters["@playerId"].Value : -1;

                return (login, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                CloseQuietly(conn);
            }
        }
    }
}
