using Application.Features.NewsVideos.Constants;
using Application.Features.NewsVideos.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.NewsVideos.Constants.NewsVideosOperationClaims;

namespace Application.Features.NewsVideos.Commands.Update;

public class UpdateNewsVideoCommand : IRequest<UpdatedNewsVideoResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string VideoURL { get; set; }

    public string[] Roles => new[] { Admin, Write, NewsVideosOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetNewsVideos";

    public class UpdateNewsVideoCommandHandler : IRequestHandler<UpdateNewsVideoCommand, UpdatedNewsVideoResponse>
    {
        private readonly IMapper _mapper;
        private readonly INewsVideoRepository _newsVideoRepository;
        private readonly NewsVideoBusinessRules _newsVideoBusinessRules;

        public UpdateNewsVideoCommandHandler(IMapper mapper, INewsVideoRepository newsVideoRepository,
                                         NewsVideoBusinessRules newsVideoBusinessRules)
        {
            _mapper = mapper;
            _newsVideoRepository = newsVideoRepository;
            _newsVideoBusinessRules = newsVideoBusinessRules;
        }

        public async Task<UpdatedNewsVideoResponse> Handle(UpdateNewsVideoCommand request, CancellationToken cancellationToken)
        {
            NewsVideo? newsVideo = await _newsVideoRepository.GetAsync(predicate: nv => nv.Id == request.Id, cancellationToken: cancellationToken);
            await _newsVideoBusinessRules.NewsVideoShouldExistWhenSelected(newsVideo);
            newsVideo = _mapper.Map(request, newsVideo);

            await _newsVideoRepository.UpdateAsync(newsVideo!);

            UpdatedNewsVideoResponse response = _mapper.Map<UpdatedNewsVideoResponse>(newsVideo);
            return response;
        }
    }
}