using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class PetRepository : IPetRepository
    {
        private readonly IColourRepository _crep;
        private readonly IPetTypeRepository _ptrep;
        private readonly IPersonRepository _prep;

        static int _nextId;
        static List<Pet> _pets;

        public PetRepository(IColourRepository colourService, IPetTypeRepository petTypeService, IPersonRepository personService)
        {
            _crep = colourService;
            _ptrep = petTypeService;
            _prep = personService;

            if (_nextId < 1)
            {
                _nextId = 1;
            }
            if (_pets == null)
            {
                _pets = new List<Pet>();
            }
        }

        public Pet Add(string name, DateTime birthDate, DateTime soldDate, Colour colour, PetType type, Person previousOwner, double price)
        {
            Pet pet;
            try
            {
                pet = new Pet(_nextId, name, birthDate, soldDate, _crep.GetById(colour.Id), _ptrep.GetById(type.Id), _prep.GetById(previousOwner.Id), price);
                _pets.Add(pet);
                _nextId++;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding pet: " + e.Message, e);
            }

            return pet;
        }

        public IEnumerable<Pet> GetAll()
        {
            //Updates data.
            for (int i = 0; i < _pets.Count; i++)
            {
                _pets[i].Colour = _crep.GetById(_pets[i].Colour.Id);
                _pets[i].Type = _ptrep.GetById(_pets[i].Type.Id);
                _pets[i].PreviousOwner = _prep.GetById(_pets[i].PreviousOwner.Id);
            }
            return _pets;
        }

        public Pet GetById(int id)
        {
            List<Pet> result = _pets.Where(pet => pet.Id == id).ToList();
            if (result.Count > 0)
            {
                //Updates data.
                result[0].Colour = _crep.GetById(result[0].Colour.Id);
                result[0].Type = _ptrep.GetById(result[0].Type.Id);
                result[0].PreviousOwner = _prep.GetById(result[0].PreviousOwner.Id);
                return result[0];
            }
            throw new RepositoryException("Pet not found!");
        }

        public Pet Update(int index, string name, DateTime birthDate, DateTime soldDate, Colour colour, PetType type, Person previousOwner, double price)
        {
            Pet pet;
            for (int i = 0; i < _pets.Count; i++)
            {
                if (_pets[i].Id == index)
                {
                    pet = new Pet(index, name, birthDate, soldDate, _crep.GetById(colour.Id), _ptrep.GetById(type.Id), _prep.GetById(previousOwner.Id), price);
                    _pets[i] = pet;
                    return pet;
                }
            }

            throw new RepositoryException("Pet not found!");
        }

        public Pet Delete(int index)
        {
            for (int i = 0; i < _pets.Count; i++)
            {
                if (_pets[i].Id == index)
                {
                    Pet pet = _pets[i];
                    _pets.Remove(_pets[i]);
                    return pet;
                }
            }

            throw new RepositoryException("Error deleting pet");
        }
    }
}
