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
    public class AdditionalController : Controller
    {
        private readonly IManagerAdditional _managerAdditional;
        private readonly IConfiguration _config;

        public AdditionalController(IConfiguration config)
        {
            _config = config;
            _managerAdditional = new AdditionalManager(_config);
        }

        [HttpGet("getadditionals")]
        public IActionResult GetAdditionals()
        {
            var result = _managerAdditional.GetAdditionals();
            return Ok(result);
        }
    }
}
