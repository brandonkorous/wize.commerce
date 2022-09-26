using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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
    public class OrderRepository : RepositoryBase<Guid, Order>, IOrderRepository
    {
        public OrderRepository(ILogger<IOrderRepository> logger, WizeContext context) : base(logger, context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}
