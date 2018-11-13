using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.Entity
{
    public class OrderPetRelation
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
