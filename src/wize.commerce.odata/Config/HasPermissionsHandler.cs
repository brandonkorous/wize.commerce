using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace wize.commerce.odata.Config
{
    public class HasPermissionsHandler : AuthorizationHandler<HasPermissionsRequirement>
    {
        private readonly IHttpContextAccessor _accessor;
        public HasPermissionsHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionsRequirement requirement)
        {
            //var tokenHandler = new JwtSecretTokenHandler();
            if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            var scopes = context.User.FindAll(c => c.Type == "permissions" && c.Issuer == requirement.Issuer);

            if (scopes.Any(p => p.Value == requirement.Permissions))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
