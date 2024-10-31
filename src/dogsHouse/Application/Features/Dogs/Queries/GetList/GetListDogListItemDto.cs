using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Dogs.Queries.GetList;

public class GetListDogListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public ushort TailLength { get; set; }
    public ushort Weight { get; set; }
}