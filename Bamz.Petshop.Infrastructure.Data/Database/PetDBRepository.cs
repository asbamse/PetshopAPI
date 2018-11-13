using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class PetDBRepository : IRepository<Pet>
    {
        private readonly PetshopContext _ctx;

        public PetDBRepository(PetshopContext context)
        {
            _ctx = context;
        }

        public Pet Add(Pet pet)
        {
            Pet item;

            try
            {
                var entityEntry = _ctx.Attach(pet);
                entityEntry.State = EntityState.Added;
                item = entityEntry.Entity;
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding pet: " + e.Message, e);
            }

            return item;
        }

        public IEnumerable<Pet> GetAll()
        {
            List<Pet> pets = _ctx.Pets.Include(s => s.Colours).ThenInclude(pc => pc.Colour).Include(s => s.Type).Include(s => s.PreviousOwner).ToList();
            return pets;
        }

        public IEnumerable<Pet> GetPage(PageProperty pageProperty)
        {
            if(pageProperty == null)
            {
                return GetAll();
            }

            IQueryable<Pet> quaryPets = _ctx.Pets
                .Include(s => s.Colours).ThenInclude(pc => pc.Colour)
                .Include(s => s.Type)
                .Include(s => s.PreviousOwner);

            if(pageProperty.SortBy != null)
            {
                PropertyInfo propertyInfo = typeof(Pet).GetProperty(pageProperty.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new RepositoryException($"Cannot sort by {pageProperty.SortBy} because it is not a property in Pet! Try another.");
                }

                if (pageProperty.SortOrder == null || pageProperty.SortOrder.ToLower().Equals("asc"))
                {
                    quaryPets = quaryPets.OrderBy(p => propertyInfo.GetValue(p, null));
                }
                else if (pageProperty.SortOrder.ToLower().Equals("desc"))
                {
                    quaryPets = quaryPets.OrderByDescending(p => propertyInfo.GetValue(p, null));
                }
                else
                {
                    throw new RepositoryException($"Sort order can only be 'asc' or 'desc'! Not {pageProperty.SortOrder}.");
                }
            }

            List<Pet> pets = quaryPets
                .Skip((pageProperty.Page - 1) * pageProperty.Limit)
                .Take(pageProperty.Limit)
                .ToList();

            return pets;
        }

        public Pet GetById(int id)
        {
            var item = _ctx.Pets.Include(s => s.Colours).ThenInclude(pc => pc.Colour).Include(s => s.Type).Include(s => s.PreviousOwner).FirstOrDefault(b => b.Id == id);
            if (item == null)
            {
                throw new ArgumentException("Id not found!");
            }
            return item;
        }

        public Pet Update(int id, Pet pet)
        {
            // Makes sure the list of colour relations is set.
            List<PetColourRelation> petColours;
            if (pet.Colours != null)
            {
                petColours = new List<PetColourRelation>(pet.Colours);
            }
            else
            {
                petColours = new List<PetColourRelation>();
            }

            // Deletes the pet colours which is already in the pet colour relation.
            var coloursFromDb = _ctx.Pets.Include(p => p.Colours).AsNoTracking().FirstOrDefault(p => p.Id == id).Colours.ToList();

            foreach (var petColour in coloursFromDb)
            {
                if (petColours.Exists(pc => pc.ColourId == petColour.ColourId))
                {
                    _ctx.Entry(petColour).State = EntityState.Unchanged;
                    petColours.RemoveAll(p => p.ColourId == petColour.ColourId);
                }
                else if (!petColours.Exists(pc => pc.ColourId == petColour.ColourId))
                {
                    _ctx.Entry(petColour).State = EntityState.Deleted;
                }
            }

            pet.Colours = petColours;

            _ctx.Attach(pet);
            _ctx.Attach(pet).State = EntityState.Modified;

            _ctx.Entry(pet).Collection(p => p.Colours).IsModified = false;
            _ctx.Entry(pet).Reference(p => p.PreviousOwner).IsModified = true;
            _ctx.Entry(pet).Reference(p => p.Type).IsModified = true;

            DisplayStates(_ctx.ChangeTracker.Entries());

            _ctx.SaveChanges();

            return _ctx.Pets.Include(p => p.Colours).FirstOrDefault(p => p.Id == id);
        }

        public Pet Delete(int index)
        {
            var item = GetById(index);

            var entityEntry = _ctx.Pets.Attach(item);
            entityEntry.State = EntityState.Deleted;
            _ctx.SaveChanges();

            return entityEntry.Entity;
        }

        public int Count()
        {
            return _ctx.Pets.Count();
        }

        private static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: { entry.State.ToString()}");
            }
        }
    }
}
