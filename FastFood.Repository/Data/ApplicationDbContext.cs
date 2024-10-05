using FastFood.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHeader> orderHeaders { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(
                  new Category { Id = 1, Title = "Burgers" },
                  new Category { Id = 2, Title = "Drinks" },
                  new Category { Id = 3, Title = "Desserts" }
                  );
            builder.Entity<SubCategory>().HasData(
                 new SubCategory { Id = 1, Title = "Beef Burgers", CategoryId = 1 },
                 new SubCategory { Id = 2, Title = "Chicken Burgers", CategoryId = 1 },
                 new SubCategory { Id = 3, Title = "Soft Drinks", CategoryId = 2 },
                 new SubCategory { Id = 4, Title = "Juices", CategoryId = 2 },
                 new SubCategory { Id = 5, Title = "Ice Cream", CategoryId = 3 },
                 new SubCategory { Id = 6, Title = "Cakes", CategoryId = 3 }
                   );
            builder.Entity<Item>().HasData(
                new Item { Id = 1, Title = "Cheeseburger", Description = "Juicy beef patty with cheese, lettuce, and tomato.", Price = 5.99, CategoryId = 1, SubCategoryId = 1 },
                new Item { Id = 2, Title = "Chicken Sandwich", Description = "Grilled chicken breast with lettuce and mayo.", Price = 4.99, CategoryId = 1, SubCategoryId = 2 },
                new Item { Id = 3, Title = "Cola", Description = "Chilled soft drink.", Price = 1.99, CategoryId = 2, SubCategoryId = 3 },
                new Item { Id = 4, Title = "Orange Juice", Description = "Freshly squeezed orange juice.", Price = 2.99, CategoryId = 2, SubCategoryId = 4 },
                new Item { Id = 5, Title = "Vanilla Ice Cream", Description = "Creamy vanilla-flavored ice cream.", Price = 3.50, CategoryId = 3, SubCategoryId = 5 },
                new Item { Id = 6, Title = "Chocolate Cake", Description = "Rich chocolate cake with a smooth frosting.", Price = 4.50, CategoryId = 3, SubCategoryId = 6 }
                );

        }
    }
}
