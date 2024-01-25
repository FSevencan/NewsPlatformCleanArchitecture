using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.ElasticSearch;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Services.Advertisements;
using Application.Services.Articles;
using Application.Services.ArticleReactions;
using Application.Services.ArticleTags;
using Application.Services.Categories;
using Application.Services.ColumnArticles;
using Application.Services.Columnists;
using Application.Services.Contacts;
using Application.Services.Polls;
using Application.Services.PollOptions;
using Application.Services.PollVotes;
using Application.Services.SubCategories;
using Application.Services.Subscribles;
using Application.Services.Tags;
using Application.Services.NewsVideos;


namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMailService, MailKitMailService>();
        services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<IElasticSearch, ElasticSearchManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<IUserService, UserManager>();

        services.AddScoped<IAdvertisementsService, AdvertisementsManager>();
        services.AddScoped<IArticlesService, ArticlesManager>();
        services.AddScoped<IArticleReactionsService, ArticleReactionsManager>();
        services.AddScoped<IArticleTagsService, ArticleTagsManager>();
        services.AddScoped<ICategoriesService, CategoriesManager>();
        services.AddScoped<IColumnArticlesService, ColumnArticlesManager>();
        services.AddScoped<IColumnistsService, ColumnistsManager>();
        services.AddScoped<IContactsService, ContactsManager>();
        services.AddScoped<IPollsService, PollsManager>();
        services.AddScoped<IPollOptionsService, PollOptionsManager>();
        services.AddScoped<IPollVotesService, PollVotesManager>();
        services.AddScoped<ISubCategoriesService, SubCategoriesManager>();
        services.AddScoped<ISubscriblesService, SubscriblesManager>();
        services.AddScoped<ITagsService, TagsManager>();
       
       services.AddScoped<INewsVideosService, NewsVideosManager>();
       services.AddScoped<IArticleReactionsService, ArticleReactionsManager>();
       services.AddScoped<IArticlesService, ArticlesManager>();
        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
