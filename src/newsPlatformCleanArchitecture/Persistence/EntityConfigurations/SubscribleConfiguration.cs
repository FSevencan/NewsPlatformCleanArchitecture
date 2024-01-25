using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SubscribleConfiguration : IEntityTypeConfiguration<Subscrible>
{
    public void Configure(EntityTypeBuilder<Subscrible> builder)
    {
        builder.ToTable("Subscribles").HasKey(s => s.Id);

        builder.Property(s => s.Id).HasColumnName("Id").IsRequired();
        builder.Property(s => s.Email).HasColumnName("Email");
        builder.Property(s => s.IsConfirmed).HasColumnName("IsConfirmed");
        builder.Property(s => s.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(s => s.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(s => s.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(s => !s.DeletedDate.HasValue);
    }
}