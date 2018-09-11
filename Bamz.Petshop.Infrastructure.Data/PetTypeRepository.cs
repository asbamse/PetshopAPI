using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class PetTypeRepository : IPetTypeRepository
    {
        static int _nextId;
        static List<PetType> _petTypes;

        public PetTypeRepository()
        {
            if (_nextId < 1)
            {
                _nextId = 1;
            }
            if (_petTypes == null)
            {
                _petTypes = new List<PetType>();
            }
        }

        public PetType Add(string type)
        {
            PetType petType;
            try
            {
                petType = new PetType(_nextId, type);
                _petTypes.Add(petType);
                _nextId++;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding petType: " + e.Message, e);
            }

            return petType;
        }

        public IEnumerable<PetType> GetAll()
        {
            return _petTypes;
        }

        public PetType GetById(int id)
        {
            List<PetType> result = _petTypes.Where(petType => petType.Id == id).ToList();
            if (result.Count > 0)
            {
                return result[0];
            }
            throw new RepositoryException("PetType not found!");
        }

        public PetType Update(int index, string type)
        {
            PetType petType;
            for (int i = 0; i < _petTypes.Count; i++)
            {
                if (_petTypes[i].Id == index)
                {
                    petType = new PetType(index, type);
                    _petTypes[i] = petType;
                    return petType;
                }
            }

            throw new RepositoryException("PetType not found!");
        }

        public PetType Delete(int index)
        {
            for (int i = 0; i < _petTypes.Count; i++)
            {
                if (_petTypes[i].Id == index)
                {
                    PetType petType = _petTypes[i];
                    _petTypes.Remove(_petTypes[i]);
                    return petType;
                }
            }

            throw new RepositoryException("Error deleting petType");
        }
    }
}
