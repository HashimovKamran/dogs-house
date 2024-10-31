using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Dog : Entity<Guid>
{
    public string Name { get; set; }
    public string Color { get; set; }
    public ushort TailLength { get; set; }
    public ushort Weight { get; set; }
}
