using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Domain.Entities;

namespace Tracker.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customers");
            
            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.CustomerId)
                .HasColumnName("customer_id")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .HasColumnName("email")
                .HasMaxLength(100);

            builder.HasIndex(c => c.Email)
                .IsUnique();

            builder.Property(c => c.Phone)
                .HasColumnName("phone")
                .HasMaxLength(15);
            
            builder.Property(c => c.DocmNumber)
                .HasColumnName("docm_number")
                .IsRequired()
                .HasMaxLength(20);

            
            builder.Property(c => c.CustomerTypeId)
                .HasColumnName("customer_type_id")
                .IsRequired();

            builder.HasOne(c => c.CustomerType)
                .WithMany()
                .HasForeignKey(c => c.CustomerTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}