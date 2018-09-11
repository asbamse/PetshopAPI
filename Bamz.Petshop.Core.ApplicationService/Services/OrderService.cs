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
        private readonly IOrderRepository _orep;
        private readonly IPetRepository _prep;
        private readonly IPersonRepository _psrep;

        public OrderService(IOrderRepository orderRepository, IPetRepository petRepository, IPersonRepository personRepository)
        {
            _orep = orderRepository;
            _prep = petRepository;
            _psrep = personRepository;
        }

        public Order Add(Person customer, List<Pet> pets, DateTime orderDate, double price)
        {
            try
            {
                List<Pet> updatedPets = new List<Pet>();
                for (int i = 0; i < pets.Count; i++)
                {
                    updatedPets.Add(_prep.GetById(pets[i].Id));
                }
                return _orep.Add(_psrep.GetById(customer.Id), updatedPets, orderDate, price);
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

        public Order Update(int index, Person customer, List<Pet> pets, DateTime orderDate, double price)
        {
            try
            {
                List<Pet> updatedPets = new List<Pet>();
                for (int i = 0; i < pets.Count; i++)
                {
                    updatedPets.Add(_prep.GetById(pets[i].Id));
                }
                return _orep.Update(index, _psrep.GetById(customer.Id), updatedPets, orderDate, price);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error updating order: " + e.Message, e);
            }
        }
    }
}
