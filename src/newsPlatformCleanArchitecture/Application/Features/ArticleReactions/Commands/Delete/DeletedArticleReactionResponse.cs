using Core.Application.Responses;

namespace Application.Features.ArticleReactions.Commands.Delete;

public class DeletedArticleReactionResponse : IResponse
{
    public Guid Id { get; set; }
}