using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wize.commerce.data;
using wize.commerce.data.v1.Models;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.odata.V1.Controllers
{
    [ODataRoutePrefix("TaxRates")]
    [ApiVersion("1.0")]
    public partial class TaxRatesController : BaseODataController<int, TaxRate>
    {
        private readonly WizeContext _context;
        private readonly ITenantProvider _tenantProvider;
        public TaxRatesController(ILogger<BaseODataController<int, TaxRate>> logger, IActionDescriptorCollectionProvider actionProvider, WizeContext context, ITenantProvider tenantProvider)
            : base(logger, actionProvider, context, tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }
    }
}
