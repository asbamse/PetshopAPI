using Bamz.Petshop.Core.ApplicationService;
using Bamz.Petshop.Core.ApplicationService.Services;
using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Infrastructure.Data;
using Bamz.Petshop.UserInterface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;

namespace Bamz.Petshop
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create service collection.
            ServiceCollection sc = new ServiceCollection();

            // Adds wanted modules to scope.
            sc.AddScoped<IColourService, ColourService>();
            sc.AddScoped<IPersonService, PersonService>();
            sc.AddScoped<IPetService, PetService>();
            sc.AddScoped<IPetTypeService, PetTypeService>();
            sc.AddScoped<IColourRepository, ColourDBRepository>();
            sc.AddScoped<IPersonRepository, PersonDBRepository>();
            sc.AddScoped<IPetRepository, PetDBRepository>();
            sc.AddScoped<IPetTypeRepository, PetTypeDBRepository>();
            sc.AddScoped<IUserInterface, ConsoleUI>();

            // Build Service.
            ServiceProvider sp = sc.BuildServiceProvider();

            #region TestData

            IAddressService ads = sp.GetRequiredService<IAddressService>();
            var jensvej = ads.Add("Jensvej", 5, null, 0, null, 6700, "Jensbjerg");
            var global = ads.Add("Global Avenue", 66, "b", 0, null, 3322, "Gaby");
            var veggie = ads.Add("Vegtable Street", 49, "V", 42, "MF", 2743, "Salatary");

            IColourService cs = sp.GetRequiredService<IColourService>();
            var black = cs.Add("Black");
            var mortisCol = cs.Add("Orange");
            var grey = cs.Add("Grey");
            var white = cs.Add("White");

            IPetTypeService pts = sp.GetRequiredService<IPetTypeService>();
            var dog = pts.Add("Dog");
            var cat = pts.Add("Cat");
            var goat = pts.Add("Goat");
            var mortisType = pts.Add("Dreadnought");

            IPersonService pss = sp.GetRequiredService<IPersonService>();
            var mortisOwner = pss.Add("Jens", "Jensen", jensvej, 536736, "jens@jensen.dk");
            var r1Owner = pss.Add("John", "Smith", global, 66666666, "seeya@my.crib");
            var r2Owner = pss.Add("Wonda Bonda", "Sonda", veggie, 432589, "wbs@onda.co.uk");

            IPetService ps = sp.GetRequiredService<IPetService>();
            ps.Add(new Pet { Name = "Mortis", BirthDate = new DateTime(), SoldDate = new DateTime(), Colour = mortisCol, Type = mortisType, PreviousOwner = mortisOwner, Price = 12000000.0 });
            ps.Add(new Pet { Name = "Jaga", BirthDate = new DateTime(), SoldDate = new DateTime(), Colour = grey, Type = dog, PreviousOwner = r1Owner, Price = 10.0 });
            ps.Add(new Pet { Name = "Macauley", BirthDate = new DateTime(), SoldDate = new DateTime(), Colour = black, Type = cat, PreviousOwner = r1Owner, Price = 1300.0 });
            ps.Add(new Pet { Name = "Leray", BirthDate = new DateTime(), SoldDate = new DateTime(), Colour = grey, Type = cat, PreviousOwner = r1Owner, Price = 533 });
            ps.Add(new Pet { Name = "Guy", BirthDate = new DateTime(), SoldDate = new DateTime(), Colour = white, Type = dog, PreviousOwner = r2Owner, Price = 153.53 });
            ps.Add(new Pet { Name = "Fabia", BirthDate = new DateTime(), SoldDate = new DateTime(), Colour = white, Type = goat, PreviousOwner = r2Owner, Price = 99333 });

            #endregion

            // Gets generated User Interface to run Show() Method.
            IUserInterface ui = sp.GetRequiredService<IUserInterface>();
            ui.Show();
        }
    }
}
