using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EduApi.Web.Models;

namespace EduApi.Web.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("connStr") { }

        // Define Db sets.
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}