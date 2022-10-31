using Microsoft.EntityFrameworkCore;
using Belek.Services.Order.Domain.Models;

namespace Belek.Services.Order.Db
{
    public class OrderDbContext:DbContext
    {
        private const string DEFAULT_SCHEMA = "order";
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModel>().ToTable("Orders",DEFAULT_SCHEMA);
            modelBuilder.Entity<OrderItemModel>().ToTable("OrderItems", DEFAULT_SCHEMA);
            base.OnModelCreating(modelBuilder);
        }

    }
}