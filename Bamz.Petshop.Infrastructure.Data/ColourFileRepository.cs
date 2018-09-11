using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Text;
using Bamz.Petshop.Infrastructure.Data.Entity;
using System.Linq;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class ColourFileRepository : IColourRepository
    {
        static string _path = "colours.json";
        static int _nextId;
        static List<Colour> _colours;
        static Timer timer;

        public ColourFileRepository()
        {
            _nextId = 1;
            _colours = new List<Colour>();

            timer = new Timer(2000);
            timer.Elapsed += SaveChanges;
            timer.AutoReset = false;
            timer.Enabled = false;

            ReadJsonFromFile();
        }

        public Colour Add(string description)
        {
            Colour colour;
            try
            {
                colour = new Colour(_nextId, description);
                _colours.Add(colour);
                _nextId++;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding colour: " + e.Message, e);
            }

            ListUpdated();
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
                if (_colours[i].Id == index)
                {
                    colour = new Colour(index, description);
                    _colours[i] = colour;
                    ListUpdated();
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
                    ListUpdated();
                    return colour;
                }
            }

            throw new RepositoryException("Error deleting colour");
        }

        /// <summary>
        /// Resets timer.
        /// </summary>
        private void ListUpdated()
        {
            timer.Stop();
            timer.Start();
        }

        /// <summary>
        /// Saves current information to file.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void SaveChanges(Object source, ElapsedEventArgs e)
        {
            using (StreamWriter file = File.CreateText(@_path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, new CurrentIndexAndColours(_nextId, _colours));
            }
        }

        /// <summary>
        /// Reads information from file.
        /// </summary>
        void ReadJsonFromFile()
        {
            try
            {
                using (StreamReader file = File.OpenText(@_path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    CurrentIndexAndColours coloursFromFile = (CurrentIndexAndColours)serializer.Deserialize(file, typeof(CurrentIndexAndColours));

                    _nextId = coloursFromFile.NextId;
                    _colours = coloursFromFile.Colours;
                }
            }
            catch(FileNotFoundException)
            {
                // File not found no problem
            }
            catch(InvalidCastException)
            {
                Console.WriteLine("Corrupt file! " + _path);
            }
        }
    }
}
