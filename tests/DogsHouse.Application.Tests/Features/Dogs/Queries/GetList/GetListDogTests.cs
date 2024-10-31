using Application.Features.Dogs.Queries.GetList;
using DogsHouse.Application.Tests.Mocks.FakeDatas;
using DogsHouse.Application.Tests.Mocks.Repositories.Dogs;
using NArchitecture.Core.Application.Requests;
using static Application.Features.Dogs.Queries.GetList.GetListDogQuery;

namespace DogsHouse.Application.Tests.Features.Dogs.Queries.GetList;

public class GetListDogTests : DogMockRepository
{
    private readonly GetListDogQuery _query;
    private readonly GetListDogQueryHandler _handler;

    public GetListDogTests(DogFakeData fakeData, GetListDogQuery query)
        : base(fakeData)
    {
        _query = query;
        _handler = new GetListDogQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    public async Task GetListShouldSuccessfully()
    {
        _query.PageRequest = new PageRequest
        {
            PageIndex = 0,
            PageSize = 10
        };

        var result = await _handler.Handle(_query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Contains(result.Items, item => item.Name == "Buddy");
    }
}
