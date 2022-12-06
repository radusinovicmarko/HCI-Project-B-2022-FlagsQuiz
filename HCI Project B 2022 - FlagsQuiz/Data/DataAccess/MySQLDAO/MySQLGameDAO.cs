using HCI_Project_B_2022___FlagsQuiz.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project_B_2022___FlagsQuiz.Data.DataAccess.MySQLDAO
{
    internal class MySQLGameDAO : IGenericDAO<Game>
    {
        private static readonly string SELECT_ALL = @"SELECT * FROM `game` g 
                            INNER JOIN `player` p ON g.PLAYER_PlayerId=p.PlayerId WHERE true";
        private static readonly string INSERT = @"INSERT INTO `game`(NumberOfQuestions, Difficulty,
                            ElapsedTime, NumberOfCorrectAnswers, PLAYER_PlayerId) 
                            VALUES (@noQuestions, @difficulty, @time, @noCorrect, @playerId)";
        public int Add(Game item)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySQLUtils.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@noQuestions", item.NumberOfQuestions);
                cmd.Parameters.AddWithValue("@difficulty", item.Difficulty.ToString());
                cmd.Parameters.AddWithValue("@time", item.ElapsedTime);
                cmd.Parameters.AddWithValue("@noCorrect", item.NumberOfCorrectAnswers);
                cmd.Parameters.AddWithValue("@playerId", item.Player.PlayerId);
                cmd.ExecuteNonQuery();
                item.GameId = (int)cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                MySQLUtils.CloseQuietly(conn);
            }
            return item.GameId;
        }

        public List<Game> Get(Game item)
        {
            string selectQuery = SELECT_ALL;
            if (item.Player != null && item.Player.PlayerId != null)
                selectQuery += " AND g.PLAYER_PlayerId=@playerId";
            if (item.Difficulty != null)
                selectQuery += " AND g.Difficulty=@difficulty";
            if (item.NumberOfQuestions != null)
                selectQuery += " AND g.NumberOfQuestions=@noQuestions";
            selectQuery += " ORDER BY g.NumberOfCorrectAnswers DESC, g.ElapsedTime ASC";
            List<Game> result = new List<Game>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;
            try
            {
                conn = MySQLUtils.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = selectQuery;
                if (item.Player != null && item.Player.PlayerId != null)
                    cmd.Parameters.AddWithValue("@playerId", item.Player.PlayerId);
                if (item.Difficulty != null)
                    cmd.Parameters.AddWithValue("@difficulty", item.Difficulty.ToString());
                if (item.NumberOfQuestions != null)
                    cmd.Parameters.AddWithValue("@noQuestions", item.NumberOfQuestions);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    result.Add(Create(reader));
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

        public List<Game> GetAll()
        {
            List<Game> result = new List<Game>();
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
                    result.Add(Create(reader));
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

        private Game Create(MySqlDataReader reader)
        {
            return new Game()
            {
                GameId = reader.GetInt32(0),
                NumberOfQuestions = reader.GetInt32(1),
                Difficulty = (Difficulty?) Enum.Parse(typeof(Difficulty), reader.GetString(2)),
                ElapsedTime = reader.GetInt64(3),
                NumberOfCorrectAnswers = reader.GetInt32(4),
                Player = new Player()
                {
                    PlayerId = reader.GetInt32(6),
                    Username = reader.GetString(7)
                }
            };
        }
    }
}
