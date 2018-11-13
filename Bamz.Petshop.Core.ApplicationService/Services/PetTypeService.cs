using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;

namespace Bamz.Petshop.Core.ApplicationService.Services
{
    public class PetTypeService : IPetTypeService
    {
        private readonly IRepository<PetType> _ptrep;

        public PetTypeService(IRepository<PetType> petTypeRepository)
        {
            _ptrep = petTypeRepository;
        }

        public PetType Add(PetType petType)
        {
            try
            {
                return _ptrep.Add(petType);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error adding pet type: " + e.Message, e);
            }
        }

        public PetType Delete(int id)
        {
            try
            {
                return _ptrep.Delete(id);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error deleting pet type: " + e.Message, e);
            }
        }

        public List<PetType> GetAll()
        {
            try
            {
                return _ptrep.GetAll().ToList();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error gettin all pet types: " + e.Message, e);
            }
        }

        public PetType GetById(int id)
        {
            try
            {
                return _ptrep.GetById(id);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting pet type by id: " + e.Message, e);
            }
        }

        public PetType Update(int index, PetType petType)
        {
            try
            {
                return _ptrep.Update(index, petType);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error updating pet type: " + e.Message, e);
            }
        }
    }
}
