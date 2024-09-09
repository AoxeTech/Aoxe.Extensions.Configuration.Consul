namespace Aoxe.Extensions.Configuration.Consul.SharpYaml;

public static class ConsulConfigurationExtensions
{
    public static IConfigurationBuilder AddConsulYaml(
        this IConfigurationBuilder builder,
        Func<ConsulClientConfiguration> consulClientConfigurationFactory,
        string key
    ) =>
        builder.Add(
            new ConsulConfigurationSource(
                consulClientConfigurationFactory,
                key,
                new YamlFlattener()
            )
        );

    public static IConfigurationBuilder AddConsulYaml(
        this IConfigurationBuilder builder,
        ConsulClientConfiguration consulClientConfiguration,
        string key
    ) =>
        builder.Add(
            new ConsulConfigurationSource(consulClientConfiguration, key, new YamlFlattener())
        );
}
