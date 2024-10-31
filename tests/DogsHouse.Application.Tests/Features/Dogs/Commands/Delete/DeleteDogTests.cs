using Application.Features.Dogs.Commands.Delete;
using DogsHouse.Application.Tests.Mocks.FakeDatas;
using DogsHouse.Application.Tests.Mocks.Repositories.Dogs;
using static Application.Features.Dogs.Commands.Delete.DeleteDogCommand;

namespace DogsHouse.Application.Tests.Features.Dogs.Commands.Delete;

public class DeleteDogTests : DogMockRepository
{
    private readonly DeleteDogCommandValidator _validator;
    private readonly DeleteDogCommand _command;
    private readonly DeleteDogCommandHandler _handler;

    public DeleteDogTests(DogFakeData fakeData, DeleteDogCommandValidator validator, DeleteDogCommand command)
        : base(fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new DeleteDogCommandHandler(Mapper, MockRepository.Object, BusinessRules);
    }

    [Fact]
    public async Task DeleteShouldSuccessfully()
    {
        _command.Id = DogFakeData.Ids[0];

        DeletedDogResponse result = await _handler.Handle(_command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(expected: DogFakeData.Ids[0], result.Id);
    }
}
