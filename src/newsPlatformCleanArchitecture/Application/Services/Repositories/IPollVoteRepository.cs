using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IPollVoteRepository : IAsyncRepository<PollVote, Guid>, IRepository<PollVote, Guid>
{
}