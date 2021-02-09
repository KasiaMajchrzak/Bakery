using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bakery.Errors;
using Bakery.Models;
using DinkToPdf;

namespace Bakery.Toolbox
{
    public class PrintoutTemplateGenerator
    {
        public string GenerateHtml(Order order, OrdersAdditionals ordersAdditionals, OrdersDecorations ordersDecorations)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();

                

                return stringBuilder.ToString();
            }
            catch(Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return "";
            }
        }
    }
}
