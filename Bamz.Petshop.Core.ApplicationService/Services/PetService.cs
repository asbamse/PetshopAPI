using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;

namespace Bamz.Petshop.Core.ApplicationService.Services
{
    public class PetService : IPetService
    {
        private readonly IRepository<Pet> _prep;

        public PetService(IRepository<Pet> petRepository)
        {
            _prep = petRepository;
        }

        public Pet Add(Pet pet)
        {
            try
            {
                return _prep.Add(pet);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error adding pet: " + e.Message, e);
            }
        }

        public Pet Delete(int index)
        {
            try
            {
                return _prep.Delete(index);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error deleting pet: " + e.Message, e);
            }
        }

        public List<Pet> GetAll()
        {
            try
            {
                return _prep.GetAll().ToList();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting all pets: " + e.Message, e);
            }
        }

        public List<Pet> GetPage(PageProperty pageProperty)
        {
            if (pageProperty != null)
            {
                if (pageProperty.Page < 1)
                {
                    throw new ArgumentException("The page cannot be less than 1! Try another page.");
                }
                else if ((pageProperty.Page - 1) * pageProperty.Limit >= _prep.Count())
                {
                    throw new ArgumentException("Page not found! Try another page.");
                }
            }

            try
            {
                return _prep.GetPage(pageProperty).ToList();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting all pets: " + e.Message, e);
            }
        }

        public List<Pet> GetAllOrderPrice()
        {
            try
            {
                List<Pet> result = _prep.GetAll().OrderBy(pet => pet.Price).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting all pets in order by price: " + e.Message, e);
            }
        }

        public Pet GetById(int id)
        {
            try
            {
                return _prep.GetById(id);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting pet by id: " + e.Message, e);
            }
        }

        public List<Pet> GetFiveCheapest()
        {
            try
            {
                List<Pet> result = _prep.GetAll().OrderBy(pet => pet.Price).Take(5).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting pet by id: " + e.Message, e);
            }
        }

        public List<Pet> SearchByType(PetType petType)
        {
            try
            {
                List<Pet> result = _prep.GetAll().Where(pet => pet.Type.Equals(petType)).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting pet by type: " + e.Message, e);
            }
        }

        public Pet Update(int id, Pet pet)
        {
            try
            {
                return _prep.Update(id, pet);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error updating pet: " + e.Message, e);
            }
        }
    }
}
