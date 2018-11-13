using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IPetRepository _prep;
        private readonly IPersonRepository _psrep;

        static int _nextId;
        static List<Order> _orders;

        public OrderRepository(IPetRepository petService, IPersonRepository personService)
        {
            _prep = petService;
            _psrep = personService;

            if (_nextId < 1)
            {
                _nextId = 1;
            }
            if (_orders == null)
            {
                _orders = new List<Order>();
            }
        }

        public Order Add(Person customer, List<OrderPetRelation> orderlines, DateTime orderDate, double price)
        {
            Order order;
            try
            {
                List<Pet> updatedPets = new List<Pet>();
                
                order = new Order
                {
                    Id = _nextId,
                    Customer = _psrep.GetById(customer.Id),
                    Pets = orderlines,
                    OrderDate = orderDate,
                    Price = price
                };

                _orders.Add(order);
                _nextId++;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding order: " + e.Message, e);
            }

            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            return _orders;
        }

        public Order GetById(int id)
        {
            List<Order> result = _orders.Where(order => order.Id == id).ToList();
            if (result.Count > 0)
            {
                return result[0];
            }
            throw new RepositoryException("Order not found!");
        }

        public Order Update(int index, Person customer, List<OrderPetRelation> orderlines, DateTime orderDate, double price)
        {
            Order order;
            for (int i = 0; i < _orders.Count; i++)
            {
                if (_orders[i].Id == index)
                {
                    order = new Order
                    {
                        Id = _nextId,
                        Customer = _psrep.GetById(customer.Id),
                        Pets = orderlines,
                        OrderDate = orderDate,
                        Price = price
                    };
                    
                    _orders[i] = order;
                    return order;
                }
            }

            throw new RepositoryException("Order not found!");
        }

        public Order Delete(int index)
        {
            for (int i = 0; i < _orders.Count; i++)
            {
                if (_orders[i].Id == index)
                {
                    Order order = _orders[i];
                    _orders.Remove(_orders[i]);
                    return order;
                }
            }

            throw new RepositoryException("Error deleting order");
        }
    }
}
