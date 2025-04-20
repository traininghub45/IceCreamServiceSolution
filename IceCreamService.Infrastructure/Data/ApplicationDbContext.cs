using IceCreamService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IceCreamService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
     
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var configurations = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connStr = configurations.GetSection("DefaultConnection").Value;
            optionsBuilder.UseSqlServer(connStr);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasColumnName("PasswordHash");
            });
        }
    }
}

