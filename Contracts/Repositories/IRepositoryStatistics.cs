using Bakery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Contracts.Repositories
{
    public interface IRepositoryStatistics
    {
        List<Statistics<BaseProduct>> GetBaseProductStatistics();
        List<Statistics<Cake>> GetCakeStatistics(int? baseProductId);
        List<Statistics<Cream>> GetCreamStatistics(int? baseProductId);
        List<Statistics<Additional>> GetAdditionalStatistics(int? baseProductId);
        List<Statistics<Decoration>> GetDecorationStatistics(int? baseProductId);
        Statistics<Cake> GetOrderedTogetherWithCake(int cakeId);
        Statistics<Cream> GetOrderedTogetherWithCream(int creamId);
    }
}
