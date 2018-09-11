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
        private readonly IPetRepository _prep;

        public PetService(IPetRepository petRepository)
        {
            _prep = petRepository;
        }

        public Pet Add(string name, DateTime birthDate, DateTime soldDate, Colour colour, PetType type, Person previousOwner, double price)
        {
            try
            {
                return _prep.Add(name, birthDate, soldDate, colour, type, previousOwner, price);
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

        public Pet Update(int index, string name, DateTime birthDate, DateTime soldDate, Colour colour, PetType type, Person previousOwner, double price)
        {
            try
            {
                return _prep.Update(index, name, birthDate, soldDate, colour, type, previousOwner, price);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error updating pet: " + e.Message, e);
            }
        }
    }
}
