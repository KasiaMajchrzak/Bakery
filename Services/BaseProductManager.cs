using Bakery.Contracts.Repositories;
using Bakery.Contracts.Services;
using Bakery.Models;
using Bakery.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Bakery.Services
{
    public class BaseProductManager : IManagerBaseProduct
    {
        private readonly IRepositoryBaseProduct repository;

        public BaseProductManager(IConfiguration config)
        {
            repository = new BaseProductRepository(config);
        }

        public List<BaseProduct> GetBaseProducts()
        {
            return repository.GetBaseProducts();
        }

        public BaseProduct GetBaseProductByName(string name)
        {
            return repository.GetBaseProductByName(name);
        }
    }
}
