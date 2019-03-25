using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace HandleHouse.Data.Models
{
    public class Work
    {
        [Key]
        public int Id { get; set; }
        public TypeHelper.WorkType WorkType { get; set; }
        public string OptionalDescription { get; set; }
        public int Cost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual House House { get; set; }

        public Work(TypeHelper.WorkType type, int cost, DateTime start, DateTime end, House house, string description = "")
        {
            WorkType = type;
            Cost = cost;
            StartDate = start;
            EndDate = end;
            House = house;
            OptionalDescription = description;
        }

        public Work()
        {
            
        }
    }
}
