namespace Aoxe.Extensions.Configuration.Consul.NewtonsoftJson;

public static class ConsulConfigurationExtensions
{
    public static IConfigurationBuilder AddConsulJson(
        this IConfigurationBuilder builder,
        Func<ConsulClientConfiguration> consulClientConfigurationFactory,
        string key
    ) =>
        builder.Add(
            new ConsulConfigurationSource(
                consulClientConfigurationFactory,
                key,
                new JsonFlattener()
            )
        );

    public static IConfigurationBuilder AddConsulJson(
        this IConfigurationBuilder builder,
        ConsulClientConfiguration consulClientConfiguration,
        string key
    ) =>
        builder.Add(
            new ConsulConfigurationSource(consulClientConfiguration, key, new JsonFlattener())
        );
}
