using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace HandleHouse.Data.Models
{
    public class Settlement
    {
        [Key]
        public int Id { get; set; }
        public TypeHelper.SettlementType SettlementType { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string District { get; set; }

        public Settlement(TypeHelper.SettlementType type, string name, string region, string district)
        {
            SettlementType = type;
            Name = name;
            Region = region;
            District = district;
        }

        public Settlement()
        {
            
        }
    }
}
