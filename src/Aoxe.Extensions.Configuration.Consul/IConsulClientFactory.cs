namespace Aoxe.Extensions.Configuration.Consul;

internal interface IConsulClientFactory
{
    ConsulClient Create(ConsulClientConfiguration consulClientConfiguration);
    ConsulClient Create(
        Action<ConsulClientConfiguration>? configOverride,
        Action<HttpClient>? clientOverride,
        Action<HttpClientHandler>? handlerOverride
    );
}
