using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Repositories
{
    public interface IOrderRepository : IRepository<Orders>
    {
        IEnumerable<Orders> GetAllOrders();
        Orders GetOrderById(int id);
        IEnumerable<Orders> GetOrdersByUserId(int id);
    }
}
