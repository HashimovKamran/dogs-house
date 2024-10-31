using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DogRepository : EfRepositoryBase<Dog, Guid, BaseDbContext>, IDogRepository
{
    public DogRepository(BaseDbContext context) : base(context)
    {
    }
}