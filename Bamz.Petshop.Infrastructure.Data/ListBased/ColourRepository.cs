using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class ColourRepository : IColourRepository
    {
        static int _nextId;
        static List<Colour> _colours;

        public ColourRepository()
        {
            if (_nextId < 1)
            {
                _nextId = 1;
            }
            if (_colours == null)
            {
                _colours = new List<Colour>();
            }
        }

        public Colour Add(string description)
        {
            Colour colour;
            try
            {
                colour = new Colour
                {
                    Id = _nextId,
                    Description = description
                };

                _colours.Add(colour);
                _nextId++;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding colour: " + e.Message, e);
            }

            return colour;
        }

        public IEnumerable<Colour> GetAll()
        {
            return _colours;
        }

        public Colour GetById(int id)
        {
            List<Colour> result = _colours.Where(colour => colour.Id == id).ToList();
            if (result.Count > 0)
            {
                return result[0];
            }
            throw new RepositoryException("Colour not found!");
        }

        public Colour Update(int index, string description)
        {
            Colour colour;
            for (int i = 0; i < _colours.Count; i++)
            {
                if(_colours[i].Id == index)
                {
                    colour = new Colour
                    {
                        Id = _nextId,
                        Description = description
                    };

                    _colours[i] = colour;
                    return colour;
                }
            }

            throw new RepositoryException("Colour not found!");
        }

        public Colour Delete(int index)
        {
            for (int i = 0; i < _colours.Count; i++)
            {
                if (_colours[i].Id == index)
                {
                    Colour colour = _colours[i];
                    _colours.Remove(_colours[i]);
                    return colour;
                }
            }

            throw new RepositoryException("Error deleting colour");
        }
    }
}
