using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wize.commerce.odata.Config
{
    public static class ODataMvc
    {
        public static IServiceCollection AddODataMvc(this IServiceCollection services)
        {
            services.AddOData().EnableApiVersioning();
            services.AddODataApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                //options.QueryOptions.Controller<V1.Controllers.ContentsController>().Action(a => a.Get()).Allow(Microsoft.AspNet.OData.Query.AllowedQueryOptions.Filter);
            });

            return services;
        }

        public static IApplicationBuilder UseODataMvc(this IApplicationBuilder app, VersionedODataModelBuilder builder)
        {
            var edmModels = builder.GetEdmModels();
            app.UseMvc(options =>
            {
                //options.EnableDependencyInjection();
                options.Select().Filter().Count().Expand().OrderBy().SkipToken().MaxTop(100);
                options.ServiceProvider.GetRequiredService<ODataOptions>().UrlKeyDelimiter = Microsoft.OData.ODataUrlKeyDelimiter.Parentheses;

                options.MapVersionedODataRoute("odata", "v{version:apiVersion}", edmModels);
            });

            return app;
        }
    }
} 
