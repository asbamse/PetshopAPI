using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Core.Utilities;
using Bamz.Petshop.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bamz.Petshop.Infrastructure.Data.Database
{
    public static class DBInit
    {
        // This method will create and seed the database.
        public static void Initialize(PetshopContext context)
        {
            // Delete the database, if it already exists. I do this because an
            // existing database may not be compatible with the entity model,
            // if the entity model was changed since the database was created.
            context.Database.EnsureDeleted();

            // Create the database, if it does not already exists. This operation
            // is necessary, if you don't use an in-memory database.
            context.Database.EnsureCreated();

            if (context.Addresses.Any())
            {
                return;   // DB has been seeded
            }

            List<Address> addresses = new List<Address>
            {
                new Address{ Street="Jensvej", Number=5, Letter=null, Floor=null, Side=null, ZipCode=6700, City="Jensbjerg" },
                new Address{ Street="Global Avenue", Number=66, Letter="b", Floor=null, Side=null, ZipCode=3322, City="Gaby" },
                new Address{ Street="Vegtable Street", Number=49, Letter="V", Floor=42, Side="MF", ZipCode=2743, City="Salatary" }
            };

            context.Addresses.AddRange(addresses);
            
            List<Colour> colours = new List<Colour>
            {
                new Colour{ Description="Black" },
                new Colour{ Description="Orange" },
                new Colour{ Description="Grey" },
                new Colour{ Description="White" }
            };

            context.Colours.AddRange(colours);

            List<PetType> petTypes = new List<PetType>
            {
                new PetType{ Type="Dog" },
                new PetType{ Type="Cat" },
                new PetType{ Type="Goat" },
                new PetType{ Type="Dreadnought" }
            };

            context.PetTypes.AddRange(petTypes);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("1234", out passwordHash, out passwordSalt);


            List<Person> persons = new List<Person>
            {
                new Person{ Username="jens", PasswordHash=passwordHash, PasswordSalt=passwordSalt, IsAdmin=false, FirstName="Jens" , LastName="Jensen" , Address=addresses[0] , Phone=536736 , Email="jens@jensen.dk" },
                new Person{ Username="smith", PasswordHash=passwordHash, PasswordSalt=passwordSalt, IsAdmin=false, FirstName="John" , LastName="Smith" , Address=addresses[1] , Phone=66666666 , Email="seeya@my.crib" },
                new Person{ Username="konda", PasswordHash=passwordHash, PasswordSalt=passwordSalt, IsAdmin=true, FirstName="Wonda Bonda" , LastName="Sonda" , Address=addresses[2] , Phone=432589 , Email="wbs@onda.co.uk" },
            };

            context.Persons.AddRange(persons);

            List<Pet> pets = new List<Pet>
            {
                new Pet{ Name="Mortis" , BirthDate=new DateTime() , SoldDate=DateTime.Now , Colours=new List<PetColourRelation> { new PetColourRelation() { ColourId = colours[0].Id } } , Type=petTypes[3] , PreviousOwner=persons[1] , Price=12000000.0 },
                new Pet{ Name="Jaga" , BirthDate=new DateTime() , SoldDate=DateTime.Now , Colours=new List<PetColourRelation> { new PetColourRelation() { ColourId = colours[1].Id }, new PetColourRelation() { ColourId = colours[2].Id } } , Type=petTypes[2] , PreviousOwner=persons[1] , Price=10.0 },
                new Pet{ Name="Macauley" , BirthDate=new DateTime() , SoldDate=DateTime.Now , Colours=new List<PetColourRelation> { new PetColourRelation() { ColourId = colours[2].Id } } , Type=petTypes[2] , PreviousOwner=persons[0] , Price=1300.0 },
                new Pet{ Name="Leray" , BirthDate=new DateTime() , SoldDate=DateTime.Now , Colours=new List<PetColourRelation> { new PetColourRelation() { ColourId = colours[3].Id } }, Type=petTypes[1] , PreviousOwner=persons[1] , Price=533 },
                new Pet{ Name="Guy" , BirthDate=new DateTime() , SoldDate=DateTime.Now , Colours=new List<PetColourRelation> { new PetColourRelation() { ColourId = colours[2].Id },new PetColourRelation() { ColourId = colours[1].Id } }, Type=petTypes[0] , PreviousOwner=persons[2] , Price=153.53 },
                new Pet{ Name="Fabia" , BirthDate=new DateTime() , SoldDate=DateTime.Now , Colours=new List<PetColourRelation> { new PetColourRelation() { ColourId = colours[1].Id } } , Type=petTypes[0] , PreviousOwner=persons[0] , Price=99333 },
            };

            context.Pets.AddRange(pets);

            Order order = new Order { Customer = persons[2], OrderDate = DateTime.Now, Price = 2000, Pets= new List<OrderPetRelation> { new OrderPetRelation { Pet = pets[0] } } };

            context.Orders.AddRange(order);
            context.SaveChanges();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
