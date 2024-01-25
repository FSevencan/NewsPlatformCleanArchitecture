using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SubCategoryRepository : EfRepositoryBase<SubCategory, Guid, BaseDbContext>, ISubCategoryRepository
{
    public SubCategoryRepository(BaseDbContext context) : base(context)
    {
    }
}