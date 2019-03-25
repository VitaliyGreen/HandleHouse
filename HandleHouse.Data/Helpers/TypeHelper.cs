using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleHouse.Data
{
    public static class TypeHelper
    {
        public enum SettlementType
        {
            Village,
            Town,
            City,
            UrbanType
        }

        public enum WorkType
        {
            SettingUp,         // монтаж
            Dismantling,       // демонтаж
            Cleaning,          // очищення, прибирання
            Purchase,          // закупівля
            Painting,          // фарбування 
            Restoration,       // реставрація
            Changing           // заміна

        }

        public enum Sex
        {
            Male,
            Female
        }
    }
}
