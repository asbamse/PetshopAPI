using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.Entity
{
    public class Pet
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime SoldDate { get; set; }
        public List<PetColourRelation> Colours { get; set; }
        public PetType Type { get; set; }
        public Person PreviousOwner { get; set; }
        public double Price { get; set; }
    }
}
