using Microsoft.EntityFrameworkCore;
using Tracker.Domain.Entities;
using Tracker.Infrastructure.Persistence.Configurations;
//using BCrypt.Net;

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
            
            // Seed Users (password "Password123!")
            modelBuilder.Entity<User>().HasData(
                new 
                { 
                    UserId = 1, 
                    Name = "Admin User", 
                    Email = "admin@tracker.com", 
                    PasswordHash = "$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq",
                    Role = UserRole.SuperAdmin,
                    IsActive = true
                },
                new 
                { 
                    UserId = 2, 
                    Name = "Juan Pérez", 
                    Email = "juan.perez@tracker.com", 
                    PasswordHash = "$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq",
                    Role = UserRole.Admin,
                    IsActive = true
                },
                new 
                { 
                    UserId = 3, 
                    Name = "María González", 
                    Email = "maria.gonzalez@tracker.com", 
                    PasswordHash = "$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq",
                    Role = UserRole.User,
                    IsActive = true
                },
                new 
                { 
                    UserId = 4, 
                    Name = "Carlos Ramírez", 
                    Email = "carlos.ramirez@tracker.com", 
                    PasswordHash = "$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq",
                    Role = UserRole.User,
                    IsActive = true
                }
            );
            
            modelBuilder.Entity<Customer>().HasData(
                new 
                { 
                    CustomerId = 1, 
                    Name = "Acme Corporation", 
                    DocmNumber = "20123456789", 
                    Email = "contacto@acme.com", 
                    Phone = "+51987654321", 
                    CustomerTypeId = 2 // VIP
                },
                new 
                { 
                    CustomerId = 2, 
                    Name = "TechStore SAC", 
                    DocmNumber = "20234567890", 
                    Email = "ventas@techstore.pe", 
                    Phone = "+51976543210", 
                    CustomerTypeId = 1 // Regular
                },
                new 
                { 
                    CustomerId = 3, 
                    Name = "Global Imports EIRL", 
                    DocmNumber = "20345678901", 
                    Email = "info@globalimports.com", 
                    Phone = "+51965432109", 
                    CustomerTypeId = 2 // VIP
                },
                new 
                { 
                    CustomerId = 4, 
                    Name = "Pedro Sánchez", 
                    DocmNumber = "12345678", 
                    Email = "pedro.sanchez@email.com", 
                    Phone = "+51954321098", 
                    CustomerTypeId = 1 // Regular
                },
                new 
                { 
                    CustomerId = 5, 
                    Name = "Distribuidora Lima SAC", 
                    DocmNumber = "20456789012", 
                    Email = "ventas@distrilima.pe", 
                    Phone = "+51943210987", 
                    CustomerTypeId = 1 // Regular
                },
                new 
                { 
                    CustomerId = 6, 
                    Name = "Ana Torres", 
                    DocmNumber = "23456789", 
                    Email = "ana.torres@email.com", 
                    Phone = "+51932109876", 
                    CustomerTypeId = 1 // Regular
                }
            );
            
            modelBuilder.Entity<Shipment>().HasData(
                new 
                { 
                    ShipmentId = 1,
                    CustomerId = 1,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-001",
                    ShippedAt = new DateTime(2024, 12, 20, 10, 30, 0, DateTimeKind.Utc),
                    Description = "Equipos de cómputo - 5 laptops Dell",
                    Destination = "Av. Arequipa 1234, Miraflores, Lima",
                    ShipmentStatusId = 5, // Entregado
                    Receivedby = "Roberto Mendoza",
                    ReceivedAt = new DateTime(2024, 12, 22, 14, 15, 0, DateTimeKind.Utc)
                },
                new 
                { 
                    ShipmentId = 2,
                    CustomerId = 2,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-002",
                    ShippedAt = new DateTime(2024, 12, 23, 8, 0, 0, DateTimeKind.Utc),
                    Description = "Smartphones Samsung Galaxy - 10 unidades",
                    Destination = "Jr. Lampa 456, Cercado de Lima, Lima",
                    ShipmentStatusId = 3, // En Transito
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 3,
                    CustomerId = 3,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-003",
                    ShippedAt = new DateTime(2024, 12, 24, 9, 15, 0, DateTimeKind.Utc),
                    Description = "Componentes electrónicos importados",
                    Destination = "Av. Industrial 789, Callao",
                    ShipmentStatusId = 4, // LlegadaDestino
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 4,
                    CustomerId = 4,
                    UserId = 4,
                    TrackingNumber = "TRK-2024-004",
                    ShippedAt = new DateTime(2024, 12, 25, 11, 45, 0, DateTimeKind.Utc),
                    Description = "Paquete personal - Documentos",
                    Destination = "Calle Los Robles 321, San Isidro, Lima",
                    ShipmentStatusId = 2, // Empaquetado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 5,
                    CustomerId = 5,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-005",
                    ShippedAt = new DateTime(2024, 12, 26, 7, 30, 0, DateTimeKind.Utc),
                    Description = "Productos de limpieza - 50 cajas",
                    Destination = "Av. Venezuela 555, Breña, Lima",
                    ShipmentStatusId = 1, // Habilitado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 6,
                    CustomerId = 1,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-006",
                    ShippedAt = new DateTime(2024, 12, 18, 13, 0, 0, DateTimeKind.Utc),
                    Description = "Impresoras HP LaserJet - 3 unidades",
                    Destination = "Av. Arequipa 1234, Miraflores, Lima",
                    ShipmentStatusId = 5, // Entregado
                    Receivedby = "Roberto Mendoza",
                    ReceivedAt = new DateTime(2024, 12, 19, 16, 30, 0, DateTimeKind.Utc)
                },
                new 
                { 
                    ShipmentId = 7,
                    CustomerId = 6,
                    UserId = 4,
                    TrackingNumber = "TRK-2024-007",
                    ShippedAt = new DateTime(2024, 12, 15, 10, 0, 0, DateTimeKind.Utc),
                    Description = "Paquete personal - Ropa",
                    Destination = "Av. Javier Prado 999, San Borja, Lima",
                    ShipmentStatusId = 6, // Devuelto
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 8,
                    CustomerId = 3,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-008",
                    ShippedAt = new DateTime(2024, 12, 26, 14, 20, 0, DateTimeKind.Utc),
                    Description = "Repuestos automotrices - Caja grande",
                    Destination = "Av. Industrial 789, Callao",
                    ShipmentStatusId = 3, // En Tránsito
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                }
            );
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
