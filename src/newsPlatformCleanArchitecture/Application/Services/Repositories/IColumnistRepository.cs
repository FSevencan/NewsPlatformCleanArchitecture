using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IColumnistRepository : IAsyncRepository<Columnist, Guid>, IRepository<Columnist, Guid>
{
}