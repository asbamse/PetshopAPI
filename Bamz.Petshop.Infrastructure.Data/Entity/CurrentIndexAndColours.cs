using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data.Entity
{
    class CurrentIndexAndColours
    {
        public int NextId { get; }
        public List<Colour> Colours { get; }

        public CurrentIndexAndColours(int nextId, List<Colour> colours)
        {
            NextId = nextId;
            Colours = colours;
        }
    }
}
