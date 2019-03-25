using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandleHouse.Data.Models;

namespace DataAccess
{
    class HouseContext : DbContext
    {
        public HouseContext()
        : base("DbConnection")
        { }

        public DbSet<SomeClass> Classes { get; set; }
    }
}
