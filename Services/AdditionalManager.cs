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
    public class AdditionalManager : IManagerAdditional
    {
        private readonly IRepositoryAdditional repository;

        public AdditionalManager(IConfiguration config)
        {
            repository = new AdditionalRepository(config);
        }

        public List<Additional> GetAdditionals()
        {
            return repository.GetAdditionals();
        }
    }
}
