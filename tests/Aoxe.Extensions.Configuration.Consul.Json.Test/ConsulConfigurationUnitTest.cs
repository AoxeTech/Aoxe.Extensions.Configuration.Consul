namespace Aoxe.Extensions.Configuration.Consul.Json.Test;

public class ConsulConfigurationUnitTest
{
    [Fact]
    public void ConfigurationTest()
    {
        var configBuilder = new ConfigurationBuilder().AddConsul(
            new ConsulClientConfiguration
            {
                Address = new Uri("http://localhost:8500"),
                Datacenter = "dc1",
            },
            "test-json",
            new JsonFlattener()
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }

    [Fact]
    public void ConfigurationJsonTest()
    {
        var configBuilder = new ConfigurationBuilder().AddConsulJson(
            new ConsulClientConfiguration
            {
                Address = new Uri("http://localhost:8500"),
                Datacenter = "dc1",
            },
            "test-json"
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }
}