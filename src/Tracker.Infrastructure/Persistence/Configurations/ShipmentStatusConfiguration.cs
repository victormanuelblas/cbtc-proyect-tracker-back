using Microsoft.EntityFrameworkCore;
using Tracker.Domain.Entities;
using Tracker.Infrastructure.Persistence.Configurations;

namespace Tracker.Infrastructure.Persistence.Configurations
{
    public class ShipmentStatusConfiguration : IEntityTypeConfiguration<ShipmentStatus>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ShipmentStatus> builder)
        {
            builder.ToTable("shipment_statuses");

            builder.HasKey(e => e.ShipmentStatusId);

            builder.Property(e => e.ShipmentStatusId)
                .HasColumnName("shipment_status_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}