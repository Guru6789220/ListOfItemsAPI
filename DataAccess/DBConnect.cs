using ListOfItems.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ListOfItems.DataAccess
{
    public class DBConnect:DbContext
    {
        public DBConnect(DbContextOptions options):base(options) { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ItemList>().HasOne(u=>u.Category)
                .WithMany()
                .HasForeignKey(u=>u.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemList>().HasOne(u=>u.SubCategory)
                .WithMany()
                .HasForeignKey(u=>u.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Category> Category_Master { get; set; }
        public DbSet<SubCategory> SubCategory_Master { get; set; }

        public DbSet<ItemList> ItemMaster { get; set; }
    }
}
