using Core.Application.Responses;

namespace Application.Features.Columnists.Commands.Delete;

public class DeletedColumnistResponse : IResponse
{
    public Guid Id { get; set; }
}