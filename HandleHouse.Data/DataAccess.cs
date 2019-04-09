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
        public static List<Work> GetWorks(HouseContext db)
        {
            return db.Works.Include("House.Settlement").ToList();
        }

        public static List<House> GetHouses()
        {
            List<House> result = new List<House>();
            using (HouseContext db = new HouseContext())
            {
                result = db.Houses.Include("Settlement").ToList();
            }

            return result;
        }

        public static List<Settlement> GetSettlements(HouseContext db)
        {
            return db.Settlements.ToList();
        }

        public static List<Furniture> GetFurniture()
        {
            List<Furniture> result = new List<Furniture>();
            using (HouseContext db = new HouseContext())
            {
                result = db.Furniture.Include("House.Settlement").ToList();
            }

            return result;
        }

        public static List<Owner> GetOwners()
        {
            List<Owner> result = new List<Owner>();
            using (HouseContext db = new HouseContext())
            {
                result = db.Owners.Include("House.Settlement").ToList();
            }

            return result;
        }

        public static List<Person> GetPeople()
        {
            List<Person> result = new List<Person>();
            using (HouseContext db = new HouseContext())
            {
                result = db.People.Include("House.Settlement").ToList();
            }

            return result;
        }

        public static Owner GetOwnerOfHouse(House house, HouseContext db)
        {
            return db.Owners.Include("House").FirstOrDefault();
        }
    }
}
