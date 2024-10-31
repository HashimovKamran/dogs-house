using Application.Features.Dogs.Queries.GetById;
using DogsHouse.Application.Tests.Mocks.FakeDatas;
using DogsHouse.Application.Tests.Mocks.Repositories.Dogs;
using static Application.Features.Dogs.Queries.GetById.GetByIdDogQuery;

namespace DogsHouse.Application.Tests.Features.Dogs.Queries.GetById;

public class GetByIdDogTests : DogMockRepository
{
    private readonly GetByIdDogQuery _query;
    private readonly GetByIdDogQueryHandler _handler;

    public GetByIdDogTests(DogFakeData fakeData, GetByIdDogQuery query)
        : base(fakeData)
    {
        _query = query;
        _handler = new GetByIdDogQueryHandler(Mapper, MockRepository.Object, BusinessRules);
    }

    [Fact]
    public async Task GetByIdShouldSuccessfully()
    {
        _query.Id = DogFakeData.Ids[0];

        var result = await _handler.Handle(_query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(expected: DogFakeData.Ids[0], result.Id);
    }
}
