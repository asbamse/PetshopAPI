using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.DomainService
{
    public interface IAddressRepository
    {
        /// <summary>
        /// Adds address to repository.
        /// </summary>
        /// <returns></returns>
        Address Add(string street, int number, string letter, int floor, string side, int zipCode, string city);

        /// <summary>
        /// Gets all addresss.
        /// </summary>
        /// <returns>All Addresss in repository</returns>
        IEnumerable<Address> GetAll();

        /// <summary>
        /// Gets address with id.
        /// </summary>
        /// <returns>Address by id in repository</returns>
        Address GetById(int id);

        /// <summary>
        /// Updates Address already in repository.
        /// </summary>
        /// <param name="index">Index wanted editing.</param>
        /// <returns></returns>
        Address Update(int index, string street, int number, string letter, int floor, string side, int zipCode, string city);

        /// <summary>
        /// Deletes address in repository.
        /// </summary>
        /// <param name="index">Index wanted deleted.</param>
        /// <returns></returns>
        Address Delete(int index);
    }
}
