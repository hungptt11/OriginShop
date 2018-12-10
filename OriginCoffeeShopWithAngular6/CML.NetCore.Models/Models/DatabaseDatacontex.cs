using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CML.NetCore.Models.Models
{
    public class DatabaseDatacontex : DbContext
    {
        public DatabaseDatacontex(DbContextOptions<DatabaseDatacontex> options)
          : base(options) { }

        //public DbSet<Vocabularies> Vocabularies { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        
    }
}
