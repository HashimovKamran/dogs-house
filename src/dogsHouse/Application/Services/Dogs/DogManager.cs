using Application.Features.Dogs.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Dogs;

public class DogManager : IDogService
{
    private readonly IDogRepository _dogRepository;
    private readonly DogBusinessRules _dogBusinessRules;

    public DogManager(IDogRepository dogRepository, DogBusinessRules dogBusinessRules)
    {
        _dogRepository = dogRepository;
        _dogBusinessRules = dogBusinessRules;
    }

    public async Task<Dog?> GetAsync(
        Expression<Func<Dog, bool>> predicate,
        Func<IQueryable<Dog>, IIncludableQueryable<Dog, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Dog? dog = await _dogRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return dog;
    }

    public async Task<IPaginate<Dog>?> GetListAsync(
        Expression<Func<Dog, bool>>? predicate = null,
        Func<IQueryable<Dog>, IOrderedQueryable<Dog>>? orderBy = null,
        Func<IQueryable<Dog>, IIncludableQueryable<Dog, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Dog> dogList = await _dogRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return dogList;
    }

    public async Task<Dog> AddAsync(Dog dog)
    {
        Dog addedDog = await _dogRepository.AddAsync(dog);

        return addedDog;
    }

    public async Task<Dog> UpdateAsync(Dog dog)
    {
        Dog updatedDog = await _dogRepository.UpdateAsync(dog);

        return updatedDog;
    }

    public async Task<Dog> DeleteAsync(Dog dog, bool permanent = false)
    {
        Dog deletedDog = await _dogRepository.DeleteAsync(dog);

        return deletedDog;
    }
}
