using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ISubscribleRepository : IAsyncRepository<Subscrible, Guid>, IRepository<Subscrible, Guid>
{
}