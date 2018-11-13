using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.DomainService
{
    public interface IPersonRepository
    {
        int Count();
        IEnumerable<Person> GetAll();
        IEnumerable<Person> GetPage(PageProperty pageProperty);
        Person GetById(int id);
        Person Add(PersonInput entity);
        Person Update(int id, PersonInput entity);
        Person Delete(int id);
    }
}
