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

        public Order Add(Person customer, List<Pet> pets, DateTime orderDate, double price)
        {
            Order order;
            try
            {
                List<Pet> updatedPets = new List<Pet>();
                for (int i = 0; i < pets.Count; i++)
                {
                    updatedPets.Add(_prep.GetById(pets[i].Id));
                }
                order = new Order(_nextId, _psrep.GetById(customer.Id), updatedPets, orderDate, price);
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

        public Order Update(int index, Person customer, List<Pet> pets, DateTime orderDate, double price)
        {
            Order order;
            for (int i = 0; i < _orders.Count; i++)
            {
                if (_orders[i].Id == index)
                {
                    List<Pet> updatedPets = new List<Pet>();
                    for (int j = 0; j < pets.Count; j++)
                    {
                        updatedPets.Add(_prep.GetById(pets[j].Id));
                    }
                    order = new Order(index, _psrep.GetById(customer.Id), updatedPets, orderDate, price);
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
