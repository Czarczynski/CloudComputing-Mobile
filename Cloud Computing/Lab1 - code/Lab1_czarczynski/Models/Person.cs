using System;
namespace Lab1_czarczynski.Models
{

    public class PersonDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
    public class Person : PersonDTO
    {
        public int PersonId { get; set; }
    }
}
