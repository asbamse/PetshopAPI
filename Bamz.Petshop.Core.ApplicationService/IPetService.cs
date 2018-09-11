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
        /// <param name="name">Name of pet.</param>
        /// <param name="birthDate">The pets date of birth.</param>
        /// <param name="soldDate">The date the pet was sold.</param>
        /// <param name="colour">Colour of pet.</param>
        /// <param name="type">Type of pet.</param>
        /// <param name="previousOwner">Previous owner of the pet.</param>
        /// <param name="price">Price of pet.</param>
        /// <returns></returns>
        Pet Add(string name, DateTime birthDate, DateTime soldDate, Colour colour, PetType type, Person previousOwner, double price);

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
        /// <param name="index">Index of pet wanted editing.</param>
        /// <param name="name">Name of pet.</param>
        /// <param name="birthDate">The pets date of birth.</param>
        /// <param name="soldDate">The date the pet was sold.</param>
        /// <param name="colour">Colour of pet.</param>
        /// <param name="type">Type of pet.</param>
        /// <param name="previousOwner">Previous owner of the pet.</param>
        /// <param name="price">Price of pet.</param>
        /// <returns></returns>
        Pet Update(int index, string name, DateTime birthDate, DateTime soldDate, Colour colour, PetType type, Person previousOwner, double price);

        /// <summary>
        /// Deletes pet in repository.
        /// </summary>
        /// <param name="index">Id of Pet wanted deleted.</param>
        /// <returns></returns>
        Pet Delete(int index);
    }
}
