using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.DomainService
{
    public interface IRepository<T>
    {
        int Count();
        IEnumerable<T> GetAll();
        IEnumerable<T> GetPage(PageProperty pageProperty);
        T GetById(int id);
        T Add(T entity);
        T Update(int id, T entity);
        T Delete(int id);
    }
}
