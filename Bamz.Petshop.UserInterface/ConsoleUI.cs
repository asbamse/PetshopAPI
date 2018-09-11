using Bamz.Petshop.Core.ApplicationService;
using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.UserInterface
{
    public class ConsoleUI : IUserInterface
    {
        private readonly IColourService _colServ;
        private readonly IPersonService _psnServ;
        private readonly IPetService _petServ;
        private readonly IPetTypeService _ptServ;

        private readonly Menu main;
        private readonly Menu colourMenu;
        private readonly Menu personMenu;
        private readonly Menu petMenu;
        private readonly Menu petTypeMenu;
        private readonly Menu petReadMenu;

        public ConsoleUI(IColourService colourService, IPersonService personService, IPetService petService, IPetTypeService petTypeService)
        {
            _colServ = colourService;
            _psnServ = personService;
            _petServ = petService;
            _ptServ = petTypeService;

            main = new Menu("Main Menu");
            colourMenu = new Menu("Colour Menu", main);
            personMenu = new Menu("Person Menu", main);
            petMenu = new Menu("Pet Menu", main);
            petTypeMenu = new Menu("Pet Type Menu", main);
            petReadMenu = new Menu("Pet Read Menu", petMenu);

            #region Main
            main.SetMenu(
                new MenuItem[] {
                    new MenuItem("Pet", () =>
                    {
                        Console.Clear();
                        petMenu.Show();
                    }),
                    new MenuItem("Pet Type", () =>
                    {
                        Console.Clear();
                        petTypeMenu.Show();
                    }),
                    new MenuItem("Person", () =>
                    {
                        Console.Clear();
                        personMenu.Show();
                    }),
                    new MenuItem("Colour", () =>
                    {
                        Console.Clear();
                        colourMenu.Show();
                    })
                });
            #endregion

            #region Colour
            colourMenu.SetMenu(
                new MenuItem[] {
                    new MenuItem("Create", () =>
                    {
                        string description = ConsoleUtils.ReadNotEmpty("Input colour description: ");

                        try
                        {
                            Colour colour = _colServ.Add(description);
                            Console.WriteLine("Successfully added {0} to the repository!", colour.Description);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Read All", () =>
                    {
                        try
                        {
                            List<Colour> colours = _colServ.GetAll();
                            if(colours.Count > 0)
                            {
                                foreach (Colour col in colours)
                                {
                                    Console.WriteLine("ID: {0}, Description: {1}.", col.Id, col.Description);
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no colours stored.");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Update", () =>
                    {
                        int index = ConsoleUtils.ReadInt("Input id of colour wanted changed: ");
                        string description = ConsoleUtils.ReadNotEmpty("Input colour description: ");

                        try
                        {
                            Colour colour = _colServ.Update(index, description);
                            Console.WriteLine("Successfully updated {0}'s description to {1}!", index, colour.Description);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Delete", () =>
                    {
                        int index = ConsoleUtils.ReadInt("Input id of colour wanted deleted: ");

                        try
                        {
                            Colour colour = _colServ.Delete(index);
                            Console.WriteLine("Successfully deleted {0} from the repository!", colour.Description);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    })
                });
            #endregion

            #region Person
            personMenu.SetMenu(
                new MenuItem[] {
                    new MenuItem("Create", () =>
                    {
                        string firstName = ConsoleUtils.ReadNotEmpty("Input first name: ");
                        string lastName = ConsoleUtils.ReadNotEmpty("Input last name: ");
                        string address = ConsoleUtils.ReadNotEmpty("Input address: ");
                        int phone = ConsoleUtils.ReadInt("Input phone number: ");
                        string email = ConsoleUtils.ReadNotEmpty("Input email: ");

                        try
                        {
                            Person person = _psnServ.Add(firstName, lastName, address, phone, email);
                            Console.WriteLine("Successfully added {0} {1} to the repository!", person.FirstName, person.LastName);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Read All", () =>
                    {
                        try
                        {
                            List<Person> persons = _psnServ.GetAll();
                            if(persons.Count > 0)
                            {
                                foreach (Person psn in persons)
                                {
                                    Console.WriteLine("ID: {0}, First name: {1}, Last name: {2}, address: {3}, phone: {4}, email: {5}.", psn.Id, psn.FirstName, psn.LastName, psn.Address, psn.Phone, psn.Email);
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no persons stored.");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Update", () =>
                    {
                        int index = ConsoleUtils.ReadInt("Input id of person wanted changed: ");
                        string firstName = ConsoleUtils.ReadNotEmpty("Input first name: ");
                        string lastName = ConsoleUtils.ReadNotEmpty("Input last name: ");
                        string address = ConsoleUtils.ReadNotEmpty("Input address: ");
                        int phone = ConsoleUtils.ReadInt("Input phone number: ");
                        string email = ConsoleUtils.ReadNotEmpty("Input email: ");

                        try
                        {
                            Person person = _psnServ.Update(index, firstName, lastName, address, phone, email);
                            Console.WriteLine("Successfully updated {0}'s to First name: {1}, Last name: {2}, address: {3}, phone: {4}, email: {5}!", index, person.FirstName, person.LastName, person.Address, person.Address, person.Phone, person.Email);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Delete", () =>
                    {
                        int index = ConsoleUtils.ReadInt("Input id of person wanted deleted: ");

                        try
                        {
                            Person person = _psnServ.Delete(index);
                            Console.WriteLine("Successfully deleted {0} {1} from the repository!", person.FirstName, person.LastName);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    })
                });
            #endregion

            #region Pet
            petMenu.SetMenu(
                new MenuItem[] {
                    new MenuItem("Create", () =>
                    {
                        string name = ConsoleUtils.ReadNotEmpty("Input name: ");
                        DateTime birthDate = ConsoleUtils.ReadDate("Input birth date: ");
                        DateTime soldDate = ConsoleUtils.ReadDate("Input sold date: ");

                        Colour colour = ChooseColour(petMenu);
                        if(colour==null)
                        {
                            Console.WriteLine("No colours found! Create the colour before creating the pet.");
                            return;
                        }

                        PetType type = ChoosePetType(petMenu);
                        if(type==null)
                        {
                            Console.WriteLine("No pet types found! Create the pet type before creating the pet.");
                            return;
                        }

                        Person previousOwner = ChoosePerson(petMenu);
                        if(previousOwner==null)
                        {
                            Console.WriteLine("No persons found! Create the person before creating the pet.");
                            return;
                        }

                        double price = ConsoleUtils.ReadDouble("Input price: ");

                        try
                        {
                            Pet pet = _petServ.Add(name, birthDate, soldDate, colour, type, previousOwner, price);
                            Console.WriteLine("Successfully added {0} to the repository!", pet.Name);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Read", () =>
                    {
                        Console.Clear();
                        petReadMenu.Show();
                    }),
                    new MenuItem("Update", () =>
                    {
                        int index = ConsoleUtils.ReadInt("Input id of pet wanted updated: ");
                        string name = ConsoleUtils.ReadNotEmpty("Input name: ");
                        DateTime birthDate = ConsoleUtils.ReadDate("Input birth date: ");
                        DateTime soldDate = ConsoleUtils.ReadDate("Input sold date: ");

                        Colour colour = ChooseColour(petMenu);
                        if(colour==null)
                        {
                            Console.WriteLine("No colours found! Create the colour before creating the pet.");
                            return;
                        }

                        PetType type = ChoosePetType(petMenu);
                        if(type==null)
                        {
                            Console.WriteLine("No pet types found! Create the pet type before creating the pet.");
                            return;
                        }

                        Person previousOwner = ChoosePerson(petMenu);
                        if(previousOwner==null)
                        {
                            Console.WriteLine("No persons found! Create the person before creating the pet.");
                            return;
                        }

                        double price = ConsoleUtils.ReadDouble("Input price: ");

                        try
                        {
                            Pet pet = _petServ.Update(index, name, birthDate, soldDate, colour, type, previousOwner, price);
                            Console.WriteLine("Successfully updated {0}'s to Name: {1}, Birth Date: {2}, Sold Date: {3}, Colour: {4}, Pet Type: {5}, Previous Owner: {6} {7}, Price {8}.", index, pet.Name, pet.BirthDate, pet.SoldDate, pet.Colour.Description, pet.Type.Type, pet.PreviousOwner.FirstName, pet.PreviousOwner.LastName, pet.Price);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Delete", () =>
                    {
                        int index = ConsoleUtils.ReadInt("Input id of pet wanted deleted: ");

                        try
                        {
                            Pet pet = _petServ.Delete(index);
                            Console.WriteLine("Successfully deleted {0} from the repository!", pet.Name);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    })
                });
            #endregion

            #region Pet Type
            petTypeMenu.SetMenu(
                new MenuItem[] {
                    new MenuItem("Create", () =>
                    {
                        string type = ConsoleUtils.ReadNotEmpty("Input pet type: ");

                        try
                        {
                            PetType petType = _ptServ.Add(type);
                            Console.WriteLine("Successfully added {0} to the repository!", petType.Type);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Read All", () =>
                    {
                        try
                        {
                            List<PetType> petTypes = _ptServ.GetAll();
                            if(petTypes.Count > 0)
                            {
                                foreach (PetType pt in petTypes)
                                {
                                    Console.WriteLine("ID: {0}, Type: {1}.", pt.Id, pt.Type);
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no pet types stored.");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Update", () =>
                    {
                        int index = ConsoleUtils.ReadInt("Input id of pet type wanted changed: ");
                        string type = ConsoleUtils.ReadNotEmpty("Input pet type: ");

                        try
                        {
                            PetType petType = _ptServ.Update(index, type);
                            Console.WriteLine("Successfully updated {0}'s type to {1}!", index, petType.Type);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Delete", () =>
                    {
                        int index = ConsoleUtils.ReadInt("Input id of pet type wanted deleted: ");

                        try
                        {
                            PetType petType = _ptServ.Delete(index);
                            Console.WriteLine("Successfully deleted {0} from the repository!", petType.Type);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    })
                });
            #endregion

            #region Pet Read
            petReadMenu.SetMenu(
                new MenuItem[] {
                    new MenuItem("Read All", () =>
                    {
                        try
                        {
                            List<Pet> pets = _petServ.GetAll();
                            if(pets.Count > 0)
                            {
                                foreach (Pet pet in pets)
                                {
                                    Console.WriteLine("ID: {0}, Name: {1}, Birth Date: {2}, Sold Date: {3}, Colour: {4}, Pet Type: {5}, Previous Owner: {6} {7}, Price {8}.", pet.Id, pet.Name, pet.BirthDate, pet.SoldDate, pet.Colour.Description, pet.Type.Type, pet.PreviousOwner.FirstName, pet.PreviousOwner.LastName, pet.Price);
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no pets stored.");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Read By ID", () =>
                    {
                        int index = ConsoleUtils.ReadInt("Input pet id: ");
                        try
                        {
                            Pet pet = _petServ.GetById(index);
                            if(pet != null)
                            {
                                Console.WriteLine("ID: {0}, Name: {1}, Birth Date: {2}, Sold Date: {3}, Colour: {4}, Pet Type: {5}, Previous Owner: {6} {7}, Price {8}.", pet.Id, pet.Name, pet.BirthDate, pet.SoldDate, pet.Colour.Description, pet.Type.Type, pet.PreviousOwner.FirstName, pet.PreviousOwner.LastName, pet.Price);
                            }
                            else
                            {
                                Console.WriteLine("ID was not found in repository.");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Read All Ordered By Price Ascending", () =>
                    {
                        try
                        {
                            List<Pet> pets = _petServ.GetAllOrderPrice();
                            if(pets.Count > 0)
                            {
                                foreach (Pet pet in pets)
                                {
                                    Console.WriteLine("ID: {0}, Name: {1}, Birth Date: {2}, Sold Date: {3}, Colour: {4}, Pet Type: {5}, Previous Owner: {6} {7}, Price {8}.", pet.Id, pet.Name, pet.BirthDate, pet.SoldDate, pet.Colour.Description, pet.Type.Type, pet.PreviousOwner.FirstName, pet.PreviousOwner.LastName, pet.Price);
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no pets stored.");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Read Five Cheapest", () =>
                    {
                        try
                        {
                            List<Pet> pets = _petServ.GetFiveCheapest();
                            if(pets.Count > 0)
                            {
                                foreach (Pet pet in pets)
                                {
                                    Console.WriteLine("ID: {0}, Name: {1}, Birth Date: {2}, Sold Date: {3}, Colour: {4}, Pet Type: {5}, Previous Owner: {6} {7}, Price {8}.", pet.Id, pet.Name, pet.BirthDate, pet.SoldDate, pet.Colour.Description, pet.Type.Type, pet.PreviousOwner.FirstName, pet.PreviousOwner.LastName, pet.Price);
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no pets stored.");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new MenuItem("Read By Type", () =>
                    {
                        PetType type = ChoosePetType(petMenu);
                        if(type==null)
                        {
                            Console.WriteLine("No pet types found! Create the pet type before searching.");
                            return;
                        }

                        try
                        {
                            List<Pet> pets = _petServ.SearchByType(type);
                            if(pets.Count > 0)
                            {
                                foreach (Pet pet in pets)
                                {
                                    Console.WriteLine("ID: {0}, Name: {1}, Birth Date: {2}, Sold Date: {3}, Colour: {4}, Pet Type: {5}, Previous Owner: {6} {7}, Price {8}.", pet.Id, pet.Name, pet.BirthDate, pet.SoldDate, pet.Colour.Description, pet.Type.Type, pet.PreviousOwner.FirstName, pet.PreviousOwner.LastName, pet.Price);
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no pets of this type.");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    })
                });
            #endregion
        }

        public void Show()
        {
            main.Show();
        }

        /// <summary>
        /// Choose colour, returns null if no colour is found.
        /// </summary>
        /// <returns>Colour</returns>
        Colour ChooseColour(Menu parent)
        {
            Colour colour = null;
            List<Colour> colours = _colServ.GetAll();
            MenuItem[] colourItems = new MenuItem[colours.Count];
            Menu chooseColour = new Menu("Choose colour", parent, true);

            for (int i = 0; i < colours.Count; i++)
            {
                // Store i to secure constant.
                int ci = i;
                colourItems[ci] = new MenuItem(colours[ci].Description, () => { colour = colours[ci]; chooseColour.Exit(); });
            }

            if (colourItems.Length > 0)
            {
                chooseColour.SetMenu(colourItems);
                chooseColour.Show();
            }

            return colour;
        }

        /// <summary>
        /// Choose person, returns null if no person is found.
        /// </summary>
        /// <returns>Person</returns>
        Person ChoosePerson(Menu parent)
        {
            Person person = null;
            List<Person> persons = _psnServ.GetAll();
            MenuItem[] personItems = new MenuItem[persons.Count];
            Menu choosePerson = new Menu("Choose person", parent, true);

            for (int i = 0; i < persons.Count; i++)
            {
                // Store i to secure constant.
                int ci = i;
                personItems[ci] = new MenuItem(persons[ci].FirstName + " " + persons[ci].LastName, () => { person = persons[ci]; choosePerson.Exit(); });
            }

            if (personItems.Length > 0)
            {
                choosePerson.SetMenu(personItems);
                choosePerson.Show();
            }

            return person;
        }

        /// <summary>
        /// Choose petType, returns null if no petType is found.
        /// </summary>
        /// <returns>PetType</returns>
        PetType ChoosePetType(Menu parent)
        {
            PetType petType = null;
            List<PetType> petTypes = _ptServ.GetAll();
            MenuItem[] petTypeItems = new MenuItem[petTypes.Count];
            Menu choosePetType = new Menu("Choose petType", parent, true);

            for (int i = 0; i < petTypes.Count; i++)
            {
                // Store i to secure constant.
                int ci = i;
                petTypeItems[ci] = new MenuItem(petTypes[ci].Type, () => { petType = petTypes[ci]; choosePetType.Exit(); });
            }

            if (petTypeItems.Length > 0)
            {
                choosePetType.SetMenu(petTypeItems);
                choosePetType.Show();
            }

            return petType;
        }
    }
}