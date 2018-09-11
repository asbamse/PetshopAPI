using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data.Entity
{
    class CurrentIndexAndPetTypes
    {
        public int NextId { get; }
        public List<PetType> PetTypes { get; }

        public CurrentIndexAndPetTypes(int nextId, List<PetType> petTypes)
        {
            NextId = nextId;
            PetTypes = petTypes;
        }
    }
}
