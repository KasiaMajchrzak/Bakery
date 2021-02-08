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
    public class CreamController : Controller
    {
        private readonly IManagerCream _managerCream;
        private readonly IConfiguration _config;

        public CreamController(IConfiguration config)
        {
            _config = config;
            _managerCream = new CreamManager(_config);
        }

        [HttpGet("getcreams")]
        public IActionResult GetCakes()
        {
            var result = _managerCream.GetCreams();
            return Ok(result);
        }
    }
}
