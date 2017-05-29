using Model.Registers;
using Model.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Persistence.Contexts
{
   
    public class EFContext : DbContext

{
    public EFContext() : base("Asp_Net_MVC_CS") { Database.SetInitializer<EFContext>
                (new DropCreateDatabaseIfModelChanges<EFContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.
            Remove<PluralizingTableNameConvention>();
        }


        public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers{ get; set; }
    public DbSet<Product> Products { get; set; }
    }

   
}
  