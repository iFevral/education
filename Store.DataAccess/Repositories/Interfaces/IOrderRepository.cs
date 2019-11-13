using Store.DataAccess.Entities;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository 
    {
        IEnumerable<Orders> GetAllOrders();
        Orders GetOrderById(int id);
        IEnumerable<Orders> GetOrdersByUserId(int id);
    }
}