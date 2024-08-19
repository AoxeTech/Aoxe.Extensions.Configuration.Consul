namespace Aoxe.Extensions.Configuration.Consul;

public class ConsulClientFactory : IConsulClientFactory
{
    public ConsulClient Create(ConsulClientConfiguration consulClientConfiguration) =>
        new(consulClientConfiguration);

    public ConsulClient Create(
        Action<ConsulClientConfiguration>? configOverride,
        Action<HttpClient>? clientOverride,
        Action<HttpClientHandler>? handlerOverride
    ) => new(configOverride, clientOverride, handlerOverride);
}
