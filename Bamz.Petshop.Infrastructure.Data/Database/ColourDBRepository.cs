using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class ColourDBRepository : IRepository<Colour>
    {
        private readonly PetshopContext db;

        public ColourDBRepository(PetshopContext context)
        {
            db = context;
        }

        public Colour Add(Colour entity)
        {
            Colour item;
            try
            {
                Colour colour = new Colour
                {
                    Description = entity.Description
                };

                item = db.Colours.Add(colour).Entity;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding colour: " + e.Message, e);
            }

            return item;
        }

        public IEnumerable<Colour> GetAll()
        {
            return db.Colours.ToList();
        }

        public Colour GetById(int id)
        {
            var item = db.Colours.FirstOrDefault(b => b.Id == id);
            if (item == null)
            {
                throw new ArgumentException("Id not found!");
            }
            return item;
        }

        public Colour Update(int index, Colour colour)
        {
            var entity = db.Colours.FirstOrDefault(item => item.Id == index);

            if (entity != null)
            {
                entity.Description = colour.Description;

                db.Colours.Update(entity);

                db.SaveChanges();

                return entity;
            }
            throw new ArgumentException("Id not found!");
        }

        public Colour Delete(int index)
        {
            var item = GetById(index);
            db.Colours.Remove(item);
            db.SaveChanges();
            return item;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colour> GetPage(PageProperty pageProperty)
        {
            throw new NotImplementedException();
        }
    }
}
