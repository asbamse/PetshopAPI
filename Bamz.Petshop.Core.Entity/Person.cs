using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.Entity
{
    public class Person
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Address Address { get; set; }
        public int Phone { get; }
        public string Email { get; }

        public Person(int id, string firstName, string lastName, Address address, int phone, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Phone = phone;
            Email = email;
        }
    }
}
