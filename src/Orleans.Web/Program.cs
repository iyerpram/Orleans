using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orleans.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseOrleans(options =>
            {
                options.UseLocalhostClustering();
                options.AddDynamoDBGrainStorageAsDefault(configureOptions =>
                {
                    configureOptions.Service = "";
                    configureOptions.UseJson = true;
                });
                options.AddCosmosDBGrainStorageAsDefault(configureOptions => {
                    configureOptions.AccountEndpoint = "";
                    configureOptions.AccountKey = "";
                    configureOptions.ConnectionMode = Microsoft.Azure.Cosmos.ConnectionMode.Direct;
                    configureOptions.Collection = "";
                });
            });
    }
}
