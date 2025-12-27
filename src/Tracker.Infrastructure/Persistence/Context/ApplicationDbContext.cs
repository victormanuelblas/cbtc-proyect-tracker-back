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
            
            // Seed Users (password "password")
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
                },

                new 
                { 
                    ShipmentId = 9,
                    CustomerId = 2,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-009",
                    ShippedAt = new DateTime(2024, 12, 26, 15, 0, 0, DateTimeKind.Utc),
                    Description = "Accesorios de red - Cables UTP Cat6",
                    Destination = "Jr. Lampa 456, Cercado de Lima, Lima",
                    ShipmentStatusId = 1, // Habilitado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 10,
                    CustomerId = 5,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-010",
                    ShippedAt = new DateTime(2024, 12, 27, 9, 30, 0, DateTimeKind.Utc),
                    Description = "Papel Bond A4 - 100 millares",
                    Destination = "Av. Venezuela 555, Breña, Lima",
                    ShipmentStatusId = 2, // Empaquetado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 11,
                    CustomerId = 3,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-011",
                    ShippedAt = new DateTime(2024, 12, 24, 16, 45, 0, DateTimeKind.Utc),
                    Description = "Tarjetas gráficas NVIDIA RTX - 5 unidades",
                    Destination = "Av. Industrial 789, Callao",
                    ShipmentStatusId = 3, // En Tránsito
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 12,
                    CustomerId = 1,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-012",
                    ShippedAt = new DateTime(2024, 12, 22, 11, 0, 0, DateTimeKind.Utc),
                    Description = "Servidores Rack 1U - 2 unidades",
                    Destination = "Av. Arequipa 1234, Miraflores, Lima",
                    ShipmentStatusId = 4, // LlegadaDestino
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 13,
                    CustomerId = 4,
                    UserId = 4,
                    TrackingNumber = "TRK-2024-013",
                    ShippedAt = new DateTime(2024, 12, 20, 14, 20, 0, DateTimeKind.Utc),
                    Description = "Zapatillas deportivas - Regalo",
                    Destination = "Calle Los Robles 321, San Isidro, Lima",
                    ShipmentStatusId = 5, // Entregado
                    Receivedby = "Carlos Ramírez",
                    ReceivedAt = new DateTime(2024, 12, 21, 10, 15, 0, DateTimeKind.Utc)
                },
                new 
                { 
                    ShipmentId = 14,
                    CustomerId = 6,
                    UserId = 4,
                    TrackingNumber = "TRK-2024-014",
                    ShippedAt = new DateTime(2024, 12, 27, 8, 10, 0, DateTimeKind.Utc),
                    Description = "Lote de cosméticos",
                    Destination = "Av. Javier Prado 999, San Borja, Lima",
                    ShipmentStatusId = 1, // Habilitado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 15,
                    CustomerId = 2,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-015",
                    ShippedAt = new DateTime(2024, 12, 25, 13, 0, 0, DateTimeKind.Utc),
                    Description = "Tablets Lenovo 10 pulgadas - 20 unidades",
                    Destination = "Jr. Lampa 456, Cercado de Lima, Lima",
                    ShipmentStatusId = 3, // En Tránsito
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 16,
                    CustomerId = 5,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-016",
                    ShippedAt = new DateTime(2024, 12, 19, 10, 0, 0, DateTimeKind.Utc),
                    Description = "Útiles de escritorio al por mayor",
                    Destination = "Av. Venezuela 555, Breña, Lima",
                    ShipmentStatusId = 5, // Entregado
                    Receivedby = "Jefe Almacén",
                    ReceivedAt = new DateTime(2024, 12, 20, 9, 30, 0, DateTimeKind.Utc)
                },
                new 
                { 
                    ShipmentId = 17,
                    CustomerId = 1,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-017",
                    ShippedAt = new DateTime(2024, 12, 26, 17, 30, 0, DateTimeKind.Utc),
                    Description = "Proyectores Epson - 4 unidades",
                    Destination = "Av. Arequipa 1234, Miraflores, Lima",
                    ShipmentStatusId = 2, // Empaquetado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 18,
                    CustomerId = 3,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-018",
                    ShippedAt = new DateTime(2024, 12, 23, 15, 45, 0, DateTimeKind.Utc),
                    Description = "Memorias RAM DDR5 32GB - 50 unidades",
                    Destination = "Av. Industrial 789, Callao",
                    ShipmentStatusId = 4, // LlegadaDestino
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 19,
                    CustomerId = 4,
                    UserId = 4,
                    TrackingNumber = "TRK-2024-019",
                    ShippedAt = new DateTime(2024, 12, 18, 9, 0, 0, DateTimeKind.Utc),
                    Description = "Colección de libros de historia",
                    Destination = "Calle Los Robles 321, San Isidro, Lima",
                    ShipmentStatusId = 5, // Entregado
                    Receivedby = "Portería",
                    ReceivedAt = new DateTime(2024, 12, 18, 16, 20, 0, DateTimeKind.Utc)
                },
                new 
                { 
                    ShipmentId = 20,
                    CustomerId = 6,
                    UserId = 4,
                    TrackingNumber = "TRK-2024-020",
                    ShippedAt = new DateTime(2024, 12, 21, 11, 15, 0, DateTimeKind.Utc),
                    Description = "Ropa de invierno - Devolución",
                    Destination = "Av. Javier Prado 999, San Borja, Lima",
                    ShipmentStatusId = 6, // Devuelto
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 21,
                    CustomerId = 2,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-021",
                    ShippedAt = new DateTime(2024, 12, 27, 10, 0, 0, DateTimeKind.Utc),
                    Description = "Cámaras de seguridad IP - Kit completo",
                    Destination = "Jr. Lampa 456, Cercado de Lima, Lima",
                    ShipmentStatusId = 1, // Habilitado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 22,
                    CustomerId = 5,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-022",
                    ShippedAt = new DateTime(2024, 12, 27, 12, 30, 0, DateTimeKind.Utc),
                    Description = "Detergente industrial - 20 bolsas",
                    Destination = "Av. Venezuela 555, Breña, Lima",
                    ShipmentStatusId = 2, // Empaquetado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 23,
                    CustomerId = 1,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-023",
                    ShippedAt = new DateTime(2024, 12, 24, 14, 0, 0, DateTimeKind.Utc),
                    Description = "Licencias de Software en físico",
                    Destination = "Av. Arequipa 1234, Miraflores, Lima",
                    ShipmentStatusId = 3, // En Tránsito
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 24,
                    CustomerId = 3,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-024",
                    ShippedAt = new DateTime(2024, 12, 20, 8, 45, 0, DateTimeKind.Utc),
                    Description = "Discos Duros 4TB NAS - 10 unidades",
                    Destination = "Av. Industrial 789, Callao",
                    ShipmentStatusId = 5, // Entregado
                    Receivedby = "Seguridad Almacén",
                    ReceivedAt = new DateTime(2024, 12, 22, 11, 10, 0, DateTimeKind.Utc)
                },
                new 
                { 
                    ShipmentId = 25,
                    CustomerId = 4,
                    UserId = 4,
                    TrackingNumber = "TRK-2024-025",
                    ShippedAt = new DateTime(2024, 12, 23, 18, 0, 0, DateTimeKind.Utc),
                    Description = "Juguetes varios para donación",
                    Destination = "Calle Los Robles 321, San Isidro, Lima",
                    ShipmentStatusId = 4, // LlegadaDestino
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 26,
                    CustomerId = 6,
                    UserId = 4,
                    TrackingNumber = "TRK-2024-026",
                    ShippedAt = new DateTime(2024, 12, 27, 14, 20, 0, DateTimeKind.Utc),
                    Description = "Accesorios de cocina - Ollas y sartenes",
                    Destination = "Av. Javier Prado 999, San Borja, Lima",
                    ShipmentStatusId = 1, // Habilitado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 27,
                    CustomerId = 2,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-027",
                    ShippedAt = new DateTime(2024, 12, 25, 10, 10, 0, DateTimeKind.Utc),
                    Description = "Monitores Curvos 27'' - 6 unidades",
                    Destination = "Jr. Lampa 456, Cercado de Lima, Lima",
                    ShipmentStatusId = 3, // En Tránsito
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 28,
                    CustomerId = 5,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-028",
                    ShippedAt = new DateTime(2024, 12, 21, 9, 30, 0, DateTimeKind.Utc),
                    Description = "Café en grano selección - 20kg",
                    Destination = "Av. Venezuela 555, Breña, Lima",
                    ShipmentStatusId = 5, // Entregado
                    Receivedby = "Recepcionista",
                    ReceivedAt = new DateTime(2024, 12, 22, 8, 45, 0, DateTimeKind.Utc)
                },
                new 
                { 
                    ShipmentId = 29,
                    CustomerId = 1,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-029",
                    ShippedAt = new DateTime(2024, 12, 26, 16, 0, 0, DateTimeKind.Utc),
                    Description = "Laptops HP Omen Gaming - 2 unidades",
                    Destination = "Av. Arequipa 1234, Miraflores, Lima",
                    ShipmentStatusId = 2, // Empaquetado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 30,
                    CustomerId = 3,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-030",
                    ShippedAt = new DateTime(2024, 12, 19, 13, 15, 0, DateTimeKind.Utc),
                    Description = "Procesadores Intel i9 - Defectuosos",
                    Destination = "Av. Industrial 789, Callao",
                    ShipmentStatusId = 6, // Devuelto
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 31,
                    CustomerId = 4,
                    UserId = 4,
                    TrackingNumber = "TRK-2024-031",
                    ShippedAt = new DateTime(2024, 12, 22, 11, 45, 0, DateTimeKind.Utc),
                    Description = "Documentos legales urgentes",
                    Destination = "Calle Los Robles 321, San Isidro, Lima",
                    ShipmentStatusId = 5, // Entregado
                    Receivedby = "Pedro Sánchez",
                    ReceivedAt = new DateTime(2024, 12, 22, 15, 30, 0, DateTimeKind.Utc)
                },
                new 
                { 
                    ShipmentId = 32,
                    CustomerId = 6,
                    UserId = 4,
                    TrackingNumber = "TRK-2024-032",
                    ShippedAt = new DateTime(2024, 12, 27, 9, 0, 0, DateTimeKind.Utc),
                    Description = "Muebles pequeños (Mesas de noche)",
                    Destination = "Av. Javier Prado 999, San Borja, Lima",
                    ShipmentStatusId = 1, // Habilitado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 33,
                    CustomerId = 2,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-033",
                    ShippedAt = new DateTime(2024, 12, 24, 15, 30, 0, DateTimeKind.Utc),
                    Description = "Audífonos Bluetooth Noise Cancelling",
                    Destination = "Jr. Lampa 456, Cercado de Lima, Lima",
                    ShipmentStatusId = 3, // En Tránsito
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 34,
                    CustomerId = 5,
                    UserId = 3,
                    TrackingNumber = "TRK-2024-034",
                    ShippedAt = new DateTime(2024, 12, 23, 10, 20, 0, DateTimeKind.Utc),
                    Description = "Cajas de galletas y snacks",
                    Destination = "Av. Venezuela 555, Breña, Lima",
                    ShipmentStatusId = 4, // LlegadaDestino
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                },
                new 
                { 
                    ShipmentId = 35,
                    CustomerId = 1,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-035",
                    ShippedAt = new DateTime(2024, 12, 20, 11, 0, 0, DateTimeKind.Utc),
                    Description = "Estabilizadores de voltaje industriales",
                    Destination = "Av. Arequipa 1234, Miraflores, Lima",
                    ShipmentStatusId = 5, // Entregado
                    Receivedby = "Roberto Mendoza",
                    ReceivedAt = new DateTime(2024, 12, 21, 14, 0, 0, DateTimeKind.Utc)
                },
                new 
                { 
                    ShipmentId = 36,
                    CustomerId = 3,
                    UserId = 2,
                    TrackingNumber = "TRK-2024-036",
                    ShippedAt = new DateTime(2024, 12, 27, 16, 45, 0, DateTimeKind.Utc),
                    Description = "Placas madre ASUS ROG - 8 unidades",
                    Destination = "Av. Industrial 789, Callao",
                    ShipmentStatusId = 2, // Empaquetado
                    Receivedby = "",
                    ReceivedAt = (DateTime?)null
                }
            );
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
