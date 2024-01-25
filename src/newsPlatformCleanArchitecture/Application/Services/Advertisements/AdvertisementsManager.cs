using Application.Features.Advertisements.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Advertisements;

public class AdvertisementsManager : IAdvertisementsService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly AdvertisementBusinessRules _advertisementBusinessRules;

    public AdvertisementsManager(IAdvertisementRepository advertisementRepository, AdvertisementBusinessRules advertisementBusinessRules)
    {
        _advertisementRepository = advertisementRepository;
        _advertisementBusinessRules = advertisementBusinessRules;
    }

    public async Task<Advertisement?> GetAsync(
        Expression<Func<Advertisement, bool>> predicate,
        Func<IQueryable<Advertisement>, IIncludableQueryable<Advertisement, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Advertisement? advertisement = await _advertisementRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return advertisement;
    }

    public async Task<IPaginate<Advertisement>?> GetListAsync(
        Expression<Func<Advertisement, bool>>? predicate = null,
        Func<IQueryable<Advertisement>, IOrderedQueryable<Advertisement>>? orderBy = null,
        Func<IQueryable<Advertisement>, IIncludableQueryable<Advertisement, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Advertisement> advertisementList = await _advertisementRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return advertisementList;
    }

    public async Task<Advertisement> AddAsync(Advertisement advertisement)
    {
        Advertisement addedAdvertisement = await _advertisementRepository.AddAsync(advertisement);

        return addedAdvertisement;
    }

    public async Task<Advertisement> UpdateAsync(Advertisement advertisement)
    {
        Advertisement updatedAdvertisement = await _advertisementRepository.UpdateAsync(advertisement);

        return updatedAdvertisement;
    }

    public async Task<Advertisement> DeleteAsync(Advertisement advertisement, bool permanent = false)
    {
        Advertisement deletedAdvertisement = await _advertisementRepository.DeleteAsync(advertisement);

        return deletedAdvertisement;
    }
}
