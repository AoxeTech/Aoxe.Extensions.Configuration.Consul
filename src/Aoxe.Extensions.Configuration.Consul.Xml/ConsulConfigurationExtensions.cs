namespace Aoxe.Extensions.Configuration.Consul.Xml;

public static class ConsulConfigurationExtensions
{
    public static IConfigurationBuilder AddConsulXml(
        this IConfigurationBuilder builder,
        Func<ConsulClientConfiguration> consulClientConfigurationFactory,
        string key
    ) =>
        builder.Add(
            new ConsulConfigurationSource(consulClientConfigurationFactory, key, new XmlFlattener())
        );

    public static IConfigurationBuilder AddConsulXml(
        this IConfigurationBuilder builder,
        ConsulClientConfiguration consulClientConfiguration,
        string key
    ) =>
        builder.Add(
            new ConsulConfigurationSource(consulClientConfiguration, key, new XmlFlattener())
        );
}
