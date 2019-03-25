using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using HandleHouse.Data.Helpers;

namespace HandleHouse.Data
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<HouseContext>
    {
        protected override void Seed(HouseContext db)
        {
            Console.WriteLine("Seed initializer is called");
            DataHelper data = new DataHelper();
            db.Settlements.AddRange(data.Settlements);
            db.Houses.AddRange(data.Houses);
            db.Works.AddRange(data.GetWorks());
            db.Furniture.AddRange(data.GetAllFurniture());
            db.People.AddRange(data.GetPeople());
            db.Owners.AddRange(data.GetOwners());

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }

        }
    }
}
