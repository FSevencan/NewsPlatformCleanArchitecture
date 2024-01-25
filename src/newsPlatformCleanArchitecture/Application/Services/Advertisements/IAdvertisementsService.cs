using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Advertisements;

public interface IAdvertisementsService
{
    Task<Advertisement?> GetAsync(
        Expression<Func<Advertisement, bool>> predicate,
        Func<IQueryable<Advertisement>, IIncludableQueryable<Advertisement, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Advertisement>?> GetListAsync(
        Expression<Func<Advertisement, bool>>? predicate = null,
        Func<IQueryable<Advertisement>, IOrderedQueryable<Advertisement>>? orderBy = null,
        Func<IQueryable<Advertisement>, IIncludableQueryable<Advertisement, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Advertisement> AddAsync(Advertisement advertisement);
    Task<Advertisement> UpdateAsync(Advertisement advertisement);
    Task<Advertisement> DeleteAsync(Advertisement advertisement, bool permanent = false);
}
