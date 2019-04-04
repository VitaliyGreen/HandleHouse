using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandleHouse.Data.Models
{
    public class House
    {
        public int Number { get; set; }
        [Key]
        public string TechnicalPassportNumber { get; set; }
        public int RoomNumber { get; set; }
        public int Area { get; set; }
        public string Street { get; set; }
        public Settlement Settlement { get; set; }

        public House(int number, string passNum, int roomNumber, int area, string street, Settlement settlement)
        {
            Number = number;
            TechnicalPassportNumber = passNum;
            RoomNumber = roomNumber;
            Area = area;
            Street = street;
            Settlement = settlement;
        }

        public House()
        {
            
        }

        public string FullAddress => $"{Settlement.Name}, {Street}, {Number}";
    }
}
