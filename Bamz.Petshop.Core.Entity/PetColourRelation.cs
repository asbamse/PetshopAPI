using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.Entity
{
    public class PetColourRelation
    {
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        public int ColourId { get; set; }
        public Colour Colour { get; set; }
    }
}
