using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNETCoreAPI.Contexts;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyNETCoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //DEBUG throttle the thread pool for testing async performance
            //ThreadPool.SetMaxThreads(3, 3);

            var logger = NLogBuilder
                    .ConfigureNLog("nlog.config")
                    .GetCurrentClassLogger();
            try
            {
                var host = CreateWebHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    try
                    {
                        //var context = scope.ServiceProvider.GetService<CityInfoContext>();

                        //for demo purposes , delete the database and migrate on startup
                        //context.Database.EnsureDeleted();
                        //context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex, "An error occured while migrating the database.");
                    }
                }

                //run application
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application stopped because of exception.");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog();
    }
}
