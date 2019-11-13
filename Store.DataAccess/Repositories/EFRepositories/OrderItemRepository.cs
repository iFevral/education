using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public IEnumerable<OrderItems> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderItems GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(OrderItems item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderItems item)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
