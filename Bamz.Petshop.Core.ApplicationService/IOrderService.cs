using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.ApplicationService
{
    public interface IOrderService
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
        List<Order> GetAll();

        /// <summary>
        /// Gets order with given id if present.
        /// </summary>
        /// <param name="id">Id of order wanted.</param>
        /// <returns>Order with given id.</returns>
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
