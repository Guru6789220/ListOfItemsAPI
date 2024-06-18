using Microsoft.EntityFrameworkCore;

namespace ListOfItems.DataAccess
{
    public class DBConnect:DbContext
    {
        public DBConnect(DbContextOptions options):base(options) { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
