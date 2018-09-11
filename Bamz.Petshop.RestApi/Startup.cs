using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bamz.Petshop.Core.ApplicationService;
using Bamz.Petshop.Core.ApplicationService.Services;
using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bamz.Petshop.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IColourService, ColourService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IPetTypeService, PetTypeService>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IColourRepository, ColourRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetTypeRepository, PetTypeRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region TestData

            ServiceProvider sp = services.BuildServiceProvider();

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
            var testPet = ps.Add("Mortis", new DateTime(), DateTime.Now, mortisCol, mortisType, mortisOwner, 12000000.0);
            ps.Add("Jaga", new DateTime(), DateTime.Now, grey, dog, r1Owner, 10.0);
            ps.Add("Macauley", new DateTime(), DateTime.Now, black, cat, r1Owner, 1300.0);
            ps.Add("Leray", new DateTime(), DateTime.Now, grey, cat, r1Owner, 533);
            ps.Add("Guy", new DateTime(), DateTime.Now, white, dog, r2Owner, 153.53);
            ps.Add("Fabia", new DateTime(), DateTime.Now, white, goat, r2Owner, 99333);

            IOrderService os = sp.GetRequiredService<IOrderService>();
            os.Add(mortisOwner, new List<Pet>() { testPet }, DateTime.Now, testPet.Price);

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
