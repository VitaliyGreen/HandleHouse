using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandleHouse.Data.Helpers;
using HandleHouse.Data.Models;

namespace HandleHouse.Data
{
    public class HouseContext : DbContext
    {
        public HouseContext()
            : base("DbConnection")
        {
            
        }

        static HouseContext()
        {
            Database.SetInitializer<HouseContext>(new DatabaseInitializer());
        }

        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}
