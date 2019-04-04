using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using HandleHouse.Data.Models;

namespace HandleHouse.Data
{
    abstract class Program
    {
        static void Main(string[] args)
        {
            //using (HouseContext db = new HouseContext())
            //{
            //    List<Work> works = db.Works
            //        .Include(w => w.House)
            //        .ToList();
            //    foreach (var work in works)
            //    {
            //        Console.WriteLine("{0} {1} {2} {3} - {4} {5}", work.WorkType, work.OptionalDescription, work.Cost, work.StartDate, work.EndDate, work.House.TechnicalPassportNumber);
            //    }
            //}

            var works = DataAccess.GetWorks(new HouseContext());
            Console.WriteLine(works[0].House.RoomNumber);

            Console.ReadKey();
        }
    }
}
