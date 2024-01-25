using Application.Features.NewsVideos.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.NewsVideos.Rules;

public class NewsVideoBusinessRules : BaseBusinessRules
{
    private readonly INewsVideoRepository _newsVideoRepository;

    public NewsVideoBusinessRules(INewsVideoRepository newsVideoRepository)
    {
        _newsVideoRepository = newsVideoRepository;
    }

    public Task NewsVideoShouldExistWhenSelected(NewsVideo? newsVideo)
    {
        if (newsVideo == null)
            throw new BusinessException(NewsVideosBusinessMessages.NewsVideoNotExists);
        return Task.CompletedTask;
    }

    public async Task NewsVideoIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        NewsVideo? newsVideo = await _newsVideoRepository.GetAsync(
            predicate: nv => nv.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await NewsVideoShouldExistWhenSelected(newsVideo);
    }
}