using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bamz.Petshop.Infrastructure.Data.Context;
using Bamz.Petshop.Infrastructure.Data.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bamz.Petshop.RestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Build host
            var host = BuildWebHost(args).Build();

            // Run host
            host.Run();
        }

        public static IWebHostBuilder BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
