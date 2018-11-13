using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Core.ApplicationService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orep;
        private readonly IPersonRepository _psrep;

        public OrderService(IRepository<Order> orderRepository, IPersonRepository personRepository)
        {
            _orep = orderRepository;
            _psrep = personRepository;
        }

        public Order Add(Order order)
        {
            try
            {
                return _orep.Add(order);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error adding order: " + e.Message, e);
            }
        }

        public Order Delete(int index)
        {
            try
            {
                return _orep.Delete(index);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error deleting order: " + e.Message, e);
            }
        }

        public List<Order> GetAll()
        {
            try
            {
                return _orep.GetAll().ToList();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting all orders: " + e.Message, e);
            }
        }

        public Order GetById(int id)
        {
            try
            {
                return _orep.GetById(id);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting order by id: " + e.Message, e);
            }
        }

        public Order Update(int index, Order order)
        {
            try
            {
                return _orep.Update(index, order);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error updating order: " + e.Message, e);
            }
        }
    }
}
