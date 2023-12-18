using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Query.Internal;

using WebShopAppp.Core.Contracts;
using WebShopAppp.Infrastructure.Data;
using WebShopAppp.Infrastructure.Data.Domain;

namespace WebShopAppp.Core.Services
{
    public class OrderServices : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService productService;

        public OrderServices(ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            this.productService = productService;
        }

        public bool Create(int productId, string userId, int quantity)
        {
            var product = this._context.Products.SingleOrDefault(x => x.Id == productId);

            if(product == null)
            {
                return false;
            }

            Order item = new Order
            {
            OrderDate = DateTime.Now,
            ProductId = productId,
            UserId = userId,
            Quantity = quantity,
            Price = product.Price,
            Discount = product.Discount
            };

            product.Quantity -= quantity;

            this._context.Products.Update(product);
            this._context.Orders.Add(item);

            return _context.SaveChanges() != 0; 

        }

        public Order GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrderByUser(string userId)
        {
            return _context.Orders.Where(x => x.UserId == userId).OrderByDescending(x => x.OrderDate).ToList();
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.OrderByDescending(x => x.OrderDate).ToList();
        }

        public bool RemoveById(int orderId)
        {
            throw new NotImplementedException();
        }

        public bool Update(int orderId, int productId, string userId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
