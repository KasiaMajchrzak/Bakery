using Bakery.Contracts.Services;
using Bakery.Errors;
using Bakery.Models;
using Bakery.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IManagerOrder _managerOrder;
        private readonly IConfiguration _config;

        public OrderController(IConfiguration config)
        {
            _config = config;
            _managerOrder = new OrderManager(_config);
        }

        [HttpPost("addorder")]
        public IActionResult AddOrder(Order order)
        {
            try
            {
                var isAdded = _managerOrder.AddOrder(order);
                return Json(new { result = isAdded });
            }
            catch(Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return Json(new { result = false });
            }
        }
    }
}
