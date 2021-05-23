using System.Collections.Generic;

namespace SixtySixBitTest.Models
{
    public class FootballModel
    {
        public Footballer Footballer { get; set; }
        public List<string> Commands { get; set; }
        public string SelectedCommand { get; set; }
        public string WrittenCommand { get; set; }
    }
}