using Application.Features.Polls.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Polls.Rules;

public class PollBusinessRules : BaseBusinessRules
{
    private readonly IPollRepository _pollRepository;

    public PollBusinessRules(IPollRepository pollRepository)
    {
        _pollRepository = pollRepository;
    }

    public Task PollShouldExistWhenSelected(Poll? poll)
    {
        if (poll == null)
            throw new BusinessException(PollsBusinessMessages.PollNotExists);
        return Task.CompletedTask;
    }

    public async Task PollIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Poll? poll = await _pollRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PollShouldExistWhenSelected(poll);
    }
}