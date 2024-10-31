using NArchitecture.Core.Application.Responses;

namespace Application.Features.Dogs.Commands.Delete;

public class DeletedDogResponse : IResponse
{
    public Guid Id { get; set; }
}