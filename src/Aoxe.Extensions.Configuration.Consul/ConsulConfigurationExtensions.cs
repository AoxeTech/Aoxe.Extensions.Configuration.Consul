namespace Aoxe.Extensions.Configuration.Consul;

public static class ConsulConfigurationExtensions
{
    public static IConfigurationBuilder AddConsul(
        this IConfigurationBuilder builder,
        Func<ConsulClientConfiguration> consulClientConfigurationFactory,
        string key,
        IFlattener? flattener = null
    ) =>
        builder.Add(
            new ConsulConfigurationSource(consulClientConfigurationFactory, key, flattener)
        );

    public static IConfigurationBuilder AddConsul(
        this IConfigurationBuilder builder,
        ConsulClientConfiguration consulClientConfiguration,
        string key,
        IFlattener? flattener = null
    ) => builder.Add(new ConsulConfigurationSource(consulClientConfiguration, key, flattener));
}
