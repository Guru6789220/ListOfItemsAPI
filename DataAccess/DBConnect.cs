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
                .HasForeignKey(u=>u.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemList>().HasOne(u=>u.subCategory)
                .WithMany()
                .HasForeignKey(u=>u.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserDetails>().HasOne(r => r.Roles)
                .WithMany()
                .HasForeignKey(f => f.ConsumerType)
                .OnDelete(DeleteBehavior.Restrict);
          
        }
        
        public DbSet<Category> Category_Master { get; set; }
        public DbSet<SubCategory> SubCategory_Master { get; set; }

        public DbSet<ItemList> ItemMaster { get; set; }

        public DbSet<Roles> Roles_Master { get; set;}

        public DbSet<UserDetails> UserDetails { get; set; }
    }
}
