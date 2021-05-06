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
    public class StatisticsManager : IManagerStatistics
    {
        private readonly IRepositoryStatistics repository;
        
        public StatisticsManager(IConfiguration config)
        {
            repository = new StatisticsRepository(config);
        }

        public List<Statistics<BaseProduct>> GetBaseProductStatistics()
        {
            return repository.GetBaseProductStatistics();
        }

        public List<Statistics<Cake>> GetCakeStatistics(int? baseProductId)
        {
            return repository.GetCakeStatistics(baseProductId);
        }

        public List<Statistics<Cream>> GetCreamStatistics(int? baseProductId)
        {
            return repository.GetCreamStatistics(baseProductId);
        }

        public List<Statistics<Additional>> GetAdditionalStatistics(int? baseProductId)
        {
            return repository.GetAdditionalStatistics(baseProductId);
        }
        public List<Statistics<Decoration>> GetDecorationStatistics(int? baseProductId)
        {
            return repository.GetDecorationStatistics(baseProductId);
        }
        public Statistics<Cake> GetOrderedTogetherWithCake(int cakeId)
        {
            return repository.GetOrderedTogetherWithCake(cakeId);
        }
        public Statistics<Cream> GetOrderedTogetherWithCream(int creamId)
        {
            return repository.GetOrderedTogetherWithCream(creamId);
        }
    }
}
