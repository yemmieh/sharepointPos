using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class POSDBContext : DbContext
    {
        public POSDBContext(string connectionString) : base(connectionString) { }

        public DbSet<POSRequest> POSRequest { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Terminal> Terminal { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<MerchantUpdate> MerchantUpdate { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors.SelectMany(c => c.ValidationErrors).Select(c => c.ErrorMessage);
                var fullMsg = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullMsg);
                throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
