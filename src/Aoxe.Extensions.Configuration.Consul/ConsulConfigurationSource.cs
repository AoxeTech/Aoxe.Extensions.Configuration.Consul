namespace Aoxe.Extensions.Configuration.Consul;

public class ConsulConfigurationSource : IConfigurationSource
{
    private readonly ConsulClient _client;
    private readonly string _key;

    public ConsulConfigurationSource(ConsulClient client, string key)
    {
        _client = client;
        _key = key;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new ConsulConfigurationProvider(_client, _key);
    }
}
