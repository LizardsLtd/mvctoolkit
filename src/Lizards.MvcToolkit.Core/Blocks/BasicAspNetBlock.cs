namespace Lizards.MvcToolkit.Core.Blocks
{
  using System;
  using System.Collections.Generic;
  using Lizards.MvcToolkit.Core.Blocks.Defaults;
  using Lizards.MvcToolkit.Core.ModelBinder;
  using Lizards.MvcToolkit.Core.Startup;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Routing;

  /// <summary>A defaukt implementation of Basic ASP.NET configuration</summary>
  /// <seealso cref="Lizards.MvcToolkit.Core.Blocks.IConfigurationBlock" />
  public sealed class BasicAspNetBlock : IConfigurationBlock
  {
    /// <summary>
    /// The exception handling route
    /// </summary>
    /// <autogeneratedoc />
    private readonly string exceptionHandlingRoute;

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicAspNetBlock"/> class.
    /// </summary>
    /// <param name="routes">The routes.</param>
    /// <param name="exceptionHandlingRoute">The exception handling route.</param>
    /// <autogeneratedoc />
    public BasicAspNetBlock(string exceptionHandlingRoute = "/home/error")
    {
      this.exceptionHandlingRoute = exceptionHandlingRoute;
    }

    /// <summary>Applies the specified host.</summary>
    /// <param name="host">The host.</param>
    /// <autogeneratedoc />
    public void Apply(StartupConfigurations host)
    {
      host.Apply<FeaturesBlock>();
      host.Apply<UseStaticFilesBlock>();
      host.Apply<ModelBinderWithCustomProviderBlock<ImmutableModelBinderProvider>>();
      host.Apply(new ExceptionHandlingBlock(this.exceptionHandlingRoute));
      host.Apply(new MvcRouteConfigurationBlock(this.GetRoutes()));
    }

    /// <summary>Gets the routes.</summary>
    /// <returns></returns>
    /// <autogeneratedoc />
    private IEnumerable<Action<IRouteBuilder>> GetRoutes()
    {
      yield return routes
        => routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
    }
  }
}
