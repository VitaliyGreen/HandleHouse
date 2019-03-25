using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace HandleHouse.Data.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public House House { get; set; }
        public TypeHelper.Sex Sex { get; set; }

        public Person(string first, string last, string patronymic, DateTime birthday, House house, TypeHelper.Sex sex)
        {
            FirstName = first;
            LastName = last;
            Birthday = birthday;
            House = house;
            Patronymic = patronymic;
        }

        public Person()
        {
            
        }

        public string FullName
        {
            get { return $"{LastName}  {FirstName} {Patronymic}"; }
        }

        public string ShortFullName
        {
            get { return $"{LastName} {FirstName[0]}. {Patronymic[0]}."; }
        }
    }
}
