using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class OrderDBRepository : IRepository<Order>
    {
        private readonly PetshopContext db;

        private readonly IRepository<Pet> _prep;
        private readonly IPersonRepository _psrep;

        public OrderDBRepository(PetshopContext context, IRepository<Pet> petService, IPersonRepository personService)
        {
            db = context;

            _prep = petService;
            _psrep = personService;
        }

        public Order Add(Order entity)
        {
            Order item;
            try
            {
                Order order = new Order
                {
                    Customer = _psrep.GetById(entity.Customer.Id),
                    Pets = entity.Pets,
                    OrderDate = entity.OrderDate,
                    Price = entity.Price
                };

                item = db.Orders.Add(order).Entity;

                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding order: " + e.Message, e);
            }

            return item;
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders
                .Include(s => s.Customer)
                .Include(s => s.Pets).ThenInclude(ol => ol.Order)
                .Include(s => s.Pets).ThenInclude(ol => ol.Pet);
        }

        public Order GetById(int id)
        {
            var item = db.Orders.Include(s => s.Customer).Include(s => s.Pets).FirstOrDefault(b => b.Id == id);
            if (item == null)
            {
                throw new ArgumentException("Id not found!");
            }
            return item;
        }

        public Order Update(int index, Order order)
        {
            var entity = db.Orders.Include(s => s.Customer).Include(s=>s.Pets).FirstOrDefault(item => item.Id == index);

            if (entity != null)
            {
                entity.Customer = _psrep.GetById(order.Customer.Id);
                entity.Pets = order.Pets;
                entity.OrderDate = order.OrderDate;
                entity.Price = order.Price;

                db.Orders.Update(entity);

                db.SaveChanges();

                return entity;
            }
            throw new ArgumentException("Id not found!");
        }

        public Order Delete(int index)
        {
            var item = GetById(index);
            db.Orders.Remove(item);
            db.SaveChanges();
            return item;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetPage(PageProperty pageProperty)
        {
            throw new NotImplementedException();
        }
    }
}
