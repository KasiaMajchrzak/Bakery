using Bakery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Contracts.Repositories
{
    public interface IRepositoryOrder
    {
        List<Order> GetTemplatesByBaseProduct(int baseProductId);
        Order GetTemplateById(int id);
        byte[] AddOrder(Order order);
    }
}
