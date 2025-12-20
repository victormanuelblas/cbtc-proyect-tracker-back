using Microsoft.EntityFrameworkCore;
using Tracker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tracker.Infrastructure.Persistence.Configurations
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.ToTable("shipments");
            builder.HasKey(s => s.ShipmentId);

            builder.Property(s => s.ShipmentId)
                .HasColumnName("shipment_id")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.CustomerId)
                .HasColumnName("customer_id")
                .IsRequired();
            
            builder.Property(s => s.UserId)
                .HasColumnName("user_id")
                .IsRequired();  

            builder.Property(s => s.TrackingNumber)
                .HasColumnName("tracking_number")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.ShippedAt)
                .HasColumnName("shipped_at")
                .IsRequired();

            builder.Property(s => s.Description)
                .HasColumnName("description")
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(s => s.Destination)
                .HasColumnName("destination")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.ShipmentStatusId)
                .HasColumnName("shipment_status_id")
                .IsRequired();  
            builder.Property(s => s.Receivedby)
                .HasColumnName("received_by")
                .HasMaxLength(100);

            builder.Property(s => s.ReceivedAt)
                .HasColumnName("received_at");
            
            builder.HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
                
            builder.HasOne(s => s.ShipmentStatus)
                .WithMany()
                .HasForeignKey(s => s.ShipmentStatusId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}