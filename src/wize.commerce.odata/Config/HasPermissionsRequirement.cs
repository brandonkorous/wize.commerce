using Microsoft.AspNetCore.Authorization;
using System;

namespace wize.commerce.odata.Config
{
    public class HasPermissionsRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Permissions { get; }

        public HasPermissionsRequirement(string permissions, string issuer)
        {
            Permissions = permissions ?? throw new ArgumentNullException(nameof(permissions));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}
