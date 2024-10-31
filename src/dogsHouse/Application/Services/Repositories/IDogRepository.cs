using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IDogRepository : IAsyncRepository<Dog, Guid>, IRepository<Dog, Guid>
{
}