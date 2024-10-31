using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Dogs;

public interface IDogService
{
    Task<Dog?> GetAsync(
        Expression<Func<Dog, bool>> predicate,
        Func<IQueryable<Dog>, IIncludableQueryable<Dog, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Dog>?> GetListAsync(
        Expression<Func<Dog, bool>>? predicate = null,
        Func<IQueryable<Dog>, IOrderedQueryable<Dog>>? orderBy = null,
        Func<IQueryable<Dog>, IIncludableQueryable<Dog, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Dog> AddAsync(Dog dog);
    Task<Dog> UpdateAsync(Dog dog);
    Task<Dog> DeleteAsync(Dog dog, bool permanent = false);
}
