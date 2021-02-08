using Bakery.Contracts.Repositories;
using Bakery.Contracts.Services;
using Bakery.Models;
using Bakery.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Services
{
    public class OrderManager : IManagerOrder
    {
        private readonly IRepositoryOrder repository;

        public OrderManager(IConfiguration config)
        {
            repository = new OrderRepository(config);
        }

        public bool AddOrder(Order order)
        {
            return repository.AddOrder(order);
        }
    }
}
