using Application.Features.NewsVideos.Constants;
using Application.Features.NewsVideos.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.NewsVideos.Constants.NewsVideosOperationClaims;

namespace Application.Features.NewsVideos.Queries.GetById;

public class GetByIdNewsVideoQuery : IRequest<GetByIdNewsVideoResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdNewsVideoQueryHandler : IRequestHandler<GetByIdNewsVideoQuery, GetByIdNewsVideoResponse>
    {
        private readonly IMapper _mapper;
        private readonly INewsVideoRepository _newsVideoRepository;
        private readonly NewsVideoBusinessRules _newsVideoBusinessRules;

        public GetByIdNewsVideoQueryHandler(IMapper mapper, INewsVideoRepository newsVideoRepository, NewsVideoBusinessRules newsVideoBusinessRules)
        {
            _mapper = mapper;
            _newsVideoRepository = newsVideoRepository;
            _newsVideoBusinessRules = newsVideoBusinessRules;
        }

        public async Task<GetByIdNewsVideoResponse> Handle(GetByIdNewsVideoQuery request, CancellationToken cancellationToken)
        {
            NewsVideo? newsVideo = await _newsVideoRepository.GetAsync(predicate: nv => nv.Id == request.Id, cancellationToken: cancellationToken);
            await _newsVideoBusinessRules.NewsVideoShouldExistWhenSelected(newsVideo);

            GetByIdNewsVideoResponse response = _mapper.Map<GetByIdNewsVideoResponse>(newsVideo);
            return response;
        }
    }
}