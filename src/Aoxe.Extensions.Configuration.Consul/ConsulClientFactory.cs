namespace Aoxe.Extensions.Configuration.Consul;

public class ConsulClientFactory(ConsulConfigurationSource source) : IConsulClientFactory
{
    public ConsulClient Create() => new(source.ConsulClientConfiguration);
}
