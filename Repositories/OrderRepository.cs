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
using Bakery.Toolbox;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Bakery.Repositories
{
    public class OrderRepository : IRepositoryOrder
    {
        private DBContext _context;
        private readonly IConfiguration _config;
        private readonly string _connectionDefault;
        private IConverter _converter;

        public OrderRepository(IConfiguration config, IConverter converter)
        {
            var optionBuilder = new DbContextOptionsBuilder<DBContext>();
            _context = new DBContext(optionBuilder.Options);
            _config = config;
            _connectionDefault = _config.GetConnectionString("DefaultConnection");
            _converter = converter;
        }

        public List<Order> GetTemplatesByBaseProduct(int baseProductId)
        {
            try
            {
                List<Order> result = new List<Order>();

                result = _context.Order.Where(x => x.BaseProduct_Id == baseProductId && x.IsTemplate == true)
                    .Include(x => x.BaseProduct).Include(x => x.Cake).Include(x => x.Cream).ToList();

                foreach(var res in result)
                {
                    res.OrdersAdditionals = _context.OrdersAdditionals.Where(x => x.Order_Id == res.Order_Id).Include(x => x.Additional).ToList();
                    res.OrdersDecorations = _context.OrdersDecorations.Where(x => x.Order_Id == res.Order_Id).Include(x => x.Decoration).ToList();
                    res.OrdersAdditionals.ForEach(x => x.Order = null);
                    res.OrdersDecorations.ForEach(x => x.Order = null);
                }

                return result;
            }
            catch(Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new List<Order>();
            }
        }

        public Order GetTemplateById(int id)
        {
            try
            {
                var result = _context.Order.Where(x => x.Order_Id == id)
                    .Include(x => x.BaseProduct).Include(x => x.Cream).Include(x => x.Cake).FirstOrDefault();

                result.OrdersAdditionals = _context.OrdersAdditionals.Where(x => x.Order_Id == result.Order_Id).Include(x => x.Additional).ToList();
                result.OrdersDecorations = _context.OrdersDecorations.Where(x => x.Order_Id == result.Order_Id).Include(x => x.Decoration).ToList();
                result.OrdersAdditionals.ForEach(x => x.Order = null);
                result.OrdersDecorations.ForEach(x => x.Order = null);

                return result;
            }
            catch(Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return new Order();
            }
        }

        public byte[] AddOrder(Order order)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var ordersAdditionals = order.OrdersAdditionals;
                    var ordersDecorations = order.OrdersDecorations;
                 
                    order.Discount = CalculateDiscount(order);
                    order.BaseProduct = null;
                    order.Cake = null;
                    order.Cream = null;
                    order.OrderedOn = DateTime.Now;

                    _context.Order.Add(order);
                    _context.SaveChanges();

                    if(ordersAdditionals.Count > 0)
                    {
                        foreach(var orderAdditional in ordersAdditionals)
                        {
                            orderAdditional.Order = null; 
                            orderAdditional.Order_Id = order.Order_Id;
                            orderAdditional.Additional = null;
                            _context.OrdersAdditionals.Add(orderAdditional);
                        }
                        _context.SaveChanges();
                    }
                    if (ordersDecorations.Count > 0)
                    {
                        foreach (var orderDecoration in ordersDecorations)
                        {
                            orderDecoration.Order = null;
                            orderDecoration.Order_Id = order.Order_Id;
                            orderDecoration.Decoration = null;
                            _context.OrdersDecorations.Add(orderDecoration);
                        }
                        _context.SaveChanges();
                    }
                    Order addedOrder = _context.Order.Where(x => x.Order_Id == order.Order_Id)
                        .Include(x => x.BaseProduct).Include(x => x.Cake).Include(x => x.Cream).FirstOrDefault();
                    addedOrder.OrderedOnString = addedOrder.OrderedOn.ToString("dd.MM.yyyy  -  HH:mm");
                    List<OrdersAdditionals> addedAdditionals = _context.OrdersAdditionals.Where(x => x.Order_Id == order.Order_Id)
                        .Include(x => x.Order).Include(x => x.Additional).ToList();
                    List<OrdersDecorations> addedDecorations = _context.OrdersDecorations.Where(x => x.Order_Id == order.Order_Id)
                        .Include(x => x.Order).Include(x => x.Decoration).ToList();

                    var globalSettings = new GlobalSettings
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings { Top = 10 },
                        DocumentTitle = "Zamówienie " + DateTime.Now.ToString("dd.MM.yyyy HH:mm")
                    };
                    var objectSettings = new ObjectSettings
                    {
                        PagesCount = true,
                        HtmlContent = PrintoutTemplateGenerator.GenerateHtml(addedOrder, addedAdditionals, addedDecorations),
                        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                        HeaderSettings = { FontName = "Arial", FontSize = 11, Right = "Strona [page] z [toPage]", Line = true },
                        FooterSettings = { FontName = "Arial", FontSize = 11, Line = true, Center = "Bakery" }
                    };
                    
                    var pdf = new HtmlToPdfDocument()
                    {
                        GlobalSettings = globalSettings,
                        Objects = { objectSettings }
                    };
                    
                    byte[] pdfFile =_converter.Convert(pdf);
                  
                    transaction.Commit();
                    return pdfFile;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    LoggerError.Logger.Error(ex);
                    return new byte[] { };
                }
            }   
        }

        public decimal CalculateDiscount(Order order) 
        {
            if ((order.BaseProduct.Name == "Tort" && order.TotalPrice > 380) || (order.BaseProduct.Name == "Mono-deser" && order.TotalPrice > 150))
            {
                return decimal.Multiply(order.TotalPrice, (decimal)0.15); 
            }
            else if ((order.BaseProduct.Name == "Tort" && order.TotalPrice > 300) || (order.BaseProduct.Name == "Mono-deser" && order.TotalPrice > 90))
            {
                return decimal.Multiply(order.TotalPrice, (decimal)0.1);
            }
            else
            {
                return 0;
            }
        }
    }
}
