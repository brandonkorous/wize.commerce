using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using wize.commerce.data;
using wize.commerce.data.v1.Interfaces;
using wize.commerce.data.v1.Models;
using wize.common.use.paging.Interfaces;
using wize.common.use.paging.Models;
using wize.common.use.repository.Extensions;
using wize.common.use.repository.Models;
using wize.common.use.repository.Operators;

namespace wize.commerce.data.v1.Repositories
{
    public class OrderProductShippingRepository : RepositoryBase<int, OrderProductShipping>, IOrderProductShippingRepository
    {
        public OrderProductShippingRepository(ILogger<IOrderProductShippingRepository> logger, WizeContext context) : base(logger, context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}
