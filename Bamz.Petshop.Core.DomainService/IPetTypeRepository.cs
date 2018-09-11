using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.DomainService
{
    public interface IPetTypeRepository
    {
        /// <summary>
        /// Adds petType to repository.
        /// </summary>
        /// <param name="type">Adding petType.</param>
        /// <returns></returns>
        PetType Add(string type);

        /// <summary>
        /// Gets all petTypes.
        /// </summary>
        /// <returns>All PetTypes in repository</returns>
        IEnumerable<PetType> GetAll();

        /// <summary>
        /// Gets petType with id.
        /// </summary>
        /// <returns>PetType by id in repository</returns>
        PetType GetById(int id);

        /// <summary>
        /// Updates PetType already in repository.
        /// </summary>
        /// <param name="index">Index wanted editing.</param>
        /// <param name="type">New PetType information.</param>
        /// <returns></returns>
        PetType Update(int index, string type);

        /// <summary>
        /// Deletes petType in repository.
        /// </summary>
        /// <param name="id">Id of PetType wanted deleted.</param>
        /// <returns></returns>
        PetType Delete(int id);
    }
}
