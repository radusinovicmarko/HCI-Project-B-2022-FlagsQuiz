using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project_B_2022___FlagsQuiz.Data.Model
{
    internal class Player
    {
        public int? PlayerId { get; set; } = null;
        public string Username { get; set; }
        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Player player &&
                   PlayerId == player.PlayerId;
        }

        public override int GetHashCode()
        {
            return 956575109 + PlayerId.GetHashCode();
        }
    }
}
