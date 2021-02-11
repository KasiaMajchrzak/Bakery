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
using DinkToPdf;

namespace Bakery.Repositories
{
    public class OrderRepository : IRepositoryOrder
    {
        private DBContext _context;
        private readonly IConfiguration _config;
        private readonly string _connectionDefault;

        public OrderRepository(IConfiguration config)
        {
            var optionBuilder = new DbContextOptionsBuilder<DBContext>();
            _context = new DBContext(optionBuilder.Options);
            _config = config;
            _connectionDefault = _config.GetConnectionString("DefaultConnection");
        }

        public bool AddOrder(Order order)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var ordersAdditionals = order.OrdersAdditionals;
                    var ordersDecorations = order.OrdersDecorations;

                    order.TotalPrice = order.Cake.Price + order.Cream.Price;
                    if (ordersAdditionals.Count > 0)                
                        foreach (var orderAdditional in ordersAdditionals)
                        {
                            order.TotalPrice += (orderAdditional.Additional.Price * orderAdditional.Quantity);
                        }
                    if (ordersDecorations.Count > 0)
                        foreach (var orderDecorations in ordersDecorations)
                        {
                            order.TotalPrice += (orderDecorations.Decoration.Price * orderDecorations.Quantity);
                        }
                    order.TotalPrice = order.TotalPrice * order.Servings;
                    order.BaseProduct = null;
                    order.Cake = null;
                    order.Cream = null;

                    _context.Order.Add(order);
                    _context.SaveChanges();

                    if(ordersAdditionals.Count > 0)
                    {
                        foreach(var orderAdditional in ordersAdditionals)
                        {
                            orderAdditional.Order_Id = order.Order_Id;
                            _context.OrdersAdditionals.Add(orderAdditional);
                        }
                        _context.SaveChanges();
                    }
                    if (ordersDecorations.Count > 0)
                    {
                        foreach (var orderDecoration in ordersDecorations)
                        {
                            orderDecoration.Order_Id = order.Order_Id;
                            _context.OrdersDecorations.Add(orderDecoration);
                        }
                        _context.SaveChanges();
                    }

                    

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    LoggerError.Logger.Error(ex);
                    return false;
                }
            }   
        }
    }
}
