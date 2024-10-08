﻿namespace Aoxe.Extensions.Configuration.Consul.Ini;

public static class ConsulConfigurationExtensions
{
    public static IConfigurationBuilder AddConsulIni(
        this IConfigurationBuilder builder,
        Func<ConsulClientConfiguration> consulClientConfigurationFactory,
        string key
    ) =>
        builder.Add(
            new ConsulConfigurationSource(consulClientConfigurationFactory, key, new IniFlattener())
        );

    public static IConfigurationBuilder AddConsulIni(
        this IConfigurationBuilder builder,
        ConsulClientConfiguration consulClientConfiguration,
        string key
    ) =>
        builder.Add(
            new ConsulConfigurationSource(consulClientConfiguration, key, new IniFlattener())
        );
}
