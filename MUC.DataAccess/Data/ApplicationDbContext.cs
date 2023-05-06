using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MUC.Models;
using System.ComponentModel;

namespace MUC.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<ProductMenu> ProductMenus { get; set; }
    
  
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //fixing entities so they won't be maxed
            builder.Entity<ApplicationUser>(u =>
            {
                u.Property(user => user.PhoneNumber)
                        .HasMaxLength(15);

                u.Property(user => user.PasswordHash)
                        .HasMaxLength(128);

                u.Property(user => user.ConcurrencyStamp)
                        .HasMaxLength(36);

                u.Property(user => user.SecurityStamp)
                        .HasMaxLength(36);

            });

            //setting up the Menu and dish table ProductMenu

            builder.Entity<ProductMenu>()
               .HasKey(di => new { di.ProductID, di.MenuID });

            builder.Entity<ProductMenu>()
                .HasOne(i => i.Menu)
                .WithMany(i => i.ProductMenus)
                .HasForeignKey(fk => fk.MenuID);

            builder.Entity<ProductMenu>()
                .HasOne(d => d.Product)
                .WithMany(i => i.ProductMenus)
                .HasForeignKey(fk => fk.ProductID);
        }
    }
}
