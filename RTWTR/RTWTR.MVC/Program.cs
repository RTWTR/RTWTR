using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RTWTR.Data;

namespace RTWTR.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            // // Database seed
            // using (var scope = host.Services.CreateScope())
            // {
            //     var serviceProvider = scope.ServiceProvider;

            //     // Get DbContext
            //     var dbContext = serviceProvider.GetRequiredService<RTWTRDbContext>();

            //     // Get Logger
            //     var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            //     
            //     dbContext.Database.Migrate();

            //     try
            //     {
            //         DatabaseSeeder.Initialize(serviceProvider).Wait();
            //     }
            //     catch (Exception e)
            //     {
            //         // Log exceptions, if any
            //         logger.LogError(e, e.Message);
            //         throw e;
            //     }
            // }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
    }
}