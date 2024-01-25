using Core.Application.Responses;

namespace Application.Features.ArticleTags.Commands.Delete;

public class DeletedArticleTagResponse : IResponse
{
    public Guid Id { get; set; }
}