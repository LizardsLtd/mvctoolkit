﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Lizards.MvcToolkit.Configuration.Defaults
{
    public sealed class UseStaticFiles : BasicDefault
    {
        protected override void ConfigureApp(
                IApplicationBuilder app
                , IHostingEnvironment env
                , IEnumerable<object> arguments)
            => app.UseStaticFiles();
    }
}