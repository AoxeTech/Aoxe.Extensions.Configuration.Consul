using System.Text;

namespace Aoxe.Extensions.Configuration.Consul.Test;

public class ConsulConfigurationUnitTest
{
    [Fact]
    public async Task ConfigurationTest()
    {
        using var consulClient = new ConsulClient();
        await consulClient.KV.Put(
            new KVPair("test-json")
            {
                Value = Encoding.UTF8.GetBytes(
                    "{\"nestedObject\": {\"nestedStringKey\": \"nestedStringValue\"}}"
                )
            }
        );

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
}
