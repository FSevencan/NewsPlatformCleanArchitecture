using Application.Features.Advertisements.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Advertisements.Rules;

public class AdvertisementBusinessRules : BaseBusinessRules
{
    private readonly IAdvertisementRepository _advertisementRepository;

    public AdvertisementBusinessRules(IAdvertisementRepository advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
    }

    public Task AdvertisementShouldExistWhenSelected(Advertisement? advertisement)
    {
        if (advertisement == null)
            throw new BusinessException(AdvertisementsBusinessMessages.AdvertisementNotExists);
        return Task.CompletedTask;
    }

    public async Task AdvertisementIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Advertisement? advertisement = await _advertisementRepository.GetAsync(
            predicate: a => a.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AdvertisementShouldExistWhenSelected(advertisement);
    }
}