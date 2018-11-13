using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Core.ApplicationService.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _arep;

        public AddressService(IRepository<Address> addressRepository)
        {
            _arep = addressRepository;
        }

        public Address Add(Address address)
        {
            try
            {
                return _arep.Add(address);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error adding address: " + e.Message, e);
            }
        }

        public Address Delete(int index)
        {
            try
            {
                return _arep.Delete(index);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error deleting address: " + e.Message, e);
            }
        }

        public List<Address> GetAll()
        {
            try
            {
                return _arep.GetAll().ToList();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting all addresss: " + e.Message, e);
            }
        }

        public Address GetById(int id)
        {
            try
            {
                return _arep.GetById(id);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting address by id: " + e.Message, e);
            }
        }

        public Address Update(int index, Address address)
        {
            try
            {
                return _arep.Update(index, address);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error updating address: " + e.Message, e);
            }
        }
    }
}
