using NArchitecture.Core.Application.Responses;

namespace Application.Features.Dogs.Commands.Update;

public class UpdatedDogResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public ushort TailLength { get; set; }
    public ushort Weight { get; set; }
}