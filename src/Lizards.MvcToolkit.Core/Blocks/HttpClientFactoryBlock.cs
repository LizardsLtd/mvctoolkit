namespace Lizzards.MvcToolkit.Core.Blocks.Defaults
{
  using System;
  using System.Net.Http;
  using Lamar;
  using Lizzards.MvcToolkit.Core.Startup;
  using Microsoft.Extensions.DependencyInjection;

  public sealed class HttpClientFactoryBlock : ConfigurationBlockBase
  {
    private readonly Action<ServiceRegistry> configureHttpClientFactory;

    public HttpClientFactoryBlock()
      : this(services => services.AddHttpClient())
    {
    }

    public HttpClientFactoryBlock(string name)
      : this(services => services.AddHttpClient(name))
    {
    }

    public HttpClientFactoryBlock(string name, Action<HttpClient> configureClient)
      : this(service => service.AddHttpClient(name, configureClient))
    {
    }

    public HttpClientFactoryBlock(string name, Action<IServiceProvider, HttpClient> configureClient)
      : this(service => service.AddHttpClient(name, configureClient))
    {
    }

    private HttpClientFactoryBlock(Action<IServiceCollection> configureHttpClientFactory)
    {
      this.configureHttpClientFactory = configureHttpClientFactory;
    }

    protected override void ConfigureServices(ServicesConfigurator config)
      => config.Add(this.configureHttpClientFactory);
  }
}
