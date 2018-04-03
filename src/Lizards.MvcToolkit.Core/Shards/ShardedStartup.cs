﻿namespace Lizards.MvcToolkit.Core.Shards
{
    using System;
    using Lizards.MvcToolkit.Core.Shards.Defaults;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;

    public abstract class ShardedStartup
    {
        private readonly StartupConfigurations configuration;

        protected ShardedStartup(IHostingEnvironment env, IConfiguration configuration)
        {
            this.configuration = new StartupConfigurations(env, configuration);
        }

        public IConfiguration Configuration => this.configuration.Configuration;

        public IHostingEnvironment Environment => this.configuration.Environment;

        public ShardedStartup ConfigureOptions<TOption>(Action<TOption> configure)
            where TOption : class
        {
            this.configuration.Services.Configure(configure);

            return this;
        }

        public ShardedStartup ConfigureOptions<TOption>(Action<IConfiguration, TOption> configure)
            where TOption : class
        {
            this.configuration.Services.Configure<TOption>(options => configure(this.Configuration, options));

            return this;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            this.configuration.MVC.AddMvc(services);
            this.configuration.Razor.Use(services);
            this.configuration.Services.Use(services);
        }

        public void Configure(IApplicationBuilder app)
        {
            this.configuration.ASP.Use(app, this.Environment);
            this.configuration.MVC.Use(app);
        }

        //public void ApplyDefault<TDefault>(params object[] arguments)
        //        where TDefault : ShardBase, new()
        //    => this.ApplyDefault(new TDefault(), arguments);
        public void ApplyDefault<TDefault>()
               where TDefault : ShardBase, new()
           => this.ApplyDefault<TDefault, object>(new object());

        public void ApplyDefault<TDefault, TArgument>(TArgument arguments)
                where TDefault : IShard, new()
            => this.ApplyDefault(new TDefault(), arguments);

        public void ApplyDefault<TArgument>(IShard @default, TArgument arguments)
        {
            this.configuration.Apply(@default, arguments);
        }

        protected virtual void AddConfigurationBuilderDetails(ConfigurationBuilder provider)
        {
        }
    }

    public abstract class BasicShardedStartup : ShardedStartup
    {
        public Action<IRouteBuilder>[] Routes =
            new Action<IRouteBuilder>[]
            {
                routes => routes.MapRoute(
                            name: "default",
                            template: "{controller=Home}/{action=Index}/{id?}"),

                routes => routes.MapSpaFallbackRoute(
                            name: "spa-fallback",
                            defaults: new { controller = "Home", action = "Index" }),
            };

        protected BasicShardedStartup(IHostingEnvironment env, IConfiguration configuration)
                            : base(env, configuration)
        {
            this.ApplyDefault<FeaturesShard>();
            this.ApplyDefault<UseStaticFiles>();
            this.ApplyDefault<DevelopmentSetup>();

            this.ApplyDefault<RouteShard, Action<IRouteBuilder>[]>(this.Routes);
        }
    }
}