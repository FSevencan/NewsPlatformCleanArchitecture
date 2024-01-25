using Application.Features.NewsVideos.Constants;
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

namespace Application.Features.NewsVideos.Commands.Delete;

public class DeleteNewsVideoCommand : IRequest<DeletedNewsVideoResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, NewsVideosOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetNewsVideos";

    public class DeleteNewsVideoCommandHandler : IRequestHandler<DeleteNewsVideoCommand, DeletedNewsVideoResponse>
    {
        private readonly IMapper _mapper;
        private readonly INewsVideoRepository _newsVideoRepository;
        private readonly NewsVideoBusinessRules _newsVideoBusinessRules;

        public DeleteNewsVideoCommandHandler(IMapper mapper, INewsVideoRepository newsVideoRepository,
                                         NewsVideoBusinessRules newsVideoBusinessRules)
        {
            _mapper = mapper;
            _newsVideoRepository = newsVideoRepository;
            _newsVideoBusinessRules = newsVideoBusinessRules;
        }

        public async Task<DeletedNewsVideoResponse> Handle(DeleteNewsVideoCommand request, CancellationToken cancellationToken)
        {
            NewsVideo? newsVideo = await _newsVideoRepository.GetAsync(predicate: nv => nv.Id == request.Id, cancellationToken: cancellationToken);
            await _newsVideoBusinessRules.NewsVideoShouldExistWhenSelected(newsVideo);

            await _newsVideoRepository.DeleteAsync(newsVideo!);

            DeletedNewsVideoResponse response = _mapper.Map<DeletedNewsVideoResponse>(newsVideo);
            return response;
        }
    }
}