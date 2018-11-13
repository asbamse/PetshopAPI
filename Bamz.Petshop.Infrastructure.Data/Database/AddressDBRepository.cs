using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class AddressDBRepository : IRepository<Address>
    {
        private readonly PetshopContext db;

        public AddressDBRepository(PetshopContext context)
        {
            db = context;
        }

        public Address Add(Address entity)
        {
            Address item;
            try
            {
                Address address = new Address {
                    Street = entity.Street,
                    Number = entity.Number,
                    Letter = entity.Letter,
                    Floor = entity.Floor,
                    Side = entity.Side,
                    ZipCode = entity.ZipCode,
                    City = entity.City
                };

                item = db.Addresses.Add(address).Entity;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding address: " + e.Message, e);
            }

            return item;
        }

        public IEnumerable<Address> GetAll()
        {
            return db.Addresses.ToList();
        }

        public Address GetById(int id)
        {
            var item = db.Addresses.FirstOrDefault(b => b.Id == id);
            if(item == null)
            {
                throw new ArgumentException("Id not found!");
            }
            return item;
        }

        public Address Update(int index, Address address)
        {
            var entity = db.Addresses.FirstOrDefault(item => item.Id == index);

            if (entity != null)
            {
                entity.Street = address.Street;
                entity.Number = address.Number;
                entity.Letter = address.Letter;
                entity.Floor = address.Floor;
                entity.Side = address.Side;
                entity.ZipCode = address.ZipCode;
                entity.City = address.City;

                db.Addresses.Update(entity);

                db.SaveChanges();

                return entity;
            }
            throw new ArgumentException("Id not found!");
        }

        public Address Delete(int index)
        {
            var item = GetById(index);
            db.Addresses.Remove(item);
            db.SaveChanges();
            return item;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Address> GetPage(PageProperty pageProperty)
        {
            throw new NotImplementedException();
        }
    }
}
