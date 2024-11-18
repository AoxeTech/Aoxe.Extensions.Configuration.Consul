namespace Aoxe.Extensions.Configuration.Consul.SharpYaml.Test;

public class ConsulConfigurationUnitTest
{
    [Fact]
    public async Task ConfigurationTest()
    {
        using var consulClient = new ConsulClient();
        await consulClient.KV.Put(
            new KVPair("test-yaml")
            {
                Value = Encoding.UTF8.GetBytes(
                    "nestedObject:\n  nestedStringKey: nestedStringValue"
                )
            }
        );

        var configBuilder = new ConfigurationBuilder().AddConsul(
            new ConsulClientConfiguration
            {
                Address = new Uri("http://localhost:8500"),
                Datacenter = "dc1",
            },
            "test-yaml",
            new YamlFlattener()
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }

    [Fact]
    public async Task ConfigurationIniTest()
    {
        using var consulClient = new ConsulClient();
        await consulClient.KV.Put(
            new KVPair("test-yaml")
            {
                Value = Encoding.UTF8.GetBytes(
                    "nestedObject:\n  nestedStringKey: nestedStringValue"
                )
            }
        );

        var configBuilder = new ConfigurationBuilder().AddConsulYaml(
            new ConsulClientConfiguration
            {
                Address = new Uri("http://localhost:8500"),
                Datacenter = "dc1",
            },
            "test-yaml"
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }
}
