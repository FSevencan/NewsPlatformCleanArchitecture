using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.ArticleTags.Queries.GetById;

public class GetByIdArticleTagResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public Guid TagId { get; set; }
  
}