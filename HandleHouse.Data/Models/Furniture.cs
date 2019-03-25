using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace HandleHouse.Data.Models
{
    public class Furniture
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public DateTime SetDate { get; set; }
        public House House { get; set; }

        public Furniture(string name, int cost, DateTime set, House house)
        {
            Name = name;
            Cost = cost;
            SetDate = set;
            House = house;
        }

        public Furniture()
        {
            
        }
    }
}
