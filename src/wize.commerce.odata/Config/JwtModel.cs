using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace wize.commerce.odata.Config
{
    public class JwtModel
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}