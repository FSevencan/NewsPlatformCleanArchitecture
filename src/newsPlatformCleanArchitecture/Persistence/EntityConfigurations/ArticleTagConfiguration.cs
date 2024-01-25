using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
{
    public void Configure(EntityTypeBuilder<ArticleTag> builder)
    {
        builder.ToTable("ArticleTags").HasKey(at => at.Id);

        builder.Property(at => at.Id).HasColumnName("Id").IsRequired();
        builder.Property(at => at.ArticleId).HasColumnName("ArticleId");
        builder.Property(at => at.TagId).HasColumnName("TagId");
    
        builder.Property(at => at.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(at => at.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(at => at.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(at => !at.DeletedDate.HasValue);
    }
}