using System.Collections.Generic;
using System.Reflection.Emit;
using API_Portfolio.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Portfolio.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Example table
        public DbSet<Users> Users { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StockIn> StockIns { get; set; }

        public DbSet<StockInDetail> StockInDetails { get; set; }

        public DbSet<StockOut> StockOuts { get; set; }

        public DbSet<StockOutDetail> StockOutDetails { get; set; }
        //public DbSet<Users> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primary key setup (optional)
            modelBuilder.Entity<Users>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Stock>()
                .HasKey(u => u.StockId);

            modelBuilder.Entity<StockIn>()
                .HasKey(u => u.StockInId);

            modelBuilder.Entity<StockInDetail>()
                .HasKey(u => u.StockInDetailId);

            modelBuilder.Entity<StockOut>()
                .HasKey(u => u.StockOutId);

            modelBuilder.Entity<StockOutDetail>()
                .HasKey(u => u.StockOutDetailId);
        }
    }
    
}
