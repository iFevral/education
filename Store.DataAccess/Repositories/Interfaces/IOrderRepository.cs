using Store.DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Repositories
{
    public interface IOrderRepository 
    {
        IEnumerable<Orders> GetAllOrders();
        Orders GetOrderById(int id);
        IEnumerable<Orders> GetOrdersByUserId(int id);
    }
}
