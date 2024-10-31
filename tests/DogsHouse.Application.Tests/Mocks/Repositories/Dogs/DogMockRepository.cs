using Application.Features.Dogs.Profiles;
using Application.Features.Dogs.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using DogsHouse.Application.Tests.Mocks.FakeDatas;
using NArchitecture.Core.Test.Application.Repositories;

namespace DogsHouse.Application.Tests.Mocks.Repositories.Dogs;

public class DogMockRepository : BaseMockRepository<IDogRepository, Dog, Guid, MappingProfiles, DogBusinessRules, DogFakeData>
{
    public DogMockRepository(DogFakeData fakeData) : base(fakeData)
    {
    }
}
