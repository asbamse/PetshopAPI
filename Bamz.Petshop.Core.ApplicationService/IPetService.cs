using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.ApplicationService
{
    public interface IPetService
    {
        /// <summary>
        /// Adds pet to repository.
        /// </summary>
        /// <returns></returns>
        Pet Add(Pet pet);

        /// <summary>
        /// Gets pet with given id if present.
        /// </summary>
        /// <param name="id">Id of pet wanted.</param>
        /// <returns>Pet with given id.</returns>
        Pet GetById(int id);

        /// <summary>
        /// Gets all pets.
        /// </summary>
        /// <returns>All Pets in repository</returns>
        List<Pet> GetAll();

        List<Pet> GetPage(PageProperty pageProperty);

        /// <summary>
        /// Gets all pets in order cheapest to most expensive.
        /// </summary>
        /// <returns>All Pets in repository</returns>
        List<Pet> GetAllOrderPrice();

        /// <summary>
        /// Gets the five cheapest pets.
        /// </summary>
        /// <returns>Five cheapest pets.</returns>
        List<Pet> GetFiveCheapest();

        /// <summary>
        /// Search for all pets of given type.
        /// </summary>
        /// <param name="petType">The type wanted.</param>
        /// <returns>A list of pets of given type.</returns>
        List<Pet> SearchByType(PetType petType);

        /// <summary>
        /// Updates Pet already in repository.
        /// </summary>
        /// <returns></returns>
        Pet Update(int id, Pet pet);

        /// <summary>
        /// Deletes pet in repository.
        /// </summary>
        /// <param name="index">Id of Pet wanted deleted.</param>
        /// <returns></returns>
        Pet Delete(int index);
    }
}
