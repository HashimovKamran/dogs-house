using Application.Features.Dogs.Commands.Update;
using DogsHouse.Application.Tests.Mocks.FakeDatas;
using DogsHouse.Application.Tests.Mocks.Repositories.Dogs;
using static Application.Features.Dogs.Commands.Update.UpdateDogCommand;

namespace DogsHouse.Application.Tests.Features.Dogs.Commands.Update;

public class UpdateDogTests : DogMockRepository
{
    private readonly UpdateDogCommandValidator _validator;
    private readonly UpdateDogCommand _command;
    private readonly UpdateDogCommandHandler _handler;

    public UpdateDogTests(DogFakeData fakeData, UpdateDogCommandValidator validator, UpdateDogCommand command)
        : base(fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new UpdateDogCommandHandler(Mapper, MockRepository.Object, BusinessRules);
    }

    [Fact]
    public async Task UpdateShouldSuccessfully()
    {
        _command.Id = DogFakeData.Ids[0];
        _command.Name = "Milo";
        _command.Color = "Black&White";
        _command.TailLength = 18;
        _command.Weight = 32;

        UpdatedDogResponse result = await _handler.Handle(_command, CancellationToken.None);

        Assert.Equal(expected: "Milo", result.Name);
    }
}
