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
    public class BaseProductRepository : IRepositoryBaseProduct
    {
        private DBContext _context;
        private readonly IConfiguration _config;
        private readonly string _connectionDefault;

        public BaseProductRepository(IConfiguration config)
        {
            var optionBuilder = new DbContextOptionsBuilder<DBContext>();
            _context = new DBContext(optionBuilder.Options);
            _config = config;
            _connectionDefault = _config.GetConnectionString("DefaultConnection");
        }
        public List<BaseProduct> GetBaseProducts()
        {
            try
            {
                return _context.BaseProduct.ToList();
            }
            catch(Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new List<BaseProduct>();
            }
        }

        public BaseProduct GetBaseProductByName(string name)
        {
            try
            {
                return _context.BaseProduct.Where(x => x.Name == name).FirstOrDefault();
            }
            catch(Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new BaseProduct();
            }
        }
    }
}
