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
    public class DecorationManager : IManagerDecoration
    {
        private readonly IRepositoryDecoration repository;

        public DecorationManager(IConfiguration config)
        {
            repository = new DecorationRepository(config);
        }

        public List<Decoration> GetDecorations()
        {
            return repository.GetDecorations();
        }
    }
}
