using Core.Application.Responses;

namespace Application.Features.Columnists.Queries.GetById;

public class GetByIdColumnistResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Biography { get; set; }
    public string ProfilePicture { get; set; }
    public string Email { get; set; }
    public string LinkedinLink { get; set; }
}