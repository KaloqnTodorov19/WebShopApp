using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebShopAppp.Infrastructure.Data.Domain;

namespace WebShopAppp.Core.Contracts
{
    public interface IOrderService
    {

        bool Create(int productId, string userId, int quantity);

       List<Order> GetOrders();

        List<Order> GetOrderByUser(string userId);

        Order GetOrderById(int id);

        bool RemoveById(int orderId);

        bool Update(int orderId, int productId, string userId, int quantity);
    }
}
