using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ColumnArticleConfiguration : IEntityTypeConfiguration<ColumnArticle>
{
    public void Configure(EntityTypeBuilder<ColumnArticle> builder)
    {
        builder.ToTable("ColumnArticles").HasKey(ca => ca.Id);

        builder.Property(ca => ca.Id).HasColumnName("Id").IsRequired();
        builder.Property(ca => ca.ColumnistId).HasColumnName("ColumnistId");
        builder.Property(ca => ca.CategoryId).HasColumnName("CategoryId");
        builder.Property(ca => ca.Title).HasColumnName("Title");
        builder.Property(ca => ca.Content).HasColumnName("Content");
        builder.Property(ca => ca.FeaturedImage).HasColumnName("FeaturedImage");
       
        builder.Property(ca => ca.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ca => ca.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ca => ca.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ca => !ca.DeletedDate.HasValue);
    }
}