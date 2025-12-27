using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customer_types",
                columns: table => new
                {
                    customer_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_types", x => x.customer_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shipment_statuses",
                columns: table => new
                {
                    shipment_status_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipment_statuses", x => x.shipment_status_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    docm_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customer_id);
                    table.ForeignKey(
                        name: "FK_customers_customer_types_customer_type_id",
                        column: x => x.customer_type_id,
                        principalTable: "customer_types",
                        principalColumn: "customer_type_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shipments",
                columns: table => new
                {
                    shipment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    tracking_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    shipped_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    destination = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    shipment_status_id = table.Column<int>(type: "int", nullable: false),
                    received_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    received_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipments", x => x.shipment_id);
                    table.ForeignKey(
                        name: "FK_shipments_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_shipments_shipment_statuses_shipment_status_id",
                        column: x => x.shipment_status_id,
                        principalTable: "shipment_statuses",
                        principalColumn: "shipment_status_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_shipments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "customer_types",
                columns: new[] { "customer_type_id", "description" },
                values: new object[,]
                {
                    { 1, "regular" },
                    { 2, "vip" }
                });

            migrationBuilder.InsertData(
                table: "shipment_statuses",
                columns: new[] { "shipment_status_id", "description" },
                values: new object[,]
                {
                    { 1, "Habilitado" },
                    { 2, "Empaquetado" },
                    { 3, "En Tránsito" },
                    { 4, "LlegadaDestino" },
                    { 5, "Entregado" },
                    { 6, "Devuelto" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "email", "is_active", "name", "password_hash", "role" },
                values: new object[,]
                {
                    { 1, "admin@tracker.com", true, "Admin User", "$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq", 3 },
                    { 2, "juan.perez@tracker.com", true, "Juan Pérez", "$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq", 2 },
                    { 3, "maria.gonzalez@tracker.com", true, "María González", "$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq", 1 },
                    { 4, "carlos.ramirez@tracker.com", true, "Carlos Ramírez", "$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq", 1 }
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "customer_id", "customer_type_id", "docm_number", "email", "name", "phone" },
                values: new object[,]
                {
                    { 1, 2, "20123456789", "contacto@acme.com", "Acme Corporation", "+51987654321" },
                    { 2, 1, "20234567890", "ventas@techstore.pe", "TechStore SAC", "+51976543210" },
                    { 3, 2, "20345678901", "info@globalimports.com", "Global Imports EIRL", "+51965432109" },
                    { 4, 1, "12345678", "pedro.sanchez@email.com", "Pedro Sánchez", "+51954321098" },
                    { 5, 1, "20456789012", "ventas@distrilima.pe", "Distribuidora Lima SAC", "+51943210987" },
                    { 6, 1, "23456789", "ana.torres@email.com", "Ana Torres", "+51932109876" }
                });

            migrationBuilder.InsertData(
                table: "shipments",
                columns: new[] { "shipment_id", "customer_id", "description", "destination", "received_at", "received_by", "shipment_status_id", "shipped_at", "tracking_number", "user_id" },
                values: new object[,]
                {
                    { 1, 1, "Equipos de cómputo - 5 laptops Dell", "Av. Arequipa 1234, Miraflores, Lima", new DateTime(2024, 12, 22, 14, 15, 0, 0, DateTimeKind.Utc), "Roberto Mendoza", 5, new DateTime(2024, 12, 20, 10, 30, 0, 0, DateTimeKind.Utc), "TRK-2024-001", 2 },
                    { 2, 2, "Smartphones Samsung Galaxy - 10 unidades", "Jr. Lampa 456, Cercado de Lima, Lima", null, "", 3, new DateTime(2024, 12, 23, 8, 0, 0, 0, DateTimeKind.Utc), "TRK-2024-002", 3 },
                    { 3, 3, "Componentes electrónicos importados", "Av. Industrial 789, Callao", null, "", 4, new DateTime(2024, 12, 24, 9, 15, 0, 0, DateTimeKind.Utc), "TRK-2024-003", 2 },
                    { 4, 4, "Paquete personal - Documentos", "Calle Los Robles 321, San Isidro, Lima", null, "", 2, new DateTime(2024, 12, 25, 11, 45, 0, 0, DateTimeKind.Utc), "TRK-2024-004", 4 },
                    { 5, 5, "Productos de limpieza - 50 cajas", "Av. Venezuela 555, Breña, Lima", null, "", 1, new DateTime(2024, 12, 26, 7, 30, 0, 0, DateTimeKind.Utc), "TRK-2024-005", 3 },
                    { 6, 1, "Impresoras HP LaserJet - 3 unidades", "Av. Arequipa 1234, Miraflores, Lima", new DateTime(2024, 12, 19, 16, 30, 0, 0, DateTimeKind.Utc), "Roberto Mendoza", 5, new DateTime(2024, 12, 18, 13, 0, 0, 0, DateTimeKind.Utc), "TRK-2024-006", 2 },
                    { 7, 6, "Paquete personal - Ropa", "Av. Javier Prado 999, San Borja, Lima", null, "", 6, new DateTime(2024, 12, 15, 10, 0, 0, 0, DateTimeKind.Utc), "TRK-2024-007", 4 },
                    { 8, 3, "Repuestos automotrices - Caja grande", "Av. Industrial 789, Callao", null, "", 3, new DateTime(2024, 12, 26, 14, 20, 0, 0, DateTimeKind.Utc), "TRK-2024-008", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_customer_type_id",
                table: "customers",
                column: "customer_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_email",
                table: "customers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shipments_customer_id",
                table: "shipments",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipments_shipment_status_id",
                table: "shipments",
                column: "shipment_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipments_user_id",
                table: "shipments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shipments");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "shipment_statuses");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "customer_types");
        }
    }
}
