using Domain.Entities;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Repositories;

public interface IPollRepository : IAsyncRepository<Poll, Guid>, IRepository<Poll, Guid>
{
    Task<Poll?> GetLatestPollAsync(CancellationToken cancellationToken);

}