namespace Aoxe.Extensions.Configuration.Consul.YamlDotNet;

public static class ConsulConfigurationExtensions
{
    public static IConfigurationBuilder AddConsulYaml(
        this IConfigurationBuilder builder,
        ConsulClientConfiguration consulClientConfiguration,
        string key
    ) =>
        builder.Add(
            new ConsulConfigurationSource(consulClientConfiguration, key, new YamlFlattener())
        );
}
