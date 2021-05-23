using Microsoft.EntityFrameworkCore;
using SixtySixBitTest.Models;

namespace SixtySixBitTest.Data
{
    public class FootballerContext : DbContext
    {
        public FootballerContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Footballer> Footballers { get; set; }
        public DbSet<Command>Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity(typeof(Footballer))
                .Property(typeof(int),"Id")
                .ValueGeneratedOnAdd();
            
            builder.Entity(typeof(Command))
                .Property(typeof(int),"Id")
                .ValueGeneratedOnAdd();
            
            base.OnModelCreating(builder);
        }
    }
}