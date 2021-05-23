using System.Collections.Generic;
using SixtySixBitTest.Models;

namespace SixtySixBitTest.Repositories
{
    public interface IFootballRepository
    {
        public List<Footballer> GetFootballers();
        public Footballer GetFootballer(int id);
        public List<string> GetCommands();
        public void AddCommand(string command);
        public void AddOrUpdateFootballer(Footballer footballer);
    }
}