namespace Aoxe.Extensions.Configuration.Consul;

public class ConsulConfigurationProvider(
    ConsulConfigurationSource source,
    IConsulClientFactory consulClientFactory,
    IFlattener? flattener = null
) : ConfigurationProvider
{
    public override void Load()
    {
        using var consulClient = consulClientFactory.Create();
        var result = consulClient.KV.Get(source.Key).Result;
        if (result.Response is null)
            return;
        Data = flattener is null
            ? new Dictionary<string, string?>
            {
                { source.Key, Encoding.UTF8.GetString(result.Response.Value) }
            }
            : flattener.Flatten(new MemoryStream(result.Response.Value));
    }
}
