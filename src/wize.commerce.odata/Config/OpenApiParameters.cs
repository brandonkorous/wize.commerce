using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wize.commerce.odata.Config
{
    public class OpenAPIParameters : IOperationFilter
    {
        public void Apply(OpenApiOperation Operation, OperationFilterContext context)
        {
            if (Operation.Parameters == null)
                Operation.Parameters = new List<OpenApiParameter>();

            var apiDescription = context.ApiDescription;

            Operation.Deprecated |= apiDescription.IsDeprecated();

            if (Operation.Parameters != null)
            {
                foreach (var parameter in Operation.Parameters)
                {
                    var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                    if (parameter.Description == null)
                        parameter.Description = description.ModelMetadata?.Description;

                    if (parameter.Schema.Default == null && description.DefaultValue != null)
                        parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());

                    parameter.Required |= description.IsRequired;
                }
            }

            Operation.Parameters.Add(new OpenApiParameter
            {
                Name = "ducks-referrer",
                In = ParameterLocation.Header,
                Description = "something",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
        }
    }
}
