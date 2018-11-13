using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data.Entity
{
    class CurrentIndexAndPetTypes
    {
        public int NextId { get; set; }
        public List<PetType> PetTypes { get; set; }
    }
}
