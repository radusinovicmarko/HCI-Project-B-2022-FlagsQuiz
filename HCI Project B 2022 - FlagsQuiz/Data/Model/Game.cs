using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project_B_2022___FlagsQuiz.Data.Model
{
    internal enum Difficulty
    {
        Medium, Hard
    }

    internal class Game
    {
        public int GameId { get; set; }
        public int? NumberOfQuestions { get; set; } = null;
        public Difficulty? Difficulty { get; set; } = null;
        public long ElapsedTime { get; set; }
        public int NumberOfCorrectAnswers { get; set; } = 0;
        public Player Player { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Game game &&
                   GameId == game.GameId;
        }

        public override int GetHashCode()
        {
            return 354631258 + GameId.GetHashCode();
        }
    }
}
