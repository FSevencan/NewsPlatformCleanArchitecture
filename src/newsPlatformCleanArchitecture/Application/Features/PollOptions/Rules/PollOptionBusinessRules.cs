using Application.Features.PollOptions.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.PollOptions.Rules;

public class PollOptionBusinessRules : BaseBusinessRules
{
    private readonly IPollOptionRepository _pollOptionRepository;

    public PollOptionBusinessRules(IPollOptionRepository pollOptionRepository)
    {
        _pollOptionRepository = pollOptionRepository;
    }

    public Task PollOptionShouldExistWhenSelected(PollOption? pollOption)
    {
        if (pollOption == null)
            throw new BusinessException(PollOptionsBusinessMessages.PollOptionNotExists);
        return Task.CompletedTask;
    }

    public async Task PollOptionIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        PollOption? pollOption = await _pollOptionRepository.GetAsync(
            predicate: po => po.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PollOptionShouldExistWhenSelected(pollOption);
    }
}