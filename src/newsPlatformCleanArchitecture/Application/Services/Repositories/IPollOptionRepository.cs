using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IPollOptionRepository : IAsyncRepository<PollOption, Guid>, IRepository<PollOption, Guid>
{
}