using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.Entity
{
    public class PetType
    {
        public int Id { get; }
        public string Type { get; }

        public PetType(int id, string type)
        {
            Id = id;
            Type = type;
        }
    }
}
