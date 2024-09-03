# Aoxe.Extensions.Configuration.Consul

[English](README.md) | 简体中文

---

使用 consul 作为配置源, 包括不同的数据源及其对应的解析器 (Json / Xml / Ini / Toml / Yaml).

- [Aoxe.Extensions.Configuration](https://github.com/AoxeTech/Aoxe.Extensions.Configuration)
- [Aoxe.Extensions.Configuration.Consul](https://github.com/AoxeTech/Aoxe.Extensions.Configuration.Consul)
- [Aoxe.Extensions.Configuration.Etcd](https://github.com/AoxeTech/Aoxe.Extensions.Configuration.Etcd)
- [Aoxe.Extensions.Configuration.Redis](https://github.com/AoxeTech/Aoxe.Extensions.Configuration.Redis)

## 1. 为什么需要这个项目

Microsoft.Extensions.Configuration 提供开箱即用的强大配置功能。它支持多种格式（Json / Xml / Ini），涵盖了大多数使用场景。但我认为它仍有一些可以改进的地方。

### 1.1. 解析器是核心功能，但并没有定义为 public，这种设计影响了模块化和重用

每个配置器都基于其流版本，如 JsonConfigurationProvider / JsonStreamConfigurationProvider 和 XmlConfigurationProvider / XmlStreamConfigurationProvider 等。在 stream provider 中，它使用序列化器将结构流解析为 Dictionary<string，string?>。但在这些官方的 Configuration 中，解析器类或其函数被定义为 internal 或 pravite。

### 1.2. 使用其它序列化器

就像 Microsoft.Extensions.Configuration.Json 基于 System.Text.Json，它是一个很好的软件包，但由于某些原因，有人更喜欢 Newtonsoft.Json 或其他序列化器，而 Microsoft.Extensions.Configuration.NewtonsoftJson 已从 2020 年起被设置为废弃。除了 Json / Xml / Ini 之外，我们现在可能还需要其他配置格式，如 Toml / Yaml 等。

### 1.3. 更多的配置源

随着微服务和云的发展，集中配置将变得越来越重要。微软为我们提供了一些集中配置源，但它们通常基于azure。在云如此强大的情况下，我们更有理由保持云中立，因为有些项目和服务更喜欢私有部署的键值存储。

## 2. 本项目的作用

### 2.1. Flantteners

对于解析器，Aoxe.Extensions.Configuration 抽象了一个名为 Flanttener 的概念，用于将数据流解析并扁平化为 Dictionary<string, string?>。现在我们有了以下实现：

#### 2.1.1. Aoxe.Extensions.Configuration.Flattener.Ini

基于 Microsoft.Extensions.Configuration.Ini 并具有相同的行为。

#### 2.1.2. Aoxe.Extensions.Configuration.Flattener.IniParser

[ini-parser](https://github.com/rickyah/ini-parser) 是一个兼容.NET、Mono 和 Unity3d（*）的库，用于从 IO 流、文件流和 C# 编写的字符串中读取/写入 INI 数据。

该库还实现了合并操作，既可合并完整的 INI 文件，也可合并部分文件，甚至只合并文件中包含的键的子集。

(*) 该库是 100% .NET 代码，不依赖于 Windows API 调用，因此具有可移植性。

#### 2.1.3. Aoxe.Extensions.Configuration.Flattener.Json

基于 Microsoft.Extensions.Configuration.Json 并具有相同的行为。

#### 2.1.4. Aoxe.Extensions.Configuration.Flattener.NewtonsoftJson

[Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) 是.NET 中流行的高性能 JSON 框架。有了这个实现，我们就可以通过 JsonSerializerSettings 来实现反序列化。

#### 2.1.5. Aoxe.Extensions.Configuration.Flattener.SharpYaml

[SharpYaml](https://github.com/xoofx/SharpYaml) 是一个.NET 库，为.NET 对象提供 YAML 解析器和序列化引擎，与 CoreCLR 兼容。

#### 2.1.6. Aoxe.Extensions.Configuration.Flattener.Tomlet

[Tomlet](https://github.com/SamboyCoding/Tomlet) 是一个零依赖、基于模型的 TOML De/Serializer for .NET。

#### 2.1.7. Aoxe.Extensions.Configuration.Flattener.Tomlyn

[Tomlyn](https://github.com/xoofx/Tomlyn) 是一个适用于.NET Framework和.NET Core的TOML解析器、验证器和创作库。

#### 2.1.8. Aoxe.Extensions.Configuration.Flattener.Xml

基于 Microsoft.Extensions.Configuration.Xml 并具有相同的行为。

#### 2.1.9. Aoxe.Extensions.Configuration.Flattener.YamlDotNet

[YamlDotNet](https://github.com/aaubry/YamlDotNet) 是一个用于 YAML 的 .NET 库。YamlDotNet 提供 YAML 的低级解析和发射，以及类似于 XmlDocument 的高级对象模型。该库还包含一个序列化库，允许从 YAML 流中读写对象。

### 2.2. 更多的配置源

除了配置文件，现代服务越来越依赖于集中配置。为此，有以下这些配置源：

#### 2.2.1. [Aoxe.Extensions.Configuration](https://github.com/AoxeTech/Aoxe.Extensions.Configuration)

因为 Flantten 的抽象, Aoxe.Extensions.Configuration 支持不同的配置文件，如 Json、Xml、Ini、Toml 和 Yaml。

#### 2.2.2. [Aoxe.Extensions.Configuration.Consul](https://github.com/AoxeTech/Aoxe.Extensions.Configuration.Consul)

[Consul](https://www.consul.io/) 是一个服务网络解决方案，它提供了一个具有服务发现、配置和分段功能的全功能控制平面。它的设计目的是在分布式基础设施上轻松管理和保障服务间通信。

#### 2.2.3. [Aoxe.Extensions.Configuration.Etcd](https://github.com/AoxeTech/Aoxe.Extensions.Configuration.Etcd)

[Etcd](https://etcd.io/) 是一种分布式键值存储，为跨机器集群存储数据提供了一种可靠的方式。它用于存储配置数据、元数据和其他需要由分布式系统访问的关键信息。

#### 2.2.4. [Aoxe.Extensions.Configuration.Redis](https://github.com/AoxeTech/Aoxe.Extensions.Configuration.Redis)

[Redis（远程字典服务器）](https://redis.io/) 是一种开源的内存数据结构存储，可用作数据库、缓存和消息代理。它支持各种数据结构，如字符串、哈希值、列表、集合、排序集合、位图、超日志和地理空间索引。

## 3. 如何使用

以一个 json 格式的配置为例, 我们现在有一个以 "test-json" 为 key 的配置内容如下:

```json
{
  "stringKey": "stringValue",
  "numberKey": 123,
  "booleanKey": true,
  "nullKey": null,
  "nestedObject": {
    "nestedStringKey": "nestedStringValue",
    "nestedNumberKey": 456,
    "nestedBooleanKey": false,
    "nestedNullKey": null
  },
  "arrayKey": [
    "arrayStringValue",
    789,
    false,
    null,
    {
      "arrayNestedObjectKey": "arrayNestedObjectValue"
    }
  ]
}
```

然后我们通过 nuget 安装对应的包.

```shell
PM> Install-Package Aoxe.Extensions.Configuration.Consul.Json
```

现在我们可以通过这种方式读取 consul 上的 json 配置信息:

```csharp
var configBuilder = new ConfigurationBuilder().AddConsulJson(
    new ConsulClientConfiguration
    {
        Address = new Uri("http://localhost:8500"),
        Datacenter = "dc1",
    },
    "test-json"
);
var configuration = configBuilder.Build();
var value = configuration["nestedObject:nestedStringKey"]; // the value is "nestedStringValue"
```

Thank`s for [JetBrains](https://www.jetbrains.com/) for the great support in providing assistance and user-friendly environment for my open source projects.

[![JetBrains](https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg?_gl=1*f25lxa*_ga*MzI3ODk2MjY0LjE2NzA0NjY4MDQ.*_ga_9J976DJZ68*MTY4OTY4NzY5OS4zNC4xLjE2ODk2ODgwMDAuNTMuMC4w)](https://www.jetbrains.com/community/opensource/#support)
