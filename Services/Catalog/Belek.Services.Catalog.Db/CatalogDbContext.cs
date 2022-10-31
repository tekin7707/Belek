using Belek.Services.Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Infrastructure
{
    public class CatalogDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "catalog";

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>().ToTable("Catalogs", DEFAULT_SCHEMA);
            modelBuilder.Entity<Category>().ToTable("Categories", DEFAULT_SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}