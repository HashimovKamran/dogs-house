using Application.Features.Dogs.Commands.Create;
using Application.Features.Dogs.Commands.Delete;
using Application.Features.Dogs.Commands.Update;
using Application.Features.Dogs.Queries.GetById;
using Application.Features.Dogs.Queries.GetList;
using DogsHouse.Application.Tests.Mocks.FakeDatas;
using Microsoft.Extensions.DependencyInjection;

namespace DogsHouse.Application.Tests.DependencyResolvers;

public static class DogServiceRegistrations
{
    public static void AddDogsServices(this IServiceCollection services)
    {
        services.AddTransient<DogFakeData>();
        services.AddTransient<CreateDogCommand>();
        services.AddTransient<CreateDogCommandValidator>();
        services.AddTransient<UpdateDogCommand>();
        services.AddTransient<UpdateDogCommandValidator>();
        services.AddTransient<DeleteDogCommand>();
        services.AddTransient<DeleteDogCommandValidator>();
        services.AddTransient<GetListDogQuery>();
        services.AddTransient<GetByIdDogQuery>();
    }
}
