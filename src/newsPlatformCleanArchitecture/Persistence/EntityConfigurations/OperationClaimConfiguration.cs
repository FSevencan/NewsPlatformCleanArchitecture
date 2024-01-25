using Application.Features.OperationClaims.Constants;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasMany(oc => oc.UserOperationClaims);

        builder.HasData(getSeeds());
    }

    private HashSet<OperationClaim> getSeeds()
    {
        int id = 0;
        HashSet<OperationClaim> seeds =
            new()
            {
                new OperationClaim { Id = ++id, Name = GeneralOperationClaims.Admin }
            };

        
        #region Advertisements
        seeds.Add(new OperationClaim { Id = ++id, Name = "Advertisements.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Advertisements.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Advertisements.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Advertisements.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Advertisements.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Advertisements.Delete" });
        #endregion
        #region Articles
        seeds.Add(new OperationClaim { Id = ++id, Name = "Articles.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Articles.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Articles.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Articles.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Articles.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Articles.Delete" });
        #endregion
        #region ArticleReactions
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleReactions.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleReactions.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleReactions.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleReactions.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleReactions.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleReactions.Delete" });
        #endregion
        #region ArticleTags
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleTags.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleTags.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleTags.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleTags.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleTags.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ArticleTags.Delete" });
        #endregion
        #region Categories
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Delete" });
        #endregion
        #region ColumnArticles
        seeds.Add(new OperationClaim { Id = ++id, Name = "ColumnArticles.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ColumnArticles.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ColumnArticles.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ColumnArticles.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ColumnArticles.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ColumnArticles.Delete" });
        #endregion
        #region Columnists
        seeds.Add(new OperationClaim { Id = ++id, Name = "Columnists.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Columnists.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Columnists.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Columnists.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Columnists.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Columnists.Delete" });
        #endregion
        #region Contacts
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contacts.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contacts.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contacts.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contacts.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contacts.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contacts.Delete" });
        #endregion
        #region Polls
        seeds.Add(new OperationClaim { Id = ++id, Name = "Polls.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Polls.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Polls.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Polls.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Polls.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Polls.Delete" });
        #endregion
        #region PollOptions
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollOptions.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollOptions.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollOptions.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollOptions.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollOptions.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollOptions.Delete" });
        #endregion
        #region PollVotes
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollVotes.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollVotes.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollVotes.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollVotes.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollVotes.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PollVotes.Delete" });
        #endregion
        #region SubCategories
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubCategories.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubCategories.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubCategories.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubCategories.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubCategories.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubCategories.Delete" });
        #endregion
        #region Subscribles
        seeds.Add(new OperationClaim { Id = ++id, Name = "Subscribles.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Subscribles.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Subscribles.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Subscribles.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Subscribles.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Subscribles.Delete" });
        #endregion
        #region Tags
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Delete" });
        #endregion
        #region VideoNews
        seeds.Add(new OperationClaim { Id = ++id, Name = "VideoNews.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "VideoNews.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "VideoNews.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "VideoNews.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "VideoNews.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "VideoNews.Delete" });
        #endregion
        #region NewsVideos
        seeds.Add(new OperationClaim { Id = ++id, Name = "NewsVideos.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "NewsVideos.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "NewsVideos.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "NewsVideos.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "NewsVideos.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "NewsVideos.Delete" });
        #endregion
        return seeds;
    }
}
