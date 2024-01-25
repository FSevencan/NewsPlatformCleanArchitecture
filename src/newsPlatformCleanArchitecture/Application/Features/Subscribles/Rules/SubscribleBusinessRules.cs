using Application.Features.Subscribles.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Subscribles.Rules;

public class SubscribleBusinessRules : BaseBusinessRules
{
    private readonly ISubscribleRepository _subscribleRepository;

    public SubscribleBusinessRules(ISubscribleRepository subscribleRepository)
    {
        _subscribleRepository = subscribleRepository;
    }

    public Task SubscribleShouldExistWhenSelected(Subscrible? subscrible)
    {
        if (subscrible == null)
            throw new BusinessException(SubscriblesBusinessMessages.SubscribleNotExists);
        return Task.CompletedTask;
    }

    public async Task SubscribleIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Subscrible? subscrible = await _subscribleRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SubscribleShouldExistWhenSelected(subscrible);
    }
}