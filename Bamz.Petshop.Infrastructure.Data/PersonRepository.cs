using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IAddressRepository _arep;

        static int _nextId;
        static List<Person> _persons;

        public PersonRepository(IAddressRepository addressService)
        {
            _arep = addressService;

            if (_nextId < 1)
            {
                _nextId = 1;
            }
            if (_persons == null)
            {
                _persons = new List<Person>();
            }
        }

        public Person Add(string firstName, string lastName, Address address, int phone, string email)
        {
            Person person;
            try
            {
                person = new Person(_nextId, firstName, lastName, _arep.GetById(address.Id), phone, email);
                _persons.Add(person);
                _nextId++;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding person: " + e.Message, e);
            }

            return person;
        }

        public IEnumerable<Person> GetAll()
        {
            return _persons;
        }

        public Person GetById(int id)
        {
            List<Person> result = _persons.Where(person => person.Id == id).ToList();
            if (result.Count > 0)
            {
                return result[0];
            }
            throw new RepositoryException("Person not found!");
        }

        public Person Update(int index, string firstName, string lastName, Address address, int phone, string email)
        {
            Person person;
            for (int i = 0; i < _persons.Count; i++)
            {
                if (_persons[i].Id == index)
                {
                    person = new Person(index, firstName, lastName, _arep.GetById(address.Id), phone, email);
                    _persons[i] = person;
                    return person;
                }
            }

            throw new RepositoryException("Person not found!");
        }

        public Person Delete(int index)
        {
            for (int i = 0; i < _persons.Count; i++)
            {
                if (_persons[i].Id == index)
                {
                    Person person = _persons[i];
                    _persons.Remove(_persons[i]);
                    return person;
                }
            }

            throw new RepositoryException("Error deleting person");
        }
    }
}
