using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}
//public class AppDbContext : DbContext
//{
//        public DbSet<Order> Orders  => Set<Order>();
//        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
//        {
//        }
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Order>()
//                .HasKey(o => o.Id);
//            modelBuilder.Entity<Order>()
//                .Property(o => o.Id)
//                .ValueGeneratedOnAdd();
//        }
//}
