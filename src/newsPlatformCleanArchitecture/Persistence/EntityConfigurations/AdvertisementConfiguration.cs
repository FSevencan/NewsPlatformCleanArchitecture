using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.ToTable("Advertisements").HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
        builder.Property(a => a.Title).HasColumnName("Title");
        builder.Property(a => a.ImageUrl).HasColumnName("ImageUrl");
        builder.Property(a => a.RedirectUrl).HasColumnName("RedirectUrl");
        builder.Property(a => a.StartDate).HasColumnName("StartDate");
        builder.Property(a => a.EndDate).HasColumnName("EndDate");
        builder.Property(a => a.ClickCount).HasColumnName("ClickCount");
        builder.Property(a => a.ViewCount).HasColumnName("ViewCount");
        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}