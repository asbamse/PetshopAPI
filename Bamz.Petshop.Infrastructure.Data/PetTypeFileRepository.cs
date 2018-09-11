using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Infrastructure.Data.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace Bamz.Petshop.Infrastructure.Data
{
    public class PetTypeFileRepository : IPetTypeRepository
    {
        static string _path = "pettypes.json";
        static int _nextId;
        static List<PetType> _petTypes;
        static Timer timer;

        public PetTypeFileRepository()
        {
            _nextId = 1;
            _petTypes = new List<PetType>();

            timer = new Timer(2000);
            timer.Elapsed += SaveChanges;
            timer.AutoReset = false;
            timer.Enabled = false;

            ReadJsonFromFile();
        }

        public PetType Add(string type)
        {
            PetType petType;
            try
            {
                petType = new PetType(_nextId, type);
                _petTypes.Add(petType);
                _nextId++;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Error adding petType: " + e.Message, e);
            }

            ListUpdated();
            return petType;
        }

        public IEnumerable<PetType> GetAll()
        {
            return _petTypes;
        }

        public PetType GetById(int id)
        {
            List<PetType> result = _petTypes.Where(petType => petType.Id == id).ToList();
            if (result.Count > 0)
            {
                return result[0];
            }
            throw new RepositoryException("PetType not found!");
        }

        public PetType Update(int index, string type)
        {
            PetType petType;
            for (int i = 0; i < _petTypes.Count; i++)
            {
                if (_petTypes[i].Id == index)
                {
                    petType = new PetType(index, type);
                    _petTypes[i] = petType;
                    ListUpdated();
                    return petType;
                }
            }

            throw new RepositoryException("PetType not found!");
        }

        public PetType Delete(int index)
        {
            for (int i = 0; i < _petTypes.Count; i++)
            {
                if (_petTypes[i].Id == index)
                {
                    PetType petType = _petTypes[i];
                    _petTypes.Remove(_petTypes[i]);
                    ListUpdated();
                    return petType;
                }
            }

            throw new RepositoryException("Error deleting petType");
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
                serializer.Serialize(file, new CurrentIndexAndPetTypes(_nextId, _petTypes));
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
                    CurrentIndexAndPetTypes petTypesFromFile = (CurrentIndexAndPetTypes)serializer.Deserialize(file, typeof(CurrentIndexAndPetTypes));

                    _nextId = petTypesFromFile.NextId;
                    _petTypes = petTypesFromFile.PetTypes;
                }
            }
            catch (FileNotFoundException)
            {
                // File not found no problem
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Corrupt file! " + _path);
            }
        }
    }
}
