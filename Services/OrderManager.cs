using Bakery.Contracts.Repositories;
using Bakery.Contracts.Services;
using Bakery.Models;
using Bakery.Repositories;
using DinkToPdf.Contracts;
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
        private IConverter _converter;
        public OrderManager(IConfiguration config, IConverter converter)
        {
            _converter = converter;
            repository = new OrderRepository(config, converter);
        }

        public List<Order> GetTemplatesByBaseProduct(int baseProductId)
        {
            return repository.GetTemplatesByBaseProduct(baseProductId);
        }

        public Order GetTemplateById(int id)
        {
            return repository.GetTemplateById(id);
        }

        public byte[] AddOrder(Order order)
        {
            return repository.AddOrder(order);
        }
    }
}
