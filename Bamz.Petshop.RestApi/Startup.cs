using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bamz.Petshop.Core.ApplicationService;
using Bamz.Petshop.Core.ApplicationService.Services;
using Bamz.Petshop.Core.DomainService;
using Bamz.Petshop.Core.Entity;
using Bamz.Petshop.Infrastructure.Data;
using Bamz.Petshop.Infrastructure.Data.Context;
using Bamz.Petshop.Infrastructure.Data.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;

namespace Bamz.Petshop.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
            JwtSecurityKey.SetSecret("a very secret string that needs to be at least 16 characters long");
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IColourService, ColourService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IPetTypeService, PetTypeService>();
            services.AddScoped<IRepository<Address>, AddressDBRepository>();
            services.AddScoped<IRepository<Colour>, ColourDBRepository>();
            services.AddScoped<IRepository<Order>, OrderDBRepository>();
            services.AddScoped<IPersonRepository, PersonDBRepository>();
            services.AddScoped<IRepository<Pet>, PetDBRepository>();
            services.AddScoped<IRepository<PetType>, PetTypeDBRepository>();

            if (Environment.IsDevelopment())
            {
                // In-memory database:
                services.AddDbContext<PetshopContext>(opt => opt.UseSqlite("Data Source = Petshop.db", b => b.MigrationsAssembly("Bamz.Petshop.RestApi")).EnableSensitiveDataLogging());
            }
            else
            {
                // SQL Server on Azure:
                services.AddDbContext<PetshopContext>(opt =>
                         opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Bamz.Petshop.RestApi")));
            }

            // Add JWT based authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtSecurityKey.Key,
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // Add CORS
            services.AddCors();

            ServiceProvider sp = services.BuildServiceProvider();
            var dbContext = sp.GetService<PetshopContext>();
            if (Environment.IsDevelopment())
            {
                DBInit.Initialize(dbContext);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<PetshopContext>();
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();

                    app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
                }
                else
                {
                    app.UseHsts();

                    app.UseCors(builder => builder.WithOrigins("https://petshop.azurewebsites.net").AllowAnyMethod().AllowAnyHeader());
                }
            }

            // Use authentication
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseStaticFiles();
        }
    }
}
