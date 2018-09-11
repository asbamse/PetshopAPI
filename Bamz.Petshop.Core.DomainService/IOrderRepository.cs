using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.DomainService
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Adds order to repository.
        /// </summary>
        /// <returns></returns>
        Order Add(Person customer, List<Pet> pets, DateTime orderDate, double price);

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>All Orders in repository</returns>
        IEnumerable<Order> GetAll();

        /// <summary>
        /// Gets order with id.
        /// </summary>
        /// <returns>Order by id in repository</returns>
        Order GetById(int id);

        /// <summary>
        /// Updates Order already in repository.
        /// </summary>
        /// <returns></returns>
        Order Update(int index, Person customer, List<Pet> pets, DateTime orderDate, double price);

        /// <summary>
        /// Deletes order in repository.
        /// </summary>
        /// <param name="index">Id of Order wanted deleted.</param>
        /// <returns></returns>
        Order Delete(int index);
    }
}
