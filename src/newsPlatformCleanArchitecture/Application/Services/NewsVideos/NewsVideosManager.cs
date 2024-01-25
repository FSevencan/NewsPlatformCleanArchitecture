using Application.Features.NewsVideos.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.NewsVideos;

public class NewsVideosManager : INewsVideosService
{
    private readonly INewsVideoRepository _newsVideoRepository;
    private readonly NewsVideoBusinessRules _newsVideoBusinessRules;

    public NewsVideosManager(INewsVideoRepository newsVideoRepository, NewsVideoBusinessRules newsVideoBusinessRules)
    {
        _newsVideoRepository = newsVideoRepository;
        _newsVideoBusinessRules = newsVideoBusinessRules;
    }

    public async Task<NewsVideo?> GetAsync(
        Expression<Func<NewsVideo, bool>> predicate,
        Func<IQueryable<NewsVideo>, IIncludableQueryable<NewsVideo, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        NewsVideo? newsVideo = await _newsVideoRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return newsVideo;
    }

    public async Task<IPaginate<NewsVideo>?> GetListAsync(
        Expression<Func<NewsVideo, bool>>? predicate = null,
        Func<IQueryable<NewsVideo>, IOrderedQueryable<NewsVideo>>? orderBy = null,
        Func<IQueryable<NewsVideo>, IIncludableQueryable<NewsVideo, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<NewsVideo> newsVideoList = await _newsVideoRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return newsVideoList;
    }

    public async Task<NewsVideo> AddAsync(NewsVideo newsVideo)
    {
        NewsVideo addedNewsVideo = await _newsVideoRepository.AddAsync(newsVideo);

        return addedNewsVideo;
    }

    public async Task<NewsVideo> UpdateAsync(NewsVideo newsVideo)
    {
        NewsVideo updatedNewsVideo = await _newsVideoRepository.UpdateAsync(newsVideo);

        return updatedNewsVideo;
    }

    public async Task<NewsVideo> DeleteAsync(NewsVideo newsVideo, bool permanent = false)
    {
        NewsVideo deletedNewsVideo = await _newsVideoRepository.DeleteAsync(newsVideo);

        return deletedNewsVideo;
    }
}
