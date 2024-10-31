using Microsoft.Extensions.Configuration;

namespace DogsHouse.Application.Tests.Mocks.Configurations;
public static class MockConfiguration
{
    public static IConfiguration GetConfigurationMock()
    {
        var mockConfiguration = new Dictionary<string, string>
        {
            { "ConnectionStrings:MsSql", "Server=localhost,1433;Database=DogsHouse;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;" },
        };
        IConfigurationBuilder configuration = new ConfigurationBuilder().AddInMemoryCollection(mockConfiguration);
        return configuration.Build();
    }
}
