namespace Aoxe.Extensions.Configuration.Consul.Tomlet;

public static class ConsulConfigurationExtensions
{
    public static IConfigurationBuilder AddConsulToml(
        this IConfigurationBuilder builder,
        ConsulClientConfiguration consulClientConfiguration,
        string key
    ) =>
        builder.Add(
            new ConsulConfigurationSource(consulClientConfiguration, key, new TomlFlattener())
        );
}
