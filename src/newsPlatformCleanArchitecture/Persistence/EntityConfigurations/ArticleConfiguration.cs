using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Articles").HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
        builder.Property(a => a.SubcategoryId).HasColumnName("SubcategoryId");
        builder.Property(a => a.Title).HasColumnName("Title");
        builder.Property(a => a.Content).HasColumnName("Content");
        builder.Property(a => a.Summary).HasColumnName("Summary");
        builder.Property(a => a.FeaturedImage).HasColumnName("FeaturedImage");
        builder.Property(a => a.Slug).HasColumnName("Slug");
        builder.Property(a => a.TotalLikes).HasColumnName("TotalLikes");
        builder.Property(a => a.TotalDislikes).HasColumnName("TotalDislikes");
        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}