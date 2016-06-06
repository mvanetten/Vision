using System.Data.Entity;
using Vision.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using Vision.Domain.Attributes;

namespace Vision.Domain.DAL
{
    public class DBVisionContext : DbContext
    {
        public DBVisionContext() : base("DBVisionContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentLine> DocumentLines { get; set; }
        public DbSet<Tax> Taxs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SettingEmail> SettingEmail { get; set; }
        public DbSet<Setting> Setting { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }



    }
}
