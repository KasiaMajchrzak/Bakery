using Bakery.Contracts.Repositories;
using Bakery.DAO;
using Bakery.Errors;
using Bakery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Repositories
{
    public class StatisticsRepository : IRepositoryStatistics
    {
        private DBContext _context;
        private readonly IConfiguration _config;
        private readonly string _connectionDefault;

        public StatisticsRepository(IConfiguration config)
        {
            var optionBuilder = new DbContextOptionsBuilder<DBContext>();
            _context = new DBContext(optionBuilder.Options);
            _config = config;
            _connectionDefault = _config.GetConnectionString("DefaultConnection");
        }
        public List<Statistics<BaseProduct>> GetBaseProductStatistics()
        {
            try
            {
                List<BaseProduct> baseProducts = _context.BaseProduct.ToList();
                List<Statistics<BaseProduct>> baseProductsStatistics = new List<Statistics<BaseProduct>>();

                foreach (var baseProduct in baseProducts)
                {
                    Statistics<BaseProduct> baseProductStatistic = new Statistics<BaseProduct>
                    {
                        Object = baseProduct,
                        ObjectName = baseProduct.Name,
                        NumberOfOrders = _context.Order.Where(x => x.BaseProduct_Id == baseProduct.BaseProduct_Id).Count()
                    };

                    baseProductsStatistics.Add(baseProductStatistic);
                }
                return baseProductsStatistics;
            }
            catch (Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new List<Statistics<BaseProduct>>();
            }
        }

        public List<Statistics<Cake>> GetCakeStatistics(int? baseProductId)
        {
            try
            {
                List<Statistics<Cake>> cakeStatistics = new List<Statistics<Cake>>();
                List<Cake> orderedCakes = new List<Cake>();
                if (baseProductId != null)
                {
                    orderedCakes = _context.Order.Where(x => x.BaseProduct_Id == (int)baseProductId)
                        .Include(x => x.Cake).Select(x => x.Cake).Distinct().ToList();
                }
                else
                {
                    orderedCakes = _context.Order
                        .Include(x => x.Cake).Select(x => x.Cake).Distinct().ToList();
                }

                foreach (var cake in orderedCakes)
                {
                    Statistics<Cake> cakeStatistic = GetOrderedTogetherWithCake(cake.Cake_Id);
                    cakeStatistics.Add(cakeStatistic);
                }

                return cakeStatistics;
            }
            catch (Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new List<Statistics<Cake>>();
            }
        }

        public List<Statistics<Cream>> GetCreamStatistics(int? baseProductId)
        {
            try
            {
                List<Statistics<Cream>> creamStatistics = new List<Statistics<Cream>>();
                List<Cream> orderedCreams = new List<Cream>();
                if (baseProductId != null)
                {
                    orderedCreams = _context.Order.Where(x => x.BaseProduct_Id == (int)baseProductId)
                        .Include(x => x.Cream).Select(x => x.Cream).Distinct().ToList();
                }
                else
                {
                    orderedCreams = _context.Order
                        .Include(x => x.Cream).Select(x => x.Cream).Distinct().ToList();
                }

                foreach (var cream in orderedCreams)
                {
                    Statistics<Cream> creamStatistic = GetOrderedTogetherWithCream(cream.Cream_Id);
                    creamStatistics.Add(creamStatistic);
                }

                return creamStatistics;
            }
            catch (Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new List<Statistics<Cream>>();
            }
        }

        public List<Statistics<Additional>> GetAdditionalStatistics(int? baseProductId)
        {
            try
            {
                List<Statistics<Additional>> additionalStatistics = new List<Statistics<Additional>>();
                List<Additional> orderedAdditionals = new List<Additional>();
                if (baseProductId != null)
                {
                    orderedAdditionals = _context.OrdersAdditionals.Where(x => x.Order.BaseProduct_Id == (int)baseProductId)
                        .Include(x => x.Order).Include(x => x.Additional).Select(x => x.Additional).Distinct().ToList();
                }
                else
                {
                    orderedAdditionals = _context.OrdersAdditionals.Include(x => x.Additional).Select(x => x.Additional).Distinct().ToList();
                }

                foreach (var additional in orderedAdditionals)
                {
                    Statistics<Additional> additionalStatistic = GetOrderedTogetherWithAdditional(additional.Additional_Id);
                    additionalStatistics.Add(additionalStatistic);
                }

                return additionalStatistics;
            }
            catch (Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new List<Statistics<Additional>>();
            }
        }

        public List<Statistics<Decoration>> GetDecorationStatistics(int? baseProductId)
        {
            try
            {
                List<Statistics<Decoration>> decorationStatistics = new List<Statistics<Decoration>>();
                List<Decoration> orderedDecorations = new List<Decoration>();
                if (baseProductId != null)
                {
                    orderedDecorations = _context.OrdersDecorations.Where(x => x.Order.BaseProduct_Id == (int)baseProductId)
                        .Include(x => x.Order).Include(x => x.Decoration).Select(x => x.Decoration).Distinct().ToList();
                }
                else
                {
                    orderedDecorations = _context.OrdersDecorations.Include(x => x.Decoration).Select(x => x.Decoration).Distinct().ToList();
                }

                foreach (var decoration in orderedDecorations)
                {
                    Statistics<Decoration> decorationStatistic = GetOrderedTogetherWithDecoration(decoration.Decoration_Id);
                    decorationStatistics.Add(decorationStatistic);
                }

                return decorationStatistics;
            }
            catch (Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new List<Statistics<Decoration>>();
            }
        }

        public Statistics<Cake> GetOrderedTogetherWithCake(int cakeId)
        {
            try
            {
                Cake cake = _context.Cake.Where(x => x.Cake_Id == cakeId).FirstOrDefault();
                Statistics<Cake> result = new Statistics<Cake>
                {
                    Object = cake,
                    ObjectName = cake.Name,
                    NumberOfOrders = _context.Order.Where(x => x.Cake_Id == cakeId).Count()
                };

                List<Cream> orderedCreams = _context.Order.Where(x => x.Cake_Id == cakeId)
                    .Include(x => x.Cream).Select(x => x.Cream).Distinct().ToList();

                foreach(var cream in orderedCreams)
                {
                    var totalAmount = _context.Order.Where(x => x.Cake_Id == cakeId && x.Cream_Id == cream.Cream_Id).Count();
                    Statistics<Cream> statistics = new Statistics<Cream>
                    {
                        Object = cream,
                        ObjectName = cream.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherCreams.Add(statistics);
                }

                List<Additional> orderedAdditionals = _context.OrdersAdditionals.Where(x => x.Order.Cake_Id == cakeId)
                   .Include(x => x.Order).Include(x => x.Additional).Select(x => x.Additional).Distinct().ToList();

                foreach (var additional in orderedAdditionals)
                {
                    var totalAmount = _context.OrdersAdditionals.Where(x => x.Order.Cake_Id == cakeId && x.Additional_Id == additional.Additional_Id)
                        .Include(x => x.Order).Include(x => x.Additional).Count();
                    Statistics<Additional> statistics = new Statistics<Additional>
                    {
                        Object = additional,
                        ObjectName = additional.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherAdditionals.Add(statistics);
                }

                List<Decoration> orderedDecorations = _context.OrdersDecorations.Where(x => x.Order.Cake_Id == cakeId)
                   .Include(x => x.Order).Include(x => x.Decoration).Select(x => x.Decoration).Distinct().ToList();

                foreach (var decoration in orderedDecorations)
                {
                    var totalAmount = _context.OrdersDecorations.Where(x => x.Order.Cake_Id == cakeId && x.Decoration_Id == decoration.Decoration_Id)
                        .Include(x => x.Order).Include(x => x.Decoration).Count();
                    Statistics<Decoration> statistics = new Statistics<Decoration>
                    {
                        Object = decoration,
                        ObjectName = decoration.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherDecorations.Add(statistics);
                }

                return result;
            }
            catch(Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new Statistics<Cake>();
            }
        }

        public Statistics<Cream> GetOrderedTogetherWithCream(int creamId)
        {
            try
            {
                Cream cream = _context.Cream.Where(x => x.Cream_Id == creamId).FirstOrDefault();
                Statistics<Cream> result = new Statistics<Cream>
                {
                    Object = cream,
                    ObjectName = cream.Name,
                    NumberOfOrders = _context.Order.Where(x => x.Cream_Id == creamId).Count()
                };

                List<Cake> orderedCakes = _context.Order.Where(x => x.Cream_Id == creamId)
                    .Include(x => x.Cake).Select(x => x.Cake).Distinct().ToList();

                foreach (var cake in orderedCakes)
                {
                    var totalAmount = _context.Order.Where(x => x.Cream_Id == creamId && x.Cake_Id == cake.Cake_Id).Count();
                    Statistics<Cake> statistics = new Statistics<Cake>
                    {
                        Object = cake,
                        ObjectName = cake.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherCakes.Add(statistics);
                }

                List<Additional> orderedAdditionals = _context.OrdersAdditionals.Where(x => x.Order.Cream_Id == creamId)
                   .Include(x => x.Order).Include(x => x.Additional).Select(x => x.Additional).Distinct().ToList();

                foreach (var additional in orderedAdditionals)
                {
                    var totalAmount = _context.OrdersAdditionals.Where(x => x.Order.Cream_Id == creamId && x.Additional_Id == additional.Additional_Id)
                        .Include(x => x.Order).Include(x => x.Additional).Count();
                    Statistics<Additional> statistics = new Statistics<Additional>
                    {
                        Object = additional,
                        ObjectName = additional.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherAdditionals.Add(statistics);
                }

                List<Decoration> orderedDecorations = _context.OrdersDecorations.Where(x => x.Order.Cream_Id == creamId)
                   .Include(x => x.Order).Include(x => x.Decoration).Select(x => x.Decoration).Distinct().ToList();

                foreach (var decoration in orderedDecorations)
                {
                    var totalAmount = _context.OrdersDecorations.Where(x => x.Order.Cream_Id == creamId && x.Decoration_Id == decoration.Decoration_Id)
                        .Include(x => x.Order).Include(x => x.Decoration).Count();
                    Statistics<Decoration> statistics = new Statistics<Decoration>
                    {
                        Object = decoration,
                        ObjectName = decoration.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherDecorations.Add(statistics);
                }

                return result;
            }
            catch (Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new Statistics<Cream>();
            }
        }

        public Statistics<Additional> GetOrderedTogetherWithAdditional(int additionalId)
        {
            try
            {
                Additional additional = _context.Additional.Where(x => x.Additional_Id == additionalId).FirstOrDefault();
                List<int> orderIds = _context.OrdersAdditionals.Where(x => x.Additional_Id == additionalId).Select(x => x.Order_Id).Distinct().ToList();

                Statistics<Additional> result = new Statistics<Additional>
                {
                    Object = additional,
                    ObjectName = additional.Name,
                    NumberOfOrders = _context.OrdersAdditionals.Where(x => x.Additional_Id == additionalId).Count()
                };

                List<Cake> orderedCakes = _context.Order.Where(x => orderIds.Contains(x.Order_Id))
                    .Include(x => x.Cake).Select(x => x.Cake).Distinct().ToList();

                foreach (var cake in orderedCakes)
                {
                    var totalAmount = _context.Order.Where(x => orderIds.Contains(x.Order_Id) && x.Cake_Id == cake.Cake_Id).Count();
                    Statistics<Cake> statistics = new Statistics<Cake>
                    {
                        Object = cake,
                        ObjectName = cake.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherCakes.Add(statistics);
                }

                List<Cream> orderedCreams = _context.Order.Where(x => orderIds.Contains(x.Order_Id))
                    .Include(x => x.Cream).Select(x => x.Cream).Distinct().ToList();

                foreach (var cream in orderedCreams)
                {
                    var totalAmount = _context.Order.Where(x => orderIds.Contains(x.Order_Id) && x.Cream_Id == cream.Cream_Id).Count();
                    Statistics<Cream> statistics = new Statistics<Cream>
                    {
                        Object = cream,
                        ObjectName = cream.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherCreams.Add(statistics);
                }

                List<Decoration> orderedDecorations = _context.OrdersDecorations.Where(x => orderIds.Contains(x.Order_Id))
                   .Include(x => x.Order).Include(x => x.Decoration).Select(x => x.Decoration).Distinct().ToList();

                foreach (var decoration in orderedDecorations)
                {
                    var totalAmount = _context.OrdersDecorations.Where(x => orderIds.Contains(x.Order_Id) && x.Decoration_Id == decoration.Decoration_Id)
                        .Include(x => x.Order).Include(x => x.Decoration).Count();
                    Statistics<Decoration> statistics = new Statistics<Decoration>
                    {
                        Object = decoration,
                        ObjectName = decoration.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherDecorations.Add(statistics);
                }

                return result;
            }
            catch (Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new Statistics<Additional>();
            }
        }

        public Statistics<Decoration> GetOrderedTogetherWithDecoration(int decorationId)
        {
            try
            {
                Decoration decoration = _context.Decoration.Where(x => x.Decoration_Id == decorationId).FirstOrDefault();
                List<int> orderIds = _context.OrdersDecorations.Where(x => x.Decoration_Id == decorationId).Select(x => x.Order_Id).Distinct().ToList();

                Statistics<Decoration> result = new Statistics<Decoration>
                {
                    Object = decoration,
                    ObjectName = decoration.Name,
                    NumberOfOrders = _context.OrdersDecorations.Where(x => x.Decoration_Id == decorationId).Count()
                };

                List<Cake> orderedCakes = _context.Order.Where(x => orderIds.Contains(x.Order_Id))
                    .Include(x => x.Cake).Select(x => x.Cake).Distinct().ToList();

                foreach (var cake in orderedCakes)
                {
                    var totalAmount = _context.Order.Where(x => orderIds.Contains(x.Order_Id) && x.Cake_Id == cake.Cake_Id).Count();
                    Statistics<Cake> statistics = new Statistics<Cake>
                    {
                        Object = cake,
                        ObjectName = cake.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherCakes.Add(statistics);
                }

                List<Cream> orderedCreams = _context.Order.Where(x => orderIds.Contains(x.Order_Id))
                    .Include(x => x.Cream).Select(x => x.Cream).Distinct().ToList();

                foreach (var cream in orderedCreams)
                {
                    var totalAmount = _context.Order.Where(x => orderIds.Contains(x.Order_Id) && x.Cream_Id == cream.Cream_Id).Count();
                    Statistics<Cream> statistics = new Statistics<Cream>
                    {
                        Object = cream,
                        ObjectName = cream.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherCreams.Add(statistics);
                }

                List<Additional> orderedAdditionals = _context.OrdersAdditionals.Where(x => orderIds.Contains(x.Order_Id))
                   .Include(x => x.Order).Include(x => x.Additional).Select(x => x.Additional).Distinct().ToList();

                foreach (var additional in orderedAdditionals)
                {
                    var totalAmount = _context.OrdersAdditionals.Where(x => orderIds.Contains(x.Order_Id) && x.Additional_Id == additional.Additional_Id)
                        .Include(x => x.Order).Include(x => x.Additional).Count();
                    Statistics<Additional> statistics = new Statistics<Additional>
                    {
                        Object = additional,
                        ObjectName = additional.Name,
                        NumberOfOrders = totalAmount
                    };
                    result.OrderedTogetherAdditionals.Add(statistics);
                }

                return result;
            }
            catch (Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new Statistics<Decoration>();
            }
        }
    }
}
