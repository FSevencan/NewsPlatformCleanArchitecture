using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ColumnistRepository : EfRepositoryBase<Columnist, Guid, BaseDbContext>, IColumnistRepository
{
    public ColumnistRepository(BaseDbContext context) : base(context)
    {
    }
}