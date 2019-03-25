using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandleHouse.Data;
using HandleHouse.Data.Models;

namespace DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HouseContext db = new HouseContext())
            {
               SomeClass c1 = new SomeClass("vitaliy", Males.Male, "Roz");
                db.Classes.Add(c1);
                db.SaveChanges();
                List<SomeClass> classes = db.Classes.ToList();
                Console.WriteLine(classes[0].Name);
            }

        }
    }
}
