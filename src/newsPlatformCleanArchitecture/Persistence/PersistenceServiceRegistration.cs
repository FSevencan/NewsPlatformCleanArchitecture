using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("NewsPlatformConnectionString")));
        services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

        services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IArticleReactionRepository, ArticleReactionRepository>();
        services.AddScoped<IArticleTagRepository, ArticleTagRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IColumnArticleRepository, ColumnArticleRepository>();
        services.AddScoped<IColumnistRepository, ColumnistRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IPollRepository, PollRepository>();
        services.AddScoped<IPollOptionRepository, PollOptionRepository>();
        services.AddScoped<IPollVoteRepository, PollVoteRepository>();
        services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        services.AddScoped<ISubscribleRepository, SubscribleRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
     
     services.AddScoped<INewsVideoRepository, NewsVideoRepository>();
     services.AddScoped<IArticleReactionRepository, ArticleReactionRepository>();
     services.AddScoped<IArticleRepository, ArticleRepository>();
        return services;
    }
}
