using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace wize.commerce.odata.Config
{
    public class OpenAPIOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public OpenAPIOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (string file in Directory.GetFiles(PlatformServices.Default.Application.ApplicationBasePath, "*.xml"))
            {
                options.IncludeXmlComments(file);
            }
            //options.IncludeXmlComments(XmlFilePath);
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                        new OpenApiInfo()
                        {
                            Title = $"wize.commerce.odata API{description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                            Description = "OData API for commerce." + (description.IsDeprecated ? " This API Version is depreciated." : string.Empty),
                            Contact = new OpenApiContact() { Name = "Brandon Korous", Email = "me@brandonkorous.com", Url = new Uri("https://brandonkorous.com") }
                        });
            }
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() { In = ParameterLocation.Header, Description = "Please insert JWT with Bearer into field", Name = "Authorization", Type = SecuritySchemeType.ApiKey });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } } });
        }
    }
}
