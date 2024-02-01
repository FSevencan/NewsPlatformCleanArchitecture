using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class NewsVideoConfiguration : IEntityTypeConfiguration<NewsVideo>
{
    public void Configure(EntityTypeBuilder<NewsVideo> builder)
    {
        builder.ToTable("NewsVideos").HasKey(nv => nv.Id);

        builder.Property(nv => nv.Id).HasColumnName("Id").IsRequired();
        builder.Property(nv => nv.Title).HasColumnName("Title");
        builder.Property(nv => nv.VideoURL).HasColumnName("VideoURL");
        builder.Property(nv => nv.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(nv => nv.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(nv => nv.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(nv => !nv.DeletedDate.HasValue);
    }
}