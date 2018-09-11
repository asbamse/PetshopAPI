using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.DomainService
{
    public interface IPetRepository
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
        /// Gets all pets.
        /// </summary>
        /// <returns>All Pets in repository</returns>
        IEnumerable<Pet> GetAll();

        /// <summary>
        /// Gets pet with id.
        /// </summary>
        /// <returns>Pet by id in repository</returns>
        Pet GetById(int id);

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
