using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.Entity
{
    public class Colour
    {
        public int Id { get; }
        public string Description { get; }

        public Colour(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
