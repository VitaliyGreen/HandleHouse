using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandleHouse.Data.Models;
using System.Data.Entity;

namespace HandleHouse.Data
{
    public static class DataAccess
    {
        public static List<Work> GetWorks()
        {
            List<Work> result = new List<Work>();
            using (HouseContext db = new HouseContext())
            {
                result = db.Works.ToList();
            }

            return result;
        }
    }
}
