using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.ApplicationService
{
    public interface IPetTypeService
    {
        /// <summary>
        /// Adds petType to repository.
        /// </summary>
        /// <returns></returns>
        PetType Add(PetType petType);

        /// <summary>
        /// Gets all petTypes.
        /// </summary>
        /// <returns>All PetTypes in repository</returns>
        List<PetType> GetAll();

        /// <summary>
        /// Gets petType with given id if present.
        /// </summary>
        /// <param name="id">Id of petType wanted.</param>
        /// <returns>PetType with given id.</returns>
        PetType GetById(int id);

        /// <summary>
        /// Updates PetType already in repository.
        /// </summary>
        /// <returns></returns>
        PetType Update(int index, PetType petType);

        /// <summary>
        /// Deletes petType in repository.
        /// </summary>
        /// <param name="id">Id of PetType wanted deleted.</param>
        /// <returns></returns>
        PetType Delete(int id);
    }
}
