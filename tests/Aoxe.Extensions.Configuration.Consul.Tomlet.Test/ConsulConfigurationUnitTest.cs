namespace Aoxe.Extensions.Configuration.Consul.Tomlet.Test;

public class ConsulConfigurationUnitTest
{
    [Fact]
    public async Task ConfigurationTest()
    {
        using var consulClient = new ConsulClient();
        await consulClient.KV.Put(
            new KVPair("test-toml")
            {
                Value = Encoding.UTF8.GetBytes(
                    "[nestedObject]\nnestedStringKey=\"nestedStringValue\""
                )
            }
        );

        var configBuilder = new ConfigurationBuilder().AddConsul(
            new ConsulClientConfiguration
            {
                Address = new Uri("http://localhost:8500"),
                Datacenter = "dc1",
            },
            "test-toml",
            new TomlFlattener()
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }

    [Fact]
    public async Task ConfigurationIniTest()
    {
        using var consulClient = new ConsulClient();
        await consulClient.KV.Put(
            new KVPair("test-toml")
            {
                Value = Encoding.UTF8.GetBytes(
                    "[nestedObject]\nnestedStringKey=\"nestedStringValue\""
                )
            }
        );

        var configBuilder = new ConfigurationBuilder().AddConsulToml(
            new ConsulClientConfiguration
            {
                Address = new Uri("http://localhost:8500"),
                Datacenter = "dc1",
            },
            "test-toml"
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }
}
