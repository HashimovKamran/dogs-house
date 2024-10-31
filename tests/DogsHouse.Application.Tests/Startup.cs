using DogsHouse.Application.Tests.DependencyResolvers;
using Microsoft.Extensions.DependencyInjection;

namespace DogsHouse.Application.Tests;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDogsServices();
    }
}
