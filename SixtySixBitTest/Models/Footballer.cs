using System;
using System.ComponentModel.DataAnnotations;

namespace SixtySixBitTest.Models
{
    public class Footballer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public Country Country { get; set; }
        public DateTime BirthDateTime { get; set; }
        public string Command { get; set; }
        
        public override bool Equals(object? obj)
        {
            if (obj is not Footballer)
                return false;
            var otherFootballer = (Footballer) obj;
            return FirstName.Equals(otherFootballer.FirstName) &&
                   Lastname.Equals(otherFootballer.Lastname) &&
                   Gender == otherFootballer.Gender &&
                   Country == otherFootballer.Country &&
                   Command.Equals(otherFootballer.Command);
        }
    }
}