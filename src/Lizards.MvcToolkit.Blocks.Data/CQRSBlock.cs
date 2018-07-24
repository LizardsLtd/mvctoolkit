namespace Lizards.MvcToolkit.Core.Blocks.Data
{
  using Lizards.Data.CQRS.DataAccess;
  using Lizards.MvcToolkit.Core.Startup;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.DependencyInjection;

  /// <summary>
  /// Automatic registration of all CQRS required items
  /// </summary>
  public sealed class CQRSBlock : ConfigurationBlockBase
  {
    protected override void ConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
        => app
            .ApplicationServices
            .GetService<IDataContextInitialiser>()
            ?.Initialise()
            ?.Wait();

    protected override void ConfigureServices(ServicesConfigurator config)
    {
      config.AddConvention(new CqrsConvention());
    }
  }
}
