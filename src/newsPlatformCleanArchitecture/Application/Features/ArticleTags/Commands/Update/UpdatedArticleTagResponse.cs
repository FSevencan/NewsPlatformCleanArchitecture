
using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.ArticleTags.Commands.Update;

public class UpdatedArticleTagResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public Guid TagId { get; set; }
  
}