using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IPollRepository : IAsyncRepository<Poll, Guid>, IRepository<Poll, Guid>
{
}