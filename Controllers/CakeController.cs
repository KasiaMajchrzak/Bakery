using Bakery.Contracts.Services;
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
    public class CakeController : Controller
    {
        private readonly IManagerCake _managerCake;
        private readonly IConfiguration _config;

        public CakeController(IConfiguration config)
        {
            _config = config;
            _managerCake = new CakeManager(_config);
        }

        [HttpGet("getcakes")]
        public IActionResult GetCakes()
        {
            var result = _managerCake.GetCakes();
            return Json(result);
        }
    }
}
