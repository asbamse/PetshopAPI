using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.ApplicationService
{
    public interface IColourService
    {
        /// <summary>
        /// Adds colour to repository.
        /// </summary>
        /// <returns></returns>
        Colour Add(Colour colour);

        /// <summary>
        /// Gets all colours.
        /// </summary>
        /// <returns>All Colours in repository</returns>
        List<Colour> GetAll();

        /// <summary>
        /// Gets colour with given id if present.
        /// </summary>
        /// <param name="id">Id of colour wanted.</param>
        /// <returns>Colour with given id.</returns>
        Colour GetById(int id);

        /// <summary>
        /// Updates Colour already in repository.
        /// </summary>
        /// <returns></returns>
        Colour Update(int index, Colour colour);

        /// <summary>
        /// Deletes colour in repository.
        /// </summary>
        /// <param name="index">Index wanted deleted.</param>
        /// <returns></returns>
        Colour Delete(int index);
    }
}
