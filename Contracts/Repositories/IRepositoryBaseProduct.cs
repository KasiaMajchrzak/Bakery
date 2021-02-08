using System;
using System.Collections.Generic;
using Bakery.Models;

namespace Bakery.Contracts.Repositories
{
    public interface IRepositoryBaseProduct
    {
        List<BaseProduct> GetBaseProducts();
        BaseProduct GetBaseProductByName(string name);
    }
}