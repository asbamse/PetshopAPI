using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;

namespace Bamz.Petshop.Core.ApplicationService.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _prep;

        public PersonService(IPersonRepository personRepository)
        {
            _prep = personRepository;
        }

        public Person Add(string firstName, string lastName, Address address, int phone, string email)
        {
            try
            {
                return _prep.Add(firstName, lastName, address, phone, email);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error adding person: " + e.Message, e);
            }
        }

        public Person Delete(int index)
        {
            try
            {
                return _prep.Delete(index);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error deleting person: " + e.Message, e);
            }
        }

        public List<Person> GetAll()
        {
            try
            {
                return _prep.GetAll().ToList();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting all persons: " + e.Message, e);
            }
        }

        public Person GetById(int id)
        {
            try
            {
                return _prep.GetById(id);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting person by id: " + e.Message, e);
            }
        }

        public Person Update(int index, string firstName, string lastName, Address address, int phone, string email)
        {
            try
            {
                return _prep.Update(index, firstName, lastName, address, phone, email);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error updating person: " + e.Message, e);
            }
        }
    }
}
