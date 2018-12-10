namespace CML.Models.Migrations
{
    using CML.Models.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CML.Models.Model.DatabaseDatacontex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;            
        }

        protected override void Seed(CML.Models.Model.DatabaseDatacontex context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.ProductCategories.AddOrUpdate(new ProductCategories
            {
                Id = 1,
                Name = "Hạt Cafe",
                MetaTitle = "hat-cafe",
                DisplayOrder = 1,
                ShowOnHome = true,
                Status = true,                
            });
            context.ProductCategories.AddOrUpdate(new ProductCategories
            {
                Id = 2,
                Name = "Chè Thơm",
                MetaTitle = "che-thom",
                DisplayOrder = 2,
                ShowOnHome = true,
                Status = true,
            });
        }
    }
}
