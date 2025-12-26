using Microsoft.EntityFrameworkCore;
using Tracker.Domain.Entities;
using Tracker.Infrastructure.Persistence.Configurations;

namespace Tracker.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentStatus> ShipmentStatuses { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ShipmentConfiguration());
            modelBuilder.ApplyConfiguration(new ShipmentStatusConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerTypeConfiguration());


            modelBuilder.Entity<ShipmentStatus>().HasData(
                new { ShipmentStatusId = 1, Description = "Habilitado" },
                new { ShipmentStatusId = 2, Description = "Empaquetado" },
                new { ShipmentStatusId = 3, Description = "En Tránsito" },
                new { ShipmentStatusId = 4, Description = "LlegadaDestino" },
                new { ShipmentStatusId = 5, Description = "Entregado" },
                new { ShipmentStatusId = 6, Description = "Devuelto" }
            );

            modelBuilder.Entity<CustomerType>().HasData(
                new { CustomerTypeId = 1, Description = "regular" },
                new { CustomerTypeId = 2, Description = "vip" }
            );




            base.OnModelCreating(modelBuilder);
        }
    }
}
