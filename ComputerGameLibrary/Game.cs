using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGameLibrary
{
    class Game
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Platform { get; set; }
        public string Summary { get; set; }
        public string Genre { get; set; }
        public string[] Genres { get; set; }
        public bool Online { get; set; }

    }
}
