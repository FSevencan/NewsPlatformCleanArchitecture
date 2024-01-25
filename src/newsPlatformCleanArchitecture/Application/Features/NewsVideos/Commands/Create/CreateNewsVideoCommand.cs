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

namespace Application.Features.NewsVideos.Commands.Create;

public class CreateNewsVideoCommand : IRequest<CreatedNewsVideoResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string VideoURL { get; set; }

    public string[] Roles => new[] { Admin, Write, NewsVideosOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetNewsVideos";

    public class CreateNewsVideoCommandHandler : IRequestHandler<CreateNewsVideoCommand, CreatedNewsVideoResponse>
    {
        private readonly IMapper _mapper;
        private readonly INewsVideoRepository _newsVideoRepository;
        private readonly NewsVideoBusinessRules _newsVideoBusinessRules;

        public CreateNewsVideoCommandHandler(IMapper mapper, INewsVideoRepository newsVideoRepository,
                                         NewsVideoBusinessRules newsVideoBusinessRules)
        {
            _mapper = mapper;
            _newsVideoRepository = newsVideoRepository;
            _newsVideoBusinessRules = newsVideoBusinessRules;
        }

        public async Task<CreatedNewsVideoResponse> Handle(CreateNewsVideoCommand request, CancellationToken cancellationToken)
        {
            NewsVideo newsVideo = _mapper.Map<NewsVideo>(request);

            await _newsVideoRepository.AddAsync(newsVideo);

            CreatedNewsVideoResponse response = _mapper.Map<CreatedNewsVideoResponse>(newsVideo);
            return response;
        }
    }
}