using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public Person Customer { get; set; }
        public List<OrderPetRelation> Pets { get; set; }
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
    }
}
