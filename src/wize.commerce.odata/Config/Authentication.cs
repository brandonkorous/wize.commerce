using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wize.commerce.odata.Config
{
    public static class Authentication
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            JwtModel jwt = new JwtModel();
            jwt.ValidAudience = configuration.GetValue<string>("JwtAuthentication_ValidAudience");
            jwt.ValidIssuer = configuration.GetValue<string>("JwtAuthentication_ValidIssuer");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwt.ValidIssuer,
                    ValidAudience = jwt.ValidAudience,
                };
                options.SaveToken = true;
                options.IncludeErrorDetails = true;
                options.RequireHttpsMetadata = false;
                options.Authority = jwt.ValidIssuer;
                options.Audience = jwt.ValidAudience;
                //options.TokenValidationParameters = tokenParameters;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:commerce", policy => {
                    policy.Requirements.Add(new HasPermissionsRequirement("read:commerce", jwt.ValidIssuer));
                });
                options.AddPolicy("add:commerce", policy => {
                    policy.Requirements.Add(new HasPermissionsRequirement("add:commerce", jwt.ValidIssuer));
                });
                options.AddPolicy("list:commerce", policy => {
                    policy.Requirements.Add(new HasPermissionsRequirement("list:commerce", jwt.ValidIssuer));
                });
                options.AddPolicy("update:commerce", policy => {
                    policy.Requirements.Add(new HasPermissionsRequirement("update:commerce", jwt.ValidIssuer));
                });
                options.AddPolicy("delete:commerce", policy => {
                    policy.Requirements.Add(new HasPermissionsRequirement("delete:commerce", jwt.ValidIssuer));
                });
            });
            services.AddSingleton<IAuthorizationHandler, HasPermissionsHandler>();

            return services;
        }

        public static IApplicationBuilder UseJwt(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
