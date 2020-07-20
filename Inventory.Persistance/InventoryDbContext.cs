using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Inventory.Persistance
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Prodcut> Prodcuts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
