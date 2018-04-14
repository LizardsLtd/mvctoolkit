using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Lizards.MvcToolkit.Core.Blocks
{
  public sealed class ServicesConfigurationBlock
  {
    private readonly List<Action<IServiceCollection>> services;

    public ServicesConfigurationBlock()
    {
      this.services = new List<Action<IServiceCollection>>();
    }

    public void Add(Action<IServiceCollection> service)
        => this.services.Add(service);

    public void Configure<TOptions>(Action<TOptions> configureAction)
            where TOptions : class
        => this.services.Add(services => services.Configure(configureAction));

    internal void Use(IServiceCollection services)
        => this.services.ForEach(x => x(services));
  }
}
