using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleHouse.Data.Models
{
    public class Owner : Person
    {
        [Key]
        public string PassportNumber { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Owner(string passport, string idNumber, string phone, string email, Person person)
            : base(person.FirstName, person.LastName, person.Patronymic, person.Birthday, person.House, person.Sex)
        {
            PassportNumber = passport;
            IdNumber = idNumber;
            PhoneNumber = phone;
            Email = email;
        }

        public Owner()
        {
            
        }
    }
}
