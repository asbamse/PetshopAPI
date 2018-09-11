using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data.Entity
{
    class CurrentIndexAndPersons
    {
        public int NextId { get; }
        public List<Person> Persons { get; }

        public CurrentIndexAndPersons(int nextId, List<Person> persons)
        {
            NextId = nextId;
            Persons = persons;
        }
    }
}
