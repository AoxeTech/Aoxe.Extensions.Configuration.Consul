namespace Aoxe.Extensions.Configuration.Consul.Xml.Test;

public class ConsulConfigurationUnitTest
{
    [Fact]
    public async Task ConfigurationTest()
    {
        using var consulClient = new ConsulClient();
        await consulClient.KV.Put(
            new KVPair("test-xml")
            {
                Value = Encoding.UTF8.GetBytes(
                    "<root><nestedObject><nestedStringKey>nestedStringValue</nestedStringKey></nestedObject></root>"
                )
            }
        );

        var configBuilder = new ConfigurationBuilder().AddConsul(
            new ConsulClientConfiguration
            {
                Address = new Uri("http://localhost:8500"),
                Datacenter = "dc1",
            },
            "test-xml",
            new XmlFlattener()
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }

    [Fact]
    public async Task ConfigurationXmlTest()
    {
        using var consulClient = new ConsulClient();
        await consulClient.KV.Put(
            new KVPair("test-xml")
            {
                Value =
                    "<root><nestedObject><nestedStringKey>nestedStringValue</nestedStringKey></nestedObject></root>"u8.ToArray()
            }
        );

        var configBuilder = new ConfigurationBuilder().AddConsulXml(
            new ConsulClientConfiguration
            {
                Address = new Uri("http://localhost:8500"),
                Datacenter = "dc1",
            },
            "test-xml"
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }
}
