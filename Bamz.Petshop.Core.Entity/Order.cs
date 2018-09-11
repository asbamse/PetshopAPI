using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.Entity
{
    public class Order
    {
        public int Id { get; }
        public Person Customer { get; set; }
        public List<Pet> Pets { get; set; }
        public DateTime OrderDate { get; }
        public double Price { get; }

        public Order(int id, Person customer, List<Pet> pets, DateTime orderDate, double price)
        {
            Id = id;
            Customer = customer;
            Pets = pets;
            OrderDate = orderDate;
            Price = price;
        }
    }
}
