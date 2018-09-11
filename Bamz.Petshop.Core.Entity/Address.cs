using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.Entity
{
    public class Address
    {
        public int Id { get; }
        public string Street { get; }
        public int Number { get; }
        public string Letter { get; }
        public int Floor { get; }
        public string Side { get; }
        public int ZipCode { get; }
        public string City { get; }

        public Address(int id, string street, int number, string letter, int floor, string side, int zipCode, string city)
        {
            Id = id;
            Street = street;
            Number = number;
            Letter = letter;
            Floor = floor;
            Side = side;
            ZipCode = zipCode;
            City = city;
        }
    }
}
