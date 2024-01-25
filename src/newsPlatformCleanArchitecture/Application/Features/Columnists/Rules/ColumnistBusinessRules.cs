using Application.Features.Columnists.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Columnists.Rules;

public class ColumnistBusinessRules : BaseBusinessRules
{
    private readonly IColumnistRepository _columnistRepository;

    public ColumnistBusinessRules(IColumnistRepository columnistRepository)
    {
        _columnistRepository = columnistRepository;
    }

    public Task ColumnistShouldExistWhenSelected(Columnist? columnist)
    {
        if (columnist == null)
            throw new BusinessException(ColumnistsBusinessMessages.ColumnistNotExists);
        return Task.CompletedTask;
    }

    public async Task ColumnistIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Columnist? columnist = await _columnistRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ColumnistShouldExistWhenSelected(columnist);
    }
}