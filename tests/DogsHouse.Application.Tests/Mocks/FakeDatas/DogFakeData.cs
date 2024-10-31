using Domain.Entities;
using NArchitecture.Core.Test.Application.FakeData;

namespace DogsHouse.Application.Tests.Mocks.FakeDatas;

public class DogFakeData : BaseFakeData<Dog, Guid>
{
    public static Guid[] Ids = [Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()];

    public override List<Dog> CreateFakeData()
    {
        return [
            new Dog { Id = Ids[0], Name = "Buddy", Color = "Brown&White", TailLength = 15, Weight = 30 },
            new Dog { Id = Ids[1], Name = "Bella", Color = "Black&Gray", TailLength = 20, Weight = 25 },
            new Dog { Id = Ids[2], Name = "Max", Color = "Golden&White", TailLength = 18, Weight = 28 },
            new Dog { Id = Ids[3], Name = "Luna", Color = "White&Gray", TailLength = 16, Weight = 22 },
            new Dog { Id = Ids[4], Name = "Charlie", Color = "Gray&Black", TailLength = 14, Weight = 24 }
        ];
    }
}