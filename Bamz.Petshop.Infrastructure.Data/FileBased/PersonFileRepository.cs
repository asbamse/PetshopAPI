using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Infrastructure.Data.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class PersonFileRepository : IPersonRepository
    {
        private readonly IAddressRepository _arep;

        static string _path = "persons.json";
        static int _nextId;
        static List<Person> _persons;
        static Timer timer;

        public PersonFileRepository(IAddressRepository addressService)
        {
            _arep = addressService;

            _nextId = 1;
            _persons = new List<Person>();

            timer = new Timer(2000);
            timer.Elapsed += SaveChanges;
            timer.AutoReset = false;
            timer.Enabled = false;

            ReadJsonFromFile();
        }

        public Person Add(string firstName, string lastName, Address address, int phone, string email)
        {
            Person person;
            try
            {
                person = new Person
                {
                    Id = _nextId,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = _arep.GetById(address.Id),
                    Phone = phone,
                    Email = email
                };

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
                    person = new Person
                    {
                        Id = _nextId,
                        FirstName = firstName,
                        LastName = lastName,
                        Address = _arep.GetById(address.Id),
                        Phone = phone,
                        Email = email
                    };

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

        /// <summary>
        /// Resets timer.
        /// </summary>
        private void ListUpdated()
        {
            timer.Stop();
            timer.Start();
        }

        /// <summary>
        /// Saves current information to file.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void SaveChanges(Object source, ElapsedEventArgs e)
        {
            using (StreamWriter file = File.CreateText(@_path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, new CurrentIndexAndPersons { NextId = _nextId, Persons = _persons });
            }
        }

        /// <summary>
        /// Reads information from file.
        /// </summary>
        void ReadJsonFromFile()
        {
            try
            {
                using (StreamReader file = File.OpenText(@_path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    CurrentIndexAndPersons personsFromFile = (CurrentIndexAndPersons)serializer.Deserialize(file, typeof(CurrentIndexAndPersons));

                    _nextId = personsFromFile.NextId;
                    _persons = personsFromFile.Persons;
                }
            }
            catch (FileNotFoundException)
            {
                // File not found no problem
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Corrupt file! " + _path);
            }
        }
    }
}
