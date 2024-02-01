using Application.Features.Tags.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tags.Queries.GetArticlesByTag;
public class GetArticlesByTagQuery : IRequest<GetListResponse<GetArticleByTagListDto>>
{
    public PageRequest PageRequest { get; set; }
    public string TagName { get; set; }

    public class GetArticlesByTagHandler : IRequestHandler<GetArticlesByTagQuery, GetListResponse<GetArticleByTagListDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetArticlesByTagHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetArticleByTagListDto>> Handle(GetArticlesByTagQuery request, CancellationToken cancellationToken)
        {
            var tagNameNormalized = request.TagName.Replace("-", " ").ToLower();

           
            IPaginate<Article> articles = await _articleRepository.GetListAsync(
                predicate: a => a.ArticleTags.Any(at => at.Tag.Name.ToLower() == tagNameNormalized), 
                include: a => a.Include(at => at.ArticleTags)
                               .ThenInclude(at => at.Tag)
                               .Include(at => at.SubCategory), 
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );


            GetListResponse<GetArticleByTagListDto> response = _mapper.Map<GetListResponse<GetArticleByTagListDto>>(articles);
            return response;
        }
    }



}
