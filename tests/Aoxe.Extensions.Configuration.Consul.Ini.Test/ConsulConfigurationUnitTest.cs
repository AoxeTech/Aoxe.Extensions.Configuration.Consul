namespace Aoxe.Extensions.Configuration.Consul.Ini.Test;

public class ConsulConfigurationUnitTest
{
    [Fact]
    public async Task ConfigurationTest()
    {
        using var consulClient = new ConsulClient();
        await consulClient.KV.Put(
            new KVPair("test-ini")
            {
                Value = Encoding.UTF8.GetBytes("[nestedSection]\nnestedStringKey=nestedStringValue")
            }
        );

        var configBuilder = new ConfigurationBuilder().AddConsul(
            new ConsulClientConfiguration
            {
                Address = new Uri("http://localhost:8500"),
                Datacenter = "dc1",
            },
            "test-ini",
            new IniFlattener()
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedSection:nestedStringKey"]);
    }

    [Fact]
    public async Task ConfigurationIniTest()
    {
        using var consulClient = new ConsulClient();
        await consulClient.KV.Put(
            new KVPair("test-ini")
            {
                Value = Encoding.UTF8.GetBytes("[nestedSection]\nnestedStringKey=nestedStringValue")
            }
        );

        var configBuilder = new ConfigurationBuilder().AddConsulIni(
            new ConsulClientConfiguration
            {
                Address = new Uri("http://localhost:8500"),
                Datacenter = "dc1",
            },
            "test-ini"
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedSection:nestedStringKey"]);
    }
}
