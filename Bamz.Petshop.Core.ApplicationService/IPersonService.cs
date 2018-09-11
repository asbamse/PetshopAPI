﻿using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.ApplicationService
{
    public interface IPersonService
    {
        /// <summary>
        /// Adds person to repository.
        /// </summary>
        /// <param name="firstName">Firstname.</param>
        /// <param name="lastName">Lastname.</param>
        /// <param name="address">Address.</param>
        /// <param name="phone">Phone.</param>
        /// <param name="email">Email.</param>
        /// <returns></returns>
        Person Add(string firstName, string lastName, Address address, int phone, string email);

        /// <summary>
        /// Gets all persons.
        /// </summary>
        /// <returns>All Persons in repository</returns>
        List<Person> GetAll();

        /// <summary>
        /// Gets person with given id if present.
        /// </summary>
        /// <param name="id">Id of person wanted.</param>
        /// <returns>Person with given id.</returns>
        Person GetById(int id);

        /// <summary>
        /// Updates Person already in repository.
        /// </summary>
        /// <param name="index">Index of person wanted editing.</param>
        /// <param name="firstName">Firstname.</param>
        /// <param name="lastName">Lastname.</param>
        /// <param name="address">Address.</param>
        /// <param name="phone">Phone.</param>
        /// <param name="email">Email.</param>
        /// <returns></returns>
        Person Update(int index, string firstName, string lastName, Address address, int phone, string email);

        /// <summary>
        /// Deletes person in repository.
        /// </summary>
        /// <param name="index">Id of Person wanted deleted.</param>
        /// <returns></returns>
        Person Delete(int index);
    }
}