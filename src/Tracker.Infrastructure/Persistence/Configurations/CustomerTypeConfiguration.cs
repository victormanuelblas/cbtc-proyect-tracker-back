using Microsoft.EntityFrameworkCore;
using Tracker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tracker.Infrastructure.Persistence.Configurations
{
    public class CustomerTypeConfiguration : IEntityTypeConfiguration<CustomerType>
    {
        public void Configure(EntityTypeBuilder<CustomerType> builder)
        {
            builder.ToTable("customer_types");

            builder.HasKey(ct => ct.CustomerTypeId);

            builder.Property(ct => ct.CustomerTypeId)
                .HasColumnName("customer_type_id")
                .ValueGeneratedOnAdd();

            builder.Property(ct => ct.Description)
                .HasColumnName("description")
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}