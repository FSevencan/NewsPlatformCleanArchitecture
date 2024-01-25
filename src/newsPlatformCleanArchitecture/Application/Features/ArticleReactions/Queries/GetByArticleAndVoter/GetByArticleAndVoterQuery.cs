using Application.Features.ArticleReactions.Queries.GetById;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;

public class GetByArticleAndVoterQuery : IRequest<GetByArticleAndVoterResponse>
{
    public Guid ArticleId { get; set; }
    public string VoterIdentifier { get; set; }

    public class GetByArticleAndVoterQueryHandler : IRequestHandler<GetByArticleAndVoterQuery, GetByArticleAndVoterResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleReactionRepository _articleReactionRepository;

        public GetByArticleAndVoterQueryHandler(IMapper mapper, IArticleReactionRepository articleReactionRepository)
        {
            _mapper = mapper;
            _articleReactionRepository = articleReactionRepository;
        }

        public async Task<GetByArticleAndVoterResponse> Handle(GetByArticleAndVoterQuery request, CancellationToken cancellationToken)
        {
            var articleReaction = await _articleReactionRepository.GetAsync(
                predicate: ar => ar.ArticleId == request.ArticleId && ar.VoterIdentifier == request.VoterIdentifier,
                cancellationToken: cancellationToken);


            return _mapper.Map<GetByArticleAndVoterResponse>(articleReaction);
        }
    }
}