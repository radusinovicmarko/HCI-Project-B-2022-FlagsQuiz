using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project_B_2022___FlagsQuiz.Data.Model
{
    internal class Flag
    {
        public string Png { get; set; }
        public string Svg { get; set; } 
    }

    internal class Country
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public Flag Flags { get; set; }
    }
}
