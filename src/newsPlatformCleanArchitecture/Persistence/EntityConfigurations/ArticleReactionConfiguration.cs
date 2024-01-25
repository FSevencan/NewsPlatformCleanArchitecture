using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ArticleReactionConfiguration : IEntityTypeConfiguration<ArticleReaction>
{
    public void Configure(EntityTypeBuilder<ArticleReaction> builder)
    {
        builder.ToTable("ArticleReactions").HasKey(ar => ar.Id);

        builder.Property(ar => ar.Id).HasColumnName("Id").IsRequired();
        builder.Property(ar => ar.ArticleId).HasColumnName("ArticleId");
        builder.Property(ar => ar.IsLiked).HasColumnName("IsLiked");
        builder.Property(ar => ar.VoterIdentifier).HasColumnName("VoterIdentifier");
        builder.Property(ar => ar.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ar => ar.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ar => ar.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ar => !ar.DeletedDate.HasValue);
    }
}