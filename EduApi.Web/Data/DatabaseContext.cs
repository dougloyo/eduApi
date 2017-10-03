using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EduApi.Web.Models;

namespace EduApi.Web.Data
{
    // TODO: Move to its own project. EduApi.Data 
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("connStr") { }

        // Define Db sets.
        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}