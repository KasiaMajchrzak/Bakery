using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Contracts.Services;
using Bakery.Models;
using Bakery.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bakery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseProductController : Controller
    {
        private readonly IManagerBaseProduct _managerBaseProduct;
        private readonly IConfiguration _config;
        
        public BaseProductController(IConfiguration config)
        {
            _config = config;
            _managerBaseProduct = new BaseProductManager(_config);
        }

        [HttpGet("getbaseproducts")]
        public IActionResult GetBaseProducts()
        {
            List<BaseProduct> baseProducts = _managerBaseProduct.GetBaseProducts();
            return Ok(baseProducts);
        }

        [HttpGet("getbaseproductbyname")]
        public IActionResult GetBaseProductByName(string name)
        {
            BaseProduct result = _managerBaseProduct.GetBaseProductByName(name);
            return Ok(result);
        }
    }
}
