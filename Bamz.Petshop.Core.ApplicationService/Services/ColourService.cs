using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Core.ApplicationService.Services
{
    public class ColourService : IColourService
    {
        private readonly IRepository<Colour> _crep;

        public ColourService(IRepository<Colour> colourService)
        {
            _crep = colourService;
        }

        public Colour Add(Colour colour)
        {
            try
            {
                return _crep.Add(colour);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error adding colour: " + e.Message, e);
            }
        }

        public Colour Delete(int index)
        {
            try
            {
                return _crep.Delete(index);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error deleting colour: " + e.Message, e);
            }
        }

        public List<Colour> GetAll()
        {
            try
            {
                return _crep.GetAll().ToList();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting all colours: " + e.Message, e);
            }
        }

        public Colour GetById(int id)
        {
            try
            {
                return _crep.GetById(id);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error getting colour by id: " + e.Message, e);
            }
        }

        public Colour Update(int index, Colour colour)
        {
            try
            {
                return _crep.Update(index, colour);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error updating colour: " + e.Message, e);
            }
        }
    }
}
