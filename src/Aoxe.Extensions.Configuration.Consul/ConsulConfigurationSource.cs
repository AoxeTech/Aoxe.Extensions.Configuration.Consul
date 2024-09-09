namespace Aoxe.Extensions.Configuration.Consul;

public class ConsulConfigurationSource(
    ConsulClientConfiguration consulClientConfiguration,
    string key,
    IFlattener? flattener = null
) : IConfigurationSource
{
    public ConsulClientConfiguration ConsulClientConfiguration { get; } = consulClientConfiguration;
    public string Key { get; } = key;

    public ConsulConfigurationSource(
        Func<ConsulClientConfiguration> consulClientConfigurationFactory,
        string key,
        IFlattener? flattener = null
    )
        : this(consulClientConfigurationFactory(), key, flattener) { }

    public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new ConsulConfigurationProvider(this, new ConsulClientFactory(this), flattener);
}
