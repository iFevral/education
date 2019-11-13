using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.AppContext;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class OrderRepository : EFBaseRepository<Orders>, IOrderRepository
    {
        private ApplicationContext _db;
        
        public OrderRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Orders> GetAllOrders()
        {
            return _db.Orders.AsEnumerable();
        }

        public IEnumerable<Orders> GetOrdersByUserId(string id)
        {
            return _db.Orders.Where(o => o.UserId.Equals(id)).AsEnumerable();
        }

        public Orders GetOrderById(int id)
        {
            return _db.Orders.Find(id);
        }

        public void Create(Orders order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
        }
        public void Update(Orders order)
        {
            _db.Entry(order).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Orders order = _db.Orders.Find(id);
            if (order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Orders> GetOrdersByUserId(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
