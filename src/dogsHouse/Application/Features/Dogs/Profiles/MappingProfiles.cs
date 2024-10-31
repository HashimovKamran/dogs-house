using Application.Features.Dogs.Commands.Create;
using Application.Features.Dogs.Commands.Delete;
using Application.Features.Dogs.Commands.Update;
using Application.Features.Dogs.Queries.GetById;
using Application.Features.Dogs.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Dogs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateDogCommand, Dog>();
        CreateMap<Dog, CreatedDogResponse>();

        CreateMap<UpdateDogCommand, Dog>();
        CreateMap<Dog, UpdatedDogResponse>();

        CreateMap<DeleteDogCommand, Dog>();
        CreateMap<Dog, DeletedDogResponse>();

        CreateMap<Dog, GetByIdDogResponse>();

        CreateMap<Dog, GetListDogListItemDto>();
        CreateMap<IPaginate<Dog>, GetListResponse<GetListDogListItemDto>>();
    }
}