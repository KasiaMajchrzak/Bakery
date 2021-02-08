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
    public class AdditionalRepository : IRepositoryAdditional
    {
        private DBContext _context;
        private readonly IConfiguration _config;
        private readonly string _connectionDefault;

        public AdditionalRepository(IConfiguration config)
        {
            var optionBuilder = new DbContextOptionsBuilder<DBContext>();
            _context = new DBContext(optionBuilder.Options);
            _config = config;
            _connectionDefault = _config.GetConnectionString("DefaultConnection");
        }
        public List<Additional> GetAdditionals()
        {
            try
            {
                return _context.Additional.ToList();
            }
            catch (Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new List<Additional>();
            }
        }
    }
}
