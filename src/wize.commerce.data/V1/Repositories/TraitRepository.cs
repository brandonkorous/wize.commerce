﻿using Microsoft.EntityFrameworkCore;
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
    public class TraitRepository : RepositoryBase<int, Trait>, ITraitRepository
    {
        public TraitRepository(ILogger<ITraitRepository> logger, WizeContext context) : base(logger, context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}