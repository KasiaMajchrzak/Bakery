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
    public class CakeManager : IManagerCake
    {
        private readonly IRepositoryCake repository;

        public CakeManager(IConfiguration config)
        {
            repository = new CakeRepository(config);
        }
        
        public List<Cake> GetCakes()
        {
            return repository.GetCakes();
        }
    }
}
