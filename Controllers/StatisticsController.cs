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
    public class StatisticsController : Controller
    {
        private readonly IManagerStatistics _managerStatistics;
        private readonly IConfiguration _config;

        public StatisticsController(IConfiguration config)
        {
            _config = config;
            _managerStatistics = new StatisticsManager(_config);
        }

        [HttpGet("getbaseproductstatistics")]
        public IActionResult GetBaseProductsStatistics()
        {
            var result = _managerStatistics.GetBaseProductStatistics();
            return Json(result);
        }

        [HttpGet("getcakestatistics")]
        public IActionResult GetCakeStatistics(int? baseProductId)
        {
            var result = _managerStatistics.GetCakeStatistics(baseProductId);
            return Json(result);
        }

        [HttpGet("getcreamstatistics")]
        public IActionResult GetCreamStatistics(int? baseProductId)
        {
            var result = _managerStatistics.GetCreamStatistics(baseProductId);
            return Json(result);
        }

        [HttpGet("getadditionalstatistics")]
        public IActionResult GetAdditionalStatistics(int? baseProductId)
        {
            var result = _managerStatistics.GetAdditionalStatistics(baseProductId);
            return Json(result);
        }

        [HttpGet("getdecorationstatistics")]
        public IActionResult GetDecorationStatistics(int? baseProductId)
        {
            var result = _managerStatistics.GetDecorationStatistics(baseProductId);
            return Json(result);
        }

        [HttpGet("getorderedtogetherwithcake")]
        public IActionResult GetOrderedTogetherWithCake(int cakeId)
        {
            var result = _managerStatistics.GetOrderedTogetherWithCake(cakeId);
            return Json(result);
        }

        [HttpGet("getorderedtogetherwithcream")]
        public IActionResult GetOrderedTogetherWithCream(int creamId)
        {
            var result = _managerStatistics.GetOrderedTogetherWithCream(creamId);
            return Json(result);
        }
    }
}
