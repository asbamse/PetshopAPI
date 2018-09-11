using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class AddressRepository : IAddressRepository
    {
        static int _nextId;
        static List<Address> _addresss;

        public AddressRepository()
        {
            if (_nextId < 1)
            {
                _nextId = 1;
            }
            if (_addresss == null)
            {
                _addresss = new List<Address>();
            }
        }

        public Address Add(string street, int number, string letter, int floor, string side, int zipCode, string city)
        {
            Address address;
            try
            {
                address = new Address(_nextId, street, number, letter, floor, side, zipCode, city);
                _addresss.Add(address);
                _nextId++;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding address: " + e.Message, e);
            }

            return address;
        }

        public IEnumerable<Address> GetAll()
        {
            return _addresss;
        }

        public Address GetById(int id)
        {
            List<Address> result = _addresss.Where(address => address.Id == id).ToList();
            if (result.Count > 0)
            {
                return result[0];
            }
            throw new RepositoryException("Address not found!");
        }

        public Address Update(int index, string street, int number, string letter, int floor, string side, int zipCode, string city)
        {
            Address address;
            for (int i = 0; i < _addresss.Count; i++)
            {
                if (_addresss[i].Id == index)
                {
                    address = new Address(_nextId, street, number, letter, floor, side, zipCode, city);
                    _addresss[i] = address;
                    return address;
                }
            }

            throw new RepositoryException("Address not found!");
        }

        public Address Delete(int index)
        {
            for (int i = 0; i < _addresss.Count; i++)
            {
                if (_addresss[i].Id == index)
                {
                    Address address = _addresss[i];
                    _addresss.Remove(_addresss[i]);
                    return address;
                }
            }

            throw new RepositoryException("Error deleting address");
        }
    }
}
