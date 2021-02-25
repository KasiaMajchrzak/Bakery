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
    public class DecorationController : Controller
    {
        private readonly IManagerDecoration _managerDecoration;
        private readonly IConfiguration _config;

        public DecorationController(IConfiguration config)
        {
            _config = config;
            _managerDecoration = new DecorationManager(_config);
        }

        [HttpGet("getdecorations")]
        public IActionResult GetDecorations()
        {
            var result = _managerDecoration.GetDecorations();
            return Json(result);
        }
    }
}
