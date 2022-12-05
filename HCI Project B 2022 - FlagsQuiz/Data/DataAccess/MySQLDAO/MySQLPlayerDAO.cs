using HCI_Project_B_2022___FlagsQuiz.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project_B_2022___FlagsQuiz.Data.DataAccess.MySQLDAO
{
    internal class MySQLPlayerDAO : IGenericDAO<Player>
    {
        private static readonly string SELECT_ALL = @"SELECT p.PlayerId, p.Username FROM `Player` p WHERE true";
        private static readonly string INSERT = @"INSERT INTO `Player`(Username, Password) VALUES (@Username, @Password)";

        public int Add(Player item)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySQLUtils.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@Username", item.Username);
                cmd.Parameters.AddWithValue("@Password", item.Password);
                cmd.ExecuteNonQuery();
                item.PlayerId = (int)cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                MySQLUtils.CloseQuietly(conn);
            }
            return (int)item.PlayerId;
        }

        public List<Player> Get(Player item)
        {
            string selectQuery = SELECT_ALL;
            if (item.PlayerId != null)
                selectQuery += " AND p.PlayerId=@playerId";
            List<Player> result = new List<Player>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySQLUtils.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = selectQuery;
                if (item.PlayerId != null)
                    cmd.Parameters.AddWithValue("@playerId", item.PlayerId);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    result.Add(new Player()
                    {
                        PlayerId = reader.GetInt32(0),
                        Username = reader.GetString(1)
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                MySQLUtils.CloseQuietly(reader, conn);
            }
            return result;
        }

        public List<Player> GetAll()
        {
            List<Player> result = new List<Player>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;
            try
            {
                conn = MySQLUtils.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT_ALL;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Player()
                    {
                        PlayerId = reader.GetInt32(0),
                        Username = reader.GetString(1)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                MySQLUtils.CloseQuietly(reader, conn);
            }
            return result;
        }
    }
}
