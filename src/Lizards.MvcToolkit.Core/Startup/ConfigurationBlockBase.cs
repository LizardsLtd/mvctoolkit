namespace Lizzards.MvcToolkit.Core.Startup
{
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;

  public abstract class ConfigurationBlockBase : IConfigurationBlock
  {
    protected IConfiguration Configuration { get; private set; }

    protected MvcConfig Mvc { get; private set; }

    public void Apply(StartupConfigurations host)
    {
      this.Configuration = host.Configuration;
      this.Mvc = host.MVC;

      this.Configure();
      host.ASP.Add((app, env) => this.ConfigureApp(app, env));
      this.ConfigureServices(host.Services);
    }

    protected virtual void Configure()
    {
    }

    protected virtual void ConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
    {
    }

    protected virtual void ConfigureServices(ServicesConfigurator config)
    {
    }
  }
}
