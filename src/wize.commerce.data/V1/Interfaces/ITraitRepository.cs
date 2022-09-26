using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wize.commerce.data.v1.Models;
using wize.common.use.repository.Interfaces;

namespace wize.commerce.data.v1.Interfaces
{
    public interface ITraitRepository : IRepositoryBase<int, Trait>
    {
    }
}
