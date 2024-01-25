using Application.Features.Columnists.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Columnists;

public class ColumnistsManager : IColumnistsService
{
    private readonly IColumnistRepository _columnistRepository;
    private readonly ColumnistBusinessRules _columnistBusinessRules;

    public ColumnistsManager(IColumnistRepository columnistRepository, ColumnistBusinessRules columnistBusinessRules)
    {
        _columnistRepository = columnistRepository;
        _columnistBusinessRules = columnistBusinessRules;
    }

    public async Task<Columnist?> GetAsync(
        Expression<Func<Columnist, bool>> predicate,
        Func<IQueryable<Columnist>, IIncludableQueryable<Columnist, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Columnist? columnist = await _columnistRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return columnist;
    }

    public async Task<IPaginate<Columnist>?> GetListAsync(
        Expression<Func<Columnist, bool>>? predicate = null,
        Func<IQueryable<Columnist>, IOrderedQueryable<Columnist>>? orderBy = null,
        Func<IQueryable<Columnist>, IIncludableQueryable<Columnist, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Columnist> columnistList = await _columnistRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return columnistList;
    }

    public async Task<Columnist> AddAsync(Columnist columnist)
    {
        Columnist addedColumnist = await _columnistRepository.AddAsync(columnist);

        return addedColumnist;
    }

    public async Task<Columnist> UpdateAsync(Columnist columnist)
    {
        Columnist updatedColumnist = await _columnistRepository.UpdateAsync(columnist);

        return updatedColumnist;
    }

    public async Task<Columnist> DeleteAsync(Columnist columnist, bool permanent = false)
    {
        Columnist deletedColumnist = await _columnistRepository.DeleteAsync(columnist);

        return deletedColumnist;
    }
}
