using System;
using System.Collections.Generic;
using System.Text;
using wize.commerce.data.v1.Models;
using wize.common.use.repository.Interfaces;

namespace wize.commerce.data.v1.Interfaces
{
    public interface IOrderProductRepository : IRepositoryBase<int, OrderProduct>
    {
    }
}
