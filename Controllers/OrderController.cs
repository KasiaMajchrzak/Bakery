using Bakery.Contracts.Services;
using Bakery.Errors;
using Bakery.Models;
using Bakery.Services;
using DinkToPdf.Contracts;
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
        private IConverter _converter;

        public OrderController(IConfiguration config, IConverter converter)
        {
            _config = config;
            _converter = converter;
            _managerOrder = new OrderManager(_config, _converter);
        }

        [HttpGet("getordertemplates")]
        public IActionResult GetOrderTemplatesByBaseProductId(int baseProductId)
        {
            var result = _managerOrder.GetTemplatesByBaseProduct(baseProductId);
            return Json(result);
        }

        [HttpGet("gettemplatebyid")]
        public IActionResult GetTemplateById (int id)
        {
            var result = _managerOrder.GetTemplateById(id);
            return Json(result);
        }

        [HttpPost("addorder")]
        public IActionResult AddOrder(Order order)
        {
            try
            {
                var pdfFile = _managerOrder.AddOrder(order);
                return File(pdfFile, "application/pdf");
            }
            catch(Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return Json(new { result = false });
            }
        }
    }
}
