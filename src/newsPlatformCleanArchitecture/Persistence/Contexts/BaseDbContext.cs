using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleReaction> ArticleReactions { get; set; }
    public DbSet<ArticleTag> ArticleTags { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ColumnArticle> ColumnArticles { get; set; }
    public DbSet<Columnist> Columnists { get; set; }
    public DbSet<Contact> Messages { get; set; }
    public DbSet<Poll> Polls { get; set; }
    public DbSet<PollOption> PollOptions { get; set; }
    public DbSet<PollVote> PollVotes { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Subscrible> Subscribles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<NewsVideo> NewsVideoItems { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<NewsVideo> NewsVideos { get; set; }

  

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
    : base(dbContextOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
