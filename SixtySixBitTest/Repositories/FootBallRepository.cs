using System.Collections.Generic;
using System.Linq;
using SixtySixBitTest.Data;
using SixtySixBitTest.Models;

namespace SixtySixBitTest.Repositories
{
    public class FootBallRepository : IFootballRepository
    {
        private readonly FootballerContext _contextFactory;

        public FootBallRepository(FootballerContext contextFactory)
        {
            _contextFactory = contextFactory;
        }
        
        public List<Footballer> GetFootballers()
        {
            return _contextFactory.Footballers.ToList();
        }
        
        public Footballer GetFootballer(int id)
        {
            return _contextFactory.Footballers.FirstOrDefault(f => f.Id.Equals(id));
        }

        public List<string> GetCommands()
        {
            return _contextFactory.Commands.Select(c => c.Name).ToList();
        }

        public void AddCommand(string command)
        {
            _contextFactory.Commands.Add(new Command() {Name = command});
            _contextFactory.SaveChanges();
        }

        public void AddOrUpdateFootballer(Footballer footballer)
        {
            if (Enumerable.Contains(_contextFactory.Footballers, footballer))
                return;
            var oldFootballer = _contextFactory.Footballers.FirstOrDefault(f => f.Id.Equals(footballer.Id));
            if (oldFootballer is not null)
                _contextFactory.Footballers.Remove(oldFootballer);
            footballer.Id = 0;
            _contextFactory.Footballers.Add(footballer);
            if(!GetCommands().Contains(footballer.Command))
                AddCommand(footballer.Command);
            _contextFactory.SaveChanges();
        }
    }
}