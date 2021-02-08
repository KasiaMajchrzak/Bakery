using Bakery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Bakery.DAO
{
    public class DBContext : DbContext
    {
        protected static IConfigurationRoot config;

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables().Build();
        } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Additional>().ToTable("Additional");
            modelBuilder.Entity<BaseProduct>().ToTable("BaseProduct");
            modelBuilder.Entity<Cake>().ToTable("Cake");
            modelBuilder.Entity<Cream>().ToTable("Cream");
            modelBuilder.Entity<Decoration>().ToTable("Decoration");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrdersAdditionals>().ToTable("OrdersAdditionals");
            modelBuilder.Entity<OrdersDecorations>().ToTable("OrdersDecorations");
        }

        public DbSet<Additional> Additional { get; set; }
        public DbSet<BaseProduct> BaseProduct { get; set; }
        public DbSet<Cake> Cake { get; set; }
        public DbSet<Cream> Cream { get; set; }
        public DbSet<Decoration> Decoration { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrdersAdditionals> OrdersAdditionals { get; set; }
        public DbSet<OrdersDecorations> OrdersDecorations { get; set; }
    }
}
