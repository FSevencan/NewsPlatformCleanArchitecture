using Application.Features.Subscribles.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Subscribles;

public class SubscriblesManager : ISubscriblesService
{
    private readonly ISubscribleRepository _subscribleRepository;
    private readonly SubscribleBusinessRules _subscribleBusinessRules;

    public SubscriblesManager(ISubscribleRepository subscribleRepository, SubscribleBusinessRules subscribleBusinessRules)
    {
        _subscribleRepository = subscribleRepository;
        _subscribleBusinessRules = subscribleBusinessRules;
    }

    public async Task<Subscrible?> GetAsync(
        Expression<Func<Subscrible, bool>> predicate,
        Func<IQueryable<Subscrible>, IIncludableQueryable<Subscrible, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Subscrible? subscrible = await _subscribleRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return subscrible;
    }

    public async Task<IPaginate<Subscrible>?> GetListAsync(
        Expression<Func<Subscrible, bool>>? predicate = null,
        Func<IQueryable<Subscrible>, IOrderedQueryable<Subscrible>>? orderBy = null,
        Func<IQueryable<Subscrible>, IIncludableQueryable<Subscrible, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Subscrible> subscribleList = await _subscribleRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return subscribleList;
    }

    public async Task<Subscrible> AddAsync(Subscrible subscrible)
    {
        Subscrible addedSubscrible = await _subscribleRepository.AddAsync(subscrible);

        return addedSubscrible;
    }

    public async Task<Subscrible> UpdateAsync(Subscrible subscrible)
    {
        Subscrible updatedSubscrible = await _subscribleRepository.UpdateAsync(subscrible);

        return updatedSubscrible;
    }

    public async Task<Subscrible> DeleteAsync(Subscrible subscrible, bool permanent = false)
    {
        Subscrible deletedSubscrible = await _subscribleRepository.DeleteAsync(subscrible);

        return deletedSubscrible;
    }
}
