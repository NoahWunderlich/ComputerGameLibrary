using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGameLibrary
{
    public class Game
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Summary { get; set; }
        public string Genre { get; set; }
        public string ReleaseDate { get; set; }
        public string ReleaseYear { get; set; }
        public string UserReview { get; set; }
        public int MetaScore  { get; set; }
        public string RawLine { get; set; }
        public string OwnReview { get; set; }
        public int OwnScore { get; set; }
        public string CoverSource { get; set; }

    }
}
