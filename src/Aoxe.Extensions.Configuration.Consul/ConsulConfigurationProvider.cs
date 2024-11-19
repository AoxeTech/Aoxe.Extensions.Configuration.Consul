namespace Aoxe.Extensions.Configuration.Consul;

public class ConsulConfigurationProvider : ConfigurationProvider, IDisposable
{
    private readonly ConsulClient _client;
    private readonly string _key;
    private ulong _waitIndex;
    private CancellationTokenSource _cts;

    public ConsulConfigurationProvider(ConsulClient client, string key)
    {
        _client = client;
        _key = key;
        _cts = new CancellationTokenSource();

        // Load initial configuration
        Load();

        // Start watching for changes
        Watch(_cts.Token);
    }

    public override void Load()
    {
        var getPair = _client.KV.Get(_key).Result;
        if (getPair.Response != null)
        {
            Data = ParseData(getPair.Response.Value);
            _waitIndex = getPair.LastIndex;
        }
    }

    private async void Watch(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var queryOptions = new QueryOptions()
            {
                WaitIndex = _waitIndex,
                WaitTime = TimeSpan.FromMinutes(10)
            };

            var getPair = await _client.KV.Get(_key, queryOptions, token);
            if (getPair.LastIndex != _waitIndex)
            {
                Load();
                OnReload();
            }

            _waitIndex = getPair.LastIndex;
        }
    }

    private IDictionary<string, string> ParseData(byte[] data)
    {
        // Implement parsing logic based on your configuration format (e.g., JSON, YAML)
        // For example, deserialize JSON into a dictionary
    }

    public void Dispose()
    {
        _cts.Cancel();
    }
}
