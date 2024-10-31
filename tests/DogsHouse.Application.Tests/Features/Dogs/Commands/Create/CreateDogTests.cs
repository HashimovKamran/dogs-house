using Application.Features.Dogs.Commands.Create;
using DogsHouse.Application.Tests.Mocks.FakeDatas;
using DogsHouse.Application.Tests.Mocks.Repositories.Dogs;
using static Application.Features.Dogs.Commands.Create.CreateDogCommand;

namespace DogsHouse.Application.Tests.Features.Dogs.Commands.Create;

public class CreateDogTests : DogMockRepository
{
    private readonly CreateDogCommandValidator _validator;
    private readonly CreateDogCommand _command;
    private readonly CreateDogCommandHandler _handler;

    public CreateDogTests(DogFakeData fakeData, CreateDogCommandValidator validator, CreateDogCommand command)
        : base(fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new CreateDogCommandHandler(Mapper, MockRepository.Object, BusinessRules);
    }

    [Fact]
    public async Task CreateShouldSuccessfully()
    {
        _command.Name = "Rocky";
        _command.Color = "White&Brown";
        _command.TailLength = 12;
        _command.Weight = 28;

        CreatedDogResponse result = await _handler.Handle(_command, CancellationToken.None);

        Assert.Equal(expected: "Rocky", result.Name);
    }
}
