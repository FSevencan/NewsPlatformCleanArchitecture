using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.NewsVideos;

public interface INewsVideosService
{
    Task<NewsVideo?> GetAsync(
        Expression<Func<NewsVideo, bool>> predicate,
        Func<IQueryable<NewsVideo>, IIncludableQueryable<NewsVideo, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<NewsVideo>?> GetListAsync(
        Expression<Func<NewsVideo, bool>>? predicate = null,
        Func<IQueryable<NewsVideo>, IOrderedQueryable<NewsVideo>>? orderBy = null,
        Func<IQueryable<NewsVideo>, IIncludableQueryable<NewsVideo, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<NewsVideo> AddAsync(NewsVideo newsVideo);
    Task<NewsVideo> UpdateAsync(NewsVideo newsVideo);
    Task<NewsVideo> DeleteAsync(NewsVideo newsVideo, bool permanent = false);
}
