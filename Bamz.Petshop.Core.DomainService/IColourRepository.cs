using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.DomainService
{
    public interface IColourRepository
    {
        /// <summary>
        /// Adds colour to repository.
        /// </summary>
        /// <param name="description">Description of colour.</param>
        /// <returns></returns>
        Colour Add(String description);

        /// <summary>
        /// Gets all colours.
        /// </summary>
        /// <returns>All Colours in repository</returns>
        IEnumerable<Colour> GetAll();

        /// <summary>
        /// Gets colour with id.
        /// </summary>
        /// <returns>Colour by id in repository</returns>
        Colour GetById(int id);

        /// <summary>
        /// Updates Colour already in repository.
        /// </summary>
        /// <param name="index">Index wanted editing.</param>
        /// <param name="description">Description of colour.</param>
        /// <returns></returns>
        Colour Update(int index, String description);

        /// <summary>
        /// Deletes colour in repository.
        /// </summary>
        /// <param name="index">Index wanted deleted.</param>
        /// <returns></returns>
        Colour Delete(int index);
    }
}
