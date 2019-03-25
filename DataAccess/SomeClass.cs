using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public enum Males
    {
        Male,
        Female
    }

    class SomeClass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Males Male { get; set; }
        public string Region { get; set; }

        public SomeClass(string name, Males male, string region)
        {
            Name = name;
            Male = male;
            Region = region;
        }

        public SomeClass()
        {
            
        }
    }
}
