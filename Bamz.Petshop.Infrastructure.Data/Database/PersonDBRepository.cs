using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class PersonDBRepository : IPersonRepository
    {
        private readonly PetshopContext db;

        private readonly IRepository<Address> _arep;

        public PersonDBRepository(PetshopContext context, IRepository<Address> addressService)
        {
            db = context;

            _arep = addressService;
        }

        public Person Add(PersonInput person)
        {
            Person item;
            try
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(person.Password, out passwordHash, out passwordSalt);

                Person newPerson = new Person
                {
                    Username = person.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    IsAdmin = person.IsAdmin,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Address = _arep.GetById(person.Address.Id),
                    Phone = person.Phone,
                    Email = person.Email
                };

                item = db.Persons.Add(newPerson).Entity;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding person: " + e.Message, e);
            }

            return item;
        }

        public IEnumerable<Person> GetAll()
        {
            return db.Persons.Include(s => s.Address).ToList();
        }

        public Person GetById(int id)
        {
            var item = db.Persons.Include(s => s.Address).FirstOrDefault(b => b.Id == id);
            if (item == null)
            {
                throw new ArgumentException("Id not found!");
            }
            return item;
        }

        public Person Update(int index, PersonInput person)
        {
            var entity = db.Persons.Include(s => s.Address).FirstOrDefault(item => item.Id == index);

            if (entity != null)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(person.Password, out passwordHash, out passwordSalt);

                entity.Username = person.Username;
                entity.PasswordHash = passwordHash;
                entity.PasswordSalt = passwordSalt;
                entity.IsAdmin = person.IsAdmin;
                entity.FirstName = person.FirstName;
                entity.LastName = person.LastName;
                entity.Address = _arep.GetById(person.Address.Id);
                entity.Phone = person.Phone;
                entity.Email = person.Email;

                db.Persons.Update(entity);

                db.SaveChanges();

                return entity;
            }
            throw new ArgumentException("Id not found!");
        }

        public Person Delete(int index)
        {
            var item = GetById(index);
            db.Persons.Remove(item);
            db.SaveChanges();
            return item;
        }
        
        // This method computes a hashed and salted password using the HMACSHA512 algorithm.
        // The HMACSHA512 class computes a Hash-based Message Authentication Code (HMAC) using 
        // the SHA512 hash function. When instantiated with the parameterless constructor (as
        // here) a randomly Key is generated. This key is used as a password salt.

        // The computation is performed as shown below:
        //   passwordHash = SHA512(password + Key)

        // A password salt randomizes the password hash so that two identical passwords will
        // have significantly different hash values. This protects against sophisticated attempts
        // to guess passwords, such as a rainbow table attack.
        // The password hash is 512 bits (=64 bytes) long.
        // The password salt is 1024 bits (=128 bytes) long.
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetPage(PageProperty pageProperty)
        {
            throw new NotImplementedException();
        }
    }
}
