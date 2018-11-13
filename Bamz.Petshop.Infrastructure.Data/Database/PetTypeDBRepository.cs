using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class PetTypeDBRepository : IRepository<PetType>
    {
        private readonly PetshopContext db;

        public PetTypeDBRepository(PetshopContext context)
        {
            db = context;
        }

        public PetType Add(PetType entity)
        {
            PetType item;
            try
            {
                PetType petType = new PetType
                {
                    Type = entity.Type
                };

                item = db.PetTypes.Add(petType).Entity;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding petType: " + e.Message, e);
            }

            return item;
        }

        public IEnumerable<PetType> GetAll()
        {
            return db.PetTypes.ToList();
        }

        public PetType GetById(int id)
        {
            var item = db.PetTypes.FirstOrDefault(b => b.Id == id);
            if (item == null)
            {
                throw new ArgumentException("Id not found!");
            }
            return item;
        }

        public PetType Update(int index, PetType petType)
        {
            var entity = db.PetTypes.FirstOrDefault(item => item.Id == index);

            if (entity != null)
            {
                entity.Type = petType.Type;

                db.PetTypes.Update(entity);

                db.SaveChanges();

                return entity;
            }
            throw new ArgumentException("Id not found!");
        }

        public PetType Delete(int index)
        {
            var item = GetById(index);
            db.PetTypes.Remove(item);
            db.SaveChanges();
            return item;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PetType> GetPage(PageProperty pageProperty)
        {
            throw new NotImplementedException();
        }
    }
}
